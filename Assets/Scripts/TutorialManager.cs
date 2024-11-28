using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // Reference to the TextMeshProUGUI component
    public string[] tutorialMessages; // Array of tutorial messages
    public GameObject tutorialPanel; // Reference to the tutorial panel
    public float typingSpeed = 0.05f; // Speed of the typewriter effect
    private int currentMessageIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        // Pause the game logic
        Time.timeScale = 0f;

        // Show the tutorial panel
        tutorialPanel.SetActive(true);

        // Display the first tutorial message
        DisplayMessage();
    }

    private void Update()
    {
        // Check for player click to advance the tutorial
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            AdvanceTutorial();
        }
    }

    private void DisplayMessage()
    {
        if (currentMessageIndex < tutorialMessages.Length)
        {
            StartCoroutine(TypeText(tutorialMessages[currentMessageIndex]));
        }
    }

    private IEnumerator TypeText(string message)
    {
        isTyping = true;
        tutorialText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            tutorialText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        isTyping = false;
    }

    private void AdvanceTutorial()
    {
        currentMessageIndex++;
        if (currentMessageIndex < tutorialMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            // End the tutorial
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        // Hide the tutorial panel
        tutorialPanel.SetActive(false);

        // Resume the game logic
        Time.timeScale = 1f;
    }
}