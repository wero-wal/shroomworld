using System;

namespace QuestPrototype
{
    internal class Button<TOutput>
    {
        // some standard buttons
        public static Button Next = new Button();
        public static Button Previous = new Button();
        public static Button Confirm = new Button();
        public static Button Cancel = new Button();
        public static Button Yes = new Button();
        public static Button No = new Button();
        public static Button Okay = new Button();

        private static int numberOfUnnamedButtons = 0;


        private readonly string _displayText;


        public event EventHandler Pressed;

        public Button()
        {
            _displayText = nameof(this) ?? $"Button{++numberOfUnnamedButtons}";
        }

        public Button(string displayText)
        {
            _displayText = displayText;
        }

        protected void OnPressed(EventArgs eventArgs)
        {
            Pressed?.Invoke(this, eventArgs);
        }
    }
}
