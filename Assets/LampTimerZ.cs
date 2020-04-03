using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampTimerZ : MonoBehaviour
{
    float timeLeftPedestrian = 15f;
    float timeLeftCars = 15f;
    // True = green light / False = red light
    // Start is called before the first frame update
    public bool redLight;

    void Start()
    {
        redLight = true;
        BoxCollider[] human = GameObject.Find("Stopper For Humans").GetComponents<BoxCollider>();
        human[1].enabled = false;
        human[3].enabled = false;
        human[4].enabled = false;
        human[7].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(redLight)
        {
            gameObject.GetComponent<Light>().color = Color.red;

            timeLeftPedestrian -= Time.deltaTime;
            if (timeLeftPedestrian < 0)
            {
                timeLeftPedestrian = 15f;
                redLight = false;
                BoxCollider[] human = GameObject.Find("Stopper For Humans").GetComponents<BoxCollider>();
                human[1].enabled = true;
                human[3].enabled = true;
                human[4].enabled = true;
                human[7].enabled = true;
                BoxCollider[] car = GameObject.Find("Stopper For Cars").GetComponents<BoxCollider>();
                car[0].enabled = false;
                car[1].enabled = false;

            }
        }
        else
        {
            gameObject.GetComponent<Light>().color = Color.green;

            timeLeftCars -= Time.deltaTime;
            if (timeLeftCars < 0)
            {
                timeLeftCars = 15f;
                redLight = true;
                BoxCollider[] human = GameObject.Find("Stopper For Humans").GetComponents<BoxCollider>();
                human[1].enabled = false;
                human[3].enabled = false;
                human[4].enabled = false;
                human[7].enabled = false;
                BoxCollider[] car = GameObject.Find("Stopper For Cars").GetComponents<BoxCollider>();
                car[0].enabled = true;
                car[1].enabled = true;
            }
        }


    }
}
