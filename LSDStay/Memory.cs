using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LSDStay
{
	public static class Memory
	{
		[Flags]
		public static enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VMOperation = 0x00000008,
			VMRead = 0x00000010,
			VMWrite = 0x00000020,
			DupHandle = 0x00000040,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			Synchronize = 0x00100000
		}

		public static readonly int PSXGameOffset = 0x171A5C;

		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle,
			int dwProcessId);

		[DllImport("kernel32.dll")]
		public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress,
			byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
			byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesWritten);

		[DllImport("kernel32.dll")]
		public static extern Int32 CloseHandle(IntPtr hProcess);

		public static bool WriteMemory(Process p, IntPtr address, long v)
		{
			var hProc = OpenProcess((uint)ProcessAccessFlags.All, false, (int)p.Id);
			var val = new byte[] { (byte)v };

			int wtf = 0;
			bool returnVal = WriteProcessMemory(hProc, address, val, (UInt32)val.LongLength, out wtf);

			CloseHandle(hProc);

			return returnVal;
		}
	}
}
