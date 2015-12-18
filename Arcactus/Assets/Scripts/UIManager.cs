using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Manages the UI elements of the screen.
/// </summary>
public class UIManager : MonoBehaviour {

	/// <summary>
	/// The hud canvas.
	/// </summary>
    public Canvas hudCanvas;
	/// <summary>
	/// The highscore canvas.
	/// </summary>
    public Canvas highscoreCanvas;
	/// <summary>
	/// The crosshair raw image.
	/// </summary>
    public RawImage crosshair;
	/// <summary>
	/// The event text.
	/// </summary>
    public Text eventText;
	/// <summary>
	/// The secondary event text.
	/// </summary>
    public Text secondaryEventText;

    void Start () {
        hudCanvas.enabled = true;
        highscoreCanvas.enabled = false;
        crosshair.enabled = true;
        RemoveEventText();
        RemoveSecondaryEventText();
	}

	/// <summary>
	/// Shows the event text.
	/// </summary>
	/// <param name="text">The text to show.</param>
    public void ShowEventText(string text)
    {
        ShowAndRemoveEventText(eventText, text, -1);
    }

	/// <summary>
	/// Shows the secondary event text.
	/// </summary>
	/// <param name="text">The text to show.</param>
    public void ShowSecondaryEventText(string text)
    {
        ShowAndRemoveEventText(secondaryEventText, text, -1);
    }

	/// <summary>
	/// Shows the event text for a specific duration.
	/// </summary>
	/// <param name="text">The text to show.</param>
	/// <param name="duration">The duration the text is shown.</param>
    public void ShowEventText(string text, float duration)
    {
        ShowAndRemoveEventText(eventText, text, duration);
    }
	/// <summary>
	/// Shows the secondary event text for a specific duration.
	/// </summary>
	/// <param name="text">The text to show.</param>
	/// <param name="duration">The duration the text is shown.</param>
	public void ShowSecondaryEventText(string text, float duration)
    {
        ShowAndRemoveEventText(secondaryEventText, text, duration);
    }

	/// <summary>
	/// Shows the and remove event text after a specific time.
	/// </summary>
	/// <param name="text">The text to show.</param>
	/// <param name="duration">The duration the text is shown.</param>
	/// <param name="waitTime">Wait time.</param>
    void ShowAndRemoveEventText(Text eventText, string text, float waitTime)
    {
        if(hudCanvas.enabled)
        {
            while(true)
            {
                if(!eventText.enabled)
                {
                    eventText.enabled = true;
                    eventText.text = text;
                    if (waitTime != -1)
                    {
                        StartCoroutine(RemoveEventText(eventText, waitTime));
                    }
                    break;
                }
            }
        }
    }

	/// <summary>
	/// Removes the event text immeditialy.
	/// </summary>
    public void RemoveEventText()
    {
        StartCoroutine(RemoveEventText(eventText, 0));
    }

	/// <summary>
	/// Removes the secondary event text immeditialy.
	/// </summary>
    public void RemoveSecondaryEventText()
    {
        StartCoroutine(RemoveEventText(secondaryEventText, 0));
    }
	/// <summary>
	/// Removes the event text after a specific time.
	/// </summary>
	/// <param name="eventText">The event text.</param>
	/// <param name="waitTime">Wait time.</param>
    IEnumerator RemoveEventText(Text eventText, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        eventText.text = "";
        eventText.enabled = false;
    }

	/// <summary>
	/// Shows the highscore.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show the highscore, otherwise hide the highscore.</param>
    public void ShowHighscore(bool show)
    {
        highscoreCanvas.enabled = show;
    }

	/// <summary>
	/// Shows the HUD.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show the HUD, otherwise hide the hud.</param>
    internal void ShowHUD(bool show)
    {
        hudCanvas.enabled = show;
    }

	/// <summary>
	/// Shows the crosshair.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show the crosshair, otherwise hide the crosshair.</param>
    internal void ShowCrosshair(bool show)
    {
        hudCanvas.enabled = show;
    }

}
