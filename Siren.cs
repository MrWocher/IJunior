using System.Collections;
using UnityEngine;

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
        if (_changeSirenVolumeSpeed < 0f) _changeSirenVolumeSpeed = 0f;
    }

    private void FixedUpdate()
    {
        if(_audioSource.isPlaying)
            _sirenVolume = Mathf.MoveTowards(_sirenVolume, 1f, _changeSirenVolumeSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator CheckSirenVolume()
    {
        var waitUntil = new WaitUntil(() => _sirenVolume <= 0f);

        StopCoroutine(CheckSirenVolume());

        yield return waitUntil;

        _audioSource.Stop();
    }

    public void OnPlayerSiren()
    {
        _audioSource.Play();
        _changeSirenVolumeSpeed = Mathf.Abs(_changeSirenVolumeSpeed);
    }
    public void OnStopSiren()
    {
        _changeSirenVolumeSpeed = -Mathf.Abs(_changeSirenVolumeSpeed);
        StartCoroutine(CheckSirenVolume());
    }

}
