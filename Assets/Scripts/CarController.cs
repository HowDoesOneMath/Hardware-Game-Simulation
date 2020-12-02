﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public CarManager manager;

    public float MAX_SPEED;
    public float ACCELERATION;
    public float MAX_TURN_SPEED;
    public float TORQUE;

    public float turnStrength { get; private set; } = 0;
    public float turnSharpness { get; private set; } = 0;
    public Rigidbody rb { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        Vector3 turning = Vector3.zero;

        if (Input.GetKey(manager.fKey))
        {
            movement += transform.forward;
        }
        if (Input.GetKey(manager.bKey))
        {
            movement -= transform.forward;
        }

        rb.AddForce(movement * ACCELERATION, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MAX_SPEED);

        turnStrength = rb.velocity.magnitude / MAX_SPEED * Vector3.Dot(rb.velocity.normalized, transform.forward);
        

        if (Input.GetKey(manager.lKey))
        {
            turning -= transform.up;
        }
        if (Input.GetKey(manager.rKey))
        {
            turning += transform.up;
        }

        rb.AddTorque(turning * TORQUE * turnStrength, ForceMode.Impulse);
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, MAX_TURN_SPEED * Mathf.Abs(turnStrength));

        turnSharpness = rb.angularVelocity.magnitude / MAX_TURN_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(manager.reset))
        {
            ResetCar();
        }
    }

    void ResetCar()
    {
        transform.position = manager.transform.position;
        transform.rotation = manager.transform.rotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}