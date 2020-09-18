using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using UnityEngine;


public class weatherScript : MonoBehaviour
{
    public WeatherAPI api;
    public TextMeshPro text;
    public GameObject[] weatherObjects = new GameObject[9];

    Regex condition = new Regex("(clear sky)|(few clouds)|(scattered clouds)|(broken clouds)|(shower rain)|(rain)|(thunderstorm)|(snow)|(mist)");
    Regex condition2 = new Regex("\"id\":(800|80[1-4]|5[0-3][0-4]|2[0-3][0-2]|6[0-2][0-6]|7[0-8][1-2])");
    string currentCondition = "";
    string[] listOfConditions = { "clear sky", "few clouds", "scattered clouds", "broken clouds", "shower rain", "rain", "thunderstorm", "snow", "mist" };

    string webText = "";
    Match weatherMatch;
    int currentCase = -1;

    public
    // Start is called before the first frame update
    void Start()
    {
        api = api.gameObject.GetComponent<WeatherAPI>();
        for(int i = 0; i < weatherObjects.Length; i++)
        {
            weatherObjects[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(webText == "") {
            webText = api.GetWebText();
            weatherMatch = condition.Match(webText);
            currentCondition = weatherMatch.Groups[0].Value;
        }

        Debug.Log("RN it's " + currentCondition);

        if (webText != "" && currentCase == -1) 
        {
            currentCase = Array.IndexOf(listOfConditions, currentCondition);
        } 
        else if(webText != "" && currentCase != -1)
        {
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentCase++;
                if (currentCase > 8)
                    currentCase = 0;
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentCase--;
                if (currentCase < 0)
                    currentCase = 8;
            }

            text.SetText(listOfConditions[currentCase]);

            for (int i = 0; i < weatherObjects.Length; i++)
            {
                weatherObjects[i].SetActive(false);
                if (i == currentCase)
                {
                    weatherObjects[i].SetActive(true);
                }
            }
        } 
    }
}
