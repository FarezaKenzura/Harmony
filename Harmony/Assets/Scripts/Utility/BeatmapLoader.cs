using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeatmapLoader : MonoBehaviour
{
    public BeatmapBinary LoadBinary(string path)
    {
        BeatmapBinary beatmap = new BeatmapBinary();

        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            beatmap.songName = reader.ReadString();
            beatmap.offset = reader.ReadSingle();

            int count = reader.ReadInt32();
            beatmap.notes = new List<NoteEvent>(count);

            for (int i = 0; i < count; i++)
            {
                NoteEvent e = new NoteEvent();
                e.hitTime = reader.ReadDouble();
                e.lane = reader.ReadInt32();
                beatmap.notes.Add(e);
            }
        }

        return beatmap;
    }
}
