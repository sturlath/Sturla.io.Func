using System;

namespace Sturla.io.Func.Two.Lib
{
	public class MathRunner
    {
		/// <summary>
		/// This is a generic method in the sense that it just runs the 
		/// delegate method passed in.
		/// </summary>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <param name="mathProbem">The Func delegate method passed in.</param>
		public void RunMethod(int value1, int value2, Action<int,int> mathProbem)
		{
			 mathProbem(value1, value2);
		}
    }
}
