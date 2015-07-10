using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LSDStay
{
	public static class PSX
	{
		public static Process PSXProcess;
		public static IntPtr PSXHandle;
		
		public static bool FindPSX()
		{
			PSXProcess = Process.GetProcessesByName("psxfin").FirstOrDefault();
			return (PSXProcess != null);
		}

		public static bool OpenPSX()
		{
			int PID = PSXProcess.Id;
			PSXHandle = Memory.OpenProcess((uint)Memory.ProcessAccessFlags.All, false, PID);
			return (PSXHandle != null);
		}

		public static bool ClosePSX()
		{
			int result = Memory.CloseHandle(PSXHandle);
			if (result == 0)
			{
				Console.WriteLine("ERROR: Could not close psx handle");
				return false;
			}
			return true;
		}

		public static string Read(IntPtr address, ref byte[] buffer)
		{
			int bytesRead = 0;
			int absoluteAddress = Memory.PSXGameOffset + (int)address;
			//IntPtr absoluteAddressPtr = new IntPtr(absoluteAddress);
			
			Memory.ReadProcessMemory((int)PSXHandle, absoluteAddress, buffer, buffer.Length, ref bytesRead);
			
			return "Address " + address.ToString("x2") + " contains " + Memory.FormatToHexString(buffer);
		}

		public static string Write(IntPtr address, byte[] data)
		{
			int bytesWritten;
			int absoluteAddress = Memory.PSXGameOffset + (int)address;
			IntPtr absoluteAddressPtr = new IntPtr(absoluteAddress);

			Memory.WriteProcessMemory(PSXHandle, absoluteAddressPtr, data, (uint)data.Length, out bytesWritten);
			
			return "Address " + address.ToString("x2") + " is now " + Memory.FormatToHexString(data);
		}
	}
}
