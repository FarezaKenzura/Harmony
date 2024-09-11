using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activater : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    private bool active = false;
    private GameObject note;

    void Update()
    {
        if(Input.GetKeyDown(key) && active)
            Destroy(note);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        active = true;
        if(col.gameObject.tag == "Note")
            note = col.gameObject;
    }

    private void OnTriggerExit2D(Collider2D col) {
        active = false;        
    }
}
