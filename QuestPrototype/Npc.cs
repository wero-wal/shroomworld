using System;

namespace QuestPrototype
{
    internal class Npc
    {
        //----- Enums -----
        //----- Properties -----
        //----- Fields -----
        private const char _symbol = 'N';
        private const ConsoleColor _colour = ConsoleColor.Red;

        public event EventHandler QuestGiven;

        private readonly string _name;
        private readonly Point _position;
        private readonly Quest _quest;
        private readonly Interaction[] _interactions;

        //----- Constructors -----
        internal Npc(string name, Point position, Quest quest, string[] interactions)
        {
            _name = name;
            _position = position;
            _quest = quest;

            _interactions = new Interaction[(int)QuestStatus.Count]
            {
                new Interaction(interactions[(int)QuestStatus.Uninitiated], new Button("Maybe later..."), new Button("Challenge accepted!")),
                new Interaction(interactions[(int)QuestStatus.Active], Button.Okay),
                new Interaction(interactions[(int)QuestStatus.Completed], new Button("You're welcome!"), new Button("Thank you!")),
            };
        }

        //----- Methods -----
        internal void Draw()
        {
            _position.SetCursorToPoint();
            Console.ForegroundColor = _colour;
            Console.Write(_symbol);
        }
        internal void OnInteractKeyPressed(object source, EventArgs eventArgs)
        {
            // display box thing
            // select response
            // update status if needed
            if(_position.DistanceTo(eventArgs.PlayerPos) > Settings.InteractionRange) // player is out of range
            {
                return;
            }

            switch(Quest.CurrentStatus)
            {
                case Quest.Status.Uninitiated:
                    // initiate quest
                    break;
                case Quest.Status.Active:
                    // give hint or encouragement
                    Button giveQuest = new Button("Challenge accepted!");
                    Button questDenied = new Button("No, thank you.");
                    break;
                case Quest.Status.Completed:
                    return;
            }
            giveQuest.Pressed += OnGiveQuestButtonPressed;
        }
        private void UpdateStatus()
        {
            if(_currentStatus != QuestStatus.Completed)
            {
                _currentStatus = (QuestStatus)((int)_currentStatus + 1)
            }
        }

        public void OnGiveQuestButtonPressed(object source, EventArgs eventArgs)
        {
            var questEventArgs.Quest = _quest;
            QuestGiven?.Invoke(this, questEventArgs);
        }
    }

    internal class QuestEventArgs : EventArgs
    {
        public Quest Quest;
    }
}
