using UnityEngine;
using System.Collections;

public class PieceScript : MonoBehaviour
{
    public float HorizontalOffset;
    public float MinVertical;
    public float MaxVertical;

    public float rotation;

	// Use this for initialization
	void Start ()
	{
	    var body = GetComponent<Rigidbody2D>();
	    body.velocity = new Vector2(Random.Range(-HorizontalOffset, HorizontalOffset), Random.Range(MinVertical, MaxVertical));
	    body.angularVelocity = rotation;
	    Destroy(gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
