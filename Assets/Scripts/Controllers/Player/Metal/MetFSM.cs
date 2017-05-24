using UnityEngine;
using System.Collections;

public class MetFSM : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void JumpableUpdate()
    {
        if (InputController.Jump)
        {
            gameObject.Send<IPlayer>(_ => _.Jump()).Run();
        }
    }

    public void SkillableUpdate()
    {
        if (InputController.Skill)
        {
            gameObject.Send<IPlayer>(_ => _.Skill()).Run();
        }
    }
}
