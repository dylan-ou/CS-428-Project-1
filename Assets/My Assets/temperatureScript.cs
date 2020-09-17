using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class temperatureScript : MonoBehaviour
{
    public WeatherAPI api;
    public TextMeshPro temperatureText;
    public TextMeshPro humidityText;

    Regex temp = new Regex("\"temp\":[0-9]+");
    Regex humidity = new Regex("\"humidity\":[0-9]{2}");

    string webText = "";

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
            Match tempMatch = temp.Match(webText);
            Match humidMatch = humidity.Match(webText);

            string currentTemp = tempMatch.Groups[0].Value;
            string currentHumid = humidMatch.Groups[0].Value;
            temperatureText.SetText("Temperature: " + currentTemp.Substring(7) + " F");
            humidityText.SetText("Humidity: " + currentHumid.Substring(11) + "%");
        }
    }
}
