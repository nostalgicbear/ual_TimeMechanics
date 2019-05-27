using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LunarCatsStudio.SuperRewinder;
using Valve.VR;

public class TimeRewindManager : MonoBehaviour
{
    public SteamVR_Action_Single trigger; //The trigger button
    public List<Rewind3DObject> rewinders; //List of all things to be rewinded
    bool m_is_rewinding = true;
    // Start is called before the first frame update
    void Start()
    {
        rewinders.AddRange(FindObjectsOfType<Rewind3DObject>()); //Find all things with the Rewind script attached
    }

    /// <summary>
    /// Starts the rewind process
    /// </summary>
    public void StartRewinding()
    {
        if (m_is_rewinding == false)
        {
            m_is_rewinding = true;

            foreach (Rewind3DObject rw in rewinders) //Loops through the rewind objects and calls the Rewind themoth
            { 
                rw.StartRewind();
            }
        }
    }

    /// <summary>
    /// Stops rewinding
    /// </summary>
    void StopRewind()
    {
        if (m_is_rewinding == true)
        {
            m_is_rewinding = false;

            //send stop rewind for all rewind object in the current scene
            foreach (Rewind3DObject rewinder in rewinders)
            {
                rewinder.StopRewind();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.GetAxis(SteamVR_Input_Sources.LeftHand) > 0) //f the left trigger is held down then rewind...
        {
            StartRewinding();
        }


        if (trigger.GetAxis(SteamVR_Input_Sources.LeftHand) == 0) //If the left trigger is not held down, do not rewind
        {
            StopRewind();
        }
    }
}
