using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Shroomworld {
    public class BiomeType : IType {

        // ----- Properties -----
		public IdData IdData => _idData;
		public Texture Background => _background;
		public int[] Layers => _layers;
		public int TreeType => _treeType;
		public int[] FlowerTypes => _flowerTypes;
        public int FlowerAmount => _flowerAmount;
		public int TreeAmount => _treeAmount;
		public int ChestAmount => _chestAmount;


		// -----Fields -----
		private readonly IdData _idData;
        private readonly Texture _background;
        private readonly int[] _layers;
        private readonly int _treeType;
        private readonly int[] _flowerTypes;
        private readonly int _treeAmount;
        private readonly int _chestAmount;
        private readonly int _flowerAmount;


        // ----- Constructors -----
        public BiomeType(IdData idData, Texture background, int[] layers, int treeType, int[] flowerTypes,
            int treeAmount, int chestAmount /*, int flowerAmount, int friendlyAmount, int enemyAmount*/) {
            _idData = idData;
            _background = background;
            _layers = layers;
            _treeType = treeType;
            _flowerTypes = flowerTypes;
            _treeAmount = treeAmount;
            _chestAmount = chestAmount;
            _flowerAmount = 50;
        }

        public bool Equals(BiomeType biome) {
            if (biome is null) {
                return false;
            }
            return biome.IdData.Id == _idData.Id;
        }

        // ----- Methods -----

    }
}
