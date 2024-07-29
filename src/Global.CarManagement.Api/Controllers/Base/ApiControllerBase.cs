using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Models.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;

namespace Global.CarManagement.Api.Controllers.Base
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly INotificationsHandler _notifications;

        public ApiControllerBase(IMediator mediator, INotificationsHandler notifications)
        {
            _mediator = mediator;
            _notifications = notifications;
        }

        protected async Task<IActionResult> SendAsync<TOutput>(IRequest<TOutput> request, HttpStatusCode successStatusCode = HttpStatusCode.OK)
        {
            var response = await _mediator.Send(request);

            if (_notifications.HasErrors())
                return GetResult(_notifications.Erros);

            return new ObjectResult(response) { StatusCode = (int)successStatusCode };
        }

        protected async Task<IActionResult> SendFileAsync<TOutput>(IRequest<TOutput> request, Func<TOutput, string> selectorFile)
        {
            var response = await _mediator.Send(request);

            if (_notifications.HasErrors())
                return GetResult(_notifications.Erros);

            var base64 = selectorFile(response);

            var match = Regex.Match(base64, @"data:image/(?<type>.+?);base64,(?<data>.+)");

            var mimeType = $"image/{match.Groups["type"].Value}";
            var base64Data = match.Groups["data"].Value;

            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(base64Data);
            }
            catch (FormatException)
            {
                return  new ObjectResult("Dados base64 inválidos.") { StatusCode = StatusCodes.Status500InternalServerError };
            }

            return new FileContentResult(imageBytes, mimeType);
        }

        ObjectResult GetResult(object result)
        {
            return _notifications.GetNotificationType() switch
            {
                ENotificationType.BusinessValidation => new UnprocessableEntityObjectResult(result),
                ENotificationType.Unauthorized => new UnauthorizedObjectResult(result),
                ENotificationType.NotFound => new NotFoundObjectResult(result),
                _ => new ObjectResult(result) { StatusCode = (int)HttpStatusCode.InternalServerError },
            };
        }
    }
}
