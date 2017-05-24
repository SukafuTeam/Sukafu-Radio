using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public float Amount;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();
	    _renderer.sprite = GameController.Instance.Eletronic.GetComponent<SpriteRenderer>().sprite;
	    StartCoroutine(Disapear());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator Disapear()
    {
        var color = _renderer.color;
        while (color.a >= 0)
        {
            color.a -= Amount;
            _renderer.color = color;
            yield return null;
        }

        Destroy(gameObject);

    }
}
