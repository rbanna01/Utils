using System;
using System.Text;
namespace Utils {
	/*
	 * Includes: selection, insertion, shellsort, bubblesort, mergesort, quicksort 
	 * (latter two modified as in http://algs4.cs.princeton.edu/23quicksort
	 * and http://algs4.cs.princeton.edu/22mergesort)
	 */ 
	public class Sorts {
		public int[] material;

		private int CUTOFF = 10;

		/*"Eliminate the copy to the auxiliary array. It is possible to eliminate the time (but not the space) taken to copy 
		 * to the auxiliary array used for merging. To do so, we use two invocations of the sort method, one that takes its 
		 * input from the given array and puts the sorted output in the auxiliary array; the other takes its input from the 
		 * auxiliary array and puts the sorted output in the given array. With this approach, in a bit of mindbending 
		 * recursive trickery, we can arrange the recursive calls such that the computation switches the roles of the 
		 * input array and the auxiliary array at each level." */
		public void MergeSort(int[] input) {
			//so need a copy
			int[] material = new int[input.Length];
			for (int i = 0; i < material.Length; i++) {
				material [i] = input [i];
			}
			Merge(material, 0, input.Length-1, input);

		 }
		// Improvements: Insertion Sort for small subarrays ( < CUTOFF),
		// using two arrays recursively saves time, and subarrays already
		//sorted not remerged (i.e.when toSort[mid-1] < toSort[mid]
		private void Merge(int[] toSort, int lo, int hi, int[] output) {
			if (hi - lo < CUTOFF) {
				PartialInsertionSort (output, lo, hi);
			} else {					
				int mid = lo + ((hi - lo) / 2);
				//Console.WriteLine (mid);
				Merge (output, lo, mid, toSort);
				Merge (output, mid, hi, toSort);
				int x = lo;//zeroes appended here
				int y = mid;
				if (toSort [mid - 1] < toSort [mid]) {
					for (int i = lo; i <= hi; i++) {
						output [i] = toSort [i];
					}
				}
				else {
				for (int i = lo; i <= hi; i++) {
					if (x >= mid) {
						while (y <= hi) {
							output[i] = toSort [y];
							y++;
							i++;
						}
					} else if (y > hi) {
						while (x < mid) {
							output [i] = toSort [x];
							x++;
							i++;
						}
					}
					else if (toSort [x] < toSort [y]) {
						output [i] = toSort [x];
						x++;
					} else {
						output [i] = toSort [y];
						y++;
					}
				}//ends for
				} //inner else
			}//else
		} //ends Merge


		//Plain merge
		private void VanillaMerge(int[] toSort, int lo, int hi) {
			if (hi - lo == 1) {
				if (toSort [hi] < toSort [lo])
					Swap (toSort, hi, lo);
			} else {
				int mid = lo + ((hi - lo) / 2);
				VanillaMerge (toSort, lo, mid);
				VanillaMerge (toSort, mid, hi);// used with recursive merge used
				int x = lo;//zeroes appended here
				int y = mid;
				//Console.WriteLine (mid);

				int[] material = new int[hi - lo + 1];
				for (int i = 0; i < material.Length; i++) {
					if (x >= mid) {
						while (y <= hi) {
							material [i] = toSort [y];
							y++;
							i++;
						}

					} else if (y > hi) {
						while (x < mid) {
							material [i] = toSort [x];
							x++;
							i++;
						}
					}
					else if (toSort [x] < toSort [y]) {
						material [i] = toSort [x];
						x++;
					} else {
						material [i] = toSort [y];
						y++;
					}
				}
				for (int i = 0; i < material.Length; i++) {
					toSort[lo+i] = material[i];
					}
			}//else
		} //ends Merge
		/* In-place works with test array, but not in recursive method?
            while (x < hi && y < hi) {
					//can be thought of as insertion sort; one is sorted, so if one lower need to shuffle
					//find first difference, then insert?
					//use lowest
					if (x > y) {//misses comparison of mid to mid-1
						int temp = x;
						x = y;
						y = temp;
					}
					if (toSort [x] > toSort [y]) { //when y hits hi?
						int temp = toSort[y];
						for (int i = y; i > x; i--) {
							toSort [i] = toSort [i-1];
						}
						toSort [x] = temp;
						y++;

						/*
					Swap (toSort, x, y);
					x++; //can be considered sorted now, having been checked against x and y 


		} else
			x++;
	} //ends for 
*/

		public void QuickSort(int[] input) {
			Shuffle (input); //guarantees running time
			QuickSortStep(input, 0, input.Length-1);
		}

