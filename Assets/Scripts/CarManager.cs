using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [Header("Car Keys")]
    public string FORWARD_KEY;
    public string BACKWARD_KEY;
    public string LEFT_KEY;
    public string RIGHT_KEY;
    public string RESET_KEY;

    public KeyCode fKey { get; private set; }
    public KeyCode bKey { get; private set; }
    public KeyCode rKey { get; private set; }
    public KeyCode lKey { get; private set; }
    public KeyCode reset { get; private set; }

    [Header("Camera Keys")]
    public string FRONT_TILT_KEY;
    public string BACK_TILT_KEY;
    public string LEFT_TILT_KEY;
    public string RIGHT_TILT_KEY;

    public KeyCode fTilt { get; private set; }
    public KeyCode bTilt { get; private set; }
    public KeyCode rTilt { get; private set; }
    public KeyCode lTilt { get; private set; }

    void Awake()
    {
        fKey = ParseKey(FORWARD_KEY);
        bKey = ParseKey(BACKWARD_KEY);
        lKey = ParseKey(LEFT_KEY);
        rKey = ParseKey(RIGHT_KEY);
        reset = ParseKey(RESET_KEY);

        fTilt = ParseKey(FRONT_TILT_KEY);
        bTilt = ParseKey(BACK_TILT_KEY);
        lTilt = ParseKey(LEFT_TILT_KEY);
        rTilt = ParseKey(RIGHT_TILT_KEY);
    }

    void Update()
    {
        
    }

    public KeyCode ParseKey(string key)
    {
        KeyCode toReturn = KeyCode.None;

        toReturn = (KeyCode)System.Enum.Parse(typeof(KeyCode), key, true);

        return toReturn;
    }
}
