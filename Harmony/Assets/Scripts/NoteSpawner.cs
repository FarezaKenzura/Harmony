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
    public float fallSpeed = 5f;

    private Beatmap beatmap;
    public static double songStartDsp;

    void Start()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/beatmap.json");
        beatmap = JsonUtility.FromJson<Beatmap>(json);

        songStartDsp = AudioSettings.dspTime;
        music.Play();

        foreach (var note in beatmap.notes)
        {
            StartCoroutine(ScheduleNote(note));
        }
    }

    IEnumerator ScheduleNote(NoteData note)
    {
        float distance = spawnY - hitY;
        float fallTime = distance / fallSpeed;

        double spawnTime = note.time - fallTime;
        double now = AudioSettings.dspTime - songStartDsp;
        double delay = spawnTime - now;

        if (delay < 0) delay = 0;

        yield return new WaitForSeconds((float)delay);

        GameObject obj = Instantiate(notePrefab, new Vector2(laneX[note.lane], spawnY), Quaternion.identity);

        Note n = obj.GetComponent<Note>();
        n.hitY = hitY;
        n.speed = fallSpeed;
    }
}
