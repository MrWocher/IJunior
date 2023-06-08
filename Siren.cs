using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Siren : MonoBehaviour
{
    [Header("Siren Params")]
    [SerializeField] private float _changeVolumePerFrame = 0.1f;
    private float _sirenVolume
    {
        get { return _audioSource.volume; }
        set { _audioSource.volume = value; }
    }

    private float _maxSirenVolume = 1f;
    private float _minSirenVolume = 0f;

    private float _targetSirenVolume
    {
        get { return !thiefInHouse ? _minSirenVolume : _maxSirenVolume; }
    }

    private bool thiefInHouse = false;

    private AudioSource _audioSource;

    private Coroutine _changeSirenVolume;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnValidate()
    {
        if (_changeVolumePerFrame < 0f)
            _changeVolumePerFrame = 0.1f;
    }

    private void _startChangeSirenVolume()
    {

        if (_changeSirenVolume != null)
            StopCoroutine(_changeSirenVolume);
        _changeSirenVolume = StartCoroutine(_ChangeSirenVolume(_targetSirenVolume));

    }

    private IEnumerator _ChangeSirenVolume(float _targetVolume)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        _audioSource.Play();

        while (_audioSource.isPlaying)
        {
            yield return waitForEndOfFrame;
            
            _sirenVolume = Mathf.MoveTowards(_sirenVolume, _targetVolume, _changeVolumePerFrame * Time.fixedDeltaTime);

            if (_sirenVolume <= 0f)
                _audioSource.Stop();
        }
    }

    public void OnPlaySiren()
    {
        thiefInHouse = true;

        _startChangeSirenVolume();
    }

    public void OnStopSiren()
    {
        thiefInHouse = false;

        _startChangeSirenVolume();
    }

}


