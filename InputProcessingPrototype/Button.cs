using System;

namespace InputProcessingPrototype
{
	class Button<TOutput>
	{
		public event EventHandler Pressed { get => _pressed; set => _pressed = value; }
		private TOutput _output;

		private event EventHandler _pressed;

		public Button(TOutput output)
		{
			_output = output;
		}

		protected void OnPressed(object source, EventArgs eventArgs)
		{
			_pressed?.Invoke();
		}
	}
}
