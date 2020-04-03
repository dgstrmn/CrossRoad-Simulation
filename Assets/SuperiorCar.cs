using UnityEngine;

public class SuperiorCar : MonoBehaviour
{
    float speed = 10f;
    float speedDV;
    bool stopped = false;
    bool emerge_stopped = false;

    // Update is called once per frame
    void Update()
    {
        

        if (emerge_stopped)
        {
            if (speed > 0)
            {
                speed = Mathf.SmoothDamp(speed, 0f, ref speedDV, 0.1f);
            }
        }

        if (!stopped && speed < 10f)
        {
            speed = Mathf.SmoothDamp(speed, 10f, ref speedDV, 0.4f);
        }
        else if (stopped)
        {
            if (speed > 0)
            {
                speed = Mathf.SmoothDamp(speed, 0f, ref speedDV, 0.1f);
            }
        }

        emerge_stopped = false;
        stopped = false;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.x > 55 || transform.position.x < -55)
            transform.position -= transform.forward * 110;
        if (transform.position.z > 55 || transform.position.z < -55)
            transform.position -= transform.forward * 110;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stopper For Cars") || collision.gameObject.CompareTag("Super Car") || collision.gameObject.CompareTag("Player"))
        {
            var heading = collision.gameObject.transform.position - gameObject.transform.position - (gameObject.transform.forward * 20);
            float dot = Vector3.Dot(heading, gameObject.transform.forward);
            if (dot > 0)
            {
                stopped = true;
            }
        }
    }
    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Stopper For Cars") || collision.gameObject.CompareTag("Super Car") || collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log(gameObject.name + "exited " + collision.gameObject.name);
    //        emerge_stopped = false;
    //        stopped = false;
    //    }
    //}
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stopper For Cars") || collision.gameObject.CompareTag("Super Car") || collision.gameObject.CompareTag("Player"))
        {
            var heading = collision.gameObject.transform.position - gameObject.transform.position;
            float dot = Vector3.Dot(heading, gameObject.transform.forward);
            if (dot > 0)
            {
                stopped = true;
                if (collision.gameObject.CompareTag("Player"))
                    emerge_stopped = true;
            }
        }
    }

}
