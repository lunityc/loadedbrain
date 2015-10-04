using UnityEngine;
using System.Collections.Generic;

public class TouchControl : MonoBehaviour 
{
	Camera mainCamera;

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
				Debug.Log( "Touch position (pixels): " + touch.position );

				Vector2 worldPos = mainCamera.ScreenToWorldPoint(touch.position);
				Debug.Log( "Touch ScreenToWorldPoint: " + worldPos );

				// Effect for feedback
				TouchEffects.MakeExplosion((worldPos));
			}
		}
	}
}