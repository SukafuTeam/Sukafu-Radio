using System.Collections.Generic;
using UnityEngine;

public class DemoWeaponsData : ScriptableObject {
    public DictWeaponTypeWeaponInfo Table = new DictWeaponTypeWeaponInfo();
}

[System.Serializable]
public class DictWeaponTypeWeaponInfo : SerializableDictionary<WeaponType, WeaponInfo> { }

[System.Serializable]
public struct WeaponInfo
{
    public float Damage, Pierce, PercentualDmgByDistance;

    public WeaponInfo(float dmg, float pierce, float perDmg){
        Damage = dmg;
        Pierce = pierce;
        PercentualDmgByDistance = perDmg;
    }
}

[System.Serializable]
public enum WeaponType
{
    Lightning,
    Grenade,
    Missile,
    Buckshot,
    Sniper,
    Healing,
}
