namespace Shroomworld
{
    internal class BiomeType
    {
        // ----- Properties -----
        // -----Fields -----
        private readonly IdData _idData;
        private readonly Texture _background;
        private readonly int[] _layers;
        private readonly int _treeType;
        private readonly int[] _flowerTypes;
        private readonly int _treeAmount;
        private readonly int _chestAmount;

        // ----- Constructors -----
        public BiomeType(IdData idData, Texture background, int[] layers, int treeType, int[] flowerTypes, int treeAmount, int chestAmount /*, int friendlyAmount, int enemyAmount*/)
        {
            _idData = idData;
            _background = background;
            _layers = layers;
            _treeType = treeType;
            _flowerTypes = flowerTypes;
            _treeAmount = treeAmount;
            _chestAmount = chestAmount;
        }

        // ----- Methods -----
    }
}
