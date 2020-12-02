using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
{
    [Header("Car Reference")]

    public CarController car;

    [Header("Photoresistors")]
    public Image[] PHR;
    public Text pauseText;

    public bool paused { get; private set; } = false;

    float initialTimeScale;

    [Header("Car Keys")]
    public string FORWARD_KEY;
    public string BACKWARD_KEY;
    public string LEFT_KEY;
    public string RIGHT_KEY;
    public string RESET_KEY;
    public string PAUSE_KEY;

    public KeyCode fKey { get; private set; }
    public KeyCode bKey { get; private set; }
    public KeyCode rKey { get; private set; }
    public KeyCode lKey { get; private set; }
    public KeyCode reset { get; private set; }
    public KeyCode pause { get; private set; }

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

        pause = ParseKey(PAUSE_KEY);

        SetPhotoresistors(1.0f / 6.0f);

        initialTimeScale = Time.timeScale;
        pauseText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(pause))
        {
            if (paused)
            {
                paused = false;
                SetPhotoresistors(1.0f / 6.0f);

                Time.timeScale = initialTimeScale;

                car.engineSound.Play();
            }
            else
            {
                paused = true;
                SetPhotoresistors(0f);

                Time.timeScale = 0;

                car.engineSound.Stop();
            }

            pauseText.gameObject.SetActive(paused);
        }
    }

    public KeyCode ParseKey(string key)
    {
        KeyCode toReturn = KeyCode.None;

        toReturn = (KeyCode)System.Enum.Parse(typeof(KeyCode), key, true);

        return toReturn;
    }

    public void SetPhotoresistors(float hue)
    {
        for (int i = 0; i < PHR.Length; ++i)
        {
            PHR[i].color = Color.HSVToRGB(hue, 1.0f, 0.7f);
        }
    }
}
