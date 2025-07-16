using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Utils
{
    public class ObjectPool<TEnum> : IObjectPool where TEnum : Enum
    {
        Dictionary<TEnum, Queue<GameObject>> _objectPool = new Dictionary<TEnum, Queue<GameObject>>();
        Dictionary<TEnum, IFactory<GameObject>> _factories = new Dictionary<TEnum, IFactory<GameObject>>();

        TEnum[] _enumValue;

        Transform _parent;

        void IObjectPool.Init(Transform parent)
        {
            _parent = parent;
            _enumValue = (TEnum[])Enum.GetValues(typeof(TEnum));

            for (int i = 0; i < _enumValue.Length; i++)
                _objectPool[_enumValue[i]] = new Queue<GameObject>();
        }

        public void RegisterFactory(Enum type, IFactory<GameObject> factory)
        {
            TEnum key = (TEnum)type;
            if (!_factories.ContainsKey(key))
                _factories.Add(key, factory);
            else
                _factories[key] = factory;
        }

        public GameObject Push(Enum type)
        {
            TEnum key = (TEnum)type;
            Queue<GameObject> queue;
            _objectPool.TryGetValue(key, out queue);

            GameObject returnObject = null;
            if (queue != null && queue.Count > 0)
            {
                returnObject = queue.Dequeue();
                returnObject.SetActive(true);
            }
            else
            {
                if (_factories.TryGetValue(key, out IFactory<GameObject> factory))
                {
                    returnObject = factory.Create();
                    returnObject.transform.SetParent(_parent);
                }
            }
            return returnObject;
        }

        public void Pull(Enum type, GameObject obj)
        {
            TEnum key = (TEnum)type;
            Queue<GameObject> queue;
            _objectPool.TryGetValue(key, out queue);
            if (queue == null)
                return;

            obj.SetActive(false);
            queue.Enqueue(obj);
            obj.transform.SetParent(_parent);
        }
    }
}