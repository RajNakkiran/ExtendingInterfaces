using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDriver
{
    public interface IDriver
    {
        string Name { get; }
        int DeviceId { get; }
        bool WriteDevice(byte[] data);
    }
}
