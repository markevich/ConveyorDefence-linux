using System;
using System.Collections.Generic;
using ConveyorDefence.Map;
using ConveyorDefence.Missiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConveyorDefence.Nodes.Strategies;
using ConveyorDefence.Nodes.Strategies.Process;

namespace ConveyorDefence.Nodes
{
    internal class Node
    {
        private readonly float _processCooldown;
        private InputStrategy _inputStrategy;
        private OutputStrategy _outputStrategy;
        private ProcessStrategy _processStrategy;

        protected float TimeSinseLastProcess;
        public Node NextNode { get; set; }
        protected List<Missile> _missiles;
        public NodeDirection Direction { get; set; }
        public Point Index { get; set; }
        public int LeftDownTileID;
        public int RightDownTileID;

        private int TileID
        {
            get
            {
                switch (Direction)
                {
                    case NodeDirection.LeftDown:
                        {
                            return LeftDownTileID;

                        }
                    case NodeDirection.RightDown:
                        {
                            return RightDownTileID;
                        }
                }
                return 0;
            }
        }

        public Node(float outputCooldown)
        {
            _processCooldown = outputCooldown;
            _missiles = new List<Missile>();
        }

        public Node(float outputCooldown, int leftDownTileID, int rightDownTileID, NodeDirection direction,
                    InputStrategy inputStrategy, ProcessStrategy processStrategy, OutputStrategy outputStrategy)
            : this(outputCooldown)
        {
            LeftDownTileID = leftDownTileID;
            RightDownTileID = rightDownTileID;
            Direction = direction;
            _inputStrategy = inputStrategy;
            _outputStrategy = outputStrategy;
            _processStrategy = processStrategy;
        }

        public virtual void Input(Missile missile)
        {

            _inputStrategy.Input(ref missile, ref _missiles);



            missile.NodeIndex = Index;

        }

        protected virtual void Process()
        {
            _processStrategy.Process(ref _missiles);
        }

        protected int OutputsCount;

        protected virtual void Output()
        {
            Node nextNode = NextNode;
            _outputStrategy.Output(ref _missiles, ref nextNode);

            OutputsCount++;
        }

        public void Update(GameTime gameTime)
        {

            if (_processStrategy.GetType() != typeof (GenerateRock) && _missiles.Count == 0) return;
            TimeSinseLastProcess += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinseLastProcess > _processCooldown)
            {
                TimeSinseLastProcess = 0;
                Process();
                Output();
            }


        }

        protected virtual bool HasNodeDatas()
        {
            return _missiles.Count > 0;
        }

        private Missile GetCurrentMissile()
        {
            return _missiles[0];
        }

        private bool NextNodeExists()
        {
            return NextNode != null;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (TileID == 0) return;
            var depth = DepthCalculator.CalculateDepth(Index.X, Index.Y);

            batch.Draw(
                Tile.TileTexture,
                Position,
                Tile.GetSourceRectangle(TileID),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                depth
                );
            //var position = new Vector2(Position.X + Tile.TileWidth / 2 - 4, Position.Y + Tile.TileHeight / 3);
            //batch.DrawString(Game.CountFont, _missiles.Count.ToString(), position, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

        }

        protected Vector2 Position
        {
            get
            {
                int rowOffset = Index.Y%2 == 1 ? Tile.OddRowXOffset : 0;

                var position = Camera.WorldToScreen(
                    new Vector2((Index.X*Tile.TileStepX) + rowOffset, Index.Y*Tile.TileStepY));
                return position;
            }
        }
    }
}
