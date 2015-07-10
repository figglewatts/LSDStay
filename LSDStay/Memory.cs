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
		public enum ProcessAccessFlags : uint
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

		public static int GameMemoryStartAddr = 0;

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

		public static void GetGameDataOffset()
		{
			byte[] buffer = new byte[4];
			int numberOfBytesRead = 0;
			ReadProcessMemory((int)PSX.PSXHandle, (int)PSX.PSXProcess.MainModule.BaseAddress + PSXGameOffset, buffer, buffer.Length, ref numberOfBytesRead);
			GameMemoryStartAddr = BitConverter.ToInt32(buffer, 0);
			Console.WriteLine("Game start address is: " + GameMemoryStartAddr.ToString("x4"));
		}

		public static bool WriteMemory(Process p, IntPtr address, long v)
		{
			var hProc = OpenProcess((uint)ProcessAccessFlags.All, false, (int)p.Id);
			var val = new byte[] { (byte)v };

			int wtf = 0;
			bool returnVal = WriteProcessMemory(hProc, address, val, (UInt32)val.LongLength, out wtf);

			CloseHandle(hProc);

			return returnVal;
		}

		public static string FormatToHexString(byte[] data)
		{
			string toReturn = "";
			for (int i = 0; i < data.Length; i++)
			{
				toReturn += string.Format("{0:x2}", data[i]);
			}
			return toReturn;
		}
	}
}
