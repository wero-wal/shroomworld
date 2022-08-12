using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class ItemQuestItem:QuestItem
    {
        public ItemQuestItem(int id, int targetAmount) : base(id, targetAmount)
        {
            ItemName = Game.Resource_Dictionary[id].Name;
            PluralName = Game.Resource_Dictionary[id].Plural_Name;
            Action = "Collect";
        }
    }
}
