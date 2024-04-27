using Demo.Presentaion.Services.interfaces;
using System;

namespace Demo.Presentaion.Services.Classes
{
    public class ScopedServices : IScopedServices
    {
        public Guid guid { get; set; }
        public ScopedServices()
        {
            guid = Guid.NewGuid();
        }

        public Guid Guid { get; set ; }

        public string GetGuid()
        {
            return guid.ToString();
        }
        public override string ToString()
        {
            return guid.ToString();
        }
    }
}
