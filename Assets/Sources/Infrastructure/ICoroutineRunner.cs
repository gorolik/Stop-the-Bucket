using System.Collections;
using UnityEngine;

namespace Sources.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine); 
    }
}