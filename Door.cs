using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{

    [Header("Siren Params")]
    [SerializeField] private float _speedLouder;

    private bool _thiefOnHome = false;

    private AudioSource _audioSource;
    private float _soundVolume { 
        get { return audioSource.volume; } 
        set { audioSource.volume = value; } 
    }


    private void OnEnable()
    {
        
        _audioSource = GetComponent<AudioSource>();

    }
    
    private void OnValidate()
    {

        if (_speedLouder < 0f) _speedLouder = 0f; 

    }

    private void FixedUpdate()
    {

        if (_thiefOnHome)
        {

            _soundVolume = Mathf.MoveTowards(_soundVolume, 1f, _speedLouder * Time.fixedDeltaTime);

        }
        if (!thiefOnHome)
        {

            _soundVolume = Mathf.MoveTowards(_soundVolume, 0f, _speedLouder * Time.fixedDeltaTime);

        }

    }

    IEnumerator checkVolumeSound()
    {

        var waitUntil = new WaitUntil(() => _soundVolume <= 0f);

        yield return waitUntil;

        _audioSource.Stop();
    
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.TryGetComponent(out MoveThief thief))
        {

            _audioSource.Play();
            _thiefOnHome = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent(out MoveThief thief))
        {

            _thiefOnHome = false;
            StartCoroutine(checkVolumeSound());

        }

    }

}
