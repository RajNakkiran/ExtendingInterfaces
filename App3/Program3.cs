using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Reflection;

using IDriver;
using IDriver2;
using IDriver3;

#if false
 Directory of c:\t15\ExtendingInterfaces\out

04/20/2015  10:18 AM             6,144 App1.exe
04/20/2015  10:18 AM             6,656 App2.exe
04/20/2015  10:18 AM             7,168 App3.exe
04/20/2015  10:18 AM             5,120 Driver1.dll
04/20/2015  10:18 AM             5,632 Driver2.dll
04/20/2015  10:18 AM             6,144 Driver3.dll
04/20/2015  10:18 AM             4,096 IDriver.dll
04/20/2015  10:18 AM             4,096 IDriver2.dll
04/20/2015  10:18 AM             4,096 IDriver3.dll
 
c:\t15\ExtendingInterfaces\out>app1 c:\t15\ExtendingInterfaces\out\Driver1.dll          <-- At Time1,  latest app (v1) with latest driver(v1)
INFO: Using c:\t15\ExtendingInterfaces\out\Driver1.dll
INFO: Listing types in c:\t15\ExtendingInterfaces\out\Driver1.dll
INFO: c:\t15\ExtendingInterfaces\out\Driver1.dll has type Driver.Driver
INFO: Driver::IDriver created
INFO: Object Properties:  Name = My Name is IDriver.IDriver  DeviceId = 0
INFO: Interface Properties:  Name = My Name is IDriver.IDriver  DeviceId = 0
INFO: Driver::IDriver::WriteDevice(). Data Length = 1                                   <-- just one op in ver 1

c:\t15\ExtendingInterfaces\out>app2 c:\t15\ExtendingInterfaces\out\Driver2.dll          <-- At Time2,  latest app(v2) with latest driver(v2)
INFO: Using c:\t15\ExtendingInterfaces\out\Driver2.dll
INFO: Listing types in c:\t15\ExtendingInterfaces\out\Driver2.dll
INFO: c:\t15\ExtendingInterfaces\out\Driver2.dll has type Driver.Driver
INFO: Driver2::IDriver2 created
INFO: Object Properties:  Name = My Name is Driver2.cs  DeviceId = 0
INFO: Good News. c:\t15\ExtendingInterfaces\out\Driver2.dll supports latest Interface
INFO: Interface Properties:  Name = My Name is Driver2.cs  DeviceId = 0
INFO: Driver2::IDriver2::WriteDevice(). Data Length = 1
INFO: Driver2::IDriver2::ReadDevice(). Data Length = 1                                  <-- new op in ver 2

c:\t15\ExtendingInterfaces\out>app3 c:\t15\ExtendingInterfaces\out\Driver3.dll          <-- At Time3, latest app(v3) with latest driver(v3)
INFO: Using c:\t15\ExtendingInterfaces\out\Driver3.dll
INFO: Listing types in c:\t15\ExtendingInterfaces\out\Driver3.dll
INFO: c:\t15\ExtendingInterfaces\out\Driver3.dll has type Driver.Driver
INFO: Driver3::IDriver3 created
INFO: Object Properties:  Name = My Name is Driver3.cs  DeviceId = 0
INFO: Great News. c:\t15\ExtendingInterfaces\out\Driver3.dll supports the latest Interface
INFO: Interface Properties:  Name = My Name is Driver3.cs  DeviceId = 0
INFO: Driver3::IDriver3::WriteDevice(). Data Length = 1
INFO: Driver3::IDriver3::ReadDevice(). Data Length = 1                                  <-- new op in ver 2
INFO: Driver3::IDriver3::VerifyDevice(). Data Length = 1                                <-- new op in ver 3

c:\t15\ExtendingInterfaces\out>app1 c:\t15\ExtendingInterfaces\out\Driver3.dll          <--  oldest app(v1) and newest driver(v3)
INFO: Using c:\t15\ExtendingInterfaces\out\Driver3.dll
INFO: Listing types in c:\t15\ExtendingInterfaces\out\Driver3.dll
INFO: c:\t15\ExtendingInterfaces\out\Driver3.dll has type Driver.Driver
INFO: Driver3::IDriver3 created
INFO: Object Properties:  Name = My Name is Driver3.cs  DeviceId = 0
INFO: Interface Properties:  Name = My Name is Driver3.cs  DeviceId = 0
INFO: Driver3::IDriver3::WriteDevice(). Data Length = 1

c:\t15\ExtendingInterfaces\out>app3 c:\t15\ExtendingInterfaces\out\Driver1.dll          <-- newest app(v3) and oldest driver(v1) 
INFO: Using c:\t15\ExtendingInterfaces\out\Driver1.dll
INFO: Listing types in c:\t15\ExtendingInterfaces\out\Driver1.dll
INFO: c:\t15\ExtendingInterfaces\out\Driver1.dll has type Driver.Driver
INFO: Driver::IDriver created
INFO: Object Properties:  Name = My Name is IDriver.IDriver  DeviceId = 0
INFO: Running  c:\t15\ExtendingInterfaces\out\Driver1.dll with the oldest Interface. ReadDevice and VerifyDevice not supported.
INFO: Interface Properties:  Name = My Name is IDriver.IDriver  DeviceId = 0
INFO: Driver::IDriver::WriteDevice(). Data Length = 1


#endif


namespace App3
{
    class Program3
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


                    // See if latest GetInterface3 is available
                    PropertyInfo interfacePropertyInfo = driverType.GetProperty("GetInterface3");
                    if (interfacePropertyInfo != null)
                    {
                        // GetInterface3
                        Console.WriteLine("INFO: Great News. {0} supports the latest Interface", assemblyName);
                        IDriver3.IDriver3 iDriver3 = (IDriver3.IDriver3)interfacePropertyInfo.GetValue(driverObj, null);
                        Console.WriteLine("INFO: Interface Properties:  Name = {0}  DeviceId = {1}", iDriver3.Name, iDriver3.DeviceId);

                        // 3 operations possible
                        iDriver3.WriteDevice(new byte[1] { 0 });
                        byte[] data = iDriver3.ReadDevice();
                        bool result = iDriver3.VerifyDevice(data);

                    }
                    else if (  (interfacePropertyInfo = driverType.GetProperty("GetInterface2")) != null )
                    {

                        // GetInterface2
                        Console.WriteLine("INFO: Good News. {0} supports intermediate Interface. VerifyDevice not supported.", assemblyName);
                        IDriver2.IDriver2 iDriver2 = (IDriver2.IDriver2)interfacePropertyInfo.GetValue(driverObj, null);                       
                        Console.WriteLine("INFO: Interface Properties:  Name = {0}  DeviceId = {1}", iDriver2.Name, iDriver2.DeviceId);

                        // Only 2 operations possible 
                        iDriver2.WriteDevice(new byte[1] { 0 });
                        byte[] data = iDriver2.ReadDevice();
                    }
                    else if ((interfacePropertyInfo = driverType.GetProperty("GetInterface")) != null)
                    {
                        // GetInterface
                        Console.WriteLine("INFO: Running  {0} with the oldest Interface. ReadDevice and VerifyDevice not supported.", assemblyName);
                        IDriver.IDriver iDriver = (IDriver.IDriver)interfacePropertyInfo.GetValue(driverObj, null);
                        Console.WriteLine("INFO: Interface Properties:  Name = {0}  DeviceId = {1}", iDriver.Name, iDriver.DeviceId);

                        // Only 1 operation possible
                        iDriver.WriteDevice(new byte[1] { 0 });
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Invalid Driver assembly: {0}", assemblyName);
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
