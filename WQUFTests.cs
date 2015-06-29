using NUnit.Framework;
using System;
using System.Collections.Generic;
using Structures;

namespace Application
{
	[TestFixture ()]
	public class WQUFTest
	{
    	private int size = 50;
		private Random r;
		private int x;
		private int y;
		WQUF uF;
		[SetUp]
		public void Init() {
				uF = new WQUF(50);
				r = new Random ();
				x = r.Next (size);
				y = r.Next (size);
				while (x==y) y = r.Next(size);
			}

		[TestCase ()]
		public void TestConnected ()
			{
				uF.Union (x, y);
				Assert.IsTrue(uF.Connected(x,y));
			}

		[TestCase()]
		public void TestSize()
			{
				Assert.IsTrue (uF.Count () == size);
			}

		[TestCase()]
		public void TestCountChange()
			{
				int reps = r.Next (10+1);
				int expected = size;
				for (int i = 0; i < reps; i++) {
					uF.Union (x, y);
					expected--;
				int temp = r.Next (size);
				while(temp == x || temp == y) temp = r.Next (size);
				x = temp;
				temp = r.Next (size);
				while(temp == x || temp == y) temp = r.Next (size);
				y = temp;
				} //ends for
				Assert.IsTrue (uF.Count () == expected);
			}

		[TestCase()]
		public void TestTransitiveConnectivity() {
				uF.Union (x, y);
				int z = r.Next (size);
				uF.Union (z,x);
				Assert.IsTrue (uF.Connected (y, z));
			} //ends transitive connectivity test

		[TestCase()]
		public void WeightingTest() {
			//so: add union of 2 to single, and check latter changes
			x = 0;
			y = 1;
			int z = 2;
			uF.Union (x, y);
			uF.Union (x, z);
			Assert.IsTrue (uF.Find (z) == y);
		}

		[TestCase()]
		public void FindTest() {
			//root should = y;
			uF.Union(x,y);
			int[] indices = new int[10];
			//next, ten other unique vals to check find and size fns
		} //ends FindTest

		[TestCase()]
		public void CompleteConnection() {
			int expected = size;
			for (int i = 1; i < size; i++) {
				uF.Union (i, i - 1);
				expected--;
			} //ends for 

			Assert.IsTrue(expected == 1);
			for (int i = 0; i < size; i++) {
				for (int j = i + 1; j < size; j++)
					Assert.IsTrue (uF.Connected (i, j));
			}
		} //ends CompleteConnection
		/*
		[TestCase()]
		public void ConnectAll() {
			//do in random order
			int[] sequence = new int[50];
			Dictionary<int, int> d = new Dictionary<int, int> ();
			for (int i = 0; i < 50; i++)
				d.Add (i, i);
			//first is val, second is root
			int expected = 50;
			int action; //4 vals: count, union, connected, find
			while (expected > 1) {
				x = r.Next (size);
				y = r.Next (size);
				while (y == x)
					y = r.Next (size);
				action = r.Next (4);
				switch (action) {
				case 0: //count
					Assert.IsTrue (uF.Count () == expected);
					break;
				case 1: //union of 2.
					//in dictionary: change roots to other
					//just one node modified

					if (!(GetRoot (d, x) == GetRoot(d, y))) {
						uF.Union (x, y); //default is change x to y
						expected--;
						try {
						d.Add (x, y);
						}
						catch(ArgumentException aE) {
							d.Remove (x);
							d.Add (x, y);
						}
						Assert.IsTrue (GetRoot (d, x) == uF.Find (x));
					} else {
						//size shouldn't change
						Assert.IsTrue (uF.Connected(x,y));
						uF.Union(x,y);
						Assert.IsTrue (uF.Count() == expected);
						Assert.IsTrue (uF.Connected(x,y));
					}
					break;
				case 2:
					if (GetRoot (d, x) == GetRoot (d, y)) {
						Assert.IsTrue (uF.Connected (x, y));
					} else
						Assert.IsFalse (uF.Connected (x, y));
					break;
				default:
					Assert.IsTrue (GetRoot(d, x) == uF.Find(x));
					Assert.IsTrue (GetRoot(d, y) == uF.Find(y));
					break;

				}
				Console.WriteLine (action);
			}

		} //ends ConnectAll test case  */
		private int GetRoot(Dictionary<int, int> d, int index) {
			int temp = 0;
			do {
				d.TryGetValue (index, out temp);
			} while(temp != index);
			return temp;
			}
 
	/*public static void Main(string[] args) {
		Dictionary<int, int> d = new Dictionary<int, int>();
			Random r = new Random ();
			int x;
			int y;
			int[] xs = new int[5];
			int[] ys = new int[5];
			for (int i = 0; i < 10; i++)
				d.Add (i, i);
			for (int i = 0; i < 5; i++) {
				x = r.Next (10);
				y = r.Next (10);
				while (x == y)
					y = r.Next (10);
				xs[i] = x;
				ys[i] = y;
				try {
				d.Add(x,y);
				}
				catch(ArgumentException aE) {
				
				}
			}
			string xOut = "";
			string yOut = "";
			for (int i = 0; i < 5; i++) {
				xOut += xs[i] + " ";
				yOut += ys[i] + " ";
			}
			Console.WriteLine ("xs " + xOut);
			Console.WriteLine ("ys " + yOut);
			string connections ="";
			string indices = "";
			int temp;
			for (int i = 0; i < 10; i++) {
				indices += i + " ";
				d.TryGetValue (i, out temp);
				connections += temp + " ";
			}
			Console.WriteLine (indices);
			Console.WriteLine (connections);
			//should give output 
		}*/

	} 
}

