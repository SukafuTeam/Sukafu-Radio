using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public static class EzFSMExtensions {

    public static AnimatorState[] GetStates(this Animator animator) {
        AnimatorController controller = animator ?
            animator.runtimeAnimatorController as AnimatorController
            : null;
        return controller == null ?
            null
            : controller.layers.SelectMany(l => l.stateMachine.states).Select(s => s.state).ToArray();
    }

    public static void SafeInvoke(this UnityEvent unityEvent)
    {
        if (unityEvent != null)
            unityEvent.Invoke();
    }

    public static void SafeFsmInvoke(this EzFSM fsm, SMBcallback callback, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fsm != null)
            fsm.StateMachineEvent(callback, animator, stateInfo, layerIndex);
    }

    /// <summary> Overrides a given clip (from its name, not from the state) with another clip </summary>
    /// <param name="animator">Animator where the operation should be processed</param>
    /// <param name="name">Clip (not state!) name at the original AnimatorController</param>
    /// <param name="clip">New clip to be assigned in place of the original clip</param>
    public static void OverrideAnimationClip(this Animator animator, string name, AnimationClip clip)
    {
        var overrideController = new AnimatorOverrideController
        {
            runtimeAnimatorController = GetEffectiveController(animator)
        };
        overrideController[name] = clip;
        animator.runtimeAnimatorController = overrideController;
    }

    /// <summary> Returns the effective Runtime AnimatorController
    /// overrideController will become null as soon as soon as controller becomes a runtimeAnimController
    /// </summary>
    private static RuntimeAnimatorController GetEffectiveController(Animator animator)
    {
        var controller = animator.runtimeAnimatorController;

        var overrideController = controller as AnimatorOverrideController;
        while (overrideController != null)
        {
            controller = overrideController.runtimeAnimatorController;
            overrideController = controller as AnimatorOverrideController;
        }
        return controller;
    }
}