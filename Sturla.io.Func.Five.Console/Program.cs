using System;

namespace Sturla.io.Func.Five.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			//Action delegate method with setup code we just want in one place.
			Action<Button, Action<Button>> setupButton = (btn, x) =>
			{
				btn.Height = 42;
				btn.Width = 64;
				//...more setup code for a button

				x(btn);
			};

			var btn1 = new Button();
			var btn2 = new Button();
			var btn3 = new Button();

			// Cleaner setup of the code. Makes the method shorter and the code more readable.
			setupButton(btn1, btn => btn.Text = "OK");
			setupButton(btn2, btn => btn.Text = "Cancel");
			setupButton(btn3, btn => btn.Text = "Save");
		}
	}
}
