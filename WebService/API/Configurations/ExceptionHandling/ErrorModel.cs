using System.Collections.Generic;

namespace API.Configurations.ExceptionHandling
{
    public class ErrorModel
    {
        /// <summary>
        /// One of the server defined error codes.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A human-readable representation of the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// List of errors which were the reason for the error.
        /// </summary>
        public IEnumerable<InnerError> Details { get; set; }
            = new List<InnerError>();

        public class InnerError
        {
            /// <summary>
            /// One of the server defined error codes.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// A human-readable representation of the error.
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// Target of the error.
            /// </summary>
            public string Target { get; set; }
        }
    }
}
