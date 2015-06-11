using System;
using System.Text;
namespace Utils {
	/*
	 * Includes: selection, insertion, shellsort, bubblesort, mergesort
	 * 
	 */ 
	public class Sorts {
		//to do: quick, and optimized versions
		//modify for inverted sort? All in-place
		int[] material;
		//check whether top-down or bottom-up faster
		//segmentation fault?
		public void MergeSort(int[] input) {
			Merge(input, 0, input.Length-1);
		}
		private void Merge(int[] toSort, int lo, int hi) {
			if (hi - lo == 1) {
				if (toSort [hi] < toSort [lo])
					Swap (toSort, hi, lo);
				
			} else {
				int mid = lo + ((hi - lo) / 2);
				Merge (toSort, lo, mid);
				Merge (toSort, mid, hi);// used with recursive merge used
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
					}
			}//else
		} //ends Merge
		/*
				int mid = lo + ((hi - lo) / 2);
				Merge (toSort, lo, mid);
				Merge (toSort, mid, hi);// used with recursive merge used
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
				
				}
			}//else
	} //ends Merge

		/*  Conventional merge with another array
		 * for (int i = 0; i < material.Length; i++) {
				if (x >= mid) {
					while (y <= hi) {
						material [i] = toSort [y];
					    y++;
					}
					break;
				} else if (y > hi) {
					while (x < mid) {
						material [i] = toSort [x];
						x++;
						}
					break;
				}
				else if (toSort [x] < toSort [y]) {
					material [i] = toSort [x];
					x++;
				} else {
					material [i] = toSort [y];
					y++;
				}
			}
            In-place
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
		public void SelectionSort(int[] input) {
			//in-place
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
		//to do: optimize
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
			//deciding on number of passes?
			//sequence of increments: 3n+1 in reverse order
			//(3^k-1)/2, not greater than Ceiling(n/3)
			int max = (int) Math.Ceiling(Math.Log(input.Length, 3)) -1;
			int[] increments = new int[ max]; //since there's no need to do 1-sorting here
			int[] indices;
			for (int i = 0; i < max; i++) { 
				//i+2 since there's no need to sort 1- increments or with another array or 0-increments at all
				increments [i] = (int) ((Math.Pow (3, i+2) - 1) / 2);//todo: check this library call
				} 
			for (int i = increments.Length-1; i >= 1; i--) {
				
				//possible start indices: [0...(size-n)) for n-sorting 
				for(int j =0; j < increments[i]; j++) {     //weird 
					int size = (int) Math.Ceiling( (double) ((input.Length-j)/increments[i])); //size of each subarray to sort
					indices = new int[size]; //get all possible subsets of array and sort them
					int element = 0;
					int loc = j;
					while (element < indices.Length) {
						indices [element] = loc; //issue here: out of range
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
					} //could add an output statement here if there are problems
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
			s.ShellSortTest();
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

	   private void ShellSortTest() {
		    material = InitMaterial();
			ShellSort (material);
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
