using Infra.Services.Messages;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Domain.Entities;
using RabbitMQ.Client;
using Application.UseCases.Appointment.Delete.Interfaces;


namespace ScheduleWorker;

public class Worker(IDeleteAppointmentProcessingUseCase usecase, IRabbitMqProducerService rabbitMqProducerService) : IHostedService
{
    private readonly IDeleteAppointmentProcessingUseCase _useCase = usecase;
    public readonly IRabbitMqProducerService _rabbitMqProducerService = rabbitMqProducerService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        (_, var _channel) = _rabbitMqProducerService.GetConnectionAndChannel();
        _rabbitMqProducerService.DeclareQueue("delete_appointment");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var appointmentId = JsonSerializer.Deserialize<string>(message) ?? throw new Exception("Erro ao deserializar mensagem");

            Console.WriteLine($"Iniciando processamento para cancelar consulta '{appointmentId}'");
            _useCase.Execute(appointmentId!);
        };

        _channel.BasicConsume(queue: "delete_appointment", autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitMqProducerService.Dispose();
        return Task.CompletedTask;
    }
}
