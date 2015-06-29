using NUnit.Framework;
using System;
using System.Random;

namespace Application
{
	[TestFixture ()]
	public class WQUFTest
	{
		private int size = 50;
		private Random r;
		private int x;
		private int y;
		[SetUp]
		public void Init() {
		WQUF uF = new WQUFTest(50);
			r = new Random ();
			int x = r.Next (size);
			int y = r.Next (size);
			while (x==y) y = r.Next(size);
		}

		[TestCase ()]
		public void TestConnected ()
		{
			uF.union (x, y);
			AssertIsTrue(uF.Connected(x,y));
		}

		[TestCase()]
		public void TestSize()
		{
			Assert.isTrue (uD.Size () == size);
		}

		[TestCase()]
		public void TestSizeChange()
		{
			int reps = r.Next (10+1);
			int expected = size;
			for (int i = 0; i < reps; i++) {
				uF.union (x, y);
				expected--;
		    } //ends for
			Assert.isTrue (uD.Size () == expected);
		}
	}
}

