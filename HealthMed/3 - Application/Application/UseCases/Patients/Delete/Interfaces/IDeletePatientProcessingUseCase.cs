using ErrorOr;

namespace Application.UseCases.Patient.Delete.Interfaces;

public interface IDeletePatientProcessingUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);  
}
