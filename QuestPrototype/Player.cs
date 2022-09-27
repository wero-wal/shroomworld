using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestPrototype
{
    internal class Player
    {
        // ----- Properties -----
        // ----- Fields -----
        private readonly string _name;

        private List<Quest> _quests;
        private QuestMenu _questMenu;

        // ----- Constructors -----
        internal Player(string name)
        {
            _name = name;
            _quests = new List<Quest>;
            _questMenu = new QuestMenu(_quests);
        }

        // ----- Methods -----
        public void Update(Game.Input input)
        {
            switch (input)
            {
                case Game.Input.MoveUp:
                    break;
                case Game.Input.MoveDown:
                    break;
                case Game.Input.MoveLeft:
                    break;
                case Game.Input.MoveRight:
                    break;

                default:
                    break;
            }

            foreach(Quest quest in _quests)
            {
                if(!quest.Complete)
                {
                    quest.Update();
                }
            }

            _questMenu.Update(input == Game.Input.ToggleQuestMenu);
        }
    }
}
