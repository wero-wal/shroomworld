using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestPrototype
{
    internal class Player
    {
        // ---------- Properties ----------
        // ---------- Fields ----------
        QuestMenu _questMenu;

        // ---------- Constructors ----------
        // ---------- Methods ----------
        public void Update(Program.UserInput input)
        {
            switch (input)
            {
                case Program.UserInput.MoveUp:
                    break;
                case Program.UserInput.MoveDown:
                    break;
                case Program.UserInput.MoveLeft:
                    break;
                case Program.UserInput.MoveRight:
                    break;

                case Program.UserInput.ToggleQuestMenu:
                    _questMenu.ToggleVisibility();
                    break;

                default:
                    break;
            }
        }
    }
}
