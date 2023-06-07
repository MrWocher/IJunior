using UnityEngine;

public class MoveThief : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _thiefTransform;

    private void OnValidate()
    {
        if(_speed < 0f) _speed = 0f;
    }

    private void OnEnable()
    {
        _thiefTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _thiefTransform.Translate(-_speed * Time.fixedDeltaTime, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _thiefTransform.Translate(_speed * Time.fixedDeltaTime, 0f, 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Door door)){

        }
    }

}

