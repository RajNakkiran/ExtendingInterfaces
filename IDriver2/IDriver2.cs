using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IDriver;

namespace IDriver2
{
    public interface  IDriver2 : IDriver.IDriver
    {
        // IDriver2
        byte[]  ReadDevice();
    }
}
