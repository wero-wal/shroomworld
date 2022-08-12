using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Player:Entity
    {
        // Statistics
        private const string
            _totalEnemiesKilled = "Total enemies killed",
            _totalQuestsCompleted = "Total quests completed";
        private Dictionary<string, int> _statistics;


        // Statistics
        private Dictionary<Type, int> GetFreshStatsList()
        {
            Dictionary<string, int> stats = new Dictionary<Type, int>(Game.TileDictionary.Count + Game.ItemDictionary.Count + Game.EnemyDictionary.Count + Game.NpcDictionary.Count + 2);

            for (int i = 0; i < Game.TileDictionary.Count; i++)
            {
                AddNewStatistic(ref stats, Game.TileDictionary[i]);
            }
            for (int i = 0; i < Game.ItemDictionary.Count; i++)
            {
                AddNewStatistic(ref stats, Game.ItemDictionary[i]);
            }
            for (int i = 0; i < Game.EnemyDictionary.Count; i++)
            {
                AddNewStatistic(ref stats, Game.EnemyDictionary[i]);
            }
            for (int i = 0; i < Game.NpcDictionary.Count; i++)
            {
                AddNewStatistic(ref stats, Game.NpcDictionary[i]);
            }
            
            stats.Add(_totalEnemiesKilled, 0);
            stats.Add(_totalQuestsCompleted, 0);
            return stats;
        }
        private void AddNewStatistic(ref Dictionary<string, int> statistics, Type item)
        {
            statistics.Add(item.GetKey(), 0);
        }
        private List<string> ConvertStatisticsToListOfStrings(Dictionary<int, TileType> tiles, Dictionary<int, ItemType> items, Dictionary<int, EnemyType> enemies, Dictionary<int, NpcType> npcs)
        {
            List<string> display_list = new List<string>(_statistics.Count);

            for (int i = 0; i < Game.TileDictionary.Count; i++)
            {
                display_list.Add($"{Game.TileDictionary[i].Plural_Name} broken: {_statistics[Game.TileDictionary[i].GetKey()]}");
            }

            for (int i = 0; i < Game.ItemDictionary.Count; i++)
            {
                display_list.Add($"{Game.ItemDictionary[i].PluralName} collected: {_statistics[Game.ItemDictionary[i].GetKey()]}");
            }

            for (int i = 0; i < Game.EnemyDictionary.Count; i++)
            {
                display_list.Add($"{Game.EnemyDictionary[i].PluralName} killed: {_statistics[Game.EnemyDictionary[i].GetKey()]}");
            }
            display_list.Add($"{totalEnemiesKilled}: {_statistics[totalEnemiesKilled]}");

            for (int i = 0; i < Game.NpcDictionary.Count; i++)
            {
                display_list.Add($"Quests completed for {Game.NpcDictionary[i].Name}: {_statistics[Game.NpcDictionary[i].GetKey()]}");
            }
            display_list.Add($"{totalQuestsCompleted}: {_statistics[totalQuestsCompleted]}");

            return display_list;
        }
    }
}
