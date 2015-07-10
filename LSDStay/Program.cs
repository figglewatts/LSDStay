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

		private static bool running = true;

		static int Main(string[] args)
		{
			Console.WriteLine("LSDStay for psxfin - By Figglewatts");
			Console.WriteLine("-----------------------------------");
			Console.WriteLine("Use 'start' command to hook to PSX");
			
			while (running)
			{
				//bool wm = Memory.WriteMemory(process, (IntPtr)0x072A16D0, 100);
				//Console.WriteLine((wm == true ? "Success" : "Failure") + IntPtr.Add(PSXOffset, LocationTimeOffset.ToInt32()).ToString());
				ProcessConsoleInput(GetConsoleInput());
			}
			return 0;
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
					if (!PSX.FindPSX())
					{
						Console.WriteLine("ERROR: Unable to find psxfin.exe, are you sure it's running?");
						break;
					}
					Console.WriteLine("Found psxfin.exe");
					Console.WriteLine("Opening process...");
					if (!PSX.OpenPSX())
					{
						Console.WriteLine("ERROR: Unable to open psxfin.exe");
						break;
					}
					Console.WriteLine("Process psxfin.exe opened, Handle: " + PSX.PSXHandle.ToString());
				} break;
				case "exit":
				{
					Console.WriteLine("Exiting...");
					running = !PSX.ClosePSX(); // close if we successfully exited
					break;
				}
				default:
				{
					Console.WriteLine("Did not recognize command: " + inputSplit[0]);
				} break;
			}
		}

		
	}
}
