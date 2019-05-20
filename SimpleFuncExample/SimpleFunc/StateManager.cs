using System;
using System.Collections.Generic;

namespace SimpleFunc
{
	public class StateManager
	{
		public void ChangeState(Func<StateEntry> stateChangeFunction)
		{
			var entry = stateChangeFunction();

			// use the result somewhere
			dataBoundList.Add(entry);
		}

		private readonly List<StateEntry> dataBoundList = new List<StateEntry>();
	}
}
