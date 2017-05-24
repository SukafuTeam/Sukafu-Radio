using UnityEngine;
using Ez.Msg;

public class Projectile : MonoBehaviour {

	public int Damage = 10;

    // When a message or request takes a parameter, we may use a property to define the message and shorten its usage code
    private EzMsg.EventAction<IArmor> ApplyDamageMsg {
        get {return _=>_.ApplyDamage(Damage);}
    }

    // If a message or request takes no parameter, we may simply declare it as static
    public static EzMsg.EventFunc<IArmor,int> GetArmorHealth;


	public void OnTriggerEnter(Collider col) {
	    //### Request an int value from a GetHealth method from any gameObject's component implementing IArmor

//      * Inline format, no previous declaration required
//	    var health = EzMsg.Request<IArmor, int>(col.gameObject, _=>_.GetHealth());

//      * Short form with Predefined Request
//	    var health = EzMsg.Request(col.gameObject, GetArmorHealth);

//      * Extension form from gameObject
//	    var health = col.gameObject.Request(GetArmorHealth);
//
//	    Debug.Log("(Projectile) Armor Health found: " + health);

	    //### Sends an ApplyDamage(Damage) callback to all gameObject' components implementing IArmor

//      * Original 'ExecuteEvents' format:
//      ExecuteEvents.Execute<IArmor>(col.gameObject, null, (x,y)=>x.ApplyDamage(Damage));

//      * Inline format, no previous declaration required
//		EzMsg.Send<IArmor>(col.gameObject, _=>_.ApplyDamage(Damage));

//      * Short form with Predefined Request
//	    EzMsg.Send(col.gameObject, ApplyDamageMsg).Run();
//	    EzMsg.SendSimple(col.gameObject, ApplyDamageMsg);



//	    int h1 = EzMsg.Request<IArmor, int>(col.gameObject, _=>_.GetHealth());
//	    int h2 = col.gameObject.Request<IArmor, int>(_=>_.GetHealth());
//	    int h3 = col.gameObject.Request(GetArmorHealth);



//	    EzMsg.Send<IArmor>(col.gameObject, _=>_.ApplyDamage(Damage));
//	    col.gameObject.Send<IArmor>(_=>_.ApplyDamage(Damage));

//	    * Shorthand form, chainable
	    col.gameObject.Send<IArmor>(_=>_.ApplyDamage(Damage))
	        .Wait(4f)
	        .Send<IWeapon>(gameObject, _=>_.Reload());

//	    col.gameObject.Send(ApplyDamageMsg);

	    // Currently, a EzMsgManager component scene is required in the scene to hold the multiple coroutines.

	    EzMsg.Send<IArmor>(col.gameObject, _=>_.ApplyDamage(Damage))
	        .Wait(2f)
	        .Send<IWeapon>(gameObject, _=>_.Reload())
	        .Run();

//	    EzMsg.Wait(4f).Send<IWeapon>(gameObject, _=>_.Reload())
//	        .Run();

	}

}

