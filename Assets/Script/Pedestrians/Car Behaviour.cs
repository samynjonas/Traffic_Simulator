using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : CharacterNavigationController
{
    public enum CarState
    {
        Parked,     //Car doesnt drive  - has no goal check only for new goal
        Stopped,    //Car doesnt drive  - has temporarely stopped and is checken for when it can drive again
        Driving     //Car is driving    -
    }

    [SerializeField]
    private CarState _CarState;

    [SerializeField]
    private float _FrontVisualLength;

    void Start()
    {
        SetSpeedLimit(_SpeedLimit);
    }

    private void Update()
    {
        switch(_CarState)
        {
            case CarState.Parked:
                UpdateParkedState();
            break;
            case CarState.Stopped:
                UpdateStoppedState();
            break;
            case CarState.Driving:
                UpdateDrivingState();
            break;
        }

        //Debug.Log("Speed: " + _MovementSpeed);
    }

    void UpdateParkedState()
    {
        //Check for new waypoint/goal
    }

    void UpdateStoppedState()
    {
        //Check if there is a car infront - check for traffic rules
        CheckFront();
        Braking();
        UpdateNavigation();
    }

    void UpdateDrivingState()
    {
        //Check if there is a car infront - check for new waypoints - check for traffic rules
        CheckFront();
        CalculateSpeed();
        UpdateNavigation();
    }


    void CheckFront()
    {
        Debug.DrawRay(transform.position, transform.forward * _FrontVisualLength, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, _FrontVisualLength))
        {
            if (hit.distance < 10)
            {
                var hitScript = hit.collider.gameObject.GetComponent<TrafficLight_Script>();
                if (hitScript)
                {
                    if (hitScript._TrafficLightState != TrafficLight_Script.TrafficLightStates.Green)
                    {
                        _CarState = CarState.Stopped;
                    }
                    else
                    {
                        _CarState = CarState.Driving;
                    }
                }
                else
                {
                    _CarState = CarState.Stopped;
                }
            }
            else
            {
                _CarState = CarState.Driving;
            }
        }
        else
        {
            _CarState = CarState.Driving;
        }
    }

    void Braking()
    {
        if (_MovementSpeed > 0)
        {
            _MovementSpeed -= _DecelerationSpeed;
            if(_MovementSpeed < 0)
            {
                _MovementSpeed = 0;
            }
        }
    }

}
