using System;
using Serilog;

namespace Sturla.io.Func.CachingOfExpensiveMethodCalls.Console
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
			//that gets returned from the Memoize method).
			Fibonacci = Fibonacci.Memoize();

			// First run of two
			for (int i = 0; i < 50; i++)
				Log.Information("{fib}", Fibonacci(i));

			// Now every value is fetched from the Dictionary (not calculated)
			for (int i = 0; i < 50; i++)
				Log.Information("{fib}", Fibonacci(i));

			// Another example
			SuperComplexLowerCasing = SuperComplexLowerCasing.Memoize();

			// Some other super complex and time consuming lowercasing.
			Log.Information("{lower}", SuperComplexLowerCasing("Test"));
			Log.Information("{lower}", SuperComplexLowerCasing("Test"));
			Log.Information("{lower}", SuperComplexLowerCasing("Not"));
			Log.Information("{lower}", SuperComplexLowerCasing("Test"));

			System.Console.ReadKey();
		}

		public static Func<int, int> Fibonacci = (n) =>
		{
			return n > 1 ? Fibonacci(n - 1) + Fibonacci(n - 2) : n;
		};

		public static Func<string, string> SuperComplexLowerCasing = (n) =>
		{
			// I just didn't come up with anything more clever. Need to find my old Calculus books.
			return n.ToLower();
		};
	}
}
