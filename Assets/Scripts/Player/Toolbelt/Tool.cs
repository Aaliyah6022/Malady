using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tool : Item
{
    //a basic guideline tool we can inherit from so we can swap between them
    
    public bool DrawRay = true;
    public float maxRange = 3.5f;
    public override Targetable GetTarget( Transform origin ) {
        Ray ray = new Ray( origin.position, origin.forward );
        if ( DrawRay )
        {
            Debug.DrawRay( ray.origin, ray.direction * maxRange, Color.green );
        }
        if ( Physics.Raycast( ray, out RaycastHit hitInfo, maxRange ) )
        {
            if ( hitInfo.collider.GetComponent<Targetable>( ) is Targetable target )
            {
                
                return target;
            }
        }
        return null;
    }
}
