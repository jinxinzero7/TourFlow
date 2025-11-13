using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.Application.Common.Result
{
    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access Value of failed result");

        internal Result(T value, bool isSuccess, string error, int statusCode)
            : base(isSuccess, error, statusCode)
        {
            _value = value;
        }
    }
}