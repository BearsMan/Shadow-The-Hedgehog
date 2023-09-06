using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GamePadController : MonoBehaviour
{
    public GameObject controller;
    public Color controllerColor = Color.blue;
    public Color gamepadColor = Color.red;
    public Color gamepadColor2 = Color.yellow;
    public Color gamepadColor3 = Color.green;
    public static int numberOfControlButtons = 0;
    public static bool isButtonPressed = false;
    public static bool isButtonReleased = false;
    public Rigidbody body;
    private bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
