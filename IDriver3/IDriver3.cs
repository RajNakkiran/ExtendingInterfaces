using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IDriver;
using IDriver2;

namespace IDriver3
{
    public interface  IDriver3 : IDriver2.IDriver2
    {
        // IDriver3 
        bool  VerifyDevice(byte[] data);
    }
}
