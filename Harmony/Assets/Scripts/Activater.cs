using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activater : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    private SpriteRenderer sr;
    private GameObject note;
    private bool active = false;
    private Color old;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        old = sr.color;
    }

    void Update() {

        if(Input.GetKeyDown(key)) {
            StartCoroutine(Pressed());
        }

        if(Input.GetKeyDown(key) && active) {
            Destroy(note);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        active = true;
        if(col.gameObject.tag == "Note")
            note = col.gameObject;
    }

    private void OnTriggerExit2D(Collider2D col) {
        active = false;        
    }

    private IEnumerator Pressed() {
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}
