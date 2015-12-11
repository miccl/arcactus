using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    public Canvas hudCanvas;
    public Canvas highscoreCanvas;
    public RawImage crosshair;
    public Text eventText;
    public Text secondaryEventText;

    void Start () {
        hudCanvas.enabled = true;
        highscoreCanvas.enabled = false;
        crosshair.enabled = true;
        RemoveEventText();
        RemoveSecondaryEventText();
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowEventText(string text)
    {
        ShowAndRemoveEventText(eventText, text, -1);
    }

    public void ShowSecondaryEventText(string text)
    {
        ShowAndRemoveEventText(secondaryEventText, text, -1);
    }

    public void ShowEventText(string textString, float waitTime)
    {
        ShowAndRemoveEventText(eventText, textString, waitTime);
    }

    public void ShowSecondaryEventText(string textString, float waitTime)
    {
        ShowAndRemoveEventText(secondaryEventText, textString, waitTime);
    }

    void ShowAndRemoveEventText(Text text, string textString, float waitTime)
    {
        if(hudCanvas.enabled)
        {
            while(true)
            {
                if(!eventText.enabled)
                {
                    text.enabled = true;
                    text.text = textString;
                    if (waitTime != -1)
                    {
                        StartCoroutine(RemoveEventText(text, waitTime));
                    }
                    break;
                }
            }
        }
    }

    public void RemoveEventText()
    {
        StartCoroutine(RemoveEventText(eventText, 0));
    }

    public void RemoveSecondaryEventText()
    {
        StartCoroutine(RemoveEventText(secondaryEventText, 0));
    }

    IEnumerator RemoveEventText(Text text, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        text.text = "";
        text.enabled = false;
    }

    public void ShowHighscore(bool show)
    {
        highscoreCanvas.enabled = show;
    }


    internal void ShowHUD(bool show)
    {
        hudCanvas.enabled = show;
    }

    internal void ShowCrosshair(bool show)
    {
        hudCanvas.enabled = show;
    }


}
