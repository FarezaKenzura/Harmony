using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private GameObject _notePrefab;

    [SerializeField] private float[] _laneX;
    [SerializeField] private float _spawnY = 6f;
    [SerializeField] private float _hitY = -3.5f;
    [SerializeField] private float _fallSpeed = 5f;

    private BeatmapBinary _beatmap;
    private double _dspStart;

    private void Start()
    {
        string path = Application.persistentDataPath + "/beatmap.bin";
        _beatmap = SingletonHub.Instance.Get<BeatmapLoader>().LoadBinary(path);

        _dspStart = AudioSettings.dspTime + 0.1;
        _music.PlayScheduled(_dspStart);

        foreach (var e in _beatmap.Notes)
            StartCoroutine(Schedule(e));
    }

    IEnumerator Schedule(NoteEvent e)
    {
        double fallTime = (_spawnY - _hitY) / _fallSpeed;
        double spawnTime = e.HitTime + _beatmap.Offset - fallTime;

        double now = AudioSettings.dspTime - _dspStart;
        double delay = spawnTime - now;

        if (delay > 0)
            yield return new WaitForSeconds((float)delay);

        Spawn(e);
    }

    private void Spawn(NoteEvent e)
    {
        Vector2 pos = new Vector2(_laneX[e.Lane], _spawnY);
        GameObject obj = SingletonHub.Instance.Get<ObjectPool>().GetPooledObject(_notePrefab, pos, Quaternion.identity);

        Note n = obj.GetComponent<Note>();
        n.LaneIndex = e.Lane;
        n.Speed = _fallSpeed;
        n.HitY = _hitY;
        n.SpawnTime = AudioSettings.dspTime;

        SingletonHub.Instance.Get<NoteManager>().RegisterNote(n);
    }
}
