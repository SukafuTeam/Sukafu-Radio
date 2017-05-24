using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (InputController.Door)
	    {
	        if (Vector3.Distance(transform.position, GameController.Instance.Player.transform.position) < 3)
	        {
	            var fase = PlayerPrefs.GetInt("fase");

	            Debug.Log(fase);
	            if (fase == 5)
	            {
	                GameController.LoadLevel("cena_parabens");
	                return;
	            }

	            fase++;
	            PlayerPrefs.SetInt("fase", fase);
	            PlayerPrefs.SetInt("pontos", GameController.Instance.Pontos);
	            PlayerPrefs.Save();
	            SoundData.SetGameplay(1);

	            GameController.LoadLevel("arena_"+fase);
	        }
	    }
	}
}
