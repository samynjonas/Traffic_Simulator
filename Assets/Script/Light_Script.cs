using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Script : MonoBehaviour
{
    [SerializeField]
    private Material _LightOffMat;

    [SerializeField]
    private Material _LightOnMat;

    [SerializeField]
    private bool _LightState;

    private MeshRenderer _MeshRenderer;

    private void Start()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
    }

    public void LightToggle()
    {
        _LightState = !_LightState;
        LightSwitchMaterial();
    }

    public void SetLightState(bool state)
    {
        _LightState = state;
        LightSwitchMaterial();
    }


    void LightSwitchMaterial()
    {
        if(_LightState)
        {
            _MeshRenderer.material = _LightOnMat;
        }
        else
        {
            _MeshRenderer.material = _LightOffMat;
        }
    }


}
