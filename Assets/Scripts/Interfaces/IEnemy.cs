using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IEnemy : IEventSystemHandler
{
    int CanAttacK();

    IEnumerable ApplyDamage();
    IEnumerable SetPursuing(bool pursuing);
    IEnumerable Attack();
}
