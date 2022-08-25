using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    public class PlayerTemplate // default settings, maxhealth, reach, damage, ...
    {
        // ---------- Enums ----------


        // ---------- Properties ----------


        // ---------- Fields ----------
        Dictionary<string, int> _statistics;
        private static string
            _totalEnemies,
            _totalQuests;

        // ---------- Constructors ----------


        // ---------- Methods ----------
        public void CreateNewStatsList(params Dictionary<int, XType>[] dictionaries)
        {
            foreach (var dictionary in dictionaries)
            {
                for (int i = 0; i < dictionary.Count; i++)
                {
                    _statistics.Add(dictionary[i].FullId, 0);
                }
            }
            _statistics.Add(_totalEnemies, 0);
            _statistics.Add(_totalQuests, 0);
        }
    }
}
