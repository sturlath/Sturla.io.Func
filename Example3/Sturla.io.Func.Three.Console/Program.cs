using System.Linq;
using Serilog;
using Sturla.io.Func.Three.Lib;

namespace Sturla.io.Func.Three.Console
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

			var methods = new PerformanceHeavyMethdos();

			//Benchmark measure it once
			Performance.Benchmark(methods.CalculateMillionPrimeNumbers);

			//Benchmark measure it many times and get an average
			Performance.Benchmark(10, methods.CalculateMillionPrimeNumbers);

			// Benchmark the time it takes to ToList a range of 10,000 items
			Performance.Benchmark(() => { Enumerable.Range(0, 10000).ToList(); });

			//Benchmark this type of method with the signature "int parameter and int return value" and the value.
			int result = Performance.Benchmark(methods.Big, 10);

			//The use of a generic version
			Performance.Benchmark(methods.Small, new JustSomeClass());

		}
	}
}
