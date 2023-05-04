using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    // An array of strings to display
    public string[] texts = { "Welcome RoboTato...\n\nI am Fry'n'Stein... \nWe are in a bit of a crisis...\n\nAn alien invasion recently discovered the power of potatos and has launched an invasion on our world!!!\n\n\t\t\t -- Press Enter/Return to continue.", "RoboTato!!! \n\nWe need you to fight off this invasion\nTo Protect our Potato People! \n\n>>>", "The controls are WASD...\n\nW - moves up\nS - moves down\nA - moves left\nD - moves right\nSpacebar - shoots \n\n >>>", "Power to the potatoes!!!\n\n\n Hit 'P' to start" };


    // The index of the current text in the array
    private int currentTextIndex = 0;

    // The text component of the game object to display the text on

     public TextMeshProUGUI textComponent;

    void Start()
    {
        // Get a reference to the text component on the game object
        textComponent = GameObject.Find("DisplayText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // If the player presses the spacebar, display the next text
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Increase the index of the current text by 1
            currentTextIndex++;

            // If the index is greater than the length of the array, set it back to 0
            if (currentTextIndex >= texts.Length)
            {
                currentTextIndex = 3;
            }


            // Display the next text on the game object
            textComponent.text = texts[currentTextIndex+1];
        }
    }
}