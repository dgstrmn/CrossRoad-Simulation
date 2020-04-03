using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableKinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        collision.gameObject.GetComponent<Rigidbody>().WakeUp();
    }
}
