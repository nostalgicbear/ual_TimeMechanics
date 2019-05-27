
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LunarCatsStudio.SuperRewinder;
/// <summary>
/// This class manages the creating off a load of balls and then shooting them off in a random direction. 
/// </summary>

public class CreateBalls : MonoBehaviour
{
    public TimeRewindManager timeManager; //the time manager object. This is used so we can add the balls to the lsit of objects to rewind
    public Transform ballHolder; //Just an empty game object that we child the balls to. This is only done so the hierarchy doesnt end up messy
    public GameObject ball; //ball prefab
    public int numberOfBalls;

    private float power = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBalls();
    }

    /// <summary>
    /// This spawns a ball, and adds a force to it in a random direction
    /// </summary>
    private void SpawnBalls()
    {
       for(int i = 0; i < numberOfBalls; i++)
        {
            GameObject newBall = Instantiate(ball, transform.position, Quaternion.identity, ballHolder); //Instantiate a new ball
            newBall.AddComponent<Rewind3DObject>();
            newBall.GetComponent<Rewind3DObject>().m_recordTime = 10;
            timeManager.rewinders.Add(newBall.GetComponent<Rewind3DObject>()); //Add the ball to the list of things to rewind
            Vector3 forceDirection = GetRandomForceDirection(); //Get a random force 
            newBall.GetComponent<Rigidbody>().AddForce(forceDirection * power); //apply that force to the ball
        }
    }

    /// <summary>
    /// Returns a direction
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomForceDirection()
    {
        Vector3 forceDirection = new Vector3(Random.Range(-5,5), Random.Range(-1, 3), Random.Range(-5, 5));
        return forceDirection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
