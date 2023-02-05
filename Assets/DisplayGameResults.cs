using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayGameResults : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gs = GameManager.gameState;
        GetComponent<TMP_Text>().text = $"{gs["Distance"]:N} m\n{gs["Time"]:N} s\n√ {gs["Money"]:N}";
    }
}
