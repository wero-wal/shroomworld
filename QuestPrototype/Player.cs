using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestPrototype
{
    internal class Player
    {
        // ----- Properties -----
        // ----- Fields -----
        private readonly string _name;

        private List<Quest> _quests;
        private QuestMenu _questMenu;
        private Point _position;
        private int _speed;

        // ----- Constructors -----
        internal Player(string name, Point startingPosition)
        {
            _name = name;
            _quests = new List<Quest>;
            _questMenu = new QuestMenu(_quests);
            _position = startingPosition;
        }

        // ----- Methods -----
        public void OnMoveLeftKeyPressed(object source, EventArgs eventArgs)
        {
            _position.X = Max(Settings.LeftBound, _position.X - _speed);
        }
        public void OnMoveRightKeyPressed(object source, EventArgs eventArgs)
        {
            _position.X = Min(_position.X + _speed, Settings.RightBound);
        }
        public void OnMoveUpKeyPressed(object source, EventArgs eventArgs)
        {
            _position.Y = Max(Settings.TopBound, _position.Y - _speed)
        }
        public void OnMoveDownKeyPressed(object source, EventArgs eventArgs)
        {
            _position.Y = Min(_position.Y + _speed, Settings.BottomBound);
        }
        public void OnQuestGiven()
        {
            
        }
    }
}
