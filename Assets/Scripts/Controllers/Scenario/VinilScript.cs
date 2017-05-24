using UnityEngine;
using System.Collections;

public class VinilScript : MonoBehaviour
{

    public int PointAwards;
    public AudioClip clip1;
    public AudioClip clip2;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.Rotate(new Vector3(0, 5, 0));
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.PlaySound(Random.Range(0, 1000) % 2 == 0 ? clip1 : clip2);

            GameController.Instance.Pontos += PointAwards;
            Destroy(gameObject);
        }
    }
}
