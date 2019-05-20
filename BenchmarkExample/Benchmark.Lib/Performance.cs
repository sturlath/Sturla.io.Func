using System;
using System.Diagnostics;
using Serilog;

namespace Sturla.io.Func.BenchmarkLib
{
	public static class Performance
	{
		/// <summary>
		/// Benchmark a method that "returns" void. (has a void signature)
		/// </summary>
		/// <param name="action"></param>
		public static void Benchmark(Action action)
		{
			var watch = new Stopwatch();

			watch.Start();
			action(); //Run the method delegate 
			watch.Stop();

			Log.Information("ElapsedMilliseconds: {elapsedMilliseconds}ms", watch.ElapsedMilliseconds);
		}

		public static void Benchmark(int times, Action func)
		{
			var watch = new Stopwatch();
			double totalTime = 0.0;

			for (int i = 0; i < times; i++)
			{
				watch.Start();
				func();
				watch.Stop();

				totalTime += watch.ElapsedMilliseconds;
				watch.Reset();
			}

			Log.Information("Average time: {elapsedMilliseconds}ms", totalTime / times);
		}

		/// <summary>
		/// Benchmark a method with the signature "int parameter and int return value" and the value.
		/// </summary>
		/// <param name="func"></param>
		/// <param name="value1"></param>
		public static int Benchmark(Func<int, int> func, int value1)
		{
			var watch = new Stopwatch();

			watch.Start();
			var result = func(value1); //Run the method delegate 
			watch.Stop();

			Log.Information("ElapsedMilliseconds: {elapsedMilliseconds}ms", watch.ElapsedMilliseconds);

			return result;
		}

		/// <summary>
		/// A generic Benchmark for a method that takes in some value/class (of type T1) 
		/// and returns a type T
		/// </summary>
		public static void Benchmark<TArgument, TResult>(Func<TArgument, TResult> func, TArgument obj)
		{
			var watch = new Stopwatch();

			watch.Start();
			func(obj); //Run the method delegate 
			watch.Stop();

			Log.Information("ElapsedMilliseconds: {elapsedMilliseconds}ms", watch.ElapsedMilliseconds);
		}
	}
}
