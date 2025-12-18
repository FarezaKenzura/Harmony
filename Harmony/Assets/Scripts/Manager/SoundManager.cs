using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SongEntry
{
    public string SongID;
    public string DisplayName;
    public AudioClip MusicClip;
}

public class SoundManager : MonoBehaviour
{
    [field: SerializeField] public SOSong _librarySong { get; private set; }

    [SerializeField] private AudioSource _musicSource;

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    private void SetupAndPlay(string id)
    {
        SongEntry entry = _librarySong.SongRthym.Find(s => s.SongID == id);

        if (entry != null && entry.MusicClip != null)
        {
            _musicSource.clip = entry.MusicClip;
            _musicSource.Play();

            Debug.Log($"SoundManager: Memutar lagu '{entry.DisplayName}'");
        }
        else
        {
            Debug.LogError($"SoundManager: Lagu dengan ID '{id}' tidak ditemukan atau AudioClip kosong di Library!");
        }
    }

    public void StopMusic() => _musicSource.Stop();
    public void PauseMusic() => _musicSource.Pause();
    public void ResumeMusic() => _musicSource.UnPause();

    public float GetCurrentTime() => _musicSource.time;
    public bool IsPlaying() => _musicSource.isPlaying;
}
