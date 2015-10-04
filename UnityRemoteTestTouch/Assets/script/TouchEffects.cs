using UnityEngine;
using System.Collections;

public class TouchEffects : MonoBehaviour 
{
	public ParticleSystem explosionEffect;     // explosion prefab

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
}