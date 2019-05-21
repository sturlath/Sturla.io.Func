using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sturla.io.Func.Seven.Console
{
	static class Program
	{
		static void Main(string[] args)
		{
			#region Logging
			Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.WriteTo.Console()
			.CreateLogger();
			#endregion

			Log.Information("Action switch example");

			const int numberToFind = 2;

			/******************/
			/* Action example */
			/*	              */
			/******************/

			// Actions don't return so we log out if a number is found
			var actionSwitch = new Dictionary<Func<int, bool>, Action>
			{
				{ x => x < 0 ,  () => Log.Information("{val}","n/a")}, //Note that you can't do "x < 0" in a ordinary Switch statement
				{ x => x == 0,  () => Log.Information("{val}","0")},
				{ x => x == 1,  () => Log.Information("{val}","1")},
				{ x => x == 2,  () => Log.Information("{val}","2")},
				{ x => x == 3,  () => Log.Information("{val}","3")},
				{ x => x == 4,  () => Log.Information("{val}","4")},
				{ x => x == 5,  () => Log.Information("{val}","5")},
			};

			var actionSwitchResult = actionSwitch.FirstOrDefault(sw => sw.Key(Convert.ToInt32(numberToFind)));

			if (actionSwitchResult.Key != null)
			{
				actionSwitch.FirstOrDefault(sw => sw.Key(Convert.ToInt32(numberToFind))).Value();
			}

			Log.Information("and now Func switch example");


			/****************/
			/* Func example */
			/*	            */
			/****************/

			var funcSwitch = new Dictionary<Func<int, bool>, Func<string>>
			{
				{ x => x < 0 ,  () => "n/a"}, //Note that you can't do "x < 0" in a ordinary Switch statement
				{ x => x == 0,  () => "0"},
				{ x => x == 1,  () => "1" },
				{ x => x == 2,  () => "2"},
				{ x => x == 3,  () => "3"},
				{ x => x == 4,  () => "4"},
				{ x => x == 5,  () => "5"},
			};

			string returnValue = string.Empty;

			var funcSwitchReulst = funcSwitch.FirstOrDefault(sw => sw.Key(Convert.ToInt32(numberToFind)));

			if (funcSwitchReulst.Key != null)
			{
				// Please let me know how I can null check this line with Value() called so I don't have to check if the .Key is null.
				// I am tired and don´t have time/stamina to figure this out now :-)
				returnValue = funcSwitch.FirstOrDefault(sw => sw.Key(Convert.ToInt32(numberToFind))).Value();
			}

			if (returnValue?.Length == 0)
			{
				Log.Information($"The return value: Did not find '{numberToFind}'");
			}
			else
			{
				Log.Information("The return value: {value}", returnValue);
			}

			System.Console.ReadKey();


			// With C# 7.0 and up you can use 'when' in a switch statements.

			switch (numberToFind)
			{
				case var expression when numberToFind < 0:
					Log.Information("Found {val}", "0");
					break;
				case var expression when (numberToFind >= 0 && numberToFind < 5): // much more you can do now days
					Log.Information("Found {val}", "0");
					break;
				// This code does not compile
				//case numberToFind < 0:
				//	break;
				default:
					break;
			}
		}
	}
}
