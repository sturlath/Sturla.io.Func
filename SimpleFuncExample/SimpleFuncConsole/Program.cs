using SimpleFunc;
using System;

namespace SimpleFuncConsole
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			// Using lambda
			Func<StateEntry> wheatherChangeStateFunction = () =>
			{
				// state change code omitted
				return new StateEntry() { StateChanged = true };
			};

			// Using anonymous method
			Func<StateEntry> productionChangeStateFunction = delegate() 
			{
				// state change code omitted
				return new StateEntry() { StateChanged = true };
			};

			// This is an local function that can be used instead of func. 
			// I think its a lot nicer and I would stick with this one
			StateEntry customerChangeStateFunction()
			{
				// state change code omitted
				return new StateEntry() { StateChanged = true };
			}

			//etc.

			StateManager manager = new StateManager();

			manager.ChangeState(wheatherChangeStateFunction);
			manager.ChangeState(productionChangeStateFunction);
			manager.ChangeState(customerChangeStateFunction);
		}
	}
}
