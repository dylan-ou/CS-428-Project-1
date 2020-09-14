using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class thermometer : MonoBehaviour
{
    public TextMeshPro text;
    Transform transform;
    

    // Start is called before the first frame update
    void Start()
    {
        transform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float temperature = float.Parse("." + text.text.Substring(0,2));
        Debug.Log(temperature.ToString());
        temperature = Mathf.Lerp(0f, 120f, temperature);

        float height = Mathf.Lerp(0.55f, 0.8f, temperature);
        float scale = Mathf.Lerp(0.01f, 0.26f, temperature);
        Debug.Log("Height: " + height.ToString());
        Debug.Log("Scale: " + scale.ToString());
        transform.localPosition = new Vector3(-0.25f, height, -0.25f);
        transform.localScale = new Vector3(0.09f, scale, 0.09f);
        
    }
}
