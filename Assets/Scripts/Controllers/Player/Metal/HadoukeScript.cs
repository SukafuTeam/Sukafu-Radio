using UnityEngine;
using System.Collections;

public class HadoukeScript : MonoBehaviour
{
    private bool _right;
    public float MoveSpeed;
    public float DeathTime;

    private SpriteRenderer _renderer;
    private float timealive;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();
	    _renderer.flipX = !_right;
	    Destroy(gameObject, DeathTime);
	    timealive = DeathTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timealive -= Time.deltaTime;

	    var pos = transform.position;
	    pos.x += _right ? MoveSpeed * Time.deltaTime : -MoveSpeed * Time.deltaTime;
	    transform.position = pos;

	    var color = _renderer.color;
	    color.a = Mathf.InverseLerp(0, DeathTime, timealive);
	    _renderer.color = color;
	}

    public void SetSide(bool right)
    {
        _right = right;
    }
}
