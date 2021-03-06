﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

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
    /// The menu canvas.
    /// </summary>
    public Canvas menuCanvas;

	/// <summary>
	/// The game status text.
	/// </summary>
	public Text statusText;

    public Button continueButton;
    public Button newGameButton;

	/// <summary>
	/// Settings for the Item Event Text
	/// </summary>
	[Header("Item Activated Event Text")]
	public Font font;
	public int fontSize;
	public int eventTextWidth;
	public int eventTextHeight;
	public TextAnchor alignment;

    void Start () {
        
        HUDEnabled(false);
        HighscoreEnabled(false);
        MenuEnabled(true);
        statusText.enabled = false;
        ContinueButtonEnabled(false);
    }


    /// <summary>
    /// Dynamically creates Event Text objects to show Item Activation info.
    /// Object will be destroyed after a given duration.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="duration">Duration.</param>
    public void ShowItemActivatedEventText(string text, float duration)
    {
        GameObject eventTextObject = new GameObject ("Item Activated Text");
		eventTextObject.transform.SetParent (hudCanvas.transform, false);
		Text eventT = eventTextObject.AddComponent<Text> ();
		eventTextObject.AddComponent<Outline> ();
		RectTransform rect = eventTextObject.GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (eventTextWidth, eventTextHeight);
		eventT.alignment = TextAnchor.MiddleCenter;
		eventT.font = font;
		eventT.fontSize = fontSize;
		eventT.text = text;
        rect.localScale = new Vector3(0.5f, 0.5f, 1.0f);
		eventT.CrossFadeAlpha(0.0f, duration, true);
		StartCoroutine ("BubbleUp", rect);
		Destroy (eventTextObject, duration);
	}

	/// <summary>
	/// Shows a status info.
	/// </summary>
	/// <param name="text">Text.</param>
	public void ShowStatusText(String text)
	{
		statusText.text = text;
		statusText.enabled = true;
	}

	/// <summary>
	/// Shows a status info for a given duration.
	/// </summary>
	/// <param name="text">Text.</param>
	/// <param name="duration">Duration.</param>
	public void ShowStatusText(String text, float duration)
	{
		statusText.text = text;
		statusText.enabled = true;
		StartCoroutine ("HideStatusText", duration);
	}

	/// <summary>
	/// Hides a status info after a given duration.
	/// Info starts fading away after half duration.
	/// </summary>
	/// <returns>The status text.</returns>
	/// <param name="duration">Duration.</param>
	IEnumerator HideStatusText(float duration)
	{
		yield return new WaitForSeconds (duration / 2);
		statusText.CrossFadeAlpha (0.0f, duration / 2, true);

		yield return new WaitForSeconds (duration / 2);
		statusText.enabled = false;
		//Restore alpha
		statusText.color = Color.white;
	}

	/// <summary>
	/// Hides a info text.
	/// </summary>
	public void HideStatusText()
	{
		statusText.enabled = false;
	}

	/// <summary>
	/// Makes RectTransforms float upwards
	/// </summary>
	/// <param name="rect">Rect.</param>
	private IEnumerator BubbleUp (RectTransform rect)
	{
		while (true) {
			yield return new WaitForFixedUpdate();
			if(!rect.Equals(null)) {
				rect.Translate (0, .001f, 0);
			} else break;
			yield return null;
		}
	}

    /// <summary>
    /// Enables or disables the crosshair based on the given parameter.
    /// </summary>
    /// <param name="show">If set to <c>true</c> show the highscore, otherwise hide the highscore.</param>
    public void HighscoreEnabled(bool show)
    {
        highscoreCanvas.enabled = show;
    }

	/// <summary>
	/// Shows the HUD.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show the HUD, otherwise hide the hud.</param>
    internal void HUDEnabled(bool show)
    {
        hudCanvas.enabled = show;
        CrosshairEnabled(show);
    }

    /// <summary>
    /// Shows or hides the crosshair based on the given parameter.
    /// </summary>
    /// <param name="show">If set to <c>true</c> shows the crosshair, otherwise hides the crosshair.</param>
    internal void CrosshairEnabled(bool show)
    {
        //crosshair.SetActive(show);
    }

    /// <summary>
    /// Shows or hides the menu based on the given parameter.
    /// </summary>
    /// <param name="show">If set to <c>true</c> shows the menu, otherwise hides the menu.</param>
    internal void MenuEnabled(bool show)
    {
        menuCanvas.gameObject.SetActive(show);
    }

    /// <summary>
    /// Initizialzizes the game view.
    /// </summary>
    internal void InitializeGameView()
    {
        MenuEnabled(false);
        HUDEnabled(true);
        HighscoreEnabled(false);
        HideStatusText();
    }

    /// <summary>
    /// Shows the menu.
    /// </summary>
    internal void ShowMenu()
    {
        MenuEnabled(true);
        //HUDEnabled(false);
        CrosshairEnabled(false);
        HighscoreEnabled(false);
    }

    /// <summary>
    /// Shows the highscore.
    /// </summary>
    public void ShowHighscore()
    {
        MenuEnabled(false);
        //HUDEnabled(false);
        HighscoreEnabled(true);
    }

    /// <summary>
    /// Wether the continue button is enabled or not.
    /// </summary>
    /// <param name="enable">If set to <c>true</c> the continue botton is enabled, otherwise not.</param>
    internal void ContinueButtonEnabled(bool enable)
    {
        continueButton.interactable = enable;
    }

}
