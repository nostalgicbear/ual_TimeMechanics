//
//  Animation3DManager.cs
//
//  Author:
//       Lunar Cats Studio <lunar.cats.studio@gmail.com>
//
//  Copyright (c) 2018 Lunar Cats Studio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using LunarCatsStudio.SuperRewinder;

/// <summary>
/// test script for setting and control all RewindObjets in scene.
/// </summary>
public class Animation2DManager : MonoBehaviour {

	//public
	[Header ("Gui")]
	[Tooltip ("rewind image")]
	public Image m_reticle_img;
	[Tooltip ("Record time label")]
	public Text m_record_time_label;

	[Header ("Character")]
	[Tooltip ("Character ref, used for disable component during rewind")]
	public PlatformerCharacter2D m_charact;
	[Tooltip ("Control ref, used for disable component during rewind")]
	public Platformer2DUserControl m_controler;

	[Header ("Settings")]
	[Tooltip ("General record duration in seconde (apply this value for all Rewind2DObject")]
	public float m_record_duration = 5f;

	//private
	private Rewind2DObject[] m_rewinders;
	bool m_go_right;
	float m_time = 0f;
	bool m_is_rewinding = true;

	void Start () 
	{
		// list all RewindObject
		m_rewinders = FindObjectsOfType<Rewind2DObject>();


		/* check if charracter move to the right and store status */
		/* Platformer2DuserControler used a flip on x axie for play the same animation for right and left moovements) */
		/* but the variable m_FacingRight is private. You can also changed Platformer2DuserControler script */
		if(m_charact.transform.localScale.x > 0)
			m_go_right = true;
		else
			m_go_right = false;
		
		StopRewind ();
		RecordDuration (m_record_duration);

	}
	
	void Update () 
	{
		// Rewind activation
		if (Input.GetButtonDown ("Fire2"))
			StartRewind ();

		if (Input.GetButtonUp ("Fire2"))
			StopRewind ();

		UpdateTimer ();

	}

	/// <summary>
	/// Starts the rewind for all RewindObject in scene.
	/// </summary>
	void StartRewind()
	{
		if (m_is_rewinding == false) 
		{
			m_is_rewinding = true;
			m_reticle_img.enabled = true;

			/* check if charracter move to the right and store status */
			/* Platformer2DuserControler used a flip on x axie for play the same animation for right and left moovements) */
			/* but the variable m_FacingRight is private. You can also changed Platformer2DuserControler script */
			if(m_charact.transform.localScale.x > 0)
				m_go_right = true;
			else
				m_go_right = false;

			//send start rewind for all rewind object in the current scene
			foreach (Rewind2DObject rewinder in m_rewinders) {
				m_charact.enabled = false;
				m_controler.enabled = false;
				rewinder.StartRewind ();
			}
		}
	}
	/// <summary>
	/// Stops the rewind for all RewindObject in scene.
	/// </summary>
	void StopRewind()
	{
		if (m_is_rewinding == true) 
		{
			m_is_rewinding = false;
			bool go_right;

			//send stop rewind for all rewind object in the current scene
			foreach (Rewind2DObject rewinder in m_rewinders) {
				rewinder.StopRewind ();
				m_charact.enabled = true;
				m_controler.enabled = true;
			}
				
			m_reticle_img.enabled = false;

			/* apply last orientation of the character (before rewinding) */
			/* Platformer2DuserControler used a flip on x axie for play the same animation for right and left moovements) */
			/* but the variable m_FacingRight is private. You can also changed Platformer2DuserControler script */
			if (m_charact.transform.localScale.x > 0)
				go_right = true;
			else
				go_right = false;


			if (go_right != m_go_right) 
			{
				Vector3 theScale = m_charact.transform.localScale;
				theScale.x *= -1;
				m_charact.transform.localScale = theScale;
			}
		}
	}


	/// <summary>
	/// Updates the timer value.
	/// </summary>
	void UpdateTimer ()
	{
		float new_time=0f;
		if (m_is_rewinding) 
		{
			new_time = m_time - Time.deltaTime;
			if (new_time <= 0) 
			{
				new_time = 0;
				StopRewind ();
			}
		}
		else
		{
			new_time = m_time + Time.deltaTime;
			if (new_time >= m_record_duration)
				new_time = m_record_duration;
		}

		if (new_time != m_time) 
		{
			m_time = new_time;
			UpdateRecordDurationLabel ();
		}
	}

	/// <summary>
	/// set record duration for all RewindObject in scene.
	/// </summary>
	/// <param name="_duration">duration in secondes</param>
	void RecordDuration(float _duration)
	{
		m_record_duration = _duration;

		// for each Rewind3DObject, set keep_velocity param
		foreach (Rewind2DObject rewinder in m_rewinders) {
			rewinder.m_recordTime = m_record_duration;
		}

		UpdateRecordDurationLabel ();
	}

	/// <summary>
	/// Updates the record duration label.
	/// </summary>
	void UpdateRecordDurationLabel()
	{
		// update keep velocity label
		if(m_record_time_label != null)
		{
			m_record_time_label.text = "Record Time: " + Mathf.Floor(m_time) + "/" + m_record_duration + " s";
		}
	}
}
