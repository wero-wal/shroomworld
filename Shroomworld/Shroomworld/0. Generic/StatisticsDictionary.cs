using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    public class StatisticsDictionary
    {
        // ---------- Enums ----------


        // ---------- Properties ----------

        // ---------- Fields ----------
        private static string[]
            _tiles,
            _items,
            _enemies,
            _npcQuests;
        private static string
            _totalEnemies,
            _totalQuests;

        private static List<string> _allKeys; // TODO: change this to list

        private Dictionary<string, int> _statistics;

        // ---------- Constructors ----------
        public StatisticsDictionary()
        {
        }

        // ---------- Methods ----------
        public static void GetKeys()
        {
            int dictionary = 0;
            int i = 0;
            bool done = false;

            _allKeys = new List<string>(2
                + TileType.Dictionary.Count
                + ItemType.Dictionary.Count,
                + EnemyType.Dictionary.Count,
                + NpcType.Dictionary.Count);

            while (!done)
            {
                try
                {
                    switch (dictionary)
                    {
                        case 1:
                            _allKeys.Add(TileType.FullIds[i]);
                            break;
                        case 2:
                            _allKeys.Add(ItemType.FullIds[i]);
                            break;
                        case 3:
                            _allKeys.Add(EnemyType.FullIds[i]);
                            break;
                        case 4:
                            _allKeys.Add(_totalEnemies);
                            dictionary++;
                            break;
                        case 5:
                            _allKeys.Add(NpcType.FullIds[i]);
                            break;
                        case 6:
                            _allKeys.Add(_totalQuests);
                            dictionary++;
                            break;
                        default:
                            done = true;
                            break;
                    }
                }
                catch (KeyNotFoundException) // We've gone through all the items in that dictionary
                {
                    dictionary++;
                    i = 0;
                }
            }
        }


        public int GetStatisticFor(string key)
        {
            try
            {
                return _statistics[key];
            }
            catch (KeyNotFoundException k)
            {
                throw k;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
