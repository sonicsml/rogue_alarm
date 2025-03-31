using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Observer _observer;
    [SerializeField] private float _volumeSpeed = 0.5f;

    private Coroutine _coroutine;

    private float _pointVolume = 0f;
    private float _maxVolume = 1f;

    private void OnEnable()
    {
        _observer.EnterHouse += OnEnterHouse;
        _observer.ExitHouse += OnExitHouse;
        _audioSource.volume = 0f;
    }

    private void OnDisable()
    {
        _observer.EnterHouse -= OnEnterHouse;
        _observer.ExitHouse -= OnExitHouse;
    }

    private IEnumerator ChangedAlarmVolume()
    {
        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(
                _audioSource.volume,
                _pointVolume,
                _volumeSpeed * Time.deltaTime);
        
            yield return null;
        }
    }

    private void OnEnterHouse()
    {
        if (_coroutine != null) 
        {
            StopCoroutine(_coroutine);    
        }

        _coroutine = StartCoroutine(ChangedAlarmVolume());
    }

    private void OnExitHouse()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangedAlarmVolume());
    }
}