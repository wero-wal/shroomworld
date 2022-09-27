using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestPrototype
{
    internal class QuestMenu
    {
        // ----- Properties -----
        // ----- Fields -----
        private const int _standardIndent = 1;
        private const int _requirementIndent = 2;

        private List<Quest> _quests;

        private bool _show;
        private Point _location;
        private Point _size;
        private ConsoleColour _backgroundColour;
        private ConsoleColor _textColour;

        // ----- Constructors -----
        internal QuestMenu(ref List<Quest> quests, Point location)
        {
            _quests = quests;
            _location = location;
            _size = new Point(32, 0);
            _show = false;
        }

        // ----- Methods -----
        internal void Update()
        {
            _size.Y = _quests.Count;
        }

        // Display
        internal void Display()
        {
            if(_show)
            {
                Console.SetCursorPosition(_location.X, _location.Y);
                foreach(Quest quest in _quests)
                {
                    DisplayQuest(quest);
                }
            }
            // else do nothing
        }
        internal void DisplayQuest(Quest quest)
        {
            DisplayQuestTitle(quest);

            Console.BackgroundColor = _backgroundColour;
            Console.ForegroundColor = _textColour;
            foreach(IRequirement requirement in quest)
            {
                Console.CursorTop++;
                DisplayRequirement(requirement);
            }
        }
        private void DisplayQuestTitle(Quest quest)
        {
            Console.BackgroundColor = _textColour;
            Console.ForegroundColor = _backgroundColour;
            Console.CursorLeft = _location.X + _standardIndent;
            WrapAndWrite(quest.Name, requirement: false);
        }
        private void DisplayRequirement(IRequirement requirement)
        {
            Console.CursorLeft = _location.X + _standardIndent + _requirementIndent;
            WrapAndWrite(requirement.Description, requirement: true);
        }
        private void WrapAndWrite(string text, bool requirement)
        {
            int maxLength = _size.X - _standardIndent - (requirement) ? _requirementIndent : _standardIndent;

            if(text.Length > maxLength) // must be wrapped
            {
                List<string> wrappedText = new List<string>(text.Length / maxLength + 1);

                int i = 0;
                do
                {
                    int lowerBound = i * maxLength;
                    int upperBound = Math.Min(text.Length, (i + 1) * maxLength); // lowerBound + maxLength, or text.Length if that is too big
                    wrappedText.Add(text.Range(lowerBound, upperBound));
                    i++;
                } while (upperBound < text.Length); // will stop wrapping when we've reached the end
            }

            int wrappedMargin = Console.CursorLeft + _standardIndent;
            foreach(string line in wrappedText)
            {
                Console.Write(line);
                Console.CursorLeft = wrappedMargin; // indent so that the user can tell it's wrapped
                Console.CursorTop++;
            }
            Console.CursorTop--; // reset cursor indent
        }

        //
        private void OnToggleVisibilityKeyPressed(object source, EventArgs eventArgs)
        {
            _show = !_show;
        }
    }
}
