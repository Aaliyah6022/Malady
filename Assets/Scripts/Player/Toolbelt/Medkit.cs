using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Consumable
{
    public float hp, speed, durability;
    float t = 0;
    bool usingKit = false;
    public override bool showInEditor => true;
    public override void Tick( float time ) {
        if ( !usingKit )
            return;
        t+=speed*time;
        if ( t > speed )
        {
            t = 0;
            durability -= speed;
            owner.AddHealth(hp);
        }
	}

	public override bool Fire( ) {
        usingKit = !usingKit;
        return true;
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
