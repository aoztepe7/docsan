using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.SHARED.Utils
{
    public static class ResponseMessageHelper
    {
        public static string GetMessage(int code)
        {
            switch (code)
            {
                case 200:
                    return "Success";
                case 401:
                    return "Invalid Credentials";
                case 403:
                    return "Forbidden";
                case 404:
                    return "Not Found";
                case 409:
                    return "Already Exist";
                case 500:
                    return "Internal Server Error";
                default:
                    return "Unhandled Exception";
            }
        }
    }
}
