using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class showTime : MonoBehaviour
{
    public TextMeshPro text;

    
    void Update()
    {
        text.SetText(System.DateTime.Now.ToString("hh:mm tt"));
    }
}
