using System.Collections;
using UnityEngine;

public class FSMCoroutineSingleton : MonoBehaviour
{
    public static FSMCoroutineSingleton Instance { get; private set; } = null;
    public static bool IsInstanceNull => Instance == null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RunCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
