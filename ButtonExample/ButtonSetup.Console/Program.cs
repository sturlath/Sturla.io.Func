using System;

namespace Sturla.io.Func.ButtonSetup.Console
{
	static class Program
	{
		static void Main(string[] args)
		{
			// Clean setup of the code in one place
			// Action delegate method with setup code we just want in one place
			Action<Button, Action<Button>> setupButton = (btn, x) =>
			{
				btn.Height = 42;
				btn.Width = 64;
				btn.Color = "Red";
				//...more setup code for a button

				x(btn);
			};

			// new up 10 buttons without setting them up
			var btn1 = new Button();
			var btn2 = new Button();
			var btn3 = new Button();
			var btn4 = new Button();
			var btn5 = new Button();
			var btn6 = new Button();
			var btn7 = new Button();
			var btn8 = new Button();
			var btn9 = new Button();
			var btn10 = new Button();

			// This makes the method shorter and the code more readable
			// We just change code that is not part of the setup  
			setupButton(btn1, btn => btn.Text = "OK");
			setupButton(btn2, btn => btn.Text = "Cancel");
			setupButton(btn3, btn => btn.Text = "Save");
			//etc.
		}
	}
}
