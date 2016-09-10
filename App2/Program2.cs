
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Remoting;
using System.Reflection;

using IDriver;
using IDriver2;

namespace App2
{
    class Program2
    {
        static void Test1(string assemblyName)
        {
            try
            {
                Assembly testAssembly = Assembly.LoadFile(assemblyName);

                //
                //  List types
                //
                Console.WriteLine("INFO: Listing types in {0}", assemblyName);
                foreach (Type typeName in testAssembly.GetTypes())
                {

                    Console.WriteLine("INFO: {0} has type {1}", assemblyName, typeName.FullName);
                }


                //
                // Create Instance
                //

                Type driverType = testAssembly.GetType("Driver.Driver");
                if (driverType != null)
                {
                    object driverObj = Activator.CreateInstance(driverType);
                    PropertyInfo namePropertyInfo = driverType.GetProperty("Name");
                    PropertyInfo deviceIdPropertyInfo = driverType.GetProperty("DeviceId");
                    string name = (string)namePropertyInfo.GetValue(driverObj, null);
                    int deviceId = (int)deviceIdPropertyInfo.GetValue(driverObj, null);
                    Console.WriteLine("INFO: Object Properties:  Name = {0}  DeviceId = {1}", name, deviceId);


                    // See if latest GetInterface2 is available
                    PropertyInfo interfacePropertyInfo = driverType.GetProperty("GetInterface2");
                    if (interfacePropertyInfo != null)
                    {
                        Console.WriteLine("INFO: Good News. {0} supports latest Interface", assemblyName);
                        IDriver2.IDriver2 iDriver2 = (IDriver2.IDriver2)interfacePropertyInfo.GetValue(driverObj, null);                       
                        Console.WriteLine("INFO: Interface Properties:  Name = {0}  DeviceId = {1}", iDriver2.Name, iDriver2.DeviceId);
                        iDriver2.WriteDevice(new byte[1] { 0 });
                        byte[] data = iDriver2.ReadDevice();
                    }
                    else
                    {
                        // GetInterface
                        Console.WriteLine("INFO: Running  {0} with legacy Interface. ReadDevice not supported.", assemblyName);
                        interfacePropertyInfo = driverType.GetProperty("GetInterface");
                        IDriver.IDriver iDriver = (IDriver.IDriver)interfacePropertyInfo.GetValue(driverObj, null);
                        Console.WriteLine("INFO: Interface Properties:  Name = {0}  DeviceId = {1}", iDriver.Name, iDriver.DeviceId);
                        iDriver.WriteDevice(new byte[1] { 0 });
                    }
                    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION: {0}, {1}", e.Message, e.StackTrace);
            }

        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("SYNTAX: App.exe  <absolute path to driver.dll>");
                return;
            }

            string assemblyName = args[0];
            Console.WriteLine("INFO: Using {0}", assemblyName);
            Test1(assemblyName);
        }
    }
}
