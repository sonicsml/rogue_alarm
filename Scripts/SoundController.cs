using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _volumeSpeed = 0.5f;
    [SerializeField] private Transform _point;

    private bool _isTriggerHouse = false;
    private float _pointVolume = 0f;

    private void Update()
    {
        _audioSource.volume = Mathf.MoveTowards(
            _audioSource.volume,
            _pointVolume,
            _volumeSpeed * Time.deltaTime);
    }

    private void OnTriggerHouseEnter()
    {
        _isTriggerHouse = true;

        _pointVolume = 1f;
    
        if(!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void OnTriggerHouseExit()
    {
        _isTriggerHouse = false;
        _pointVolume = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerHouseEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerHouseExit();
    }
}
