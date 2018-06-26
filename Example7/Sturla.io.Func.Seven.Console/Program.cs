using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace Sturla.io.Func.Seven.Console
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

			Log.Information("Action switch example");

			var actionSwitch = new Dictionary<Func<int, bool>, Action>
			{
				//Note that you can't do "x < 0" in a ordinary Switch statement
				 { x => x < 0 ,  () => { Log.Information("{val}","n/a"); }},
				 { x => x == 0,  () => { Log.Information("{val}","0"); }},
				 { x => x == 1,  () => { Log.Information("{val}","1"); }},
				 { x => x == 2,  () => { Log.Information("{val}","2"); }},
				 { x => x == 3,  () => { Log.Information("{val}","3"); }},
				 { x => x == 4,  () => { Log.Information("{val}","4"); }},
				 { x => x == 5,  () => { Log.Information("{val}","5"); }},
			};

			int numberToFind = 1;


			var t1 = actionSwitch.FirstOrDefault(sw => sw.Key(Convert.ToInt32(numberToFind)));

			if (t1.Key != null)
			{
				//TODO: Late at night.. need to get back to this... if numberToFind is not found we get an null reference exception. 
				// and I really don't like how I do this right now...
				actionSwitch.FirstOrDefault(sw => sw.Key(Convert.ToInt32(numberToFind))).Value();
			}


			Log.Information("And now Func switch example");

			var funcSwitch = new Dictionary<Func<int, bool>, Func<string>>
			{
				//Note that you can't do "x < 0" in a ordinary Switch statement
				 { x => x < 0 ,  () => "n/a"},
				 { x => x == 0,  () => "0"},
				 { x => x == 1,  () => "1" },
				 { x => x == 2,  () => "2"},
				 { x => x == 3,  () => "3"},
				 { x => x == 4,  () => "4"},
				 { x => x == 5,  () => "5"},
			};

			string returnValue = string.Empty;

			var t2 = funcSwitch.FirstOrDefault(sw => sw.Key(Convert.ToInt32(numberToFind)));

			if (t2.Key != null)
			{
				returnValue = funcSwitch.FirstOrDefault(sw => sw.Key(Convert.ToInt32(numberToFind))).Value();
			}

			Log.Information("The return value: {value}", returnValue);

			System.Console.ReadKey();


			// With C# 7.0 and up you can use 'when' in a switch statements.

			switch (numberToFind)
			{
				case var expression when numberToFind < 0:
					Log.Information("{val}", "0");
					break;
				case var expression when (numberToFind >= 0 && numberToFind < 5):
					//some code
					break;
				default:
					break;
			}
		}



	}
}
