using System.Collections.Generic;
using EzInspector;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (DemoWeaponsData))]
public class WeaponDataInspector : Editor {

    private DemoWeaponsData _target;

    private bool _showWeaponsTable;
    private KeyValuePair<WeaponType, WeaponInfo> _newWeaponsTable;

    public override void OnInspectorGUI()
    {
        if (_target == null)
            _target = target as DemoWeaponsData;

//        DrawDefaultInspector();

        _target.Table = gui.EzDict("Weapons Table", _target.Table, ref _showWeaponsTable, ref _newWeaponsTable);

        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
}
