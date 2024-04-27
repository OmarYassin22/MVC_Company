using System;

namespace Demo.Presentaion.Services.interfaces
{
    public interface IScopedServices
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
