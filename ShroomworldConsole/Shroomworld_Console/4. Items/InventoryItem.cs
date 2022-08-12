using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class InventoryItem
    {
        //---Enums---
        //---Constants---
        //---Accessors---
        public int Id { get => _resource_id; }

        //---Variables---
        private readonly int _resource_id;
        private int _quantity;

        //---Constructor---
        public InventoryItem(int resourceId, int quantity)
        {
            _resource_id = resourceId;
            _quantity = quantity;
        }

        //---Methods---
        public void Increase_Amount(int amount)
        {
            _quantity += amount;
        }
        public void Decrease_Amount(int amount)
        {
            if ((_quantity - amount) < 0)
            {
                throw new ArgumentOutOfRangeException("You don't have that many items!");
            }
            else
            {
                _quantity -= amount;
            }
        }
    }
}
