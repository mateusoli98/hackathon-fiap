namespace Application.UseCases.AppoAssessmentintment.Create;

using Application.UseCases.Assessment.Create.Interfaces;
using Domain.Entities;
using Domain.Repositories.Relational;

public class CreateAssessmentProcessingUseCase : ICreateAssessmentProcessingUseCase
{
    private readonly IAssessmentRepository _assessmentRepository;

    public CreateAssessmentProcessingUseCase(IAssessmentRepository assessmentRepository)
    {
        _assessmentRepository = assessmentRepository;
    }

    public async Task Execute(Assessment assessment, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Validando se a avaliação já existe");
        var alreadyExists = await _assessmentRepository.Exists(assessment.Doctor.Id, assessment.Patient.Id, cancellationToken);
        if (!alreadyExists)
        {
            Console.WriteLine($"Salvando avaliação '{assessment.Id}'");
            await _assessmentRepository.SaveAsync(assessment, cancellationToken);
            return;
        }

        throw new Exception($"Erro: a avaliação já existe no banco de dados");
    }
}
