using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disappear : MonoBehaviour
{

    public float Amount;
    private Image _image;

	// Use this for initialization
	void Start ()
	{
	    _image = GetComponent<Image>();
	    StartCoroutine(Disapear());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator Disapear()
    {
        var color = _image.color;
        while (color.a > 0)
        {
            color.a -= Amount;
            _image.color = color;
            yield return null;
        }
        yield return null;
    }
}
