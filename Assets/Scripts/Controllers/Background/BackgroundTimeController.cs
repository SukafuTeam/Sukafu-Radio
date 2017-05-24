using UnityEngine;
using System.Collections;

public class BackgroundTimeController : MonoBehaviour
{
    public Sprite Night;
    public Sprite Day;
    public Sprite Dawn;

    private SpriteRenderer _renderer;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();

	    var now = System.DateTime.Now;
	    if (now.Hour < 6 && now.Hour < 20)
	    {
	        _renderer.sprite = Night;
	        return;
	    }
	    if (now.Hour >= 6 && now.Hour < 8 || now.Hour >= 18 && now.Hour < 19)
	    {
	        _renderer.sprite = Dawn;
	        return;
	    }

	    _renderer.sprite = Day;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
