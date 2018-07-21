using CommandLine;
using Serilog;
using Sturla.io.Func.CalculatorActionLib;

namespace Sturla.io.Func.CalculatorAction.Console
{
	public class Program
	{
		/// <summary>
		/// Use the following to run in a console:
		/// ActionOneConsole.exe --Add --Value1 1 --Value2 2
		/// ActionOneConsole.exe --Multiply --Value1 2 --Value2 2
		/// ActionOneConsole.exe --Subtract --Value1 2 --Value2 3
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			#region Logging
			Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.WriteTo.Console()
			.CreateLogger();
			#endregion

			var mathRunner = new MathRunner();

			Parser.Default.ParseArguments<Options>(args)
			.WithParsed(o =>
			{
				Logging(o);

				if (o.Add)
				{
					 mathRunner.RunMethod(o.Value1, o.Value2, Addition.Add);
				}
				else if (o.Multiply)
				{

					mathRunner.RunMethod(o.Value1, o.Value2, Multiplication.Multiply);
				}
				else if (o.Subtract)
				{
					 mathRunner.RunMethod(o.Value1, o.Value2, Subtraction.Substract);
				}
			});
		}

		class Options
		{
			[Option('a', "Add", HelpText = "Add two values together.")]
			public bool Add { get; set; }

			[Option('m', "Multiply", HelpText = "Multiplies two values together.")]
			public bool Multiply { get; set; }

			[Option('s', "Subtract", HelpText = "Subtract two values.")]
			public bool Subtract { get; set; }

			[Option("Value1", HelpText = "First value")]
			public int Value1 { get; set; }

			[Option("Value2", HelpText = "Second value")]
			public int Value2 { get; set; }
		}

		/// <summary>
		/// Just a output helper
		/// </summary>
		private static void Logging(Options o)
		{
			if (o.Add)
				Log.Information("Adding");
			if (o.Multiply)
				Log.Information("Multiplying");
			if (o.Subtract)
				Log.Information("Subtracting");

			Log.Information("{val1} and {val2}", o.Value1, o.Value2);
		}
	}
}
