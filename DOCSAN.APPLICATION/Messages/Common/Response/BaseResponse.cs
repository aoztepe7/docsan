using DOCSAN.SHARED.Utils;


namespace DOCSAN.APPLICATION.Messages.Common.Response
{
    public class BaseResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public BaseResponse(int code, string? message = null)
        {
            Code = code;
            Message = message != null ? message : ResponseMessageHelper.GetMessage(code);
        }
    }
}
