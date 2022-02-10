using System;
using System.Collections;
using System.Collections.Generic;
using Shapes;
using Unity.Mathematics;
using UnityEngine;

[ExecuteAlways]
public class HealthBar : ImmediateModeShapeDrawer
{
    public BasicAI ai;
    bool aiset = false;
    [ColorUsage(true,true)]
    public Color lifeColor, midColor, deadColor;
    public float thickness=0.2f, updateSpeed=4f;
    public bool aifacing;
    public Vector3 rot;
    List<damageText> items = new List<damageText>();
    // Start is called before the first frame update
    void Start()
    {
    items.Clear();
        if ( ai )
		{
			SetAiEvent( );
		}
	}
	private void SetAiEvent( )
	{
		ai.OnTakeDamage += damage =>
		{
			var aip = -transform.localPosition;
			var target = aip + new Vector3( UnityEngine.Random.Range( -1, 1 ), UnityEngine.Random.Range( -1, 1 ), UnityEngine.Random.Range( -1, 1 ) );
			items.Add( new damageText( )
			{
				pos = aip,
				target = target,
				text = $"{damage}",
				die = t =>
				{
					items.Remove( t );
				}
			} );
		};
		aiset = true;
	}

	// Update is called once per frame
	void Update()
    {
        if ( ai && !aiset )
        {
            SetAiEvent( );
        }
        transform.LookAt(Camera.main.transform );

        foreach (var item in items.ToArray()) 
            item.pos = Vector3.Lerp(item.pos, item.target, Time.deltaTime);
        inversehealth = Mathf.Lerp( inversehealth, target, Time.deltaTime*updateSpeed );


    }
    float inversehealth, target;
    public override void DrawShapes( Camera cam ) {
        if (ai)
        using(Draw.Command(cam)) {

                Draw.Matrix = transform.localToWorldMatrix;
                using ( Draw.Scope ) {
                Draw.Rotate( transform.rotation);
                foreach ( var item in items )
                        Draw.Text( pos: item.pos, item.text, color: deadColor, fontSize: 4 );
                }
                Draw.LineGeometry = LineGeometry.Volumetric3D;
                Vector3 a = -.5f * Vector3.right;
                Vector3 b = .5f * Vector3.right;

                target = Mathf.InverseLerp( 0, 100, ai.health);
                float otom = Mathf.InverseLerp( 0, .5f, inversehealth );
                float mtoe = Mathf.InverseLerp( .5f, 1, inversehealth );

                var cc = Vector3.Lerp( b, a, inversehealth );

                var o2m = Color.Lerp( deadColor, midColor, otom );
                var m2e = Color.Lerp( midColor, lifeColor, mtoe );
                var color = Color.Lerp( o2m, m2e, inversehealth );

                float inverse = Mathf.InverseLerp( 0, 0.1f, inversehealth );
                inverse *= Mathf.InverseLerp( 1, .9f, inversehealth );

                var col = new Color( color.r, color.g, color.b, inverse );
                Draw.Line(b,  cc , thickness*inverse, col);
	    }
    }
    
    class damageText {
        public string text;
        public Vector3 pos { get => p; set { 
            p = value; 
            if ( Vector3.Distance( p, target ) < 0.1f ) 
            die?.Invoke( this ); 
            } 
         }
        Vector3 p;
        public Vector3 target ;
        public Action<damageText> die;
	}
}
