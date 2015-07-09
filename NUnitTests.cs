using System;
using NUnit.Framework;
using System.Text;
namespace Deque
{
	public class NUnitTests
	{
		[TestFixture ()]
		public class DequeTests
		{
			int x = 5;
			int y = 10;
			int z = 15;
			DoublyLinkedList<int> dLL;
			int[] values = { 1, 2,3, 4, 5, 6, 7 ,8 ,9, 10};
			Deque<int> d;
			//to test: going past 0 or hi, 
			//resizing up
			//resizing down
			//removign when empty
			//use default vals for all 
			[SetUp]
			public void Setup() {
				d = new Deque<int> ();
				dLL = new DoublyLinkedList<int> ();
			}


			[Test ()]
			public void EmptyTest ()
			{
				try{
					d.GetFront();
					Assert.Fail();
				} catch(InvalidOperationException iOE) {
					Assert.Pass ();
				}
			}

			[Test()]
			public void AddRemoveFrontTest() {
				d.AddFront (values [0]);
				Assert.IsTrue (d.GetFront () == values [0]);
			}


			[Test()]
			public void CircleTest() {
				for (int i = 0; i < 10; i++) {
					d.AddFront (values [i]);
				}
				//if done right, should be output in reverse order
				for (int i = 0; i < 10; i++) {
					Assert.IsTrue (d.GetFront () == values [9-i]);
				}
			}
			[Test()]
			public void AddRemoveEndTest() {
				d.AddEnd (values [0]);
				Assert.IsTrue (d.GetEnd () == values [0]);
			}
			//check it moves round to front properly
			[Test()]
			public void EndCircleTest() {
				for (int i = 0; i < values.Length; i++) {
					d.AddEnd (values [i]);
				}
				for (int i = 0; i < values.Length; i++) { //exception here
					Assert.IsTrue (d.GetEnd () == values [values.Length-1 - i]);
				}//ends for 
			}
			//test interaction between front and End will need to
			//be a bit more extensive
			[Test()]
			public void AddFrontRemoveEndTest() {
				d.AddFront (values [0]);
				Assert.IsTrue (d.GetEnd () == values [0]);
			}
			[Test()]
			public void AddEndRemoveFrontTest() {
				d.AddEnd (values [0]);
				Assert.IsTrue (d.GetFront () == values [0]);
			}
			[Test()]
			public void FillTest() {
				for (int i = 1; i < 11; i++)
					d.AddFront (i);
				for (int i = 10; i > 0; i--) {
					//Console.WriteLine ("i is " + i);
					//Console.WriteLine (d.GetFront ());
					Assert.IsTrue (d.GetFront () == i);
				}
			}

			[Test()]
			public void ResizeFrontTest() {
				for(int i = 1; i < 12; i++) d.AddFront(i);
				for (int i = 11; i >0; i--) {
				   Assert.IsTrue (d.GetFront () == i);
				}
			}

			[Test()]
			public void ResizeEndTest() {
				for (int i = 1; i < 12; i++)
					d.AddEnd (i);
				for (int i = 11; i > 0; i--) {
					int x = d.GetEnd ();
					Assert.IsTrue (x == i);
				}
			}

			[Test()]
			public void ShrinkTest() {
				//add random guff till it expands, then remove till
				//shrinks
				//expands from 10 to 20, resizes at capacity 5
				for (int i = 0; i < 11; i++) {
					Random r = new Random ();
					int next = r.Next ();
					while (next == 0)
						next = r.Next ();
					dLL.AddFront (next);
					d.AddFront (next);
				}
				//now remove
				while (d.GetOccupied () >= 5) {
					Assert.IsTrue(dLL.GetFront () == d.GetFront ());
				} //should now have been shrunk
				while (dLL.HasNext ()) {
					Assert.IsTrue (dLL.GetFront () == d.GetFront ());
				}
			} //ends ShrinkTest
		
