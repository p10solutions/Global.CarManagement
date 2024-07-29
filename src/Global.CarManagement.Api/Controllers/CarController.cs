using Global.CarManagement.Api.Controllers.Base;
using Global.CarManagement.Application.Features.Cars.Commands.CreateCar;
using Global.CarManagement.Application.Features.Cars.Commands.DeleteCar;
using Global.CarManagement.Application.Features.Cars.Commands.UpdateCar;
using Global.CarManagement.Application.Features.Cars.Commands.UpdatePhotoCar;
using Global.CarManagement.Application.Features.Cars.Queries.GetCar;
using Global.CarManagement.Application.Features.Cars.Queries.GetCarById;
using Global.CarManagement.Domain.Contracts.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Global.CarManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController(IMediator mediator, INotificationsHandler notifications) : ApiControllerBase(mediator, notifications)
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetCarResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(string name = "", int currentPage = 1, int pageSize = 10)
            => await SendAsync(new GetCarQuery(name, pageSize, currentPage));

        [HttpGet("image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetCarResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetImage(Guid id)
            =>await SendFileAsync(new GetCarByIdQuery(id), (x)=> x.Photo);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCarResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync(CreateCarCommand createCarCommand)
            => await SendAsync(createCarCommand, HttpStatusCode.Created);

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateCarResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task PutAsync(Guid id, UpdateCarCommand updateCarCommand)
            => await SendAsync(updateCarCommand);

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatePhotoCarResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task PatchAsync(Guid id, UpdatePhotoCarCommand updateCarCommand)
            => await SendAsync(updateCarCommand);

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateCarResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeleteAsync(Guid id)
            => await SendAsync(new DeleteCarCommand(id));
    }
}
