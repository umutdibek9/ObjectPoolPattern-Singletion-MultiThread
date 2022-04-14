using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazılımTasarımKalıpları
{
    public class ClientPool
    {
        private static Lazy<ClientPool> instance
            = new Lazy<ClientPool>(() => new ClientPool());
        public static ClientPool Instance { get; } = instance.Value;
        public int Size { get { return _currentSize; } }
        public int TotalObject { get { return _counter; } }

        private const int defaultSize = 10;
        private ConcurrentBag<Client> _bag = new ConcurrentBag<Client>();
        private volatile int _currentSize;
        private volatile int _counter;
        private object _lockObject = new object();

        private ClientPool()
            : this(defaultSize)
        {
        }
        private ClientPool(int size)
        {
            _currentSize = size;
        }

        public Client AcquireObject()
        {
            if (!_bag.TryTake(out Client item))
            {
                lock (_lockObject)
                {
                    if (item == null)
                    {
                        if (_counter >= _currentSize)
                            // or throw an exception, or wait for an object to return.
                            return null;

                        item = new RequestClient();

                        // it could be Interlocked.Increment(_counter). Since, we have locked the section, I don't think we need that.
                        _counter++;

                    }
                }

            }

            return item;
        }

        public void ReleaseObject(Client item)
        {
            _bag.Add(item);
        }
        public void IncreaseSize()
        {
            lock (_lockObject)
            {
                _currentSize++;
            }
        }
        public int getSize()
        {
            return _currentSize;
         
        }

    }
}