		//Optimized: uses Insertion Sort for small subarrays
		//Takes a sample of 3 items in array and chooses median as partioning element
		public void QuickSortStep(int[] input, int lo, int hi) {
			if (hi <= lo)
				return;
			if (hi - lo < CUTOFF) {
				PartialInsertionSort (input, lo, hi);
				return;
			}
			//index value now in lo
			int toUse = 0;
			int[] indices = new int[3];
			Random r = new Random ();
			for(int w = 0; w < indices.Length; w++) {
				indices [w] = r.Next (lo, hi);
			}
			for (int k = indices.Length-1; k > 0; k--) {
				for (int l = 0; l < k; l++) {
					if (input[indices [l]] > input[indices[k]]) {
						int tempIndex = indices [k];
						for (int x = k; x > l; x--) {
							indices [x] = indices [x - 1];
						} //ends for
						indices [l] = tempIndex;
					}
				}
			}
			toUse = indices [1]; //median
			Swap(input, toUse, lo);
			int i = lo+1;
			int j = hi;
			while (true) { 
				while (input [j] > input [lo]) {
					j--;
					if (j == lo)
						break;
				}
				while (input [i] < input [lo]) {
					i++;
					if (i == hi)
						break;
				}
				if (i >= j)
					break;
			    Swap (input, i, j);
				j--;
			}
			Swap (input, lo, j); //cutoff?
			QuickSortStep(input, lo, j-1); //ending conditions
			QuickSortStep(input, j+1, hi); //recursive
		}

		//Knuth shuffle
		public void Shuffle(int[] input) {
			Random r = new Random ();
			int j = 0;
			for (int i = input.Length-1; i > 0; i--) {
				j = r.Next (i);
				Swap (input, i, j);
			} //ends for 
		} //ends shuffle


		public void SelectionSort(int[] input) {
			
			int hi;
			for (int i =input.Length-1; i >0; i--) {
				hi = 0;
				for (int j = 1; j <= i; j++) {
					if (input [j] > input [hi]) {
						hi = j;
					}
				} //ends inner for
				Swap (input, i, hi);
			} //ends outer for
		}

		public void PartialInsertionSort(int[] input, int lo, int hi) {
			for (int i = lo+1; i <= hi; i++) {
				for (int j = lo; j < i; j++) {
					if (input [j] > input [i]) {
						int temp = input [i];
						for (int k = j+1; k <= i; k++) {
							input [k] = input [k - 1];
						} //ends shifting for
						input [j] = temp;
					}//if
				} //inner for
			} // outer for
		}

		public void InsertionSort(int[] input) {
			for (int i = 1; i < input.Length; i++) {
				for (int j = 0; j < i; j++) {
					if (input [j] > input [i]) {
						int temp = input [i];
						for (int k = j+1; k <= i; k++) {
							input [k] = input [k - 1];
						} //ends shifting for
						input [j] = temp;
					}//if
				} //inner for
			} // outer for

		} //ends InsertionSort

		public void BubbleSort(int[] input) {
			for (int i = input.Length-1; i >0; i--) {
				for (int j = 0; j < i; j++) {
					if (input [j] > input [j + 1]) {
						Swap (input, j, (j + 1));
					}
				}
			}
		}

		public void ShellSort(int[] input) {
			//sequence of increments: 3n+1 in reverse order
			//(3^k-1)/2, not greater than Ceiling(n/3)
			int max = (int) Math.Ceiling(Math.Log(input.Length, 3)) -1;
			int[] increments = new int[ max]; //since there's no need to do 1-sorting here
			int[] indices;
			for (int i = 0; i < max; i++) { 
				//i+2 since there's no need to sort 1- increments or with another array or 0-increments at all
				increments [i] = (int) ((Math.Pow (3, i+2) - 1) / 2);
				} 
			for (int i = increments.Length-1; i >= 1; i--) {
				
				//possible start indices: [0...(size-n)) for n-sorting 
				for(int j =0; j < increments[i]; j++) {     
					int size = (int) Math.Ceiling( (double) ((input.Length-j)/increments[i])); //size of each subarray to sort
					indices = new int[size]; 
					int element = 0;
					int loc = j;
					while (element < indices.Length) {
						indices [element] = loc;
						loc += increments [i];
						element++;
					}
					//now: insertionSort over these indices
 					for(int k = 1; k < indices.Length; k++) {
						for (int l = 0; l < k; l++) {
							if (input [indices [l]] > input [indices [k]]) {
								int toPreserve = indices [k];
								int temp = input[toPreserve];
									for(int x = k; x >= l+1; x--) {
										input[indices[x]] = input[indices[x-1]];
									}
									input[indices[l]] = temp;
							} //ends higher if
						}
					} 
			    } //ends inner for 
			} 
			InsertionSort (input); //add later; first, check the output is okay
		} //ends ShellSort


		private void Swap(int[] input, int x, int y) {
			int temp = input [x];
			input [x] = input [y];
			input [y] = temp;
		}


