using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cvs.Add
{
    public class AddCvCommandHandler : IRequestHandler<AddCvCommand, OperationResult<CvDto>>
    {
        private readonly ICvRepository _cvRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AddCvCommandHandler> _logger;

        public AddCvCommandHandler(
            ICvRepository cvRepository,
            IUserRepository userRepository,
            ILogger<AddCvCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<OperationResult<CvDto>> Handle(AddCvCommand request, CancellationToken cancellationToken)
        {
            var cvDto = request.CvToAdd;

            _logger.LogInformation("Attempting to add CV for UserId: {UserId}", cvDto.UserId);

            try
            {
                var user = await _userRepository.GetUserByIdAsync(cvDto.UserId);
                if (user == null)
                {
                    _logger.LogWarning("Cannot add CV. User with Id {UserId} not found.", cvDto.UserId);
                    return OperationResult<CvDto>.Failure("User not found.");
                }

                var cv = new CV
                {
                    Id = Guid.NewGuid(),
                    Title = cvDto.Title,
                    UserId = cvDto.UserId,
                    PdfUrl = cvDto.PdfUrl,
                    User = user.Data
                };

                var addResult = await _cvRepository.AddAsync(cv);
                if (!addResult.IsSuccess)
                {
                    _logger.LogError("Failed to add CV: {ErrorMessage}", addResult.ErrorMessage);
                    return OperationResult<CvDto>.Failure(addResult.ErrorMessage);
                }

                _logger.LogInformation("Successfully added CV with Id: {CVId} for UserId: {UserId}", cv.Id, cvDto.UserId);

                var resultDto = new CvDto
                {
                    Id = cv.Id,
                    Title = cv.Title,
                    UserId = cv.UserId,
                    PdfUrl = cv.PdfUrl
                };

                return OperationResult<CvDto>.Success(resultDto, "CV added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding CV for UserId: {UserId}", cvDto.UserId);
                return OperationResult<CvDto>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}

