using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class Player : LivingBeing, IAggressive
    {
        // ---------- Enums ----------


        // ---------- Properties ----------


        // ---------- Fields ----------
        private HealthInfo _healthInfo;
        private AttackInfo _attackInfo;
        private List<PowerUp> _powerUps;
        private InventoryItem[,] _inventory;
        private Dictionary<string, int> _statistics;
        private List<Quest> _quests;

        // ---------- Constructors ----------
        public Player(string fileText)
        {
            string[] splitFileText = fileText.Split(PrimarySeparator_Char);
            byte i = 0; // index
            _powerUps = new PowerUps(splitFileText[i++]);
            _healthInfo = new HealthAndShieldInfo(Convert.ToByte(splitFileText [i++]), _powerUps.Shield);
            _inventory = ParseInventory(splitFileText[i++]);
            _statistics = ParseStatistics(splitFileText[i++]);
            _quests = ParseQuests(splitFileText[i++]);
            _attackInfo = new AttackAndBoostInfo(_powerUps.Damage);
            _sprite = new MoveableSprite(LoadTexture(splitFileText[i++]), ParsePosition(splitFileText[i++]));
        }
        
        private Player()
        {
            _sprite = MoveableSprite.CreateNew(_defaultTexture, MyGame.CentreOfScreen);
            _healthInfo = HealthAndShieldInfo.CreateNew(_maxHealth, 0);
            _attackInfo = AttackAndBoostInfo.CreateNew();
            _powerUps = PowerUps.CreateNew();
            _quests = new List<Quest>(NpcType.Capacity);
        }

        // ---------- Methods ----------
        public static Player CreateNew()
        {
            return Player();
        }
        public void Attack(out byte attackStrength)
        {
            attackStrength = _attackInfo.Strength;
        }
        private void ParseStatistics(string plainText)
        {
            string[] split = plainText.Split(SecondarySeparator_Char);
            byte i = 0;
            foreach (Key key in _statistics.KeyValuePairs)
            {
                _statistics[key] = Convert.ToInt(split[i++]);
            }
        }
        private void ParseQuests()
        {

        }
        private void ParseInventory()
        {

        }
        private void ParsePosition()
        {

        }

        public string ToString()
        {
            string plainText = FileFormatter.FormatAsPlainText(_powerUps.ToString(), _healthInfo.ToString(), _attackInfo.ToString(), GetStatisticsToString()),
                QuestsToString(), _sprite.ToString(), FileFormatter.PrimarySeparator);
        }
        private string StatisticsToString()
        {
            string[] array = new string[_statistics.Count];
            int i = 0;
            foreach(var item in _statistics)
            {
                array[i++] = item.Value.ToString();
            }
            return FileFormatter.FormatAsPlainText(array, FileFormatter.SecondarySeparator);
        }
        private string QuestsToString()
        {
            return FileFormatter.FormatAsPlainText(_quests.ToArray(), FileFormatter.SecondarySeparator);
        }
    }
}
