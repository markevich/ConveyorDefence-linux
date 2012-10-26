using ConveyorDefence.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ConveyorDefence.Missiles.Properties
{
    abstract class MissileProperty
    {
        public int TileID;
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch batch, Point nodeIndex)
        {
            int rowOffset = nodeIndex.Y % 2 == 1 ? Tile.OddRowXOffset : 0;
            var nodePosition = Camera.WorldToScreen(
                        new Vector2((nodeIndex.X * Tile.TileStepX) + rowOffset, nodeIndex.Y * Tile.TileStepY));
            var missilePosition = new Vector2(nodePosition.X, nodePosition.Y - Tile.TileHeight / 4 + 2);
            var depth = DepthCalculator.CalculateDepth(nodeIndex.X, nodeIndex.Y);
            batch.Draw(
               Tile.TileTexture,
               missilePosition,
               Tile.GetSourceRectangle(TileID),
               Color.White,
               0.0f,
               Vector2.Zero,
               1.0f,
               SpriteEffects.None,
               depth - DepthCalculator.DepthModifier
               );
        }
    }
}
