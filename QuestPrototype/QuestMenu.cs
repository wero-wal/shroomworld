using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestPrototype
{
    internal class QuestMenu
    {
        // ---------- Properties ----------
        // ---------- Fields ----------
        private bool _show;

        // ---------- Constructors ----------
        // ---------- Methods ----------
        public void ToggleVisibility()
        {
            _show = !_show;
            if (_show)
            {
                Display();
            }
        }
        private void Display()
        {

        }
    }
}
