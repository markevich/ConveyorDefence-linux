using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConveyorDefence.Misc
{
    public sealed class ObjectPool<T> : IEnumerable<T> where T : new()
    {
        private readonly List<T> _collection;
        private readonly int _capacity;
        public ObjectPool(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException("size", size, "The size of the pool must be greater than zero.");
            }
            _capacity = size;
            _collection = new List<T>(size);
        }

        public T AddNewObject()
        {
            if (_collection.Count > _capacity)
            {
                var message =  String.Format("Number of object in pool exceeded the maximum capacity", _collection.Count);
                throw new ArgumentOutOfRangeException("size", _capacity, message);

            }
             
            var element = new T();
            _collection.Add(element);
            return element;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>) _collection).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
