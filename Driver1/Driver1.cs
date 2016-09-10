using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IDriver;

namespace Driver
{
    public class Driver : IDriver.IDriver
    {
        public Driver()
        {
            Console.WriteLine("INFO: Driver::IDriver created");
            Name = "My Name is IDriver.IDriver";
            DeviceId = 0;
        }

        public string Name { get; set; }
        public int DeviceId { get; set; }
        public bool WriteDevice(byte[] data)
        {
            // use date here 
            Console.WriteLine("INFO: Driver::IDriver::WriteDevice(). Data Length = {0}",  data.Length);
            return true;
        }

        public IDriver.IDriver GetInterface
        {
            get
            {
                return this as IDriver.IDriver;
            }

        }
    }
}
