using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPlayer : IEventSystemHandler
{
    bool Grounded();
    bool CanDash();
    bool CanWalk();
    bool CanDoubleJump();
    bool Walking();
    bool CanGravity();
    int IsLookRight();
    float GetJumpForce();
    int IsFree(float speed, bool right);
    GameObject GetPlayer();

    IEnumerable SetCanDash(bool canDash);
    IEnumerable SetCanWalk(bool canWalk);
    IEnumerable SetCanDoubleJump(bool canDoubleJump);
    IEnumerable SetCangravity(bool canGravity);
    IEnumerable Jump();
    IEnumerable Skill();
    IEnumerable StartSkill();
    IEnumerable EndSkill();

    IEnumerable Kill();


    void HitGround();
}
