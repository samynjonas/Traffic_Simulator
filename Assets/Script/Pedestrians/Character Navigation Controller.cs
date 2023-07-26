using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
    [SerializeField]
    Vector3 _Destination;
    Vector3 _LastPosition;

    Vector3 _Velocity;

    [SerializeField]
    public bool _ReachedDestination = false;

    [SerializeField]
    float _StopDistance;

    [SerializeField]
    float _RotationSpeed;

    [SerializeField]
    float _MovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _MovementSpeed += Random.Range(-0.25f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != _Destination)
        {
            Vector3 destinationDirection = _Destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;
            if(destinationDistance >= _StopDistance) 
            {
                _ReachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _RotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * _MovementSpeed * Time.deltaTime);
            }
            else
            {
                _ReachedDestination = true;
            }

            _Velocity = (transform.position - _LastPosition) / Time.deltaTime;
            _Velocity.y = 0;

            var velocityMagnitude = _Velocity.magnitude;
            
            _Velocity = _Velocity.normalized;

            var fwdDotProduct = Vector3.Dot(transform.forward, _Velocity);
            var rightDotProduct = Vector3.Dot(transform.right, _Velocity);

        }
    }

    public void SetDestination(Vector3 destination)
    {
        this._Destination = destination;
        _ReachedDestination = false;
    }

}
