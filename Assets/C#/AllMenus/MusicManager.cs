using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] musicSources;
    public Slider volumeSlider;
    public Button nextSongButton;

    private const string PlayerPrefsKey = "MusicVolume";
    private const string PlayerPrefsSongIndexKey = "CurrentSongIndex";

    public List<AudioClip> musicTracks;
    private int currentSongIndex = 0;

    void Start()
    {
        // Load the saved volume level from PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat(PlayerPrefsKey, 1f);
        SetVolume(savedVolume);

        // Set the volume slider value based on the loaded volume
        volumeSlider.value = savedVolume;

        // Load the current song index from PlayerPrefs
        currentSongIndex = PlayerPrefs.GetInt(PlayerPrefsSongIndexKey, 0);

        // Set up the button click event for changing the song
        if (nextSongButton != null)
        {
            nextSongButton.onClick.AddListener(PlayNextSong);
        }

        // Initialize all music sources
        InitializeMusicSources();

        // Play the initial music track
        PlayCurrentSong();

        Debug.Log("Music Manager initialized.");
    }

    void Update()
    {
        // Update the volume in real-time while dragging the slider
        SetVolume(volumeSlider.value);
    }

    void InitializeMusicSources()
    {
        foreach (var source in musicSources)
        {
            source.loop = true;
            source.Stop();
        }
    }

    public void ToggleMusic()
    {
        foreach (var source in musicSources)
        {
            bool isMuted = !source.isPlaying;

            // Toggle the music playback
            if (isMuted)
            {
                source.Play();
            }
            else
            {
                source.Pause();
            }

            // Save the current volume state to PlayerPrefs
            PlayerPrefs.SetFloat(PlayerPrefsKey, isMuted ? 0f : source.volume);
            PlayerPrefs.Save();

            Debug.Log("Music toggled. Is Muted: " + isMuted);
        }
    }

    public void SetVolume(float volume)
    {
        foreach (var source in musicSources)
        {
            source.volume = volume;
        }

        // Save the current volume state to PlayerPrefs
        PlayerPrefs.SetFloat(PlayerPrefsKey, volume);
        PlayerPrefs.Save();

        Debug.Log("Volume set to: " + volume);
    }

    public void PlayNextSong()
    {
        currentSongIndex = (currentSongIndex + 1) % musicTracks.Count;

        // Save the current song index to PlayerPrefs
        PlayerPrefs.SetInt(PlayerPrefsSongIndexKey, currentSongIndex);
        PlayerPrefs.Save();

        PlayCurrentSong();
    }

    private void PlayCurrentSong()
    {
        if (musicSources.Length > 0 && musicTracks.Count > 0)
        {
            // Stop all music sources
            foreach (var source in musicSources)
            {
                source.Stop();
            }

            // Play the current song
            musicSources[currentSongIndex].clip = musicTracks[currentSongIndex];
            musicSources[currentSongIndex].Play();

            Debug.Log("Playing song: " + musicTracks[currentSongIndex].name);
        }
        else
        {
            Debug.LogWarning("Music sources or music tracks not set up.");
        }
    }
}
