using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFinder
{
	public class PathFinder
	{
		private Bitmap inputBitmap;
		private List<Node> openList;
		private List<Node> closeList;
		/// <summary>
		/// Just A star
		/// </summary>
		/// <param name="mazeImgPath"></param>
		public PathFinder(string mazeImgPath)
		{
			inputBitmap = ConvertDataToBitmap(mazeImgPath);
			if (inputBitmap == null)
			{
				return;
			}

			
			
			Node start = new Node();//color blue for start
			Node end = new Node();//color red for exit
			SetStartAndEndNodes(inputBitmap, end, start);

			openList = new List<Node>();
			closeList = new List<Node>();

			bool pathFound = false;
			AddOpenList(start);
			Node lowestF = new Node();
			while (openList.Count > 0)
			{

				//get lowest F in openlist
				lowestF = GetOpenList();
				

				if (end.Match(lowestF))
				{
					pathFound = true;
					break;
				}
				GetNodeNeighbors(lowestF);
				MoveFromOpenListToCloseList(lowestF);
				//move to closelist 
				

				//anti checking closelist , set parent
				//switch parent if lower cost
			}


			if (pathFound)
			{
				//get path
				Bitmap solved = new Bitmap(inputBitmap);
				var node = lowestF;
				while (node.Parent != null)
				{
					solved.SetPixel(node.X,node.Y,Color.Chartreuse);
					node = node.Parent;
				}
				solved.Save("solved.png");
			}
			else
			{
				//no path
				Bitmap solved = new Bitmap(inputBitmap);
				foreach (var node in closeList)
				{
					solved.SetPixel(node.X, node.Y, Color.Chartreuse);
				}
				solved.Save("solved.png");
			}
		}

		private Node GetOpenList()
		{
			//to do
			return openList.First();
		}
		private void AddOpenList( Node node)
		{
			//to do
			openList.Add(node);
		}
		private void MoveFromOpenListToCloseList(Node node)
		{
			//to do
			closeList.Add(node);
			openList.Remove(node);
		}

		private List<Node> GetNodeNeighbors(Node node)
		{
			var list = new List<Node>();
			if (node.X - 1 >= 0 )//left
			{
				//not in open or close
				//get in openlist 
				var color = inputBitmap.GetPixel(node.X - 1, node.Y);
				if (color.R > 10 || color.G > 10 || color.B > 10)
				{
					var neighborNode = IsInOpenList(node.X - 1, node.Y) ?? IsInCloseList(node.X - 1, node.Y);
					if (neighborNode == null)
					{
						neighborNode = new Node()
						{
							X = node.X - 1,
							Y = node.Y,
							Parent = node,
							Score = node.Score + 1
						};
						AddOpenList(neighborNode);
					}
					else
					{
						if (node.Score + 1 < neighborNode.Score)
						{
							neighborNode.Score = node.Score + 1;
							neighborNode.Parent = node;
						}
					}
					list.Add(neighborNode);
				}
			}
			if (node.X + 1 < inputBitmap.Width)//right
			{
				var color = inputBitmap.GetPixel(node.X + 1, node.Y);
				if (color.R > 10 || color.G > 10 || color.B > 10)
				{
					var neighborNode = IsInOpenList(node.X + 1, node.Y) ?? IsInCloseList(node.X + 1, node.Y);
					if (neighborNode == null)
					{
						neighborNode = new Node()
						{
							X = node.X + 1,
							Y = node.Y,
							Parent = node,
							Score = node.Score + 1
						};
						AddOpenList(neighborNode);
					}
					else
					{
						if (node.Score + 1 < neighborNode.Score)
						{
							neighborNode.Score = node.Score + 1;
							neighborNode.Parent = node;
						}
					}
					list.Add(neighborNode);
				}
			}
			if ( node.Y - 1 >= 0)//up
			{
				var color = inputBitmap.GetPixel(node.X, node.Y - 1);
				if (color.R > 10 || color.G > 10 || color.B > 10)
				{
					var neighborNode = IsInOpenList(node.X, node.Y - 1) ?? IsInCloseList(node.X, node.Y - 1);
					if (neighborNode == null)
					{
						neighborNode = new Node()
						{
							X = node.X,
							Y = node.Y - 1,
							Parent = node,
							Score = node.Score + 1
						};
						AddOpenList(neighborNode);
					}
					else
					{
						if (node.Score + 1 < neighborNode.Score)
						{
							neighborNode.Score = node.Score + 1;
							neighborNode.Parent = node;
						}
					}
					list.Add(neighborNode);
				}
			}
			if ( node.Y + 1 < inputBitmap.Height)//down
			{
				var color = inputBitmap.GetPixel(node.X, node.Y + 1);
				if (color.R > 10 || color.G > 10 || color.B > 10)
				{
					var neighborNode = IsInOpenList(node.X, node.Y + 1) ?? IsInCloseList(node.X, node.Y + 1);
					if (neighborNode == null)
					{
						neighborNode = new Node()
						{
							X = node.X,
							Y = node.Y + 1,
							Parent = node,
							Score = node.Score + 1
						};
						AddOpenList(neighborNode);
					}
					else
					{
						if (node.Score + 1 < neighborNode.Score)
						{
							neighborNode.Score = node.Score + 1;
							neighborNode.Parent = node;
						}
					}
					list.Add(neighborNode);
				}
			}
			return list;
		}

		private Node IsInOpenList(int X, int Y)
		{
			return openList.FirstOrDefault(item => item.X == X && item.Y == Y);
		}
		private Node IsInCloseList(int X, int Y)
		{
			return closeList.FirstOrDefault(item => item.X == X && item.Y == Y);
		}
		private void SetStartAndEndNodes(Bitmap inputBitmap, Node end, Node start)
		{
			List<Color> colors = new List<Color>();
			for (int i = 0; i < inputBitmap.Width; i++)
			{
				for (int j = 0; j < inputBitmap.Height; j++)
				{
					Color pixel = inputBitmap.GetPixel(i, j);
					if (!colors.Contains(pixel))
					{
						colors.Add(pixel);
					}

					if ( pixel.R > 200 && pixel.G < 50 && pixel.B < 50 )//Red
					{
						end.X = i;
						end.Y = j;
					}
					if (pixel.R < 50 && pixel.G < 50 && pixel.B > 200)//Blue
					{
						start.X = i;
						start.Y = j;
					}
				}
			}
			if (colors.Count > 4)
			{
				//MessageBox.Show("Too much different color " + mazeImgPath);
			}
		}

		private Bitmap ConvertDataToBitmap(string mazeImgPath)
		{
			Bitmap inputBitmap;
			try
			{
				inputBitmap = new Bitmap(mazeImgPath);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message + " " + mazeImgPath);
				return null;
			}
			return inputBitmap;
		}
	}
}
