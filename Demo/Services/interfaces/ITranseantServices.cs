using System;

namespace Demo.Presentaion.Services.interfaces
{
    public interface ITranseantServices
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
