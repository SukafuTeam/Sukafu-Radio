using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public AudioClip musica1;

    public Image Filter;

    public Image Creditos;
    public Image voltar;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if (InputController.Jump)
	    {
	        ChangeScene();
	    }

	    if (InputController.Skill)
	    {
	        Creditos.enabled = !Creditos.enabled;
	        voltar.enabled = Creditos.enabled;
	    }

	    if (Input.GetKeyDown(KeyCode.F1))
	    {
	        PlayerPrefs.SetInt("fase", 1);
	        PlayerPrefs.Save();
	        Loading();
	        GameController.LoadLevel("arena_1");
	    }
	    if (Input.GetKeyDown(KeyCode.F2))
	    {
	        PlayerPrefs.SetInt("fase", 2);
	        PlayerPrefs.Save();
	        Loading();
	        GameController.LoadLevel("arena_2");
	    }
	    if (Input.GetKeyDown(KeyCode.F3))
	    {
	        PlayerPrefs.SetInt("fase", 3);
	        PlayerPrefs.Save();
	        Loading();
	        GameController.LoadLevel("arena_3");
	    }
	    if (Input.GetKeyDown(KeyCode.F4))
	    {
	        PlayerPrefs.SetInt("fase", 4);
	        PlayerPrefs.Save();
	        Loading();
	        GameController.LoadLevel("arena_4");
	    }
	    if (Input.GetKeyDown(KeyCode.F5))
	    {
	        PlayerPrefs.SetInt("fase", 5);
	        PlayerPrefs.Save();
	        Loading();
	        GameController.LoadLevel("arena_5");
	    }
	}

    public void ChangeScene()
    {
        Loading();

        PlayerPrefs.SetInt("pontos", 0);
        PlayerPrefs.SetInt("fase", 1);
        PlayerPrefs.Save();

        SoundData.Stop(1);
        SoundData.ChangeClip(1, musica1);
        SoundData.SetGameplay(1);
        SoundData.PlayAll();

        GameController.LoadLevel("arena_1");
    }

    public void Loading()
    {
        var color = Filter.color;
        color.a = 1;
        Filter.color = color;
    }
}
