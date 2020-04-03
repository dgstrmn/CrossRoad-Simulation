using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampTimerX : MonoBehaviour
{
    float timeLeftPedestrian = 15f;
    float timeLeftCars = 15f;
    // True = green light / False = red light
    // Start is called before the first frame update
    public bool redLight;

    void Start()
    {
        redLight = false;
        BoxCollider[] car = GameObject.Find("Stopper For Cars").GetComponents<BoxCollider>();
        car[2].enabled = false;
        car[3].enabled = false;
        BoxCollider[] human = GameObject.Find("Stopper For Humans").GetComponents<BoxCollider>();
        human[0].enabled = true;
        human[2].enabled = true;
        human[5].enabled = true;
        human[6].enabled = true;
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
                human[0].enabled = true;
                human[2].enabled = true;
                human[5].enabled = true;
                human[6].enabled = true;
                BoxCollider[] car = GameObject.Find("Stopper For Cars").GetComponents<BoxCollider>();
                car[2].enabled = false;
                car[3].enabled = false;

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
                human[0].enabled = false;
                human[2].enabled = false;
                human[5].enabled = false;
                human[6].enabled = false;
                BoxCollider[] car = GameObject.Find("Stopper For Cars").GetComponents<BoxCollider>();
                car[2].enabled = true;
                car[3].enabled = true;
            }
        }


    }
}
