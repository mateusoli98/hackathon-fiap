using Application.UseCases.Doctor.Delete.Interfaces;
using Application.UseCases.Doctor.DeletePermanently.Interfaces;
using Application.UseCases.Patient.Delete.Interfaces;
using Application.UseCases.Patient.DeletePermanently.Interfaces;
using Infra.Services.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


namespace HealthMedWorker;

public class Worker
    (
    IDeleteDoctorProcessingUseCase deleteDoctorProcessingUseCase,
    IDeleteDoctorPermanentlyProcessingUseCase deleteDoctorPermanentlyProcessingUseCase,
    IDeletePatientProcessingUseCase deletePatientProcessingUseCase,
    IDeletePatientPermanentlyProcessingUseCase deletePatientPermanentlyProcessingUseCase,
    IRabbitMqProducerService rabbitMqProducerService
    ) : IHostedService
{
    private readonly IDeleteDoctorProcessingUseCase _deleteDoctorProcessingUseCase = deleteDoctorProcessingUseCase;
    private readonly IDeleteDoctorPermanentlyProcessingUseCase _deleteDoctorPermanentlyProcessingUseCase = deleteDoctorPermanentlyProcessingUseCase;
    private readonly IDeletePatientProcessingUseCase _deletePatientProcessingUseCase = deletePatientProcessingUseCase;
    private readonly IDeletePatientPermanentlyProcessingUseCase _deletePatientPermanentlyProcessingUseCase = deletePatientPermanentlyProcessingUseCase;
    private readonly IRabbitMqProducerService _rabbitMqProducerService = rabbitMqProducerService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        (_, var _channel) = _rabbitMqProducerService.GetConnectionAndChannel();

        DeleteDoctor();
        DeleteDoctor(true);
        DeletePatient();
        DeletePatient(true);

        return Task.CompletedTask;
    }

    private void DeleteDoctor(bool isPermanently = false)
    {
        (_, var _channel) = _rabbitMqProducerService.GetConnectionAndChannel();

        var queueName = isPermanently ? "delete_permanently_doctor" : "delete_doctor";

        _rabbitMqProducerService.DeclareQueue(queueName);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var doctorId = JsonSerializer.Deserialize<long?>(message) ?? throw new Exception("Erro ao deserializar mensagem");
            Console.WriteLine($"Iniciando processamento do médico '{doctorId}'");

            if (isPermanently)
            {
                _deleteDoctorPermanentlyProcessingUseCase.Execute(doctorId);
            }
            else
            {
                _deleteDoctorProcessingUseCase.Execute(doctorId);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    private void DeletePatient(bool isPermanently = false)
    {
        (_, var _channel) = _rabbitMqProducerService.GetConnectionAndChannel();

        var queueName = isPermanently ? "delete_permanently_patient" : "delete_patient";
        _rabbitMqProducerService.DeclareQueue(queueName);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var patientId = JsonSerializer.Deserialize<long?>(message) ?? throw new Exception("Erro ao deserializar mensagem");
            Console.WriteLine($"Iniciando processamento do paciente '{patientId}'");

            if (isPermanently)
            {
                _deletePatientPermanentlyProcessingUseCase.Execute(patientId!);
            }
            else
            {
                _deletePatientProcessingUseCase.Execute(patientId!);
            }           
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitMqProducerService.Dispose();
        return Task.CompletedTask;
    }
}
