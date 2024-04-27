using System;

namespace Demo.Presentaion.Services.interfaces
{
    public interface ISingletonServices
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
