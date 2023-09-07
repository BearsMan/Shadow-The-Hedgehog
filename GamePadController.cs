using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GamePadController : MonoBehaviour
{
    public GameObject controller;
    public Color gamePadColor = Color.red, gamePadColor2 = Color.blue, gamePadColor3 = Color.green, gamePadColor4 = Color.yellow, gamePadColor5 = Color.red;
    public int numberOfControlButtons = 0;
    public bool isButtonPressed, isButtonReleased = false;
    public bool leftJoyPadIsUsed, rightJoyPadIsUsed = false;
    public bool startButtonIsPressed, startButtonReleased = false;
    private bool isGrounded = false;
    public Image GamePad;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Setup()
    {
        gamePadColor = new Color();
        if (gamePadColor == Color.blue)
        {
            controller.SetActive(false);
        }
        if (gamePadColor == Color.red)
        {
            controller.SetActive(false);
        }
        if (gamePadColor == Color.green)
        {
            controller.SetActive(false);
        }
        if (gamePadColor == Color.yellow)
        {
            controller.SetActive(false);
        }
        for (int i = 0; i < numberOfControlButtons; i++)
        {
            Gizmos.color = gamePadColor;
        }
    }
    public void JoyPadControlsSetup()
    {
        if (leftJoyPadIsUsed)
        {
            if (gamePadColor == Color.black)
            {
                isButtonPressed = false;
                controller.SetActive(false);
            }
            if (rightJoyPadIsUsed)
            {
                if (gamePadColor == Color.black)
                {
                    isButtonPressed = false;
                    controller.SetActive(false);
                }
                while (isButtonPressed)
                {
                    return;
                }
            }
        }
    }
    public void StartButtonUsage()
    {

    }
}
