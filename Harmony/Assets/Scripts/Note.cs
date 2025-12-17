using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int LaneIndex { get; set; }
    public float HitY { get; set; }
    public float Speed { get; set; }
    public double HitTime { get; set; }

    private void Update()
    {
        float timeRemaining = (float)(HitTime - AudioSettings.dspTime);
        float currentY = HitY + (timeRemaining * Speed);

        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);

        if (transform.position.y < HitY - 1f)
        {
            SingletonHub.Instance.Get<ComboManager>().ProcessHit(ComboLevel.Miss);
            SingletonHub.Instance.Get<NoteManager>().UnregisterNote(this);
            SingletonHub.Instance.Get<ObjectPool>().ReturnToPool(gameObject);
        }
    }
}
