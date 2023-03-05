using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public sealed class Response
    {
        public ResponseStatus Status { get; private set; }

        public string ErrorMessage { get; private set; }

        private Response(ResponseStatus status, string errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public static Response Error(string errorMessage) => new Response(ResponseStatus.Error, errorMessage);

        public static Response Success = new Response(ResponseStatus.Success, string.Empty);

        public static Response Conflict = new Response(ResponseStatus.Conflict, string.Empty);
    }

    public sealed class Response<T>
    {
        public ResponseStatus Status { get; private set; }

        public string ErrorMessage { get; private set; }

        public T Data { get; private set; }

        private Response(ResponseStatus status, string errorMessage, T data)
        {
            Status = status;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static Response<T> Error(string errorMessage)
            => new Response<T>(ResponseStatus.Error, errorMessage, default);

        public static Response<T> Error(T data)
            => new Response<T>(ResponseStatus.Error, string.Empty, data);

        public static Response<T> Success(T data)
            => new Response<T>(ResponseStatus.Success, string.Empty, data);

        public static Response<T> Conflict = new Response<T>(ResponseStatus.Conflict, string.Empty, default);
    }

    public enum ResponseStatus
    {
        Success = 0,
        Error = 1,
        Conflict = 2,
    }
}