			[Test()]
			public void ExhaustiveTest() {
				//here: 500 random operations
				Random r = new Random ();
				bool pass = true;
				int[] operations = new int[500];
				int action;
				int dOut = 0;
				int dLLOut = 0;
				int addFront = 1; //for reference
				int addBack = 2;
				int getFront = 3;
				int getBack = 4;
				int next = 0;
				try {
				for (int i = 0; i < 500; i++) {
					//actions are
					action = r.Next(1, 4);
					operations [i] = action;
					switch (action) {
					case 1:
						next = r.Next ();
						dLL.AddFront (next);
						d.AddFront (next);
						break;
					case 2:
						next = r.Next ();
						dLL.AddFront (next);
						d.AddFront (next);
						break;
					case 3:
						try {
							dOut = d.GetFront ();
						} catch (InvalidOperationException iOE) {
							try {
								dLLOut = dLL.GetFront ();
								//Assert.Fail ();
							} catch (InvalidOperationException iOE2) {
								continue;
							}
						}
						if (dLL.GetFront () == dOut)
							continue;
						else
							pass = false;
						break;
					default:
						try {
							dOut = d.GetEnd ();
						} catch (InvalidOperationException iOE) {
							try {
								dLLOut = dLL.GetBack ();
								//Assert.Fail ();
							} catch (InvalidOperationException iOE2) {
								continue;
							}
						}
						if (dLL.GetBack () == dOut)
							continue;
						else
							pass = false;
						break;
					} //ends switch
					if (!pass) {
						StringBuilder sB = new StringBuilder ();
						for (int j = 0; j <= i; j++)
							sB.Append (" " + operations [i]);
						Console.Write(sB.ToString ());
						break;
					}
				}//ends for
				}
				catch(InvalidOperationException iOE) {
					Console.WriteLine (iOE.Message);
					StringBuilder sB = new StringBuilder ();
					int i = 0;
					while(operations[i] != 0)
						sB.Append (" " + operations [i]);
					Console.Write(sB.ToString ());
				}
			} //ends ExhaustiveTest()



			/*
			    Doubly-Linked List Tests
			*/
			[Test()]
			public void ListAddFrontTest() {
				dLL.AddFront(x);
				dLL.AddFront(y);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				Console.WriteLine (out1);
				Console.WriteLine (out2);
				Assert.IsTrue (out1 == 10 && out2 == 5);
			}


			[Test()]
			public void ListFBTest() {
				dLL.AddFront(x);
				dLL.AddBack(y);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				Assert.IsTrue (out1 == 5 && out2 == 10);
			}

			[Test()]
			public void ListBBTest() {
				dLL.AddBack(x);
				dLL.AddBack(y);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				Assert.IsTrue (out1 == 5 && out2 == 10);
			}

			[Test()]
			public void ListBFTest() {
				dLL.AddBack(x);
				dLL.AddFront(y);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				Assert.IsTrue (out1 == 10 && out2 == 5);
			}
			[Test()]
			public void ListFBFTest() {
				dLL.AddFront(x);
				dLL.AddBack(y);
				dLL.AddFront(z);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				int out3 = dLL.GetFront();
				Assert.IsTrue (out1 == 15 && out2 == 5 && out3 == 10);
			}
			[Test()]
			public void ListFBBTest() {
				dLL.AddFront(x);
				dLL.AddBack(y);
				dLL.AddBack(z);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				int out3 = dLL.GetFront();
				Assert.IsTrue (out1 == 5 && out2 == 10 && out3 == 15);
			}
			[Test()]
			public void ListEmptyTest() {
				dLL.AddFront(x);
				dLL.AddBack(y);
				dLL.AddFront(z);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				int out3 = dLL.GetFront();
				try{
					Console.WriteLine(dLL.GetFront());
					Assert.Fail();
				} catch(InvalidOperationException) {
				}
				dLL.AddFront (x);
				Assert.IsTrue (dLL.GetFront () == 5);
				try{
					dLL.GetBack();
					Assert.Fail();
				} catch(InvalidOperationException) {
				}
				Assert.Pass ();
			}
			[Test()]
			public void TestTest() {
				dLL.AddFront (x);
				dLL.AddFront (y);
				dLL.AddFront (z);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				int out3 = dLL.GetFront();
				//dLL.GetFront ();
				Assert.IsTrue (out1 == z && out2 == y && out3 == x);
			}	

			[Test()]
			public void FBBTest() {
				dLL.AddFront(x);
				dLL.AddBack(y);
				dLL.AddBack (z);
				int out1 = dLL.GetFront();
				int out2 = dLL.GetFront();
				int out3 = dLL.GetFront();
				Assert.IsTrue (out1 == x && out2 == y && out3 == z);
			}
				
			/*
				//3 vars: check adding head, adding tail, removing head, 
				//removing tail
				dLL.AddFront(x);
				dLL.AddBack(y);
				dLL.AddBack (z);
				Console.WriteLine("Ln 110 should be 5, is " + dLL.GetFront());
				Console.WriteLine("Ln 111 should be 10, is " + dLL.GetFront());
				Console.WriteLine("Ln 112 should be 15, is " + dLL.GetFront());
			*/


	}
 }
}

