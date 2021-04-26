using System;
namespace Domain.Exceptions
{

    [Serializable]
    public class BadRequestException : Exception
    {
        public string Code { get; }
        public string Message { get; }

        public BadRequestException(string code, string message)
        {
            Code = code;
            Message = message;
        }

        protected BadRequestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
