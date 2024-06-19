using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    [SerializeField] private float globalShakeForce = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
        instance = this;
        }   
    }

    public void Camera_Shake(CinemachineImpulseSource impulseSource) 
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }
    public void Camera_Shake2(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce*4);
    }

}
