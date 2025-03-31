using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    private const float MaxVolume = 1f;
    private const float MinVolume = 0f;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Observer _observer;
    [SerializeField] private float _volumeSpeed = 0.5f;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _observer.ThiefEntered += OnThiefEntered;
        _observer.ThiefExited += OnThiefExited;
        _audioSource.volume = 0f;
    }

    private void OnDisable()
    {
        _observer.ThiefEntered -= OnThiefEntered;
        _observer.ThiefExited -= OnThiefExited;
    }

    private IEnumerator ChangedAlarmVolume(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(
                _audioSource.volume,
                targetVolume,
                _volumeSpeed * Time.deltaTime);
        
            yield return null;
        }
    }

    private void OnThiefEntered()
    {
        if (_coroutine != null) 
        {
            StopCoroutine(_coroutine);    
        }

        _coroutine = StartCoroutine(ChangedAlarmVolume(MaxVolume));
    }

    private void OnThiefExited()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangedAlarmVolume(MinVolume));
    }
}