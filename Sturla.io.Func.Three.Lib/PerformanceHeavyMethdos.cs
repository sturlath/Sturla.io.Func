using System.Threading;

namespace Sturla.io.Func.Three.Lib
{
	public class PerformanceHeavyMethdos
	{
		public void CalculateMillionPrimeNumbers()
		{
			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(10); //ms
			}
		}

		public int Big(int startingFrom = 0)
		{
			for (int i = startingFrom; i < 10; i++)
			{
				Thread.Sleep(10); //ms
			}

			//This is just some return value from a method
			return 666;
		}

		public bool Small(JustSomeClass just)
		{
			for (int i = just.StartCount; i < 10; i++)
			{
				Thread.Sleep(10); //ms
			}

			return true;
		}
	}
}
