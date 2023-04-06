using MediatR;
using Microsoft.AspNetCore.Mvc;

using IMovieLoverAPI.Models;
using IMovieLover.API.Commands;
using FluentValidation;

namespace IMovieLoverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class WhatMovieNameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WhatMovieNameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> MovieName(ChatGptRequest inputText)
        {
            try
            {
                var message = await _mediator.Send(new MovieNameCommand(inputText));
                return Ok(message);
            }
            catch (ValidationException ex)
            {
                var error = ex.Message
                    .Replace("Validation failed: \r\n -- MessageRequest.prompt: ", "")
                    .Replace(" Severity: Error", "");

                return BadRequest(new { message = error });
            }
        }
    }
}
