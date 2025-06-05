using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterController : MonoBehaviour
{
    //step1
    public List <Transform> waypoints = new List<Transform>();
    public bool isMoving;
    public int waypointIndex;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotaionSpeed;

    [SerializeField] bool isLoop;

    // Start is called before the first frame update
    void Start()
    {
        StartMoving();
    }

    void StartMoving()
    {
        waypointIndex = 0;  
        isMoving = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            return;
        }

        if (waypointIndex < waypoints.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, Time.deltaTime * moveSpeed); // first movement to first point

            var direction =  transform.position -waypoints[waypointIndex].position ; 
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up); 
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotaionSpeed); // smooth rotation towards the waypoint


            // seting the distance
            var distance = Vector3.Distance(transform.position, waypoints[waypointIndex].position);

            if (distance <= 0.05f)
            {
                waypointIndex++;


                if(isLoop && waypointIndex >= waypoints.Count)
                {
                   
                    
                        waypointIndex = 0; // loop back to the first waypoint
                                       
                }
            }
        }
        
    }
}
