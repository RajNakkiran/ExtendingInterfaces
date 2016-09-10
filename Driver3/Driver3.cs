using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IDriver;
using IDriver2;
using IDriver3;

namespace Driver
{
    public class Driver : IDriver3.IDriver3
    {
        byte[] device_data;

         public Driver()
        {
            device_data = new byte[0];
            Console.WriteLine("INFO: Driver3::IDriver3 created");
            Name = "My Name is Driver3.cs";
            DeviceId = 0;
        }

        //
         //  IDriver.IDriver
        //
        public string Name { get; set; }
        public int DeviceId { get; set; }
        public bool WriteDevice(byte[] data)
        {
            // use date here 
            device_data = new byte[data.Length];
            Buffer.BlockCopy(data, 0, device_data, 0, data.Length);
            Console.WriteLine("INFO: Driver3::IDriver3::WriteDevice(). Data Length = {0}",  data.Length);
            return true;
        }

        public IDriver.IDriver GetInterface
        {
            get
            {
                return this as IDriver.IDriver;
            }
        }

        // 
        // IDriver2.IDriver2
        //
        public byte[] ReadDevice()
        {
            Console.WriteLine("INFO: Driver3::IDriver3::ReadDevice(). Data Length = {0}", device_data.Length);
            return device_data;
        }


        public IDriver2.IDriver2 GetInterface2
        {
            get
            {
                return (this as IDriver2.IDriver2);
            }
        }

        //
        //  IDriver3.IDriver3
        //
        public bool VerifyDevice(byte[] data)
        {
            Console.WriteLine("INFO: Driver3::IDriver3::VerifyDevice(). Data Length = {0}", device_data.Length);
            return data.SequenceEqual(device_data);
        }

        public IDriver3.IDriver3 GetInterface3
        {
            get
            {
                return (this as IDriver3.IDriver3);
            }
        }
    }
}
