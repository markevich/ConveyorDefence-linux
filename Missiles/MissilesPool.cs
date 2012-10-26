using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyorDefence.Misc;

namespace ConveyorDefence.Missiles
{
    class MissilesPool:IEnumerable
    {
        private ObjectPool<Missile> _missiles; 
        public MissilesPool(int size)
        {
            _missiles = new ObjectPool<Missile>(size);
        }
        public Missile GetFreeMissile()
        {
            foreach (Missile missile in _missiles)
            {
                if (missile.Active) continue;
                missile.Activate();
                return missile;
            }
            return _missiles.AddNewObject();
        }
        public int Count{
            get { 
                int count = 0;
                foreach (Missile missile in _missiles)
                {
                    if (missile.Active)
                    {
                        count++;
                    }
                }
                return count;
            }}
        public IEnumerator GetEnumerator()
        {
            foreach (Missile missile in _missiles)
            {
                yield return missile;
            }
        }

    }
}
