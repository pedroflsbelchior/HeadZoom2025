using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Oblivious : MonoBehaviour
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
