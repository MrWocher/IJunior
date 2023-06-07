using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Siren siren;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out MoveThief thief))
        {
            siren.OnPlayerSiren();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MoveThief thief))
        {
            siren.OnStopSiren();
        }
    }

}

