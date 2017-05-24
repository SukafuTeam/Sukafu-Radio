using UnityEngine;
using System.Collections;

public class NuvemController : MonoBehaviour
{

    public float MoveSpeed;
    private Vector2 offset;

    private Renderer _renderer;

	// Use this for initialization
	void Start () {
	    _renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    offset.x -= MoveSpeed;
	    _renderer.material.SetTextureOffset("_MainTex", offset);
	}

    [ContextMenu("Adjust Z")]
    public void Adjust()
    {
        GetComponent<Renderer>().sortingOrder = -99;
    }
}
