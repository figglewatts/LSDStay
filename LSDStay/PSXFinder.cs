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

		}
	}
}
