using System;
using System.Collections.Generic;

namespace TaskManagein.Exceptions.Handler
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ValidationField> Errors { get; set; }

        public ApiResponse(string message, T data, List<ValidationField> errors)
        {
            Message = message;
            Data = data;
            Errors = errors;
        }
    }
}
