using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class NoteEvent
{
    public double hitTime;
    public int lane;
}

public class BeatmapBinary
{
    public string songName;
    public float offset;
    public List<NoteEvent> notes = new List<NoteEvent>();
}

public class BeatmapRecorder : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private BeatmapBinary beatmap = new BeatmapBinary();
    private bool recording = false;

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public void StartRecording(string songName)
    {
        beatmap = new BeatmapBinary();
        beatmap.songName = songName;
        beatmap.offset = 0f;

        recording = true;
        music.Play();
    }

    public void RecordLane(int lane)
    {
        if (!recording) return;

        beatmap.notes.Add(new NoteEvent()
        {
            hitTime = music.time,
            lane = lane
        });
    }

    public void StopRecording()
    {
        recording = false;
        music.Stop();
        SaveBinary();
    }

    void SaveBinary()
    {
        string folderPath = Path.Combine(Application.dataPath, "StreamingAssets", "Beatmaps");
        Directory.CreateDirectory(folderPath);

        string safeSongName = beatmap.songName.Replace(' ', '_').Replace('\\', '_').Replace('/', '_');
        string path = Path.Combine(folderPath, safeSongName + ".bin");

        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(beatmap.songName);
            writer.Write(beatmap.offset);
            writer.Write(beatmap.notes.Count);

            foreach (var n in beatmap.notes)
            {
                writer.Write(n.hitTime);
                writer.Write(n.lane);
            }
        }

        Debug.Log("✅ Beatmap Developer Saved to StreamingAssets: " + path);
    }
}
