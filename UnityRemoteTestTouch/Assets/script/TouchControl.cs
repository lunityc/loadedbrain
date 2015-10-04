using UnityEngine;
using System.Collections.Generic;

public class TouchControl : MonoBehaviour 
{
	Camera mainCamera;
	Dictionary<int, GameObject> trails = new Dictionary<int, GameObject>();

	// Start
	// initialization
	// --------------------------------------------------------------------------
	void Start()
	{
		mainCamera = Camera.main; // GetComponent<Camera>();
	}

	// Update
	// called once per frame
	// --------------------------------------------------------------------------
	void Update()
	{
		// Capture touches
		for (int i = 0; i < Input.touchCount; i++)
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
					Debug.Log( "Touch trail begin: " + touch.position );

					// Store this new value
					if (trails.ContainsKey(i) == false)
					{
						Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
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
						
						mainCamera.ScreenToWorldPoint(touch.position);
						Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);

						trail.transform.position = position;
					}
				}
				else if (touch.phase == TouchPhase.Ended)
				{
					// Clear known trails
					if (trails.ContainsKey(i))
					{
						GameObject trail = trails[i];
						
						// Let the trail fade out
						Destroy(trail, trail.GetComponent<TrailRenderer>().time);
						trails.Remove(i);
					}
				}
			}
		}
	}
}