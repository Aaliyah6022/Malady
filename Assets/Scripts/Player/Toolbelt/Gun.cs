using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "Gun", menuName = "ZapChariot/Tools/Weapon/Gun" )]
public class Gun : Weapon
{
    public int bullets = 60; //make magazine system
    public override bool Fire (Targetable target) {
        if ( !base.Fire( target ) || bullets <= 0 )
            return false;
        bullets--;
        
        return true;
    }

	public override void Reload( ) {
        bullets = 60; //need magazines for actual reload
	}

    
}
