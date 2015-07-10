using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LSDStay
{
	public static class PSXFinder
	{
		public static Process FindPSX()
		{
			Process psx = Process.GetProcessesByName("psxfin").FirstOrDefault();
			return psx;
		}

		public static IntPtr OpenPSX(Process psx)
		{
			int PID = psx.Id;
			IntPtr psxHandle = Memory.OpenProcess((uint)Memory.ProcessAccessFlags.All, false, PID);
			
		}

		public static void ClosePSX(IntPtr processHandle)
		{
			int result = Memory.CloseHandle(processHandle);
			if (result == 0)
			{
				Console.WriteLine("ERROR: Could not close psx handle");
			}
		}
	}
}
