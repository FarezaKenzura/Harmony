using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeatmapLoader : MonoBehaviour
{
    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public BeatmapBinary LoadBinary(string path)
    {
        BeatmapBinary beatmap = new BeatmapBinary();

        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            beatmap.SongName = reader.ReadString();
            beatmap.Offset = reader.ReadSingle();

            int count = reader.ReadInt32();
            beatmap.Notes = new List<NoteEvent>(count);

            for (int i = 0; i < count; i++)
            {
                NoteEvent e = new NoteEvent();
                e.HitTime = reader.ReadDouble();
                e.Lane = reader.ReadInt32();
                beatmap.Notes.Add(e);
            }
        }

        return beatmap;
    }
}
