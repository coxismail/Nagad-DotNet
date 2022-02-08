using System;
using System.Net;

namespace Nagad
{
   public class NagadError : NagadException
    {
        public ErrorResult Error { get; private set; }
        public HttpStatusCode HttpStatusCode { get { return Error.HttpStatusCode; } }
        public string Code { get { return Error.Code; } }
        public override string Message { get { return Error.Message??"Something went wrong"; } }

        public NagadError(
            ErrorResult result,
            Exception inner
        ) : base("API call result in an error.", inner)
        {
            Error = result;
        }

        public override string ToString() => $"({(int)HttpStatusCode}/{Code}) {Error.Message}";
    }


    public class ErrorResult {
        public HttpStatusCode HttpStatusCode { get; protected internal set; }
        public string Code { get; protected internal set; }
        public string Message { get; protected internal set; }
    }

    public class NagadException : Exception
    {
        public NagadException()
        {
        }

        public NagadException(string message) : base(message)
        {
        }

        public NagadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

