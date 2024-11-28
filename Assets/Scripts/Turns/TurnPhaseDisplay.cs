using UnityEngine;
using TMPro;
using System.Collections;

public class TurnPhaseDisplay : MonoBehaviour
{
    public RectTransform rectTransform;
    public TextMeshProUGUI displayText;
    public float typingSpeed = 0.3f; // Time between each character reveal
    public float displayDuration = 3.0f; // Time the text stays visible after being fully written

    public void ShowText(string text)
    {
        StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        displayText.text = ""; // Clear the text initially
        foreach (char letter in text.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Clear the text
        displayText.text = "";
    }

}