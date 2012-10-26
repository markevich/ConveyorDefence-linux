using ConveyorDefence.Map;
using Microsoft.Xna.Framework;

namespace ConveyorDefence
{
    public static class Camera
    {
        static private Vector2 _location = Vector2.Zero;
        public static Vector2 Location
        {
            get
            {
                return _location;
            }
            private set
            {
                _location = new Vector2(
                    MathHelper.Clamp(value.X, 0f, WorldWidth - ViewWidth),
                    MathHelper.Clamp(value.Y, 0f, WorldHeight - ViewHeight));
            }
        }

        public static int ViewWidth { get; set; }
        public static int ViewHeight { get; set; }
        public static int WorldWidth { get; set; }
        public static int WorldHeight { get; set; }

        public static Vector2 DisplayOffset { get; set; }

        public static Point FirstVisibleTileIndex
        {
            get { return new Point((int)Camera.Location.X / Tile.TileStepX, (int)Camera.Location.Y / Tile.TileStepY); }
        }
        public static bool IsTileOutsideOfMap(Point tileIndex)
        {
            return (tileIndex.X >= TileMap.MapWidth) || (tileIndex.Y >= TileMap.MapHeight);
        }
        public static Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return worldPosition - Location + DisplayOffset;
        }

        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return screenPosition + Location - DisplayOffset;
        }

        public static void Move(Vector2 offset)
        {
            Location += offset;
        }
    }
}
