using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cvs.UpdateCv
{
    public class UpdateCvCommandHandler : IRequestHandler<UpdateCvCommand, OperationResult<CvDto>>
    {
        private readonly ICvRepository _cvRepository;
        private readonly ILogger<UpdateCvCommandHandler> _logger;

        public UpdateCvCommandHandler(ICvRepository cvRepository, ILogger<UpdateCvCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<OperationResult<CvDto>> Handle(UpdateCvCommand request, CancellationToken cancellationToken)
        {
            var cvDto = request.CvToUpdate;
            _logger.LogInformation("Attempting to update CV with Id: {CvId}", cvDto.Id);

            try
            {
                var existingCv = await _cvRepository.GetByIdAsync(cvDto.Id);
                if (!existingCv.IsSuccess)
                {
                    _logger.LogWarning("CV with Id: {CvId} not found", cvDto.Id);
                    return OperationResult<CvDto>.Failure(existingCv.ErrorMessage);
                }

                var cv = existingCv.Data;

                cv.Title = cvDto.Title;
                cv.PdfUrl = cvDto.PdfUrl;

                var updateResult = await _cvRepository.UpdateAsync(cv);

                if (!updateResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to update CV with Id: {CvId}. Reason: {Reason}", cvDto.Id, updateResult.ErrorMessage);
                    return OperationResult<CvDto>.Failure(updateResult.ErrorMessage);
                }

                _logger.LogInformation("Successfully updated CV with Id: {CvId}", cvDto.Id);

                var resultDto = new CvDto
                {
                    Id = cv.Id,
                    Title = cv.Title,
                    UserId = cv.UserId,
                    PdfUrl = cv.PdfUrl
                };

                return OperationResult<CvDto>.Success(resultDto, "CV updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating CV with Id: {CvId}", cvDto.Id);
                return OperationResult<CvDto>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
