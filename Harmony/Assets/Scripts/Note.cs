using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float spawnY;
    public float hitY;
    public double spawnTime;
    public double hitTime;

    void Update()
    {
        // Hitung progress dari spawn -> hit
        double t = (AudioSettings.dspTime - NoteSpawner.songStartDspTime - spawnTime) / (hitTime - spawnTime);
        t = Mathf.Clamp01((float)t);

        // Interpolasi posisi Y
        float y = Mathf.Lerp(spawnY, hitY, (float)t);
        transform.position = new Vector2(transform.position.x, y);

        // Opsional: hapus note setelah melewati hit line
        if (t >= 1f)
            Destroy(gameObject);
    }
}
