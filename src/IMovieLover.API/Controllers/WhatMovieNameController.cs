using MediatR;
using Microsoft.AspNetCore.Mvc;

using IMovieLoverAPI.Models;
using IMovieLover.API.Commands;

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
            var message = await _mediator.Send(new MovieNameCommand(inputText));

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return Ok(message);
        }
    }
}
