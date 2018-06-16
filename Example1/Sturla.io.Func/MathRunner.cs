using System;

namespace Sturla.io.Func.One.Lib
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
		public int RunMethod(int value1, int value2, Func<int,int,int> mathProbem)
		{
			return mathProbem(value1, value2);
		}
    }
}
