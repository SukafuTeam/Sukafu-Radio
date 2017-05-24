using UnityEngine;
using System.Collections;

public class FimAnimacao : MonoBehaviour
{
    public IntroController Controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FImAnimacao()
    {
        Controller.shouldContinue = true;
    }
}
