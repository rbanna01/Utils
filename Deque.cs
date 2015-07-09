using System;

namespace Deque
{
	class Deque<T>
	{ 
		//resize when 1/4 capacity used
		//do not reduce size when capacity is <= DEFAULT_CAPACITY
		private int DEFAULT_CAPACITY = 10;
		private int DEFAULT_MIN = 0;
		private T[] array;
		private int lo;
		private int hi;
		private int init;
		private bool lastFront;
		//resize when used capacity < min
		private int min;
		private int occupied; //how many elements are being used?
		//capacity will not always be even
		public Deque(int capacity) {
			array = new T[capacity];
			init = (array.Length / 2);
			hi = init;
			lo = init-1;
			occupied = 0;
			if (capacity < 20)
				min = 5;
			else 
				min = capacity / 4;
		}

		public Deque() {
			array = new T[DEFAULT_CAPACITY];
			init = (array.Length / 2);
			hi = init;
			lo = init-1;
			occupied = 0;
			min = DEFAULT_MIN;
		}
		//use circular buffer, loop end to front and VV if needed.
		//check lo != hi when adding 
		//resize to double size when full, 
		//vacant elements given by: centre-lo + hi-centre
		public void AddFront(T toAdd) { //init lo 5
			if (lo < 0) 
				lo = array.Length - 1;
			if (!array [lo].Equals (default(T))) { //equality with high not necessary
				if(lo == init-1) lo++; //bit crude
				Resize (array.Length * 2);
			}
			array [lo] = toAdd;
			lastFront = true;
			lo--;
			occupied++;
		}

		public void AddEnd(T toAdd) {
			if (hi == array.Length)
				hi = 0;
			if (!array [hi].Equals (default(T))) { //equality with lo not necessary
				if(hi == init) hi--;
				Resize (array.Length * 2);
			}
			array[hi] = toAdd;
			hi++;
			lastFront = false;
			occupied++;
    	}

		public T GetFront() { 
			if (lo == array.Length - 1)
				lo = 0;
			else
				lo++;
			if (array[lo].Equals(default(T))) 
				throw new InvalidOperationException ("Deque is empty!");
			T output = array [lo];
			array [lo] = default(T);
			occupied--;
			if (occupied < min)
				Shrink ();
			return output;
			}

		public T GetEnd() {
			if (hi == 0)
				hi = array.Length - 1;
			else hi--;
			if(array[hi].Equals(default(T))) 
			   throw new InvalidOperationException("Deque is empty!");
			T output = array [hi];
			array [hi] = default(T);
			occupied--;
			if (occupied < min)
				Shrink();
			return output;
		}
		//not tested
		//NB: assumes current array is <= 1/4 full
		//to be made private
		public void Shrink() {
			//will always be shrunk to half current array's size
			T[] newArray = new T[array.Length/2];
			int newLo = newArray.Length / 2 - 1;
			int newHi = newArray.Length / 2;
			int count = init - 1 - lo;
			int index = init - 1; //used to track lo part of current array
			while (count > 0) {
				newArray [newLo] = array [index];
				newLo--;
				index--;
				count--;
			}
			count = hi - init;
			index = init;
			while (count > 0) {
				newArray [newHi] = array [index];
				newHi++;
				index++;
				count--;
			}
			if (newArray.Length <= 20)
				min = 0;
			else
				min = newArray.Length / 4;
			lo = newLo;
			hi = newHi;
		}

		//going down?
		//needs tested. Now have lastFrotn - flags whether last addition was to front
		private void Resize(int newSize) {
			//Console.WriteLine("Resizing.. to " + newSize);
			T[] newArray = new T[newSize];
			int last;
			for (int i = 0; i < newArray.Length; i++)
				newArray [i] = default(T);
			int tLo = (newArray.Length / 2) - 1;
			int tHi = newArray.Length / 2;
			int index = (array.Length / 2) - 1; //start with lo
		//	Console.WriteLine ("Lo is " + lo + "index is " + index);
			if (lo > array.Length / 2-1) {//in extreme case
					while (index >= 0) { // old lo part of array
						newArray [tLo] = array [index]; // will occupy 
						tLo--; 
						index--;
				//		Console.WriteLine ("Reading " + index);
					}	
					//now from length to Lo
					index = array.Length - 1;
				}
				//testing if circle needed: already @ half of old array, 
				//so array.Length-1 - lo + half of old gives 
			if (lastFront)
				last = lo;
			else
				last = lo + 1;
				while (index >= last) {
					newArray [tLo] = array [index];
					index--;
					tLo--;
				//	Console.WriteLine ("reading " + index);
				}
			
			//cannot need to loop to back
			index = array.Length / 2;

				if (hi < index) { //ie has circled to front
					while (index <= array.Length - 1) {
						newArray [tHi] = array [index];
						tHi++;
						index++;
					}
					index = 0;
				}
			if (lastFront)
				last = hi - 1;
			else
				last = hi;
				while (index <= last) {
					newArray [tHi] = array [index];
					tHi++;
					index++;
				}

			lo = tLo;
			if (tLo < 0)
				lo = newArray.Length - 1;
			else
				lo = tLo;
			if (tHi > newArray.Length - 1)
				hi = 0;
			else hi = tHi;
			init = (newArray.Length / 2);
			array = newArray;
		} //ends Resize

		//testing only; remove
		public int GetOccupied() { return this.occupied;}
	}
}
