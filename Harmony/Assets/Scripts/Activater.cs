using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Activater : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int laneIndex;

    public void OnPointerDown(PointerEventData eventData)
    {
        SingletonHub.Instance.Get<BeatmapRecorder>().RecordLane(laneIndex);
    }
}
