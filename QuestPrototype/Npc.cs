using System;

namespace QuestPrototype
{
    internal class Npc
    {
        //----- Enums -----
        private Enum QuestStatus
        {
            Uninitiated,
            Active,
            Completed,
            Count,
        }

        //----- Properties -----
        //----- Fields -----
        private const char _symbol = 'N';
        private const ConsoleColor _colour = ConsoleColor.Red;

        private readonly string _name;
        private readonly Point _position;
        private Quest _quest;
        private Interaction[] _interactions;
        private QuestStatus _currentStatus;

        //----- Constructors -----
        internal Npc(string name, Point position, Quest quest, params string[] interactions)
        {
            _name = name;
            _position = position;
            _quest = quest;

            _interactions = new Interaction[(int)QuestStatus.Count]
            {
                new Interaction(interactions[(int)QuestStatus.Uninitiated], "Maybe later...", "Challenge accepted!"),
                new Interaction(interactions[(int)QuestStatus.Active], "Okay!"),
                new Interaction(interactions[(int)QuestStatus.Completed], "You're welcome!", "Thank you!"),
            };
        }

        //----- Methods -----
        internal void Draw()
        {
            _position.SetCursorToPoint();
            Console.ForegroundColor = _colour;
            Console.Write(_symbol);
        }
        internal void Interact()
        {
            // display box thing
            // select response
            // update status if needed
        }
        private void UpdateStatus()
        {
            if(_currentStatus != QuestStatus.Completed)
            {
                _currentStatus = (QuestStatus)((int)_currentStatus + 1)
            }
        }
    }
}
