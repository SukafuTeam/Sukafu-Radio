using System.Collections;
using UnityEngine.EventSystems;

public interface IArmor : IEventSystemHandler
{
	// functions that can be called via the messaging system
	IEnumerable ApplyDamage(int Damage);
	void DecreaseArmor(float Percentage);
    void IncreaseArmor(float Percentage);

    int GetHealth();
}
