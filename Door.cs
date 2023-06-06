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
        
        audioSource = GetComponent<AudioSource>();

    }
    
    private void OnValidate()
    {

        if (speedLouder < 0f) speedLouder = 0f; 

    }

    private void FixedUpdate()
    {

        if (thiefOnHome)
        {

            soundVolume = Mathf.MoveTowards(soundVolume, 1f, speedLouder * Time.fixedDeltaTime);

        }
        if (!thiefOnHome)
        {

            soundVolume = Mathf.MoveTowards(soundVolume, 0f, speedLouder * Time.fixedDeltaTime);

        }

    }

    IEnumerator checkVolumeSound()
    {

        var waitUntil = new WaitUntil(() => soundVolume <= 0f);

        yield return waitUntil;

        audioSource.Stop();
    
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.TryGetComponent(out MoveThief thief))
        {

            audioSource.Play();
            thiefOnHome = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent(out MoveThief thief))
        {

            thiefOnHome = false;
            StartCoroutine(checkVolumeSound());

        }

    }

}
