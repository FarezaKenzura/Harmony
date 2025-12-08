using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class NoteEvent
{
    public double HitTime;
    public int Lane;
}

public class BeatmapBinary
{
    public string SongName;
    public float Offset;
    public List<NoteEvent> Notes = new List<NoteEvent>();
}

public class BeatmapRecorder : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    private BeatmapBinary _beatmap = new BeatmapBinary();
    private bool _recording = false;

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public void StartRecording(string songName)
    {
        _beatmap = new BeatmapBinary();
        _beatmap.SongName = songName;
        _beatmap.Offset = 0f;

        _recording = true;
        _music.Play();
    }

    public void RecordLane(int lane)
    {
        if (!_recording) return;

        _beatmap.Notes.Add(new NoteEvent()
        {
            HitTime = _music.time,
            Lane = lane
        });
    }

    public void StopRecording()
    {
        _recording = false;
        _music.Stop();
        SaveBinary();
    }

    void SaveBinary()
    {
        string folderPath = Path.Combine(Application.dataPath, "StreamingAssets", "Beatmaps");
        Directory.CreateDirectory(folderPath);

        string safeSongName = _beatmap.SongName.Replace(' ', '_').Replace('\\', '_').Replace('/', '_');
        string path = Path.Combine(folderPath, safeSongName + ".bin");

        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(_beatmap.SongName);
            writer.Write(_beatmap.Offset);
            writer.Write(_beatmap.Notes.Count);

            foreach (var n in _beatmap.Notes)
            {
                writer.Write(n.HitTime);
                writer.Write(n.Lane);
            }
        }

        Debug.Log("✅ Beatmap Developer Saved to StreamingAssets: " + path);
    }
}
