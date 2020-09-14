using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherAPI : MonoBehaviour
{
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=ec9c09a9ccd5273a6eed0a771c7996f5&units=imperial";

    string webText;
    bool gotUrl = false;

    void Start()
    {

        // wait a couple seconds to start and then refresh every 900 seconds
        InvokeRepeating("GetDataFromWeb", 2f, 900f);
    }

    void GetDataFromWeb()
    {
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                webText = webRequest.downloadHandler.text;
                gotUrl = true;
            }
        }
    }

    public string GetWebText()
    {
        if (gotUrl)
        {
            return webText;
        }
        else
        {
            return "";
        }
    }
}
