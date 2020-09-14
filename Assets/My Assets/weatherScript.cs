using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;


public class weatherScript : MonoBehaviour
{
    public WeatherAPI api;
    public TextMeshPro text;

    Regex condition = new Regex("(clear sky)|(few clouds)|(scattered clouds)|(broken clouds)|(shower rain)|(rain)|(thunderstorm)|(snow)|(mist)");
    string currentCondition;
    string[] listOfConditions = { "clear sky", "few clouds", "scattered clouds", "broken clouds", "shower rain", "rain", "thunderstorm", "snow", "mist" };

    string webText;
    Match weatherMatch;
    public int currentCase;


    public
    // Start is called before the first frame update
    void Start()
    {
        api = api.gameObject.GetComponent<WeatherAPI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        webText = api.GetWebText();
        if (webText != "")
        {
            weatherMatch = condition.Match(webText);

            currentCondition = weatherMatch.Groups[1].Value;
            text.SetText(currentCondition);
            
            /*
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentCase++;
                if (currentCase > 8)
                {
                    currentCase = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentCase--;
                if (currentCase < 0)
                {
                    currentCase = 8;
                }
            }
            */
            
        }
    }
}
