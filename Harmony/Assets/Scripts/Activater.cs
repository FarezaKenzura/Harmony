using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Activater : MonoBehaviour, IPointerDownHandler
{
    public int laneIndex;

    public void OnPointerDown(PointerEventData eventData)
    {
        SingletonHub.Instance.Get<BeatmapManager>().RecordLane(laneIndex);
    }
}
