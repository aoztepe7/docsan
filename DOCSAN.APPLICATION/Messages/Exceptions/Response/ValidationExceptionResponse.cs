using DOCSAN.APPLICATION.Messages.Common.Response;

namespace DOCSAN.APPLICATION.Messages.Exceptions.Response
{
    public class ValidationExceptionResponse : BaseResponse
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public ValidationExceptionResponse(IDictionary<string, string[]> errors, int code) : base(code)
        {
            Errors = errors;
            Code = 400;
            Message = "An error occured while request model validation";
        }
    }
}
