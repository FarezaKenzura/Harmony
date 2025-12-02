using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private Dictionary<int, List<Note>> activeNotes = new Dictionary<int, List<Note>>();

    [SerializeField] private float hitY = -3.5f;
    [SerializeField] private float perfectThreshold = 0.1f;
    [SerializeField] private float goodThreshold = 0.5f;
    private void Awake()
    {
        SingletonHub.Instance.Register(this);
    }

    public void RegisterNote(Note n)
    {
        if (!activeNotes.ContainsKey(n.laneIndex))
        {
            activeNotes.Add(n.laneIndex, new List<Note>());
        }
        activeNotes[n.laneIndex].Add(n);
    }

    public void UnregisterNote(Note n)
    {
        if (activeNotes.ContainsKey(n.laneIndex))
        {
            activeNotes[n.laneIndex].Remove(n);
        }
    }

    public void TryHitNote(int laneIndex)
    {
        if (!activeNotes.ContainsKey(laneIndex) || activeNotes[laneIndex].Count == 0)
        {
            return;
        }

        Note closestNote = activeNotes[laneIndex][0];
        float distance = Mathf.Abs(closestNote.transform.position.y - hitY);
        ComboLevel level;

        if (distance <= perfectThreshold)
        {
            level = ComboLevel.Perfect;
        }
        else if (distance <= goodThreshold)
        {
            level = ComboLevel.Good;
        }
        else
        {
            return;
        }

        activeNotes[laneIndex].RemoveAt(0);
        SingletonHub.Instance.Get<ComboManager>().RegisterHit(level, closestNote.gameObject);
    }
}
