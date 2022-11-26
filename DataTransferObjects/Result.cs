using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class Result
    {
        public readonly bool Success;
        public readonly string? Error;

        [Description("if an object is not found for update/delete. this is to send Http Status NotFoundObjectResult")]
        public readonly bool NotFound;

        protected Result(bool success, string error, bool notFound = false)
        {
            Success = success;
            Error = error;
            NotFound = notFound;
        }

        public static Result OnFail(string message) => new Result(false, message);
        public static Result<T> OnSuccess<T>(T value) => new Result<T>(value, true, null);
    }
    public class Result<T> : Result
    {
        private readonly T _value;
        protected internal Result(T value, bool success, string? error, bool notFound = false) : base(success, error, notFound)
        {
            _value= value;
        }

        public T Value =>  _value;
    }
}
