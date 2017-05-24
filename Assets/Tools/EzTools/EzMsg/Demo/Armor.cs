using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour, IArmor {
	public int Health = 100;

	public IEnumerable ApplyDamage(int Damage) {
		Health -= Damage;
		Debug.Log("(Armor) New Health: "+Health+" ; Time: "+Time.time);
	    yield return null;
	}

    public void DecreaseArmor(float Percentage) { }

    public void IncreaseArmor(float Percentage) { }

    public int GetHealth() {
        return Health;
    }
}
