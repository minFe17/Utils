using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectPoolManager : MonoBehaviour
    {
        // 싱글턴
        Dictionary<Type, IObjectPool> _objectPools = new Dictionary<Type, IObjectPool>();

        ObjectPool<TEnum> GetOrCreatePool<TEnum>() where TEnum : Enum
        {
            Type key = typeof(TEnum);

            if (_objectPools.TryGetValue(key, out IObjectPool pool))
                return pool as ObjectPool<TEnum>;

            ObjectPool<TEnum> newPool = new ObjectPool<TEnum>();
            newPool.Init(transform); // 부모 Transform 설정
            _objectPools[key] = newPool;
            return newPool;
        }

        public void RegisterFactory(Enum type, IFactory<GameObject> factory)
        {
            GetOrCreatePool<TEnum>().RegusterFactory(type, factory);
        }

        public GameObject Push<TEnum>(TEnum type, GameObject prefab) where TEnum : Enum
        {
            return GetOrCreatePool<TEnum>().Push(type, prefab);
        }

        public void Pull<TEnum>(TEnum type, GameObject obj) where TEnum : Enum
        {
            GetOrCreatePool<TEnum>().Pull(type, obj);
        }
    }
}