using UnityEngine;
using System.Collections.Generic;

public class TouchControl : MonoBehaviour 
{
	Camera mainCamera;
	Dictionary<int, GameObject> trails = new Dictionary<int, GameObject>();

	ParticleSystem vortex;
	Touch pinch1, pinch2;

	// Start
	// initialization
	// --------------------------------------------------------------------------
	void Start()
	{
		mainCamera = Camera.main;
	}

	// Update
	// called once per frame
	// --------------------------------------------------------------------------
	void Update()
	{
		if (Input.touchCount == 2)
		{
			// Capture Pinch Gesture with two fingers
			Touch touch1 = Input.GetTouch(0);
			Touch touch2 = Input.GetTouch(1);

			// Pinch start
			if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
			{
				pinch1 = touch1;
				pinch2 = touch2;
			}
			
			// Pinching
			if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
			{
				if (vortex == null)
				{
					// Find the center between the two fingers
					Vector3 pinchPos1 = mainCamera.ScreenToWorldPoint(touch1.position);
					Vector3 pinchPos2 = mainCamera.ScreenToWorldPoint(touch2.position);				
					Vector3 vortexPosition = Vector3.Lerp(pinchPos1, pinchPos2, 0.5f);

					// Create a vortex particle effect 
					vortex = TouchEffects.MakeVortex(vortexPosition);

					// Set the particle emission rate 
					vortex.emissionRate = Mathf.Min(Screen.currentResolution.height,Screen.currentResolution.width);

					pinch1 = touch1;
					pinch2 = touch2;
				}
				else
				{
					// Update the emission rate based on the pinch distance 
					float pinchDistance = Vector2.Distance(pinch1.position, pinch2.position);
					float touchDistance = Vector2.Distance(touch1.position, touch2.position);
					float distanceDiff = touchDistance - pinchDistance;
					vortex.emissionRate += distanceDiff;
					Debug.Log( "vortex emission rate: " + vortex.emissionRate );

					pinch1 = touch1;
					pinch2 = touch2;
				}
			}
		}
		else
		{
			// Capture other touches

			// Pinch release 
			if (vortex != null)
			{
				// Destroy vortex
				Destroy(vortex.gameObject);
			}
		
			for ( int i = 0; i < Input.touchCount; i++ )
			{
				Touch touch = Input.GetTouch(i);
				
				if (touch.phase == TouchPhase.Ended && touch.tapCount == 1)
				{
					// Tap
					Debug.Log( "Touch position (pixels): " + touch.position );

					Vector2 worldPos = mainCamera.ScreenToWorldPoint(touch.position);
					Debug.Log( "Touch ScreenToWorldPoint: " + worldPos );

					// Effect for feedback
					TouchEffects.MakeExplosion((worldPos));
				}
				else
				{
					// Drag
					if (touch.phase == TouchPhase.Began)
					{
						// Store this new value
						if (trails.ContainsKey(i) == false)
						{
							Vector2 position = mainCamera.ScreenToWorldPoint(touch.position);
							GameObject trail = TouchEffects.MakeTrail(position);
							
							if (trail != null)
							{
								Debug.Log(trail);
								trails.Add(i, trail);
							}
						}
					}
					else if (touch.phase == TouchPhase.Moved)
					{
						// Move the trail
						if (trails.ContainsKey(i))
						{
							GameObject trail = trails[i];
							
							Vector2 position = mainCamera.ScreenToWorldPoint(touch.position);
							trail.transform.position = position;
						}
					}
					else if (touch.phase == TouchPhase.Ended)
					{
						// Clear trails
						if (trails.ContainsKey(i))
						{
							GameObject trail = trails[i];

							Destroy(trail, trail.GetComponent<TrailRenderer>().time);
							trails.Remove(i);
						}
					}
				}
			}
		}
	}
}