using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour {

    public AudioClip gameOverSound;
    public Text audioText;

    private bool isMuted;
    private const string AUDIO_MUTED = "AudioMuted";

    private UIManager uiManager;
    private AudioSource backgroundAudioSource;
    private AudioSource soundAudioSource;

    void Awake()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        backgroundAudioSource = audios[0];
        soundAudioSource = audios[1];
    }

    // Use this for initialization
    void Start () {
        isMuted = (PlayerPrefs.GetInt("AUDIO_MUTED", 0) != 0);

        PlayBackgroundSound();
        DisplayAudio();
    }
    
    /// <summary>
    /// Mutes or unmutes the background sound based if its was muted before.
    /// </summary>
    public void ChangeBackgroundSound()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("AUDIO_MUTED", (isMuted ? 1 : 0));
        PlayBackgroundSound();
        DisplayAudio();
    }

    /// <summary>
    /// Plays the background sound based.
    /// </summary>
    void PlayBackgroundSound()
    {
        PlayBackgroundSound(0);
    }

    /// <summary>
    /// Plays the background sound after a the given delay.
    /// </summary>
    /// <param name="delay">wait time to play</param>
    void PlayBackgroundSound(float delay)
    {
        backgroundAudioSource.mute = isMuted;
        if(!isMuted)
            backgroundAudioSource.PlayDelayed(delay);
    }

    /// <summary>
    /// Plays a sound clip.
    /// </summary>
    /// <param name="clip">The clip to play</param>
    internal void PlaySound(AudioClip clip)
    {
        soundAudioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Displays in the menu if the background sound is muted or not.
    /// </summary>
    void DisplayAudio()
    {
        audioText.text = "Music: ";
        if(!isMuted)
        {
            audioText.text += "On";
        } else
        {
            audioText.text += "Off";
        }
    }

    /// <summary>
    /// Plays the game over sound.
    /// Stops the backgrounnd sound for the time of the sound.
    /// </summary>
    internal void PlayGameOverSound()
    {
        backgroundAudioSource.Stop();
        soundAudioSource.PlayOneShot(gameOverSound);
        PlayBackgroundSound(gameOverSound.length);
    }
}
