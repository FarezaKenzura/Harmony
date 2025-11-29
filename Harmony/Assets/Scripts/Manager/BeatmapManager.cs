using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public double time;
    public int lane;
}

[System.Serializable]
public class Beatmap
{
    public List<NoteData> notes = new List<NoteData>();
}

public class BeatmapManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private Beatmap _beatmap = new Beatmap();
    private bool recording = false;

    private void Start()
    {
        SingletonHub.Instance.Register(this);
    }

    public void StartRecording()
    {
        _beatmap.notes.Clear();
        recording = true;
        _musicSource.Play();
    }

    public void StopRecording()
    {
        recording = false;
        _musicSource.Stop();
        SaveBeatmap();
    }

    public void RecordLane(int lane)
    {
        if (!recording) return;

        _beatmap.notes.Add(new NoteData()
        {
            time = _musicSource.time,
            lane = lane
        });

        Debug.Log($"Recorded lane {lane} at {_musicSource.time}");
    }

    void SaveBeatmap()
    {
        string json = JsonUtility.ToJson(_beatmap, true);
        File.WriteAllText(Application.persistentDataPath + "/beatmap.json", json);
        Debug.Log("Saved to " + Application.persistentDataPath + "/beatmap.json");
    }
}
