using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public AudioSource bumpEffect;
    public AudioSource engineSound;

    public CarManager manager;

    public Image leftPedalForward;
    public Image leftPedalBackward;
    public Image rightPedalForward;
    public Image rightPedalBackward;

    public float MAX_SPEED;
    public float ACCELERATION;
    public float MAX_TURN_SPEED;
    public float TORQUE;

    public float velocityRatio { get; private set; } = 0;
    public float turnSharpness { get; private set; } = 0;
    public Rigidbody rb { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        leftPedalForward.color = Color.yellow;
        leftPedalBackward.color = Color.yellow;
        rightPedalForward.color = Color.yellow;
        rightPedalBackward.color = Color.yellow;
    }

    private void FixedUpdate()
    {
        if (manager.paused)
            return;

        Vector3 movement = Vector3.zero;
        Vector3 turning = Vector3.zero;

        float accelerating = 0;
        float breaking = 0;

        if (Input.GetKey(manager.fKey))
        {
            accelerating = 1;
            movement += transform.forward;
        }
        if (Input.GetKey(manager.bKey))
        {
            breaking = 1;
            movement -= transform.forward;
        }

        rb.AddForce(movement * ACCELERATION, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MAX_SPEED);

        velocityRatio = rb.velocity.magnitude / MAX_SPEED * Vector3.Dot(rb.velocity.normalized, transform.forward);

        leftPedalForward.color = Color.HSVToRGB((2 - breaking) / 12f, 1, 0.7f);
        leftPedalBackward.color = Color.HSVToRGB((2 + breaking) / 12f, 1, 0.7f);
        rightPedalForward.color = Color.HSVToRGB((2 - accelerating) / 12f, 1, 0.7f);
        rightPedalBackward.color = Color.HSVToRGB((2 + accelerating) / 12f, 1, 0.7f);

        if (Input.GetKey(manager.lKey))
        {
            turning -= transform.up;
        }
        if (Input.GetKey(manager.rKey))
        {
            turning += transform.up;
        }

        rb.AddTorque(turning * TORQUE * velocityRatio, ForceMode.Impulse);
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, MAX_TURN_SPEED * Mathf.Abs(velocityRatio));

        turnSharpness = rb.angularVelocity.magnitude / MAX_TURN_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.paused)
            return;

        if (Input.GetKeyUp(manager.reset))
        {
            ResetCar();
        }

        engineSound.pitch = (1.0f + 2.0f * Mathf.Abs(velocityRatio));
    }

    void ResetCar()
    {
        transform.position = manager.transform.position;
        transform.rotation = manager.transform.rotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude < 5f)
            return;

        if(Mathf.Abs(Vector3.Dot(collision.impulse.normalized, transform.up)) < 0.5f)
        {
            bumpEffect.Play();
        }
    }
}
