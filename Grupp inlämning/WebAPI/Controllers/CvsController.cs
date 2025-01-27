using Application.Commands.Cvs.Add;
using Application.Commands.Cvs.DeleteCv;
using Application.Commands.Cvs.UpdateCv;
using Application.Dtos;
using Application.Queries.Cvs.GetAllByUserId;
using Application.Queries.Cvs.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CvsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CvsController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [Authorize]
        [HttpPost]
        [Route("CreateNewCV")]
        public async Task<IActionResult> CreateCv([FromBody] CvDto cvDto)
        {
            if (cvDto == null)
            {
                _logger.LogWarning("CreateCv failed: CvDto is null.");
                return BadRequest("CV data is required.");
            }

            try
            {
                var command = new AddCvCommand(cvDto);
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("CreateCv failed: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(result.ErrorMessage);
                }

                return CreatedAtAction(nameof(GetCvById), new { id = result.Data.Id }, result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating CV.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetCvById/{id}")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetCvById(Guid id)
        {
            try
            {
                var cv = await _mediator.Send(new GetCvByIdQuery(id));

                if (cv == null)
                {
                    _logger.LogWarning("GetCvById failed: No CV found with Id {CvId}", id);
                    return NotFound("CV not found.");
                }

                return Ok(cv);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving CV by Id.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateCv/{id}")]
        public async Task<IActionResult> UpdateCv(Guid id, [FromBody] CvDto cvDto)
        {
            if (cvDto == null || id != cvDto.Id)
            {
                _logger.LogWarning("UpdateCv failed: Invalid CV Id or CV data.");
                return BadRequest("CV data is invalid.");
            }

            try
            {
                var command = new UpdateCvCommand(cvDto);
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("UpdateCv failed: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(result.ErrorMessage);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating CV.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteCv/{id}")]
        public async Task<IActionResult> DeleteCv(Guid id)
        {
            try
            {
                var command = new DeleteCvCommand(id);
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("DeleteCv failed: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(result.ErrorMessage);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting CV.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetUsersCv/{userId}")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetAllCvsFromUser(Guid userId)
        {
            try
            {
                var result = await _mediator.Send(new GetAllCVsByUserIdQuery(userId));

                if (result == null || !result.Data.Any())
                {
                    _logger.LogWarning("No CVs found for user with Id: {UserId}.", userId);
                    return NotFound("No CVs found for the specified user.");
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving CVs for user with Id: {UserId}.", userId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
