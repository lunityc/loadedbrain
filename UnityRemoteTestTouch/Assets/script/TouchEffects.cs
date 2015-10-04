﻿using UnityEngine;
using System.Collections;

public class TouchEffects : MonoBehaviour 
{
	public ParticleSystem explosionEffect;     // explosion prefab
	public GameObject trailEffect;			   // trail prefab


	private static TouchEffects touchEffects;  // class instance

	// Awake
	// --------------------------------------------------------------------------
	void Awake()
	{
		touchEffects = this;
	}
	
	// Start
	// initialization
	// --------------------------------------------------------------------------
	void Start()
	{
		// Check prefab assigned
		if (explosionEffect == null)
		{
			Debug.LogError("Missing Explosion Effect.");
		}
		if (trailEffect == null)
		{
			Debug.LogError("Missing Rainbow Trail Effect.");
		}
	}

	// MakeExplosion
	// create an explosion effect at the touch position
	// --------------------------------------------------------------------------
	public static ParticleSystem MakeExplosion(Vector3 position)
	{
		ParticleSystem effect;

		if (touchEffects == null)
		{
			Debug.LogError("Missing TouchEffect Script instance.");
			effect = null;
		}
		else
		{
			// Create effect
			effect = Instantiate(touchEffects.explosionEffect) as ParticleSystem;
			effect.transform.position = position;
		
			// Destory at the end of the effect
			Destroy(effect.gameObject, effect.duration);
		}

		return effect;
	}

	// MakeTrail
	// create an trailing effect at the touch position
	// --------------------------------------------------------------------------
	public static GameObject MakeTrail(Vector3 position)
	{
		GameObject effect;
		
		if (touchEffects == null)
		{
			Debug.LogError("Missing TouchEffect Script instance.");
			effect = null;
		}
		else
		{
			effect = Instantiate(touchEffects.trailEffect) as GameObject;
			effect.transform.position = position;
		}

		return effect;
	}
}