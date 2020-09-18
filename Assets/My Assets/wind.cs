using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class wind : MonoBehaviour
{
    public WeatherAPI api;
    public TextMeshPro windText;
    [Range(0.0f, 360f)]
    public float rotation = 0.0f;
    

    Regex windDir = new Regex("\"deg\":[0-9]+");
    Regex windSpeed = new Regex("\"speed\":[0-9]+.[0-9]+");

    string webText;
    string direction = "N";

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
            Match speed = windSpeed.Match(webText);
            Match dir = windDir.Match(webText);

            string s = speed.Groups[0].Value.Substring(8);
            string d = dir.Groups[0].Value.Substring(6);
            //Debug.Log("Speed: " + s);
            //Debug.Log("Direction: " + d);
            float finalSpeed = float.Parse(s);
            float finalRotation = float.Parse(d);

            if (337.5f < finalRotation && finalRotation < 22.5f)
            {
                direction = "N";
            } else if(22.5f < finalRotation && finalRotation < 67.5f)
            {
                direction = "NE";
            }
            else if (67.5f < finalRotation &&finalRotation < 112.5f)
            {
                direction = "E";
            }
            else if (112.5f < finalRotation && finalRotation < 157.5f)
            {
                direction = "SE";
            }
            else if (157.5f < finalRotation && finalRotation < 202.5f)
            {
                direction = "S";
            }
            else if (202.5f < finalRotation && finalRotation < 247.5f)
            {
                direction = "SW";
            }
            else if (247.5f < finalRotation && finalRotation < 292.5f)
            {
                direction = "W";
            }
            else if (292.5f < finalRotation && finalRotation < 337.5f)
            {
                direction = "NW";
            }

            
            windText.SetText(s + " mph " + direction);
            
            this.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f + (0.05f * finalSpeed));
            this.gameObject.transform.rotation = Quaternion.Euler(0f, finalRotation, 0f);
        }
    }
}
