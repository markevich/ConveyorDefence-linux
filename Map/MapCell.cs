using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConveyorDefence.Nodes;

namespace ConveyorDefence.Map
{
    class MapCell
    {
        public int TileID
        {
            get { return _baseTiles.Count > 0 ? _baseTiles[0] : 0; }
            set
            {
                if (_baseTiles.Count > 0)
                    _baseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }

        private List<int> _baseTiles = new List<int>();
        private List<int> _heightTiles = new List<int>();
        private List<int> _topperTiles = new List<int>();

        public int Height
        {
            get { return _heightTiles.Count; }
        }
        public MapCell(int tileID)
        {
            TileID = tileID;
        }

        public void AddBaseTile(int tileID)
        {
            _baseTiles.Add(tileID);
        }

        public void AddHeightTile(int tileID)
        {
            _heightTiles.Add(tileID);
        }

        public void AddTopperTile(int tileID)
        {
            _topperTiles.Add(tileID);
        }

        public void Draw(SpriteBatch batch, Point index)
        {

            int rowOffset = index.Y%2 == 1 ? Tile.OddRowXOffset : 0;
            DrawBaseTiles(batch, index, rowOffset);

            DrawHeightTiles(batch, index, rowOffset);

            DrawTopperTiles(batch, index, rowOffset);

        }

        private void DrawBaseTiles(SpriteBatch batch, Point index, int rowOffset)
        {
            foreach (int tileID in _baseTiles)
            {
                var depthOffsetY = DepthCalculator.CalculateDepthOffsetY(index.Y);
                batch.Draw(
                    Tile.TileTexture,
                    Camera.WorldToScreen(
                        new Vector2((index.X*Tile.TileStepX) + rowOffset, index.Y*Tile.TileStepY)),
                    Tile.GetSourceRectangle(tileID),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    depthOffsetY);
            }
        }

        private void DrawHeightTiles(SpriteBatch batch, Point index, int rowOffset)
        {
            for (int i = 0; i < Height; i++)
            {
                int tileID = _heightTiles[i];
                var depth = DepthCalculator.CalculateDepth(index.X, index.Y, i);
                batch.Draw(
                    Tile.TileTexture,
                    Camera.WorldToScreen(
                        new Vector2(
                            (index.X*Tile.TileStepX) + rowOffset,
                            index.Y*Tile.TileStepY - (i*Tile.HeightTileOffset))),
                    Tile.GetSourceRectangle(tileID),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    depth);
            }
        }

        private void DrawTopperTiles(SpriteBatch batch, Point index,
                                     int rowOffset)
        {
            var depth = DepthCalculator.CalculateDepth(index.X, index.Y, Height);
            foreach (int tileID in _topperTiles)
            {
                batch.Draw(
                    Tile.TileTexture,
                    Camera.WorldToScreen(
                        new Vector2((index.X*Tile.TileStepX) + rowOffset, index.Y*Tile.TileStepY)),
                    Tile.GetSourceRectangle(tileID),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    depth
                    );
            }
        }


        
    }
}
