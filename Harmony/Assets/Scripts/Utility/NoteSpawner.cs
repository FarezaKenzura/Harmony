using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private string _songToPlay;
    [SerializeField] private AudioSource _music;
    [SerializeField] private GameObject _notePrefab;
    [SerializeField] private Transform _notePoints;

    [SerializeField] private float[] _laneX;
    [SerializeField] private float _spawnY = 6f;
    [SerializeField] private float _hitY = -3.5f;
    [SerializeField] private float _fallSpeed = 5f;

    private BeatmapBinary _beatmap;
    private double _dspStart;

    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Beatmaps", _songToPlay + ".bin");
        _beatmap = SingletonHub.Instance.Get<BeatmapLoader>().LoadBinary(path);

        _dspStart = AudioSettings.dspTime + 0.1;
        _music.PlayScheduled(_dspStart);

        foreach (var e in _beatmap.Notes)
            StartCoroutine(Schedule(e));
    }

    IEnumerator Schedule(NoteEvent e)
    {
        float fallTime = (_spawnY - _hitY) / _fallSpeed;
        double hitTime = _dspStart + e.HitTime;
        double spawnTime = hitTime - fallTime;

        while (AudioSettings.dspTime < spawnTime)
        {
            yield return null;
        }

        Spawn(e, hitTime);
    }

    private void Spawn(NoteEvent e, double hitTime)
    {
        Vector2 pos = new Vector2(_laneX[e.Lane], _spawnY);
        GameObject obj = SingletonHub.Instance.Get<ObjectPool>().GetPooledObject(_notePrefab, pos, Quaternion.identity, _notePoints);

        Note n = obj.GetComponent<Note>();
        n.LaneIndex = e.Lane;
        n.Speed = _fallSpeed;
        n.HitY = _hitY;
        n.HitTime = hitTime;

        SingletonHub.Instance.Get<NoteManager>().RegisterNote(n);
    }
}
