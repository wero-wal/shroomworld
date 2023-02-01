namespace Shroomworld
{
    internal class ToolData
    {
        public int Type => _type;
        public int Level => _level;

        private readonly int _type;
        private readonly int _level;

        public ToolData(int type, int level)
        {
            _type = type;
            _level = level;
        }
    }
}
