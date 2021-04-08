using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Extensions
{
    public static class CircularEnumeratorExtension
    {
        public static CircularEnumarator<T> GetCircularEnumerator<T>(this IList<T> t)
        {
            return new CircularEnumarator<T>(t.GetEnumerator(), t);
        }

        public class CircularEnumarator<T> : IEnumerator<T>
        {
            private readonly IEnumerator _wrapedEnumerator;
            private IList<T> _buffer;
            private int _index;

            public CircularEnumarator(IEnumerator wrapedEnumerator, IList<T> list)
            {
                this._wrapedEnumerator = wrapedEnumerator;
                this._buffer = list;
            }

            public void SetStartIndex(int index)
            {
                this._index = index;
            }

            public object Current => _buffer[_index];

            T IEnumerator<T>.Current => (T)Current;

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                ++_index;
                if (_index >= _buffer.Count)
                {
                    Reset();
                    return _wrapedEnumerator.MoveNext();
                }
                return true;
            }

            public bool MovePrevious()
            {
                --_index;

                if (_index < 0)
                {
                    _index = _buffer.Count - 1;
                }

                return true;
            }

            public void Reset()
            {
                _index = 0;
                _wrapedEnumerator.Reset();
            }
        }
    }
}
