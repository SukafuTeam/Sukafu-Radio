using UnityEngine;
using System.Collections;

public class SimpleProjectile : MonoBehaviour, IProjectile
{
    private bool _right;
    public float MoveSpeed;

	// Use this for initialization
	void Start ()
	{
	    GetComponent<SpriteRenderer>().flipX = !_right;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var pos = transform.position;
	    pos.x += _right ? MoveSpeed * Time.deltaTime : -MoveSpeed * Time.deltaTime;
	    transform.position = pos;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.Send<ITargetable>(_ => _.GetHit()).Run();
        Destroy(gameObject);
    }

    public IEnumerable SetData(bool right)
    {
        _right = right;
        yield return null;
    }
}
