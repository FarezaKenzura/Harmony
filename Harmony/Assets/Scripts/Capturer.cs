using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Capturer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int _laneIndex;

    public void OnPointerDown(PointerEventData eventData)
    {
        SingletonHub.Instance.Get<BeatmapRecorder>().RecordLane(_laneIndex);
    }
}
