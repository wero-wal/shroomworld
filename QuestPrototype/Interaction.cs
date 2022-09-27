using System;

namespace QuestPrototype
{
    internal class Interaction
    {
        // ----- Fields -----
        private string _message;
        private string[] _responses;

        // ----- Constructors -----
        internal Interaction(string message, params string[] responses)
        {
            _message = message;
            _responses = responses;
        }

        // ----- Methods -----
        internal void Update()
        {

        }
        internal void Display()
        {
            
        }
    }
}
