using DOCSAN.APPLICATION.Exceptions;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Messages.Exceptions.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DOCSAN.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ExceptionFilter()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(ModelValidationException), HandleModelValidationException },
                {typeof(DomainNotFoundException), HandleDomainNotFoundException },
                {typeof(InvalidCredentialsException), HandleInvalidCredentialsException },
                {typeof(InternalServerException), HandleInternalServerException },

            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;

            }
        }

        private void HandleModelValidationException(ExceptionContext context)
        {
            var exception = (ModelValidationException)context.Exception;

            var response = new ValidationExceptionResponse(exception.Errors, 400);
            response.Code = (int)HttpStatusCode.BadRequest;

            context.Result = new ObjectResult(response);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.ExceptionHandled = true;
        }

        private void HandleDomainNotFoundException(ExceptionContext context)
        {
            var exception = (DomainNotFoundException)context.Exception;

            var response = new BaseResponse(404, exception.ClassName + " Not Found");
            response.Code = (int)HttpStatusCode.NotFound;

            context.Result = new ObjectResult(response);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.ExceptionHandled = true;

        }

        private void HandleInvalidCredentialsException(ExceptionContext context)
        {
            var exception = (InvalidCredentialsException)context.Exception;

            var response = new BaseResponse(401);
            response.Code = (int)HttpStatusCode.Unauthorized;

            context.Result = new ObjectResult(response);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.ExceptionHandled = true;

        }

        private void HandleInternalServerException(ExceptionContext context)
        {
            var exception = (InternalServerException)context.Exception;

            var response = new BaseResponse(500);
            response.Code = (int)HttpStatusCode.InternalServerError;

            context.Result = new ObjectResult(response);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.ExceptionHandled = true;
        }


    }
}
