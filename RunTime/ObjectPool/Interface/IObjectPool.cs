using System;
using UnityEngine;

namespace Utils
{
    public interface IObjectPool
    {
        void Init(Transform parent);
        GameObject Push(Enum type, GameObject prefab);
        void Pull(Enum type, GameObject obj);
    }
}