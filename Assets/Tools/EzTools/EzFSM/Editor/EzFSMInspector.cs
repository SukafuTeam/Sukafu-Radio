using System;
using EzInspector;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

[CustomEditor(typeof(EzFSM))]
public class EzFSMInspector : Editor
{
    private static GUISkin defaultLabelSkin;
    private EzFSM _target;
    private Editor _stateEditor;

    public SerializedProperty FSMStates_p;
    public SerializedProperty CurrentState_p;
    private bool _showStatesData;

    public bool[] DisplayFlags;
    private bool _showCurrentState;

// StatesData?    
//    [SerializeField]private SerializedProperty OnEnter, OnExit, OnUpdate, OnFixedUpdate;    

    private void OnEnable()
    {
        defaultLabelSkin = Resources.Load("FSMLabelSkin") as GUISkin;
        EditorGUIUtility.wideMode = true;
        _stateEditor = null;
    }

    public override void OnInspectorGUI()
    {
        if (_target == null)
            _target = target as EzFSM;

        serializedObject.Update();

        CurrentState_p = serializedObject.FindProperty("CurrentState");
        //Debug.Log(CurrentState_p);
        FSMStates_p = serializedObject.FindProperty("FSMStates");

//        if (_stateEditor == null && _target.CurrentState != null)
//            _stateEditor = CreateEditor(_target.CurrentState);

        using (gui.Horizontal())
        {
            _target.ShowLog = gui.EzToggle("Log", _target.ShowLog, GUILayout.MaxWidth(60f));
            _target.Layer = gui.EzIntField("Layer", _target.Layer, 10f);
        }
        using (gui.Horizontal())
        {
            _target.ShowLabel = gui.EzToggle("Label", _target.ShowLabel, GUILayout.MaxWidth(60f));
            _target.LabelOffset = gui.EzV3Field("", _target.LabelOffset, 0f, 20f);
        }
        _target.LabelGUISkin = gui.EzObjectField("GUI Skin", _target.LabelGUISkin, 10f) as GUISkin;

        using (gui.Horizontal())
        {
            gui.EzLabel("States: ", GUILayout.MaxWidth(40f));
            _showStatesData = gui.EzToggle("Show Info", _showStatesData, GUILayout.MaxWidth(80f));
            if (gui.EzButton("Update"))
            {
                _target.UpdateAnimatorStates();
                InitializeDisplayFlags();
            }
        }

        if (_showStatesData)
        {
            gui.HorizontalBar();
            if (FSMStates_p.isArray && FSMStates_p.hasChildren)
            {
//            Debug.Log("FSMStates_p array size: " + FSMStates_p.arraySize);
                if (DisplayFlags == null || DisplayFlags.Length != FSMStates_p.arraySize)
                    InitializeDisplayFlags();
                for (var i = 0; i < FSMStates_p.arraySize; i++)
                {
                    var stateData_p = FSMStates_p.GetArrayElementAtIndex(i);
                    var stateDataValue = stateData_p.objectReferenceValue as FSMState;
                    InlinePropertyInspector<FSMState>(stateDataValue.Name, stateData_p, ref DisplayFlags[i],
                        typeof(FSMStateInspector));
                }
            }
            gui.HorizontalBar();
            //ShowCurrentState();
            InlinePropertyInspector<FSMState>(
                "Current State" + (_target.CurrentState == null ? "" : " (" + _target.CurrentState.Name + ")"),
                CurrentState_p, ref _showCurrentState, typeof(FSMStateInspector)
            );
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            //this.SetSceneDirty();
            EditorUtility.SetDirty(_target);
        }
    }

    private void InitializeDisplayFlags()
    {
        DisplayFlags = new bool[FSMStates_p.arraySize];
    }

    private static void InlinePropertyInspector<T>(string label, SerializedProperty property, ref bool displayFlag,
        Type inspectorType = null) where T : Object
    {
        if (property == null)
            return;
        displayFlag = gui.EzFoldout(label, displayFlag);
        var propertyValue = property.objectReferenceValue as T;
        if (displayFlag && propertyValue != null)
        {
            //TODO: Add gui.Vertical overload with inbed hor offset
            using (gui.Horizontal())
            {
                gui.EzSpacer(10f);
                using (gui.Vertical())
                {
                    var editor = CreateEditor(propertyValue, inspectorType);
                    if (editor != null)
                        editor.OnInspectorGUI();
                }
            }
        }

//     #### CachedEditor alternative:
//        Editor cachedEditor = null;
//        var propertyValue = property.objectReferenceValue as T;
//        CreateCachedEditor(propertyValue, typeof(T), ref cachedEditor);
//        if (cachedEditor != null)
//            cachedEditor.OnInspectorGUI();
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