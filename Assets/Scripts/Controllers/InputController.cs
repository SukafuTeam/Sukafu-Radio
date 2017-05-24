using UnityEngine;

public enum PlayerNumber {Player1, Player2 }

public class InputController : MonoBehaviour {

    static public float Horizontal
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }

    static public float vertical
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }

    static public bool Jump
    {
        get
        {
            return Input.GetButtonDown("Jump");
        }
    }

    static public bool Skill
    {
        get
        {
            return Input.GetButtonDown("Skill");
        }
    }

    static public bool Door
    {
        get
        {
            return Input.GetButtonDown("Door");
        }
    }

    static public bool Eletronic
    {
        get
        {
            return Input.GetButtonDown("Eletronic");

        }
    }

    static public bool Metal
    {
        get
        {
            return Input.GetButtonDown("Metal");
        }
    }

	static public float Horizontal1
	{
	    get
	    {
	        return Input.GetAxis("Horizontal1");

	    }
	}
	static public float Vertical1
	{
		get
		{
			return Input.GetAxis("Vertical1");
		}
	}

	static public float Horizontal2
	{
		get
		{
			return Input.GetAxis("Horizontal2");
		}
	}
	static public float Vertical2
	{
		get
		{
			return Input.GetAxis("Vertical2");
		}
	}

    static public float GetHorizontal(PlayerNumber number)
    {
        return number == PlayerNumber.Player1 ? Horizontal1 : Horizontal2;
    }

    static public float GetVertical(PlayerNumber number)
    {
        return number == PlayerNumber.Player1 ? Vertical1 : Vertical2;
    }

    // Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
