using System;
using Serilog;

namespace Sturla.io.Func.Six.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			#region Logging
			Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.WriteTo.Console()
			.CreateLogger();
			#endregion

			// Set up 
			// Wraps the Dictionary<TArgument, TResult> dictionary and the original function into a NEW function, 
			//that gets returned from the Meoize method).
			Fibonacci = Fibonacci.Memoize();
			SuperComplexLowerCaseing = SuperComplexLowerCaseing.Memoize();

			// First run of two
			for (int i = 0; i < 50; i++)
				Log.Information("{fib}", Fibonacci(i));

			// Some other super complex and time consuming lowercasing.
			Log.Information("{lower}", SuperComplexLowerCaseing("Test"));
			Log.Information("{lower}", SuperComplexLowerCaseing("Test"));
			Log.Information("{lower}", SuperComplexLowerCaseing("Not"));
			Log.Information("{lower}", SuperComplexLowerCaseing("Test"));

			// Now notice that everyone of these are fetched from the Dictionary of values calculated in the first run.
			for (int i = 0; i < 50; i++)
				Log.Information("{fib}", Fibonacci(i));


			System.Console.ReadKey();
		}

		public static Func<int, int> Fibonacci = (n) =>
		{
			return n > 1 ? Fibonacci(n - 1) + Fibonacci(n - 2) : n;
		};

		public static Func<string, string> SuperComplexLowerCaseing = (n) =>
		{
			// I just didn't come up with anything more clever. Need to find my old Calculus books.
			return n.ToLower();
		};
	}
}
