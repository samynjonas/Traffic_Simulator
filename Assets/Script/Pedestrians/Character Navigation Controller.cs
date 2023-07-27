using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _Destination;
    protected Vector3 _LastPosition;

    protected Vector3 _Velocity;

    [SerializeField]
    public bool _ReachedDestination = false;

    [SerializeField]
    protected float _StopDistance;

    [SerializeField]
    protected float _RotationSpeed;

    [SerializeField]
    protected float _SpeedLimit;

    protected float _MaxMovementSpeed;
    protected float _MaxAngleSpeed;

    protected float _MovementSpeed;

    [SerializeField]
    protected float _AccelerationSpeed;

    [SerializeField]
    protected float _DecelerationSpeed;

    [SerializeField]
    protected float _MaxAngleChange;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeedLimit(_SpeedLimit);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSpeed();
        UpdateNavigation();
    }

    protected void UpdateNavigation()
    {
        if (transform.position != _Destination)
        {
            Vector3 destinationDirection = _Destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;
            if (destinationDistance >= _StopDistance)
            {
                _ReachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);

                CheckForAngle(targetRotation);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _RotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * _MovementSpeed * Time.deltaTime);
            }
            else
            {
                _ReachedDestination = true;
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this._Destination = destination;
        _ReachedDestination = false;
    }

    protected void CalculateSpeed()
    {
        if(_MovementSpeed < _MaxMovementSpeed) 
        {
            _MovementSpeed += _AccelerationSpeed;
            if(_MovementSpeed > _MaxMovementSpeed)
            {
                _MovementSpeed = _MaxMovementSpeed;
            }
        }
        else if(_MovementSpeed > _MaxMovementSpeed)
        {
            _MaxMovementSpeed -= _DecelerationSpeed;
            if(_MovementSpeed < _MaxMovementSpeed)
            {
                _MovementSpeed = _MaxMovementSpeed;
            }
        }
    }

    public void SetSpeedLimit(float maxSpeed)
    {
        _SpeedLimit = maxSpeed;

        _MaxAngleSpeed = maxSpeed * 0.35f;

        _MaxMovementSpeed = _SpeedLimit;
    }

    private void CheckForAngle(Quaternion targetRotation)
    {
        float targetAngle = targetRotation.eulerAngles.y;
        float currentAngle = transform.eulerAngles.y;

        float angleChange = Mathf.Abs(targetAngle - currentAngle);

        if(angleChange > _MaxAngleChange)
        {
            _MaxMovementSpeed = _MaxAngleSpeed;
        }
        else
        {
            _MaxMovementSpeed = _SpeedLimit;
        }
    }

}
