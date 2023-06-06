using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThief : MonoBehaviour
{

    [SerializeField] private float speed;

    private Transform thiefTransform;

    private void OnValidate()
    {
        if(speed < 0f) speed = 0f;
    }

    private void OnEnable()
    {
        
        thiefTransform = GetComponent<Transform>();

    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {

            thiefTransform.Translate(-speed * Time.fixedDeltaTime, 0f, 0f);

        }
        else if (Input.GetKey(KeyCode.S))
        {

            thiefTransform.Translate(speed * Time.fixedDeltaTime, 0f, 0f);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Door door)){

            

        }

    }

}
