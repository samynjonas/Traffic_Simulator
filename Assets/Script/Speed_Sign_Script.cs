using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Sign_Script : MonoBehaviour
{
    [SerializeField]
    private float _SpeedLimit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other != null)
        {
            var car = other.gameObject.GetComponent<Vehicle_Script>();
            if (car != null)
            {
                car.SetSpeedLimit(_SpeedLimit);
                Debug.Log("Set speed limit to " + _SpeedLimit);
            }
        }
    }
}