		public static void Main(string[] args) {
			//test: random array of length 100?
			Sorts s = new Sorts ();
			//s.InsertionSortTest ();
			//s.SelectionSortTest ();
			//s.BubbleSortTest ();
			//s.MergeSortTest ();//Sort
			//s.ShellSortTest();
			//s.ShuffleTest();
			//s.QuickSortTest();
			}

		public void ShuffleTest() {
			for (int i = 0; i < 10; i++) {
				InitMaterial ();
				SelectionSort (material);
				Shuffle (material);
				PrintMaterial ();
			}

		}

		public void QuickSortTest() {
			InitMaterial ();
			QuickSort (material);
			if (!CheckMaterial())
				PrintMaterial ();
		}


		//todo: apply. Test is good.
		public void MergeTest() {
			
			//probably too slow compared to just using another array
			int[] toSort = { 2,3, 4, 12, 43, 1, 2, 6, 11, 23 };
			int mid = toSort.Length / 2;
			int hi = toSort.Length - 1;
			int lo = 0;
			int x = lo;
			int y = mid;

			while (x < hi && y < hi) {
				//can be thought of as insertion sort; one is sorted, so if one lower need to shuffle
				//find first difference, then insert?
				//use lowest
				if (x > y) {//misses comparison of mid to mid-1
					int temp = x;
					x = y;
					y = temp;
				}
				if (toSort [x] > toSort [y]) { //when y hits hi?
					int temp = toSort [y];
					for (int i = y; i > x; i--) {
						toSort [i] = toSort [i - 1];
					}
					toSort [x] = temp;
					y++;


					Swap (toSort, x, y);
					x++; //can be considered sorted now, having been checked against x and y 
				}
			}
		} /*
			//	Merge (toSort, lo, mid);
				//	Merge (toSort, mid, hi); used with recursive merge used
				int x = lo;//zeroes appended here
				int y = mid;
				int[] material = new int[hi - lo + 1];
				for (int i = 0; i < material.Length; i++) {
					if (x >= mid) {
						while (y <= hi) {
							material [i] = toSort [y];
							y++;
							i++;
						}

					} else if (y > hi) {
						while (x < mid) {
							material [i] = toSort [x];
							x++;
							i++;
						}
					}
					else if (toSort [x] < toSort [y]) {
						material [i] = toSort [x];
						x++;
					} else {
						material [i] = toSort [y];
						y++;
					}
				}
				for (int i = 0; i < material.Length; i++) {
					toSort[lo+i] = material[i];
					Console.WriteLine (material [i]);
				}


		}
		/*	while (x < hi && y < hi) {
				//can be thought of as insertion sort; one is sorted, so if one lower need to shuffle
				//find first difference, then insert?
				//use lowest
				if (x > y) {//misses comparison of mid to mid-1
					int temp = x;
					x = y;
					y = temp;
				}
				if (toSort [x] > toSort [y]) { //when y hits hi?
					int temp = toSort[y];
					for (int i = y; i > x; i--) {
						toSort [i] = toSort [i-1];
					}
					toSort [x] = temp;
					y++;

					/*
					Swap (toSort, x, y);
					x++; //can be considered sorted now, having been checked against x and y 


	} else
		x++;
      */

		public void InsertionSortTest() {
			material = InitMaterial ();
			InsertionSort (material);
			if (!CheckMaterial ()) {
				PrintMaterial ();
			}
		}

		public void SelectionSortTest() {
			material = InitMaterial ();
			SelectionSort (material);
			if (!CheckMaterial ()) {
				PrintMaterial ();
			}
		}

		public void BubbleSortTest() {
			material = InitMaterial ();
			BubbleSort (material);
			if (!CheckMaterial ()) {
				PrintMaterial ();
			}
		}

		private void MergeSortTest() {
			material = InitMaterial ();
			MergeSort (material);
			if (!CheckMaterial ())
				PrintMaterial ();
		}

	//NB: no test method for method PLainVanillaSort
/*	   private void VanillaMergeSortTest() {
	    	material = InitMaterial ();
		    PlainMergeSort (material);
		    if (!CheckMaterial ())
			   PrintMaterial ();
	       } */

	   private void ShellSortTest() {
		    material = InitMaterial();
			ShellSort (material);
			if (!CheckMaterial ())
				PrintMaterial ();
	   }

		private bool CheckMaterial() {
			//sorted: is every number greater than the one before?
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
				output += material [i] + " ";

			}
			Console.WriteLine (output);
		}

		private int[] InitMaterial() {
			material = new int[100];
			Random r = new Random ();
			for (int i = 0; i < material.Length; i++) {
				material [i] = r.Next (1, 100);
			} //ends for
			return material;
		} //ends InitMaterial
	}

}
