using System;

namespace BusConductor.Application.Requests
{
    public class RaiseTaskRequest
    {
        public string Description { get; set; }
        public Guid? AssignedToId { get; set; } 
        public Guid? TypeId { get; set; }
        public string UserName { get; set; }
    }
}
