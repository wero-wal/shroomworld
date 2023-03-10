using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    public class InventoryItem
    {
        // ----- Enums -----


        // ----- Properties -----
        public int Id => _id;
        public int Amount { get => _amount; }

        // ----- Fields -----
        private readonly int _id;

        private int _amount;

        // ----- Constructors -----
        public InventoryItem(int id, int amount)
        {
            _id = id;
            _amount = amount;
        }

        // ----- Methods -----

    }
}
