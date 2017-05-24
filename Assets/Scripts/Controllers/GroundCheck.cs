using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool Grounded;
    public float VerticalVelocity;
    public Transform GroundCheckTransform;
    public LayerMask GroundMask;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Time.deltaTime > 0.2f)
	    {
	        return;
	    }

	    if (!Grounded)
	    {
	        VerticalVelocity -= 100 * Time.deltaTime;
	    }

	    var pos = transform.position;
	    pos.y += VerticalVelocity * Time.deltaTime;
	    transform.position = pos;

	    Grounded = IsGrounded();
	}

    private bool IsGrounded()
    {
        var distance = transform.position.y - GroundCheckTransform.position.y;
        if(VerticalVelocity <= 0) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0,-1), distance, GroundMask);
            if(hit.collider != null) {
                transform.position = new Vector3(transform.position.x, hit.point.y + (distance - 0.01f), transform.position.z);
                VerticalVelocity = 0;
                Debug.DrawLine(transform.position, GroundCheckTransform.position,	Color.red);
                return true;
            }
        }
        Debug.DrawLine(transform.position, GroundCheckTransform.position,	Color.green);
        return false;
    }
}
