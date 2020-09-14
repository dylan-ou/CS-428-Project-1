using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class showDate : MonoBehaviour
{
    public TextMeshPro text;

    
    // Update is called once per frame
    void Update()
    {
        text.SetText(System.DateTime.Now.ToString("M/d/yy"));
    }
}
