using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Siren : Door
{
    [Header("Siren Params")]
    [SerializeField] private float _changeSirenVolumeSpeed;
    private float _sirenVolume
    {
        get { return _audioSource.volume; }
        set { _audioSource.volume = value; }
    }

    private bool thiefInHouse = false;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnValidate()
    {
        if (_changeSirenVolumeSpeed < 0f) 
            _changeSirenVolumeSpeed = 0.1f;
    }

    private IEnumerator _ChangeSirenVolume()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        float _targetVolume;

        _audioSource.Play();

        while (_audioSource.isPlaying)
        {
            yield return waitForEndOfFrame;

            _targetVolume = !thiefInHouse ? 0f : 1f;
            
            _sirenVolume = Mathf.MoveTowards(_sirenVolume, _targetVolume, _changeSirenVolumeSpeed * Time.fixedDeltaTime);

            if (_sirenVolume <= 0f)
                _audioSource.Stop();
        }
    }

    public void OnPlayerSiren()
    {
        thiefInHouse = true;

        if (_ChangeSirenVolume() != null)
            StopCoroutine(_ChangeSirenVolume());
        StartCoroutine(_ChangeSirenVolume());
    }

    public void OnStopSiren()
    {
        thiefInHouse = false;
    }

}

