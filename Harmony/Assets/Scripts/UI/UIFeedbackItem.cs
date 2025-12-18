using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFeedbackItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Setup(string message, Color color, float duration)
    {
        _text.text = message;
        _text.color = color;
        StartCoroutine(LifeTime(duration));
    }

    private IEnumerator LifeTime(float duration)
    {
        float elapsed = 0;
        Vector3 startPos = transform.position;
        while (elapsed < duration)
        {
            transform.position = startPos + new Vector3(0, elapsed * 2f, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
