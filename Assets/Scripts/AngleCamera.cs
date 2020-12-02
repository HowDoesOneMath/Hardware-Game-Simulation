using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AngleCamera : MonoBehaviour
{
    Vector3 trueRot = Vector3.zero;
    float trueFOV;

    float initialFOV;
    public float FOVPercentIncrease;

    public float cameraOffset;
    public float cameraLerpStrength;

    public CarManager manager;
    public CarController cc;

    public Camera mainCam { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();

        initialFOV = mainCam.fieldOfView;
        trueFOV = initialFOV;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 intendedRot = Vector3.zero;
        if (Input.GetKey(manager.fTilt))
        {
            intendedRot += Vector3.right;
        }
        if (Input.GetKey(manager.bTilt))
        {
            intendedRot -= Vector3.right;
        }
        if (Input.GetKey(manager.rTilt))
        {
            intendedRot += Vector3.up;
        }
        if (Input.GetKey(manager.lTilt))
        {
            intendedRot -= Vector3.up;
        }

        intendedRot *= cameraOffset;

        if ((trueRot - intendedRot).magnitude < 0.1)
            trueRot = intendedRot;
        else
            trueRot = Vector3.Lerp(trueRot, intendedRot, cameraLerpStrength);

        transform.localRotation = Quaternion.Euler(trueRot);


        float intendedFOV = (Mathf.Max(cc.turnStrength, 0) * FOVPercentIncrease * 0.01f + 1) * initialFOV;

        if (Mathf.Abs(trueFOV - intendedFOV)  < 0.1)
            trueFOV = intendedFOV;
        else
            trueFOV = Mathf.Lerp(trueFOV, intendedFOV, cameraLerpStrength);

        mainCam.fieldOfView = trueFOV;
    }
}
