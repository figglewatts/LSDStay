using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LSDStay
{
	class Program
	{


		public static IntPtr PSXOffset = new IntPtr(0x171A5C);

		public static IntPtr LocationTimeOffset = new IntPtr(0x8AC70);

		private static Process psx;

		static int Main(string[] args)
		{
			Console.WriteLine("LSDStay for psxfin - By Figglewatts");
			Console.WriteLine("-----------------------------------");
			Console.WriteLine("Use 'start' command to hook to PSX");
			
			for (;;)
			{
				//bool wm = Memory.WriteMemory(process, (IntPtr)0x072A16D0, 100);
				//Console.WriteLine((wm == true ? "Success" : "Failure") + IntPtr.Add(PSXOffset, LocationTimeOffset.ToInt32()).ToString());
				ProcessConsoleInput(GetConsoleInput());
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
				{

				} break;
				case "start":
				{
					Console.WriteLine("Finding psxfin.exe...");
					psx = PSXFinder.FindPSX();
					if (psx == null)
					{
						Console.WriteLine("Unable to find psxfin.exe, are you sure it's running?");
					}
					else
					{
						Console.WriteLine("Found psxfin.exe, PID: " + psx.Id);
					}
				} break;
				default:
				{
					Console.WriteLine("Did not recognize command: " + inputSplit[0]);
				} break;
			}
		}

		
	}
}
