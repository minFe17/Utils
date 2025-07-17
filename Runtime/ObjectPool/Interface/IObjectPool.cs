using System;
using UnityEngine;

namespace Utils
{
    public interface IObjectPool
    {
        void Init(Transform parent);
        GameObject Push(Enum type);
        void Pull(Enum type, GameObject obj);
    }
}