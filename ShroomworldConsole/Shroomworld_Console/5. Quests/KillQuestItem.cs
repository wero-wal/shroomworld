using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class KillQuestItem:QuestItem
    {
        public KillQuestItem(int id, int targetAmount) : base(id, targetAmount)
        {
            ItemName = Game.Enemy_Dictionary[id].Name;
            PluralName = Game.Enemy_Dictionary[id].Plural_Name;
            Action = "Kill";
        }
    }
}
