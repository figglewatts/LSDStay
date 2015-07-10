using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDStay
{
	class Program
	{


		public static IntPtr PSXOffset = new IntPtr(0x171A5C);

		public static IntPtr LocationTimeOffset = new IntPtr(0x8AC70);

		static uint DELETE = 0x00010000;
		static uint READ_CONTROL = 0x00020000;
		static uint WRITE_DAC = 0x00040000;
		static uint WRITE_OWNER = 0x00080000;
		static uint SYNCHRONIZE = 0x00100000;
		static uint END = 0xFFF;
		static uint PROCESS_ALL_ACCESS = (DELETE | READ_CONTROL | WRITE_DAC | WRITE_OWNER | SYNCHRONIZE | END);

		static int Main(string[] args)
		{
			Console.WriteLine("LSDStay for psxfin - By Figglewatts");
			Console.WriteLine("-----------------------------------");
			Console.WriteLine("Finding psxfin.exe...");
			
			Process process = Process.GetProcessesByName("psxfin").FirstOrDefault();
			if (process == null)
			{
				Console.WriteLine("Failed to find psxfin.exe");
				Console.ReadLine();
				return 1;
			}
			Console.WriteLine("Found psxfin.exe, writing to memory address...");
			for (;;)
			{
				bool wm = Memory.WriteMemory(process, (IntPtr)0x072A16D0, 100);
				Console.WriteLine((wm == true ? "Success" : "Failure") + IntPtr.Add(PSXOffset, LocationTimeOffset.ToInt32()).ToString());
			}
		}

		public static string GetConsoleInput()
		{
			Console.Write(">");
			return Console.ReadLine();
		}

		public static void ProcessConsoleInput(string input)
		{
			string[] inputSplit = input.Split(' ');

			switch (inputSplit[0])
			{
				case "setday":
					
			}
		}

		
	}
}
