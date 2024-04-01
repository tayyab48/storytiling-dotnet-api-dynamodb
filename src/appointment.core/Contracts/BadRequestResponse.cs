using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.WebApi.Core;

namespace storytiling.core.Contracts
{
    public class BadRequestResponse : Response
    {   
        public IEnumerable<string> Errors { get; }

        public BadRequestResponse(ModelStateDictionary modelState) : base(HttpStatusCode.BadRequest)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("Validation failed", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();
        }

        public BadRequestResponse(ModelStateDictionary modelState, HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("Validation failed", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors)
               .Select(x => x.ErrorMessage).ToArray();

        }


    }
}
