using UnityEngine;

public class MoveThief : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void OnValidate()
    {
        if(_speed < 0f) 
            _speed = 2f;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(-_speed * Time.fixedDeltaTime, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(_speed * Time.fixedDeltaTime, 0f, 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Door door)){

        }
    }

}

