using UnityEngine;
using System.Collections;

public class shakeCamera : MonoBehaviour {

    public AudioSource source;
    public float timeToShake;
    public float shakeAmount;

	// Use this for initialization
	void Start () {
        source = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(timeToShake > 0)
        {
            timeToShake -= Time.deltaTime;
            var pos = new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), -10);
            Camera.main.transform.position = pos;
        } 
//        else if(Game.Data.lifes > 0) {
//            Camera.main.transform.position = new Vector3(0,0,-10);
//        }
	}

    public void TomouDano()
    {
        timeToShake = 0.5f;

#if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }

    public void TocaAHH()
    {
        source.Play();
    }
}
