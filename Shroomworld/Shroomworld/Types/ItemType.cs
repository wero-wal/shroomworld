using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class ItemType
    {
        private readonly IdentifyingData _idData;
        private readonly ToolData? _toolData;
        private readonly bool _stackable;
        private readonly bool _placeable;

        /// <summary>
        /// Constructor for non-tool items
        /// </summary>
        public ItemType(IdentifyingData idData, bool stackable, bool placeable)
        {
            _idData = idData;
            _stackable = stackable;
            _placeable = placeable;
            _toolData = null;
        }
        /// <summary>
        /// Constructor for tool items
        /// </summary>
        public ItemType(IdentifyingData idData, ToolData toolData)
        {
            _idData = idData;
            _toolData = toolData;
            _stackable = false;
            _placeable = false;
        }
    }
}
