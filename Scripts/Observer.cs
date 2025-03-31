using System;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public event Action ThiefEntered;
    public event Action ThiefExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Mover>(out Mover mover))
        {
            ThiefEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Mover>(out Mover mover))
        {
            ThiefExited?.Invoke();
        }
    }
}
