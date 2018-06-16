using Serilog;

namespace Sturla.io.Func.Two.Lib
{
	public static class Multiplication
	{
		public static void Multiply(int value1, int value2)
		{
			var result = value1 * value2;
			Log.Information("Result: {result}", result);
		}
	}
}
