using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ConveyorDefence.Map
{
    static class DepthCalculator
    {
        private static float _maxDepth;
        private const float StartOffsetY =0.9f;
        public const float DepthModifier = 0.0000001f;
        public static void Initialize(float maxDepth)
        {
            _maxDepth = maxDepth;
        }

        public static float CalculateDepth(int x, int y, int height =0)
        {
            var firstVisibleTile = Camera.FirstVisibleTileIndex;
            var tileIndex = new Point(firstVisibleTile.X + x, firstVisibleTile.Y + y);
            float depthOffset = 0.7f - ((tileIndex.X + (tileIndex.Y * Tile.TileWidth)) / _maxDepth);
            return depthOffset - (height * DepthModifier);
        }

        public static float CalculateDepthOffsetY(int y)
        {
            return StartOffsetY - DepthModifier*(y + 1);
        }

    }
}
