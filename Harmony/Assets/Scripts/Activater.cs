using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Activater : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int _laneIndex;

    public void OnPointerDown(PointerEventData eventData)
    {
        SingletonHub.Instance.Get<NoteManager>().HitNote(_laneIndex);
    }
}
