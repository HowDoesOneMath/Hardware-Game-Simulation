using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBodyMotion : MonoBehaviour
{
    public CarController cc;
    public float driftAmount;
    public float driftIntensity;

    Vector3 trueRot = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 carRot = cc.rb.angularVelocity / cc.MAX_TURN_SPEED * driftAmount;

        if ((trueRot - carRot).magnitude < 0.1)
            trueRot = carRot;
        else
            trueRot = Vector3.Lerp(trueRot, carRot, driftIntensity);

        transform.localRotation = Quaternion.Euler(trueRot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
