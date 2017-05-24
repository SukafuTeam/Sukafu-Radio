using UnityEngine;

/// <summary> EzFSM StateMachineBehaviour Communication Bridge
/// BridgeSMB should be assigned to states which should communicate their Enter/Update/Exit callbacks to EzFSM
/// </summary>
public class BridgeSMB : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var fsm = animator.gameObject.GetComponent<EzFSM>();
        fsm.SafeFsmInvoke(SMBcallback.Enter, animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var fsm = animator.gameObject.GetComponent<EzFSM>();
        fsm.SafeFsmInvoke(SMBcallback.Update, animator, stateInfo, layerIndex);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var fsm = animator.gameObject.GetComponent<EzFSM>();
        fsm.SafeFsmInvoke(SMBcallback.Exit, animator, stateInfo, layerIndex);
    }

}
