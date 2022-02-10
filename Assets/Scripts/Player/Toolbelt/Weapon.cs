using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "Melee", menuName = "ZapChariot/Holdables/Tools/Weapon/Melee" )]
public class Weapon : Tool
{
    public int minDamage = 25;
    public int maxDamage = 50;
    public override bool showInEditor => true;
	public override bool Fire( Targetable target ) {
        if ( target == null ) return false;
        int damage = Random.Range( minDamage, maxDamage );
        target.TakeDamage( damage );
        return true;
    }
}
