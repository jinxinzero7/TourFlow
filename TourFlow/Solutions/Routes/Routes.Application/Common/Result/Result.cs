using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.Application.Common.Result
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }
        public int StatusCode { get; }

        protected Result(bool isSuccess, string error, int statusCode)
        {
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result Success() => new Result(true, string.Empty, 200);
        public static Result Failure(string error, int statusCode = 400) => new Result(false, error, statusCode);
        public static Result<T> Success<T>(T value) => new Result<T>(value, true, string.Empty, 200);
        public static Result<T> Failure<T>(string error, int statusCode = 400) => new Result<T>(default!, false, error, statusCode);
    }
}