using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int LaneIndex { get; set; }
    public float HitY { get; set; }
    public float Speed { get; set; }
    public double SpawnTime { get; set; }

    private void Update()
    {
        transform.position += Vector3.down * Speed * Time.deltaTime;

        if (transform.position.y < HitY - 1f)
        {
            SingletonHub.Instance.Get<ComboManager>().ProcessHit(ComboLevel.Miss);
            SingletonHub.Instance.Get<NoteManager>().UnregisterNote(this);
            SingletonHub.Instance.Get<ObjectPool>().ReturnToPool(gameObject);
        }
    }
}
