using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GamePadController : MonoBehaviour
{
    public GameObject controller;
    public Color controllerColor = Color.blue;
    public Color gamePadColor = Color.red, gamePadColor2 = Color.blue, gamePadColor3 = Color.green, gamePadColor4 = Color.yellow, gamePadColor5 = Color.red;
    public static int numberOfControlButtons = 0;
    public static bool isButtonPressed, isButtonReleased = false;
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
