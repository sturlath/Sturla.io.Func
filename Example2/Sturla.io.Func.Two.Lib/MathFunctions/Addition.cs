using Serilog;

namespace Sturla.io.Func.Two.Lib
{
	public static class Addition
	{
		public static void Add(int value1, int value2)
		{
			var result = value1 + value2;
			Log.Information("Result: {result}", result);
		}
	}
}
