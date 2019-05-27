using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MoveAI : MonoBehaviour
{
    public List<Transform> waypoints; //List of waypoints
    public int currentIndex;
    private Transform currentTarget;
    private float reachedDistance = 0.1f;
    [Range(1.0f, 5.0f)]
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        MoveToTarget();
    }
    // Update is called once per frame
    void Update()
    {


        MoveToTarget(); 
        CheckTargetReached();
    }

    /// <summary>
    /// If the distance between the object and the target position is less than the distnace reached, then it increases the currentIndex
    /// </summary>
    private void CheckTargetReached()
    {
        if(Vector3.Distance(transform.position, currentTarget.position) < reachedDistance)
        {
            currentIndex++;
            if (currentIndex > waypoints.Count-1)
            {
                currentIndex = 0;
            } 
            
           // MoveToTarget();
        }
    }

    //private void MoveToTarget()
    //{
    //    currentTarget = waypoints[currentIndex];
    //    Vector3 rotateDirection = currentTarget.position - transform.position;
    //    transform.DOLookAt(rotateDirection, 0.2f);
    //    transform.DOMove(waypoints[currentIndex].position, 2.5f);
    //}

    /// <summary>
    /// Moves the object to its next target
    /// </summary>
    private void MoveToTarget()
    {
        currentTarget = waypoints[currentIndex]; //current target is the waypoint[current index]
        Vector3 rotateDirection = currentTarget.position - transform.position; //make the object rotate towards the target it is running towards
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed); 
        transform.LookAt(currentTarget);
    }
}


