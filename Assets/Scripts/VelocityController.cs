using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityController : MonoBehaviour
{
    Vector3 lastPosition = Vector3.zero;
    public bool slowTime;
    // At each frame:


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!slowTime) //If the slowTime variable is not true, we do not progress
        {
            return;
        }

        //At this point, slowTime is set to true, so we progress...

        Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime; //Calculate our velocity
        lastPosition = transform.position;

        float newTime = Mathf.Clamp01(velocity.magnitude);

        if (newTime <= 0.0f) //If there is absolutely no movement (which is highly unlikely), just give time a very slow value
        {
            Time.timeScale = 0.05f;
        }
        else //otherwise if there is movement, time is set to pass at a rate that is determnied by how fast we are moving
        {
            Time.timeScale = newTime; //we set the the Time.timeScale value to be 
            Time.fixedDeltaTime = newTime * 0.02f; //Adjust the fixedDeltaTime so it isnt choppy
        }


    }

    /// <summary>
    /// Sets the slowTime variable to true. meaning we will progress in the Update() function
    /// </summary>
    public void SetSuperhotTime()
    {
        slowTime = true;
    }

    /// <summary>
    /// Sets the time to be a normal time scale
    /// </summary>
    public void SetNormalTime()
    {
        slowTime = false;
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Sets time to progress at a constant slow speed
    /// </summary>
    public void SetSlowMotion()
    {
        slowTime = false;
        Time.timeScale = 0.2f;
    }
}
