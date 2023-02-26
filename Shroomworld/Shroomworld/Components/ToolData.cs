namespace Shroomworld {
    /// <summary>
    /// Data about tool item types.
    /// </summary>
    public class ToolData {

        // ----- Properties -----
        public int Type => _type;
        public int Level => _level;


        // ----- Fields -----
        private readonly int _type;
        private readonly int _level;


        // ----- Constructors -----
        public ToolData(int type, int level) {
            _type = type;
            _level = level;
        }
    }
}
