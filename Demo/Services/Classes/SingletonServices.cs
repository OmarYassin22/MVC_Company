using Demo.Presentaion.Services.interfaces;
using System;

namespace Demo.Presentaion.Services.Classes
{
    public class SingletonServices : ISingletonServices
    {
        public Guid guid { get; set; }
        public SingletonServices()
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
