using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{

    [SerializeField] private UnityEvent _enterInHome;
    [SerializeField] private UnityEvent _exitFromHome;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out MoveThief thief))
        {
            _enterInHome?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MoveThief thief))
        {
            _exitFromHome?.Invoke();
        }
    }

}

