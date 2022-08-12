using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class QuestItem
    {
        //---Enums---
        //---Constants---
        //---Accessors---
        public string Description { get => _description; }

        //---Variables---
        protected readonly int
            TargetAmount,
            Id;
        protected string
            Description,
            ItemName,
            PluralName,
            Action;
        protected bool Complete;

        //---Constructors---
        public QuestItem(int id, int targetAmount)
        {
            Id = id;
            TargetAmount = targetAmount;
            // set the following in child classes:
            // . _item_name
            // . _target_name
            // . _action
        }

        //---Methods---
        public void Update_Quest_Status(int amount)
        {
            Complete = TargetAmount == amount;
            _description = $"{Action} {TargetAmount} " + ((TargetAmount > 1) ? PluralName : ItemName); // [Do something] [amount] [item(s)].
            _description += "Progress: " + ((Complete) ? "complete" : $"{amount} / {TargetAmount}"); // Progress: complete || [amount] / [target]
        }
    }
}
