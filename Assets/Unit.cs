using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    Vector3 targetPosition;
    bool stopped;
    public Transform target;
    public float speed = 1.5f;
    public float turnSpeed = 1f;
    public float turnDst = 1;
    Path path;
    int randTarget = 0;
    System.Random rnd;
    int lastNum = 0;
    Animator anim;
    int count = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        stopped = false;
        rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
        do
        {
            randTarget = rnd.Next(1, 8);
        } while (randTarget == lastNum);
        lastNum = randTarget;

        target = GetRandomTarget(randTarget);
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
    }
    void Update()
    {
        
        
        System.Random r = new System.Random();

        // ask for path in random intervals
        //count += r.Next(1, 5);       
        //if (count >= 10)
        //{
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
        //    count = 0;
        //}
        

        float dist_x = Mathf.Abs(transform.position.x - target.position.x);
        float dist_z = Mathf.Abs(transform.position.z - target.position.z);

        if (dist_x <= 2.0f && dist_z <= 2.0f)
        {
            stopped = true;
            System.Array.Clear(path.lookPoints, 0, path.lookPoints.Length);
        }

        if (stopped)
        {
            stopped = false;
            do
            {
                randTarget = rnd.Next(1, 8);
            } while (randTarget == lastNum);
            lastNum = randTarget;

            target = GetRandomTarget(randTarget);
        }

        


    }

    public Transform GetRandomTarget(int num)
    {
        switch (num)
        {
            case 1:
                return GameObject.Find("HotDog_Target").transform;
            case 2:
                return GameObject.Find("Library_Target").transform;
            case 3:
                return GameObject.Find("Ticket_Target").transform;
            case 4:
                return GameObject.Find("Apartment_Target").transform;
            case 5:
                return GameObject.Find("Mailbox_Target").transform;
            case 6:
                return GameObject.Find("School_Target").transform;
            case 7:
                return GameObject.Find("Fountain_Target").transform;
        }

        return null;
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDst);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {

        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            if (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                }
                else
                {
                    pathIndex++;
                }
            }
            if (followingPath)
            {
                //Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                //
                Vector3 targetDir = path.lookPoints[pathIndex] - transform.position;

                // The step size is equal to speed times frame time.
                float step = turnSpeed * Time.deltaTime;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 1f);
                Debug.DrawRay(transform.position, newDir, Color.red);

                // Move our position a step closer to the target.
                //
                transform.Translate(Vector3.forward * Time.deltaTime * speed * 2, Space.Self);

            }
            yield return null;
        }
    }

    //public void OnDrawGizmos()
    //{
    //    if (path != null)
    //    {
    //        path.DrawWithGizmos();
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CrossRoad"))
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    





}