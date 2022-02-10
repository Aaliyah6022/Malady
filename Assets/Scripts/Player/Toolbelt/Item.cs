using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "Empty Held Item", menuName = "ZapChariot/Holdables/Empty" )]
public class Item : ScriptableObject
{
    public MeleeSystem2 owner;
    public virtual bool showInEditor => false;
    public float cooldown = 0.2f; //replace with serverside check lol
    float c;

    public bool canFire = true;

    public virtual Targetable GetTarget( Transform origin ) {
        return owner;
	}
    public void tick( float time )
	{
		c += time;
		Tick( time );
    }

    public virtual void Tick (float time) {
        
	}

    public bool fire( Targetable target )
    {
    
        if ( c > cooldown )
        {
            c = 0;

            return Fire( target );
        }
        else
            return false;
    }

    public bool fire( )
    {
        if ( c > cooldown )
        {
            c = 0;

            return Fire( );
        }
        else
            return false;
    }
    /// <summary>
    /// Fire is called when the player fires
    /// </summary>
    /// <returns></returns>
    public virtual bool Fire( ) {
        return false;
	}
    /// <summary>
    /// Fire is called when the player fires at a target
    /// </summary>
    /// <returns></returns>
    public virtual bool Fire( Targetable target )
    {
        return false;
    }
    /// <summary>
    /// Reload is called when the player reloads 
    /// </summary>
    public virtual void Reload( )
    {

    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
