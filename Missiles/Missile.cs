using System.Collections.Generic;
using ConveyorDefence.Missiles.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ConveyorDefence.Missiles
{
    class Missile
    {
        private List<MissileProperty> _properties;
        public Point NodeIndex { get; set; }
        public int LeftDownTileID
        {
            get { return _properties[0].TileID; }
        }
        public int RightDownTileID
        {
            get { return _properties[0].TileID; }
        }
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public Missile()
        {
            Visible =  false;
            Active = true;
            _properties = new List<MissileProperty>();
        }

        public Missile WithProperty(MissileProperty property)
        {
            _properties.Add(property);
            return this;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var property in _properties)
            {
                property.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch batch)
        {
            if (!Active || !Visible) return;
            foreach (var property in _properties)
            {
                property.Draw(batch, NodeIndex);
            }
        }

        private void RemoveProperties()
        {
            _properties.Clear();
        }

        internal void Deactivate()
        {
            Active = false;
            Visible = false;
            RemoveProperties();
        }

        internal void Activate()
        {
            Active = true;
        }
    }

}
