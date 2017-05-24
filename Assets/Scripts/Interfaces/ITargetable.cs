using System.Collections;
using UnityEngine.EventSystems;

public interface ITargetable : IEventSystemHandler
{
    IEnumerable GetHit();
    IEnumerable SplashHit();
}
