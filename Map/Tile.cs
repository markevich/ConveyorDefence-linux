using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ConveyorDefence.Map
{
    static class Tile
    {
        public static Texture2D TileTexture;
        public const int TileWidth = 64;
        public const int TileHeight = 64;
        public const int TileStepX = 64;
        public const int TileStepY = 16;
        public const int OddRowXOffset = 32;
        public const int HeightTileOffset = 32;

        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            var tileY = tileIndex / (TileTexture.Width / TileWidth);
            var tileX = tileIndex % (TileTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
