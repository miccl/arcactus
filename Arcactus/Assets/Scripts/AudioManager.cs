using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    private UIManager uiManager;

    private AudioSource song1;
    private AudioSource gameOverSound;


    public Text audioText;
    private bool isMuted;
    private const string AUDIO_MUTED = "AudioMuted";

    // Use this for initialization
    void Start () {
        AudioSource[] audios = GetComponents<AudioSource>();
        song1 = audios[0];
        gameOverSound = audios[1];
        isMuted = (PlayerPrefs.GetInt("AUDIO_MUTED", 0) != 0);

        DoAudio();
        DisplayAudio();
    }

    public void ChangeAudio()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("AUDIO_MUTED", (isMuted ? 1 : 0));
        DoAudio();
        DisplayAudio();
    }

    void DoAudio()
    {
        if(!isMuted)
        {
            song1.Play();
            song1.mute = false;
        }
        else
        {
            song1.mute = true;
        }
    }

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

    internal void PlayGameOver()
    {
        gameOverSound.Play();
        song1.Stop();
        song1.PlayDelayed(3.0f);
    }
}
