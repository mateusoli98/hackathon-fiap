using Application.UseCases.Appointment.Delete.Interfaces;
using Application.UseCases.Appointment.DeletePermanently.Interfaces;
using Infra.Services.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


namespace ScheduleWorker;

public class Worker(
    IDeleteAppointmentProcessingUseCase deleteAppointmentProcessingUseCase, 
    IDeleteAppointmentPermanentlyProcessingUseCase deleteAppointmentPermanentlyProcessingUseCase, 
    IRabbitMqProducerService rabbitMqProducerService
    ) : IHostedService
{
    private readonly IDeleteAppointmentProcessingUseCase _deleteAppointmentProcessingUseCase = deleteAppointmentProcessingUseCase;
    private readonly IDeleteAppointmentPermanentlyProcessingUseCase _deleteAppointmentPermanentlyProcessingUseCase = deleteAppointmentPermanentlyProcessingUseCase;
    public readonly IRabbitMqProducerService _rabbitMqProducerService = rabbitMqProducerService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        DeleteAppointment();
        DeleteAppointment(true);

        return Task.CompletedTask;
    }

    private void DeleteAppointment(bool isPermanently = false)
    {
        (_, var _channel) = _rabbitMqProducerService.GetConnectionAndChannel();

        var queueName = isPermanently ? "delete_permanently_appointment" : "delete_appointment";

        _rabbitMqProducerService.DeclareQueue(queueName);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var appointmentId = JsonSerializer.Deserialize<long?>(message) ?? throw new Exception("Erro ao deserializar mensagem");

            Console.WriteLine($"Iniciando processamento para cancelar consulta '{appointmentId}'");

            if (isPermanently)
            {
                _deleteAppointmentPermanentlyProcessingUseCase.Execute(appointmentId!);
            }
            else
            {
                _deleteAppointmentProcessingUseCase.Execute(appointmentId!);
            }
        };

        _channel.BasicConsume(queue: "delete_appointment", autoAck: true, consumer: consumer);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitMqProducerService.Dispose();
        return Task.CompletedTask;
    }
}
