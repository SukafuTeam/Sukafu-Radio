using UnityEngine;
using System.Collections;

public class CongratsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (InputController.Jump)
	    {
	        SoundData.SetGameplay(1);
	        SoundData.Stop(2);
	        GameController.LoadLevel("cena_menu");
	    }
	}

    [ContextMenu("Cheat")]
    public void CHeat()
    {
        PlayerPrefs.SetInt("fase", 4);
        PlayerPrefs.Save();
    }
}
