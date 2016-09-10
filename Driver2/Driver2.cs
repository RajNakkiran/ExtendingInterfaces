using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IDriver;
using IDriver2;


namespace Driver
{
    public class Driver : IDriver2.IDriver2  
    {
        byte[] device_data;

         public Driver()
        {
            device_data = new byte[0];
            Console.WriteLine("INFO: Driver2::IDriver2 created");
            Name = "My Name is Driver2.cs";
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
            Console.WriteLine("INFO: Driver2::IDriver2::WriteDevice(). Data Length = {0}",  data.Length);
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
            Console.WriteLine("INFO: Driver2::IDriver2::ReadDevice(). Data Length = {0}", device_data.Length);
            return device_data;
        }


        public IDriver2.IDriver2 GetInterface2
        {
            get
            {
                return (this as IDriver2.IDriver2);
            }

        }
    }
}
