using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TrafficLight_Script : MonoBehaviour
{
    public enum TrafficLightStates
    {
        Red, 
        Orange, 
        Green
    }

    private TrafficLightStates _TrafficLightState;

    [SerializeField]
    private Light_Script _RedLight;

    [SerializeField]
    private Light_Script _OrangeLight;

    [SerializeField]
    private Light_Script _GreenLight;

    [SerializeField]
    private float _SwitchTime;

    private float _Counter;

    private Vehicle_Script _WaitingCar;

    private void Start()
    {
        _TrafficLightState = TrafficLightStates.Red;
        UpdateLights();
    }

    void UpdateLights()
    {
        _RedLight.SetLightState(false);
        _OrangeLight.SetLightState(false);
        _GreenLight.SetLightState(false);

        switch (_TrafficLightState) 
        { 
            case TrafficLightStates.Red:
                _RedLight.SetLightState(true);
            break;
            case TrafficLightStates.Orange:
                _OrangeLight.SetLightState(true);
            break;
            case TrafficLightStates.Green:
                _GreenLight.SetLightState(true);
            break;
        }
    }

    void Update()
    {
        _Counter += Time.deltaTime;
        if(_Counter > _SwitchTime)
        {
            //Reset counter
            _Counter = 0;

            //Go to next state
            int stateAsInt = (int)_TrafficLightState;

            stateAsInt++;

            stateAsInt %= (int)TrafficLightStates.Green + 1;
            _TrafficLightState = (TrafficLightStates)stateAsInt;

            UpdateLights();

            //Update the traffic light
            UpdateWaitingCarState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if(_WaitingCar == null)
            {
                Debug.Log("Car added");

                _WaitingCar = other.gameObject.GetComponent<Vehicle_Script>();
                UpdateWaitingCarState();
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other != null) 
        {
            Debug.Log("Car removed");
            if(other.gameObject == _WaitingCar.gameObject)
            {
                _WaitingCar = null;
            }
        }
    }


    void UpdateWaitingCarState()
    {
        if(_WaitingCar == null)
        {
            return;
        }

        if(_TrafficLightState == TrafficLightStates.Green)
        {
            _WaitingCar.SetCanDrive(true);
        }
        else
        {
            _WaitingCar.SetCanDrive(false);
        }
    }

}
