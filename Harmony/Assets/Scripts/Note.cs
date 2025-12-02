using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int laneIndex { get; set; }
    public float hitY { get; set; }
    public float speed { get; set; }
    public double spawnTime { get; set; }

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < hitY - 1f)
        {
            SingletonHub.Instance.Get<ComboManager>().RegisterHit(ComboLevel.Miss, gameObject);
            SingletonHub.Instance.Get<NoteManager>().UnregisterNote(this);
            Destroy(gameObject);
        }
    }
}
