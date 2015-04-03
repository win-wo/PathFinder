using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PathFinder
{
	class Program
	{
		static void Main(string[] args)
		{
			PathFinder pathFinder2 = new PathFinder(@"E:\learnSpace\mazeSolver\mazeSolver\mazeSolver\example2.bmp");


			var output = new StringBuilder();
			foreach (var arg in args)
			{
				PathFinder pathFinder = new PathFinder(arg);
				output.Append(arg + " ");
			}
			MessageBox.Show(output.ToString());
		}
	}
}
