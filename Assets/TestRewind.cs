using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LunarCatsStudio.SuperRewinder;
using Valve.VR;

public class TestRewind : MonoBehaviour
{
    public SteamVR_Action_Single trigger;
    public List<Rewind3DObject> rewinders;
    bool m_is_rewinding = true;
    // Start is called before the first frame update
    void Start()
    {
        rewinders.AddRange(FindObjectsOfType<Rewind3DObject>());
    }

    public void StartRewinding()
    {
        if (m_is_rewinding == false)
        {
            m_is_rewinding = true;

            foreach (Rewind3DObject rw in rewinders)
            {
                rw.StartRewind();

            }
        }
    }

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


        if (Input.GetButtonDown("Fire2"))
        {
            StartRewinding();
        }


        if (Input.GetButtonUp("Fire2"))
        {
            StopRewind();
        }
    }
}
