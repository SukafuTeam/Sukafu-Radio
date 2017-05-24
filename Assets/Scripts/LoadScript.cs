using UnityEngine;
using System.Collections;

public class LoadScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    var fase = PlayerPrefs.GetString("scene");
	    Application.LoadLevelAsync(fase);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
