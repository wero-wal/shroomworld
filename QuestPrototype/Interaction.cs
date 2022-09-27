using System;

namespace QuestPrototype
{
    internal readonly class Interaction
    {
        // ----- Fields -----
        private string _message;
        private Button[] _responses;

        // ----- Constructors -----
        internal Interaction(string message, params Button[] responses)
        {
            _message = message;
            _responses = responses;
        }

        // ----- Methods -----
        internal void Display()
        {
            
        }
    }
}
