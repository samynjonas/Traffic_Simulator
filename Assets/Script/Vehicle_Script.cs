using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vehicle_Script : MonoBehaviour
{
    public enum vehicleStates
    {
        Parked,
        Driving,
        Stopped
    }

    [SerializeField]
    private vehicleStates _VehicleState;

    [SerializeField]
    private float _SpeedLimit; //Current street speed limit
    public void SetSpeedLimit(float speedLimit)
    {
        _SpeedLimit = speedLimit;
    }

    [SerializeField]
    private float _Acceleration;

    [SerializeField]
    private float _BrakeStrength;

    [SerializeField]
    private float _FrontVisual;

    private float _Speed; //The speed the car is going

    [SerializeField]
    private bool _CanDrive = true;
    public void SetCanDrive(bool state)
    {
        _CanDrive = state;
        Debug.Log("Set CanDrive: " + _CanDrive);
    }

    private void Start()
    {
        _VehicleState = vehicleStates.Driving;
    }

    void Update()
    {
        CheckFront();

        CalculateSpeed();
        transform.position += new Vector3(1, 0, 0) * _Speed * Time.deltaTime;
    }

    public float GetSpeed()
    {
        return _Speed;
    }

    void CalculateSpeed()
    {
        if(_CanDrive == false)
        {
            if (_Speed > 0)
            {
                _Speed -= _BrakeStrength * Time.deltaTime;
                if (_Speed < 0)
                {
                    _Speed = 0;
                }
            }
            return;
        }

        if(_Speed < _SpeedLimit)
        {
            _Speed += _Acceleration * Time.deltaTime;
            if (_Speed > _SpeedLimit)
            {
                _Speed = _SpeedLimit;
            }
        }
        else if( _Speed > _SpeedLimit) 
        {
            _Speed -= _BrakeStrength * Time.deltaTime;
            if (_Speed < _SpeedLimit)
            {
                _Speed = _SpeedLimit;
            }
        }
    }

    void CheckFront()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(_FrontVisual, 0, 0), Color.red);
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, _FrontVisual))
        {
            if(hit.collider != null) 
            {
                //if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Waypoint"))
                //{
                //    var waypoint = hit.collider.gameObject.GetComponent<Waypoint_Script>();
                //    if(waypoint != null) 
                //    {
                //        Debug.Log("Apply rule");
                //        waypoint.ApplyRule(this);
                //    }
                //
                //    //Debug.Log("Waypoint distance: " + hit.distance);
                //}
                //else
                //{
                //    _CanDrive = false;
                //    Debug.Log("HIT");
                //}
            }
        }
        else
        {
            _CanDrive = true;
        }
    }
}