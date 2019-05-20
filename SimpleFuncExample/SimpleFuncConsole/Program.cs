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
				return new StateEntry() { StateChanged = true, Message = "The whether will be great" };
			};

			// Using anonymous method
			Func<StateEntry> productionChangeStateFunction = delegate ()
			{
				// state change code omitted
				return new StateEntry() { StateChanged = true, Message = "Production is now Stopped!" };
			};

			// This is an local function that can be used instead of func. 
			// I think its a lot nicer and I would stick with this one
			StateEntry customerChangeStateFunction()
			{
				// state change code omitted
				return new StateEntry() { StateChanged = true, Message = "Customer has been notified" };
			}

			//etc.

			StateManager manager = new StateManager();

			manager.ChangeState(wheatherChangeStateFunction);
			manager.ChangeState(productionChangeStateFunction);
			manager.ChangeState(customerChangeStateFunction);
		}
	}
}
