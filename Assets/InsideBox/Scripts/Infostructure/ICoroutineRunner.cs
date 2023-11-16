using System.Collections;
using UnityEngine;

namespace Scripts.Infostructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}