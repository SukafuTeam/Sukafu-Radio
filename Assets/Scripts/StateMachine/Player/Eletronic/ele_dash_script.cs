using UnityEngine;
using System.Collections;
using System.Timers;
using Ez.Msg;

public class ele_dash_script : StateMachineBehaviour
{
    public float DashSpeed;
    public float TimeDash;
    public float _timeDash;
    public GameObject Shadow;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    animator.gameObject.Send<IPlayer>(_=>_.StartSkill()).Run();
	    animator.SetBool("Dashing", true);
	    _timeDash = TimeDash;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	    GameController.Spawn(Shadow, animator.transform.position);

	    if (InputController.Jump)
	    {
	        animator.SetBool("Dashing", false);
	        animator.gameObject.Send<IPlayer>(_=>_.Jump()).Run();
	    }

	    var pos = animator.gameObject.transform.position;
	    var lookRight = animator.gameObject.Request<IPlayer, int>(_ => _.IsLookRight());

	    if (lookRight == 2)
	    {
	        if (animator.gameObject.Request<IPlayer, int>(_ => _.IsFree(DashSpeed, true)) == 2)
	        {
	            pos.x += DashSpeed * Time.deltaTime;
	        }
	        else
	        {
	            animator.SetBool("Dashing", false);
	        }
	    }
	    else
	    {
	        if (animator.gameObject.Request<IPlayer, int>(_ => _.IsFree(DashSpeed, false)) == 2)
	        {
	            pos.x -= DashSpeed * Time.deltaTime;
	        }
	        else
	        {
	            animator.SetBool("Dashing", false);
	        }
	    }

	    animator.gameObject.transform.position = pos;

	    _timeDash -= Time.deltaTime;
	    if (_timeDash <= 0)
	    {
	        animator.SetBool("Dashing", false);
	    }
	}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    animator.gameObject.Send<IPlayer>(_=>_.EndSkill()).Run();
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
