using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BasicAI : Targetable
{
    public int health = 100;
    public float destroyDelay = 1;
    public float viewRange = 25f, attackRange = 10f;

    private NavMeshAgent agent;
    public Transform playerTransform;

    public event Action<float> OnTakeDamage;
    public UnityEvent onDie;
    private bool isChasing = false, dead = false;

	private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
	{
		if ( isChasing )
			agent.SetDestination( playerTransform.position );

		Ray viewRay = new Ray( transform.position, Vector3.forward );

        if ( Physics.Raycast( viewRay, out RaycastHit viewRayHit, viewRange ) )
        {
            if ( viewRayHit.collider.tag == "Player" )
            {
                if ( isChasing == false )
                {
                    playerTransform = viewRayHit.collider.GetComponent<Transform>( );
                    isChasing = true;
                }
            }
        }

        if ( Physics.Raycast( viewRay, out viewRayHit, attackRange ) )
        {

        }

	}

	private void CheckDeath( )
	{
        
		if ( health <= 0  )
		{
            if ( dead == false )
            {
                dead = true;
                onDie?.Invoke(  );
                Destroy( gameObject, destroyDelay );
            }
		}
	}

	private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            playerTransform = target.transform;
            isChasing = true;
        }
    }

    public override void TakeDamage(int damage)
    {
            health -= damage;
            OnTakeDamage?.Invoke( damage );
            CheckDeath( );

            Debug.Log( "Enemy health is: " + health );
    }

}
