using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem2 : Targetable
{
    //if we need to change how a tool or weapon functions
    //we do it in the weapon script
    public PlayerStats player;
    [HideInInspector] public Item tool; 
    [HideInInspector] public Transform FPSCamera;
    [HideInInspector] public Item t;

	private void Start( )
	{
		player = GetComponent<PlayerStats>();
	}
	private void Update()
    {
        if ( t == null )
            t = Instantiate ( tool );
        t.tick( Time.deltaTime );

        if(Input.GetKey(KeyCode.Mouse0)) 
        {
            t.owner = this; 
            var target = t.GetTarget( FPSCamera );
            if ( target != null )
            {
                t.fire( target ); //when fired at a target
            }
            else
            {
                t.fire( ); //when not fired at a target (this is where we can potentially fire at ourselves (medicine, etc))
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            t.Reload( );
		}
    }
    public void AddHealth( float hp ) {
        player.health += hp;
	}
}


