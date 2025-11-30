using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private GameObject notePrefab;

    [SerializeField] private float[] laneX;
    [SerializeField] private float spawnY = 6f;
    [SerializeField] private float hitY = -3.5f;
    [SerializeField] private float fallSpeed = 5f;

    private BeatmapBinary beatmap;
    private double dspStart;

    private void Start()
    {
        string path = Application.persistentDataPath + "/beatmap.bin";
        beatmap = new BeatmapLoader().LoadBinary(path);

        dspStart = AudioSettings.dspTime + 0.1;
        music.PlayScheduled(dspStart);

        foreach (var e in beatmap.notes)
            StartCoroutine(Schedule(e));
    }

    IEnumerator Schedule(NoteEvent e)
    {
        double fallTime = (spawnY - hitY) / fallSpeed;
        double spawnTime = e.hitTime + beatmap.offset - fallTime;

        double now = AudioSettings.dspTime - dspStart;
        double delay = spawnTime - now;

        if (delay > 0)
            yield return new WaitForSeconds((float)delay);

        Spawn(e);
    }

    private void Spawn(NoteEvent e)
    {
        Vector2 pos = new Vector2(laneX[e.lane], spawnY);
        GameObject obj = Instantiate(notePrefab, pos, Quaternion.identity);

        Note n = obj.GetComponent<Note>();
        n.speed = fallSpeed;
        n.hitY = hitY;
    }
}
