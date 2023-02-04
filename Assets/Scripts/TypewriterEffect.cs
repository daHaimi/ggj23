using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typeWriterSpeed = 30;
    public Coroutine Run(string textInput, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textInput, textLabel));
    }

    private IEnumerator TypeText(string textInput, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        
        float t = 0;
        int charIndex = 0;

        while (charIndex < textInput.Length)
        {
            t += Time.deltaTime * typeWriterSpeed;
            charIndex = Mathf.Clamp(Mathf.FloorToInt(t), 0, textInput.Length);

            textLabel.text = textInput.Substring(0, charIndex);

            yield return null;
        }

        textLabel.text = textInput;
    }
}
