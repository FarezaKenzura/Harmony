using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private Dictionary<int, List<Note>> _activeNotes = new Dictionary<int, List<Note>>();

    [SerializeField] private float _hitY = -3.5f;
    [SerializeField] private float _perfectThreshold = 0.1f;
    [SerializeField] private float _goodThreshold = 0.5f;

    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public void RegisterNote(Note n)
    {
        if (!_activeNotes.ContainsKey(n.LaneIndex))
        {
            _activeNotes.Add(n.LaneIndex, new List<Note>());
        }
        _activeNotes[n.LaneIndex].Add(n);
    }

    public void UnregisterNote(Note n)
    {
        if (_activeNotes.ContainsKey(n.LaneIndex))
        {
            _activeNotes[n.LaneIndex].Remove(n);
        }
    }

    public void HitNote(int laneIndex)
    {
        if (!_activeNotes.ContainsKey(laneIndex) || _activeNotes[laneIndex].Count == 0)
        {
            return;
        }

        Note closestNote = _activeNotes[laneIndex][0];
        float distanceToHitPosition = GetDistanceToHitPosition(closestNote);

        ComboLevel hitLevel = DetermineCombo(distanceToHitPosition);

        if (hitLevel == ComboLevel.Miss)
        {
            return;
        }

        _activeNotes[laneIndex].RemoveAt(0);

        HandleHitRegistration(hitLevel, closestNote.gameObject, laneIndex);
    }

    private void HandleHitRegistration(ComboLevel level, GameObject note, int laneIndex)
    {
        SingletonHub.Instance.Get<ComboManager>().ProcessHit(level, laneIndex);
        SingletonHub.Instance.Get<ObjectPool>().ReturnToPool(note);
    }

    private float GetDistanceToHitPosition(Note note)
    {
        return Mathf.Abs(note.transform.position.y - _hitY);
    }

    private ComboLevel DetermineCombo(float distance)
    {
        if (distance <= _perfectThreshold)
        {
            return ComboLevel.Perfect;
        }
        else if (distance <= _goodThreshold)
        {
            return ComboLevel.Good;
        }
        else
        {
            return ComboLevel.Miss;
        }
    }
}
