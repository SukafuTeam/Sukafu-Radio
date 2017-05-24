using UnityEngine;
using System.Collections;

public class range_weak_pursue : StateMachineBehaviour
{
    public float FleeDistance;
    public float ShootDistance;
    public float MoveSpeed;
    private float _realMoveSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.Send<IEnemy>(_=>_.SetPursuing(true)).Run();
        _realMoveSpeed = Random.Range(MoveSpeed - 1f, MoveSpeed + 1f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = GameController.Instance.Player;
        if(Mathf.Abs(player.transform.position.x - animator.transform.position.x) > FleeDistance)
        {
            animator.SetBool("Pursuing", false);
            return;
        }
        if (Mathf.Abs(player.transform.position.x - animator.transform.position.x) < ShootDistance)
        {
            if (animator.gameObject.Request<IEnemy, int>(_ => _.CanAttacK()) == 2)
            {
                animator.SetTrigger("Shoot");
                return;
            }
        }

        if (Mathf.Abs(player.transform.position.x - animator.transform.position.x) < ShootDistance)
        {
            return;
        }

        var pos = animator.transform.position;

        if (player.transform.position.x < animator.transform.position.x)
        {
            pos.x -= _realMoveSpeed * Time.deltaTime;
        }
        if (player.transform.position.x > animator.transform.position.x)
        {
            pos.x += _realMoveSpeed * Time.deltaTime;
        }
        animator.transform.position = pos;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.Send<IEnemy>(_=>_.SetPursuing(false)).Run();
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
