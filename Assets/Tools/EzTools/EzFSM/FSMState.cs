using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FSMState : ScriptableObject {
    //[Show]
    public string Name;
    [HideInInspector]
    public int NameHash;
    [HideInInspector]
    public string ClipName;

    public AnimationClip OverrideClip;

    public UnityEvent OnEnter, OnExit, OnUpdate, OnFixedUpdate;

    public static FSMState New(string name, int nameHash, string clipName)
    {
        var newState = CreateInstance<FSMState>();
        newState.Name = name;
        newState.NameHash = nameHash;
        newState.ClipName = clipName;
        return newState;
    }
}
