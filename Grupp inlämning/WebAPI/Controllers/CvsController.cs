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
        

        public CvsController(IMediator mediator)
        {
            _mediator = mediator;
        }


       // [Authorize]
        [HttpPost]
        [Route("CreateNewCV")]
        public async Task<IActionResult> CreateCv([FromBody] CvDto cvDto)
        {
            if (cvDto == null)
            {
                return BadRequest("CV data is required.");
            }

            try
            {
                var command = new AddCvCommand(cvDto);
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }

                return CreatedAtAction(nameof(GetCvById), new { id = result.Data.Id }, result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //[Authorize]
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
                    return NotFound("CV not found.");
                }

                return Ok(cv);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

       // [Authorize]
        [HttpPut]
        [Route("UpdateCv/{id}")]
        public async Task<IActionResult> UpdateCv(Guid id, [FromBody] CvDto cvDto)
        {
            if (cvDto == null || id != cvDto.Id)
            {
                return BadRequest("CV data is invalid.");
            }

            try
            {
                var command = new UpdateCvCommand(cvDto);
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

       // [Authorize]
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
                    return BadRequest(result.ErrorMessage);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //[Authorize]
        [HttpGet]
        [Route("GetUsersCv/{userId}")]
        //[ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetAllCvsFromUser(Guid userId)
        {
            try
            {
                var result = await _mediator.Send(new GetAllCVsByUserIdQuery(userId));

                if (result == null || !result.Data.Any())
                {
                    return NotFound("No CVs found for the specified user.");
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
