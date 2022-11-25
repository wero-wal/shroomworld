﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class Player : LivingBeing, IAttackable
    {
        // ----- Enums -----


        // ----- Properties -----

        // ----- Fields -----
        public static AttackData DefaultAttackData;

        private Sprite _sprite;
        private HealthInfo _healthInfo;
        private AttackData _attack;
        private PowerUps _powerUps;
        private InventoryItem[,] _inventory;
        private List<Quest> _quests;
        private Dictionary<string, int> _statistics;

        // ----- Constructors -----
        public Player(string plainText) // might change this to a List<string> lines. or maybe not (cos if it's on one line, it can fit nicely into the world file).
        {
            string[] parts = plainText.Split(FileFormatter.Separator_Chars[FileFormatter.Primary]);
            int i = 0; // index
            _sprite = new MoveableSprite(LoadTexture(parts[i++]), ParsePosition(parts[i++]));
            _powerUps = new PowerUps(parts[i++]);
            _healthInfo = new HealthAndShieldInfo(healthPlainText: parts[i++], shieldPlainText: parts[i++], _powerUps.Shield);
            _attack = new AttackAndBoostInfo(_powerUps.Damage);
            _inventory = ParseInventory(parts[i++]); // doesn't even need to be stored as 2-dimensional if we store inventory height / width in settings
            _quests = ParseQuests(parts[i++]);
            _statistics = ParseStatistics(parts[i++]);
        }
        private Player()
        {
            _sprite = MoveableSprite.CreateNew(_defaultTexture, MyGame.CentreOfScreen);
            _healthInfo = HealthAndShieldInfo.CreateNew(_maxHealth, 0);
            _attack = AttackAndBoostInfo.CreateNew();
            _powerUps = PowerUps.CreateNew();
            _quests = new List<Quest>(NpcType.Capacity);
        }

        // ----- Methods -----
        // public ---
        // . admin stuff
        public static Player CreateNew()
        {
            return Player();
        }
        public string ToString()
        {
            string plainText = FileFormatter.FormatAsPlainText(_sprite, _powerUps, _healthInfo, _attack,
            InventoryToString(), _quests.ToArray(), _statistics.Values,
                separatorLevel: FileFormatter.Primary);
        }
        
        // . actions
        public void Attack(out int attackStrength)
        {
            attackStrength = _attack.Strength;
        }
        

        // private ---
        // . parsing
        private void ParsePosition()
        {

        }
        private void ParseInventory()
        {

        }
        private void ParseQuests()
        {

        }
        private void ParseStatistics(string plainText)
        {
            string[] split = plainText.Split(FileFormatter.SecondarySeparator_Char);
            for (int i = 0; i < _statistics.Count; i++)
            {
                _statistics.Values[i] = Convert.ToInt32(split[i++]);
            }
        }
        
        // . formatting
        private string InventoryToString()
        {
            // can be 1D
        }

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