using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _text.text = string.Empty;
        Application.logMessageReceived += HandleLog;
    }
    
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        _text.text += logString + Environment.NewLine;
    }
}
