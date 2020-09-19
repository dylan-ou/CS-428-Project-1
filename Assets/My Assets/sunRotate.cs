using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunRotate : MonoBehaviour
{
    public GameObject rayball;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        rayball.transform.Rotate((new Vector3(0f, 0f, -speed)) * Time.deltaTime);
    }
}
