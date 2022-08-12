using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Quest
    {
        //---Enums---
        //---Constants---
        //---Accessors---
        //---Variables---
        private List<QuestItem> _items;
        private string _name;
        private string _message; // name:
                                 // item_1_name: [quest_item.Type] x things. Progress: [yours] / [target]
                                 // item_2_name: [quest_item.Type] x things. Progress: [yours] / [target]
        private bool _complete;

        //---Constructors---
        //---Methods---
        private void Update_Status(List<int> amounts)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].Update_Quest_Status(amounts[i]);
            }            
        }
        private bool Check_If_Complete()
        {

        }
    }
}
