using System;

namespace Structures
{
	public class WQUF
	{
		private int[] nodes;
		private int count;
		public WQUF (int size)
		{
			if (size < 1)
				throw new ArgumentException (
					"Argument provided to constructor must be > 0");
			nodes = new int[size];
			for (int i = 0; i < size; i++)
				nodes [i] = i;
			count = size;
		}

		public void Union(int x, int y) {
			CheckValid(x);
			CheckValid (y);
			if (Find (x) != Find (y)) {
				if (GetSize (x) > GetSize (y))
					nodes [y] = nodes [x];
				else
					nodes [x] = nodes [y]; 
				count--;
			}

		}

		public int Find(int x) {
			CheckValid(x);
			//returns root of component @ x
			while (nodes [x] != x) {
				x = nodes [x];
			}
			return x;
		}
		// returns whether x and y are in same component
		public bool Connected(int x, int y) {
			CheckValid(x);
			CheckValid (y);
			return Find(x) == Find(y);
		}

		public int Count() {
			//number of components in UF
			return count;
		}
		private void CheckValid(int input) {
			if (input >= nodes.Length || input < 0)
				throw new
				ArgumentException ("Input to WQUF must be >0 && within object's specified size!");
		}

		private int GetSize(int input) {
			int output = 1; //will always be at least 1
			while(nodes[input] != input) {
				input = nodes [input];
				output++;	
         	}
			return output;
	   } 
       }
}

