using System;
using System.Collections.Generic;
using System.Text;

namespace MySpace_Common
{
    public class ApiResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

}
