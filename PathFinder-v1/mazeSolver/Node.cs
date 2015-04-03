using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
	public class Node
	{
		public int X;
		public int Y;
		public Color Color;
		public int Score;
		
	
		public int movementCost;
		public int heuristic;
		public Node Parent;

		public int F()
		{
			return (movementCost + heuristic);
		}

		public bool Match(Node node)
		{
			if (X == node.X && Y == node.Y)
			{
				return true;
			}
			return false;
		}
	}
}
