using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Drop
    {
        //---Enums---

        //---Constants---

        //---Variables---
        private int
            _resource_id,
            _min_amount,
            _max_amount;

        //---Constructor---
        public Drop(int resourceId, int minAmount, int maxAmount)
        {
            _resource_id = resourceId;
            _min_amount = minAmount;
            _max_amount = maxAmount;
        }

        //---Methods---
        public InventoryItem Drop_Item()
        {
            int amount = new Random().Next(_min_amount, _max_amount + 1); // random between min and max
            return new InventoryItem(_resource_id, amount);
        }
    }
}
