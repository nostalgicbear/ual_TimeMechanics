//
//  BasicManager.cs
//
//  Author:
//       Lunar Cats Studio <lunarcatsstudio@gmail.com>
//
//  Copyright (c) 2018 Lunar Cats Studio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LunarCatsStudio.SuperRewinder;

/// <summary>
/// Rewind manager is the test script for setting and control all RewindObjets in scene.
/// </summary>
public class  Shoot3DManager: MonoBehaviour {

	//public
	[Header ("Gui")]
	[Tooltip ("Velocity label text")]
	public Text m_keep_velocity_label;
	[Tooltip ("Record time label")]
	public Text m_record_time_label;
	[Tooltip ("Reticle image")]
	public Image m_reticle_img;
	[Tooltip ("Srite used for gun reticle")]
	public Sprite m_reticle_sprite;
	[Tooltip ("Srite used during rewind")]
	public Sprite m_rewind_sprite;

	[Header ("Settings")]
	[Tooltip ("Gun manager")]
	public ExplosionForce m_gun;
	[Tooltip ("General velocity status (apply this status for all Rewind3DObject")]
	public bool m_keep_velocity = true;
	[Tooltip ("General record duration in seconde (apply this value for all Rewind3DObject")]
	public float m_record_duration = 5f;

	//private
	private Rewind3DObject[] m_rewinders;
	float m_time = 0f;
	bool m_is_rewinding = true;


	void Start () 
	{
		// list all RewindObject
		m_rewinders = FindObjectsOfType<Rewind3DObject>();

		KeepVelocity (m_keep_velocity);
		RecordDuration (m_record_duration);

		StopRewind ();
	}
	
	void Update () 
	{
		//gun management
		if (Input.GetButtonDown("Fire1"))
			fire();

		// Rewind activation
		if (Input.GetButtonDown ("Fire2"))
			StartRewind ();

		if (Input.GetButtonUp ("Fire2"))
			StopRewind ();

		// keep velocity activation
		if (Input.GetButtonDown ("Fire3"))
			KeepVelocity (!m_keep_velocity);

		UpdateTimer ();
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
	/// Starts the rewind for all RewindObject in scene.
	/// </summary>
	void StartRewind()
	{
		if (m_is_rewinding == false) 
		{
			m_is_rewinding = true;

			//send start rewind for all rewind object in the current scene
			foreach (Rewind3DObject rewinder in m_rewinders) {
				rewinder.StartRewind ();
			}

			//Change reticle sprite for display rewind icon
			if (m_reticle_img != null)
				m_reticle_img.sprite = m_rewind_sprite;
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

			//send stop rewind for all rewind object in the current scene
			foreach (Rewind3DObject rewinder in m_rewinders) {
				rewinder.StopRewind ();
			}

			//restaure reticle sprite
			if (m_reticle_img != null)
				m_reticle_img.sprite = m_reticle_sprite;
		}
	}

	/// <summary>
	/// Keeps or not the velocity for all RewindObject in scene.
	/// </summary>
	/// <param name="_keep">If set to <c>true</c> keep.</param>
	void KeepVelocity(bool _keep)
	{
		m_keep_velocity = _keep;

		// for each Rewind3DObject, set keep_velocity param
		foreach (Rewind3DObject rewinder in m_rewinders) {
			rewinder.m_keepVelocity = m_keep_velocity;
		}

		// update keep velocity label
		if(m_keep_velocity_label != null)
		{
			if (m_keep_velocity)
				m_keep_velocity_label.text = "Keep velocity: yes";
			else
				m_keep_velocity_label.text = "Keep velocity: no";
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
		foreach (Rewind3DObject rewinder in m_rewinders) {
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

	/// <summary>
	/// Create an explosion.
	/// </summary>
	void fire()
	{
		if(m_gun != null && m_is_rewinding == false)
			m_gun.fire ();
	}
}
