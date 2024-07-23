using System;
using System.Collections.Generic;

namespace Game.Code.Core
{
    public class Pool<T>
    {
        private readonly Stack<T> _stack;
        private readonly Func<T> _factory;

        public Pool(Func<T> factory)
        {
            _factory = factory;
            _stack = new Stack<T>();
        }

        public T Get()
        {
            if (_stack.TryPop(out var t))
                return t;

            return _factory();
        }
        
        public void Release(T t)
        {
            _stack.Push(t);
        }
    }
}