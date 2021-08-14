using System;
using System.Collections.Generic;

namespace LRUCache
{
    public class LRUCache
    {
        Dictionary<int, Tuple<int, DateTime>> list = new Dictionary<int, Tuple<int, DateTime>>();
        int cap;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            LRUCache lRUCache = new LRUCache(2);
            lRUCache.Put(1, 1); // cache is {1=1}
            lRUCache.Put(2, 2); // cache is {1=1, 2=2}
            lRUCache.Get(1);    // return 1
            lRUCache.Put(3, 3); // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
            lRUCache.Get(2);    // returns -1 (not found)
            lRUCache.Put(4, 4); // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
            lRUCache.Get(1);    // return -1 (not found)
            lRUCache.Get(3);    // return 3
            lRUCache.Get(4);    // return 4
        }

        public LRUCache(int capacity)
        {
            cap = capacity;
        }


        public int Get(int key)
        {
            if (list.ContainsKey(key))
            {
                list[key] = new Tuple<int, DateTime>(list[key].Item1, DateTime.UtcNow);
                return list[key].Item1;
            }
            else
            {
                return -1;
            }
        }

        public void Put(int key, int value)
        {
            TimeSpan minDateTime = TimeSpan.Zero;
            DateTime RightNow = DateTime.UtcNow;
            if (list.Count == cap)
            {
                int keyToRemove = 0;
                foreach (var item in list)
                {
                    TimeSpan temp = RightNow - item.Value.Item2;
                    if (temp > minDateTime)
                    {
                        keyToRemove = item.Key;
                        minDateTime = temp;
                    }
                }
                list.Remove(keyToRemove);
            }
            list[key] = new Tuple<int, DateTime>(value, DateTime.UtcNow);
        }
    }

    /**
     * Your LRUCache object will be instantiated and called as such:
     * LRUCache obj = new LRUCache(capacity);
     * int param_1 = obj.Get(key);
     * obj.Put(key,value);
     */
}
