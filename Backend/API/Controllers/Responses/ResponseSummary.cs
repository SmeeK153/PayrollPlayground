using System;

namespace API.Controllers.Responses
{
    public abstract class ResponseSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}