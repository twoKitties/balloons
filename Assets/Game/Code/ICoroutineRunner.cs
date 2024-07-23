using System.Collections;
using UnityEngine;

namespace Game.Code
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}