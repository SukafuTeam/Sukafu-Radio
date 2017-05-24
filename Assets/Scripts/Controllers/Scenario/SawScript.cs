using UnityEngine;
using System.Collections;

public class SawScript : MonoBehaviour
{
    public float RotateSpeed;
    private Rigidbody2D _body;
    public AudioClip sawdeath;

	// Use this for initialization
	void Start ()
	{
	    _body = GetComponent<Rigidbody2D>();
	    _body.angularVelocity = RotateSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	    _body.angularVelocity = RotateSpeed;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.Send<IPlayer>(_ => _.Kill()).Run();
        other.gameObject.Send<IEnemy>(_=>_.ApplyDamage()).Run();
        if (other.gameObject.tag == "Player")
        {
            GameController.PlaySound(sawdeath);
        }
    }
}
