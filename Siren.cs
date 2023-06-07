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

    private IEnumerator IncreaseSirenVolume()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        _audioSource.Play();

        while (_sirenVolume < 1f)
        {
            _sirenVolume = Mathf.MoveTowards(_sirenVolume, 1f, _changeSirenVolumeSpeed * Time.fixedDeltaTime);

            yield return waitForEndOfFrame;
        }
    }

    private IEnumerator ReduceSirenVolume()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        while (_sirenVolume > 0f)
        {
            _sirenVolume = Mathf.MoveTowards(_sirenVolume, 0f, _changeSirenVolumeSpeed * Time.fixedDeltaTime);

            yield return waitForEndOfFrame;
        }

        _audioSource.Stop();
    }

    public void OnPlayerSiren()
    {
        StopAllCoroutines();
        StartCoroutine(IncreaseSirenVolume());
    }
    public void OnStopSiren()
    {
        StopAllCoroutines();
        StartCoroutine(ReduceSirenVolume());
    }

}

