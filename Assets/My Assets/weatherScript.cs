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
    public AudioSource audioSource;
    public TextMeshPro text;
    public GameObject[] weatherObjects;
    public AudioClip[] audioClips;
    public GameObject[] particles;

    //Regex condition = new Regex("(clear sky)|(few clouds)|(scattered clouds)|(broken clouds)|(shower rain)|(rain)|(thunderstorm)|(snow)|(mist)");
    Regex condition2 = new Regex("\"id\":[0-9]{3}");
    Match weatherMatch;

    string currentCondition = "";
    string conditionID = "";
    int currentCase = -1;
    int idNum;
    string[] listOfConditions = { "clear sky", "few clouds", "scattered clouds", "broken clouds", "shower rain", "rain", "thunderstorm", "snow", "mist" };

    string webText = "";
    
    public
    // Start is called before the first frame update
    void Start()
    {
        api = api.gameObject.GetComponent<WeatherAPI>();
        audioSource = this.GetComponentInParent<AudioSource>();
        for(int i = 0; i < weatherObjects.Length; i++)
        {
            weatherObjects[i].SetActive(false);
        }
        for(int i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(webText == "") {
            webText = api.GetWebText();
            weatherMatch = condition2.Match(webText);
            conditionID = weatherMatch.Groups[0].Value;
            idNum = Int32.Parse(conditionID.Substring(5));
        }

        Debug.Log("RN it's " + currentCondition);

        if (webText != "" && currentCase == -1) 
        {
            switch (idNum)
            {
                case 800:
                    currentCondition = "clear sky";
                    break;
                case 801:
                    currentCondition = "few clouds";
                    break;
                case 802:
                    currentCondition = "scattered clouds";
                    break;
                case 803:
                case 804:
                    currentCondition = "broken clouds";
                    break;
                case 300:
                case 301:
                case 302:
                case 310:
                case 311:
                case 312:
                case 313:
                case 314:
                case 321:
                case 520:
                case 521:
                case 522:
                case 531:
                    currentCondition = "shower rain";
                    break;
                case 500:
                case 501:
                case 502:
                case 503:
                case 504:
                    currentCondition = "rain";
                    break;
                case 200:
                case 201:
                case 202:
                case 210:
                case 211:
                case 212:
                case 221:
                case 230:
                case 231:
                case 232:
                    currentCondition = "thunderstorm";
                    break;
                case 511:
                case 600:
                case 601:
                case 602:
                case 611:
                case 612:
                case 613:
                case 615:
                case 616:
                case 620:
                case 621:
                case 622:
                    currentCondition = "snow";
                    break;
                case 701:
                case 711:
                case 721:
                case 731:
                case 741:
                case 751:
                case 761:
                case 762:
                case 771:
                case 781:
                    currentCondition = "mist";
                    break;
                default:
                    currentCase = 0;
                    break;
            }
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

            if(currentCase == 5)
            {
                particles[0].SetActive(true);
                particles[1].SetActive(false);
                particles[2].SetActive(false);
            }
            else if (currentCase == 7)
            {
                particles[0].SetActive(false);
                particles[1].SetActive(true);
                particles[2].SetActive(false);
            }
            else if (currentCase == 8)
            {
                particles[0].SetActive(false);
                particles[1].SetActive(false);
                particles[2].SetActive(true);
            }
            else
            {
                particles[0].SetActive(false);
                particles[1].SetActive(false);
                particles[2].SetActive(false);
            }

            if (audioSource.clip == null)
            {
                audioSource.clip = audioClips[currentCase];
            } else if (audioSource.clip != audioClips[currentCase])
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                    audioSource.clip = audioClips[currentCase];
                    audioSource.Play();
                }
            }

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
