using System;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public event Action EnterHouse;
    public event Action ExitHouse;

    private void OnTriggerEnter(Collider other)
    {
        EnterHouse?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        ExitHouse?.Invoke();
    }
}
