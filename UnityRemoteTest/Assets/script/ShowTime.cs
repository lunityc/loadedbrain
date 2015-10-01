using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowTime : MonoBehaviour 
{
	Text showTimeText;

	// initialization
	void Start () 
	{
		showTimeText = GetComponent<Text>();

		string theTime = System.DateTime.Now.ToString("hh:mm:ss"); 
		string theDate = System.DateTime.Now.ToString("MM/dd/yyyy"); 

		Debug.Log( "Time: " + theTime);
		Debug.Log( "Date: " + theDate);
	}
	
	// called once per frame
	void Update () 
	{
		string theTime = System.DateTime.Now.ToString("hh:mm:ss"); 
		string theDate = System.DateTime.Now.ToString("MM/dd/yyyy"); 
		showTimeText.text = theDate + " \n " + theTime;
	}
}
