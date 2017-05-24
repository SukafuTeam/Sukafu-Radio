using EzInspector;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomEditor(typeof(FSMState))]
public class FSMStateInspector : Editor
{
    private static GUISkin defaultLabelSkin;
    private FSMState _target;
    [SerializeField]private SerializedProperty OnEnter, OnExit, OnUpdate, OnFixedUpdate;

    public void OnEnable()
    {
//        if (serializedObject == null)
//            return;
//        OnEnter = serializedObject.FindProperty("OnEnter");
//        OnExit = serializedObject.FindProperty("OnExit");
//        OnUpdate = serializedObject.FindProperty("OnUpdate");
//        OnFixedUpdate = serializedObject.FindProperty("OnFixedUpdate");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (_target == null)
            _target = target as FSMState;

        OnEnter = serializedObject.FindProperty("OnEnter");
        OnExit = serializedObject.FindProperty("OnExit");
        OnUpdate = serializedObject.FindProperty("OnUpdate");
        OnFixedUpdate = serializedObject.FindProperty("OnFixedUpdate");

        using (gui.Horizontal())
        {
            _target.Name= gui.EzTextField("Name", _target.Name, 15f, GUILayout.MaxWidth(110f));
            _target.NameHash = gui.EzIntField("Hash", _target.NameHash, 10f);
        }
        //++EditorGUI.indentLevel;

        _target.OverrideClip = gui.EzObjectField("Override Clip", _target.OverrideClip, 10f) as AnimationClip;

//        EditorGUIUtility.LookLikeControls();

        EditorGUILayout.PropertyField(OnEnter);
        EditorGUILayout.PropertyField(OnUpdate);
        EditorGUILayout.PropertyField(OnFixedUpdate);
        EditorGUILayout.PropertyField(OnExit);


//        if (gui.EzButton("Update States"))
//            _target.UpdateAnimatorStates();

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(_target);
        }

//        base.OnInspectorGUI();
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawState(EzFSM fsm, GizmoType gizmoType)
    {
        if (!fsm.ShowLabel || fsm.CurrentState == null)
            return;
        if (fsm.LabelGUISkin == null)
            Handles.Label(fsm.transform.position + fsm.LabelOffset,
                fsm.CurrentState.Name,
                defaultLabelSkin.label);
        else
            Handles.Label(fsm.transform.position + fsm.LabelOffset,
                fsm.CurrentState.Name,
                fsm.LabelGUISkin.label);
    }
}