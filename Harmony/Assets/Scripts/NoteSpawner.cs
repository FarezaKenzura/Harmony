using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public AudioSource music;
    public GameObject notePrefab;

    public float[] laneX = { -4f, -2f, 0f, 2f, 4f };
    public float spawnY = 6f;
    public float hitY = -3.5f;
    public float speed = 5f; // untuk menghitung fallTime

    private Beatmap beatmap;
    private int nextIndex = 0;
    public static double songStartDspTime;

    void Start()
    {
        // Baca beatmap JSON
        string path = Application.persistentDataPath + "/beatmap.json";
        if (!File.Exists(path))
        {
            Debug.LogError("Beatmap JSON tidak ditemukan di " + path);
            return;
        }

        string json = File.ReadAllText(path);
        beatmap = JsonUtility.FromJson<Beatmap>(json);

        songStartDspTime = AudioSettings.dspTime;
        music.Play();
    }

    void Update()
    {
        if (beatmap == null || nextIndex >= beatmap.notes.Count) return;

        double songTime = AudioSettings.dspTime - songStartDspTime;
        var note = beatmap.notes[nextIndex];

        // Waktu yang dibutuhkan note untuk jatuh dari spawnY ke hitY
        double fallTime = (spawnY - hitY) / speed;
        // Spawn lebih awal supaya note sampai hit line tepat waktu
        if (songTime >= note.time - fallTime)
        {
            Debug.Log("songTime = " + songTime + " | SpawnTime = " + (note.time - fallTime));
            GameObject obj = Instantiate(notePrefab, new Vector2(laneX[note.lane], spawnY), Quaternion.identity);
            Note n = obj.GetComponent<Note>();
            n.hitY = hitY;
            n.spawnTime = songTime;
            n.hitTime = note.time;
            n.spawnY = spawnY;
            nextIndex++;
        }
    }
}
