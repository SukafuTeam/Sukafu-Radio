using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class MainCameraController : MonoBehaviour
{

    public float MaxX = 20.33f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var pos = Vector3.Lerp(transform.position, GameController.Instance.Player.transform.position, 0.1f);
	    pos.z = -10;

	    if (pos.x < 0)
	    {
	        pos.x = 0;
	    }
	    if (pos.x > MaxX)
	    {
	        pos.x = MaxX;
	    }
	    if (pos.y < 0)
	    {
	        pos.y = 0;
	    }
	    if (pos.y > 25.72f)
	    {
	        pos.y = 25.72f;
	    }

	    transform.position = pos;
	}
}
