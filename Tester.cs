using System;
using Utils;
namespace Utils
{ //to-do: use StringBuilder equivalent for printing output and figure namespace thing out
	class Tester
	{
		Utils.Sorts s;
		int[] material;
		public Tester ()
		{
			s = new Sorts ();
			InsertionSortTest ();
			SelectionSortTest ();
			BubbleSortTest ();
			MergeSortTest ();

		} //ends constructor
		private void InsertionSortTest() {
			material = InitMaterial ();
			Sorts.InsertionSort (material);
			if (!CheckMaterial ()) {
				PrintMaterial ();
			}
		}

		private void SelectionSortTest() {
			material = InitMaterial ();
			s.SelectionSort (material);
			if (!CheckMaterial ()) {
				PrintMaterial ();
			}
		}

		private void BubbleSortTest() {
			material = InitMaterial ();
			s.BubbleSort (material);
			if (!CheckMaterial ()) {
				PrintMaterial ();
			}
		}

		private void MergeSortTest() {
			material = InitMaterial ();
			s.MergeSort (material);
			if (!CheckMaterial ())
				PrintMaterial ();
		}

		private bool CheckMaterial() {
			//checking: assume every number greater than one before
			for (int i = 1; i < material.Length; i++) {
				if (material [i] >= material [i - 1])
					continue; //output entire array
				else {
					return false;
					}
			} //ends for
			return true;

		} //ends CheckMaterial
		//ideally, would have string with name of method as argument obtained using reflection
		private void PrintMaterial() {
				Console.WriteLine ("Sort gives wrong results");
				string output = "";
				for (int i = 0; i < material.Length; i++) {
					for(int j = 0; j < 10; j++)
					output += material [(i*10+j)] + " ";
					output += "\n";
				}
				Console.WriteLine (output);
			}

	    private int[] InitMaterial() {
			int[] material = new int[100];
			Random r = new Random ();
			for (int i = 0; i < 100; i++) {
				material [i] = r.Next (100);
			} //ends for
			return material;
		} //ends InitMaterial
	
	}//ends class Tester
}

