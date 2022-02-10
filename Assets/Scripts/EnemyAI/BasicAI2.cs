using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class BasicAI2 : Targetable
{
    public int health = 100;
    public float viewRange = 25f;
    public float attackRange = 10f;

    private NavMeshAgent agent;
    private Transform playerTransform;

    public event Action<float> OnTakeDamage;
    public UnityEvent onDie;

    private bool isChasing = false, dead = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Ray viewRay = new Ray(transform.position, Vector3.forward);
        RaycastHit viewRayHit;


        if (Physics.Raycast(viewRay, out viewRayHit, viewRange))
        {
            if(viewRayHit.collider.tag == "Player")
            {
                if (isChasing == false)
                {
                    playerTransform = viewRayHit.collider.GetComponent<Transform>();
                    isChasing = true;
                }
            }
        }

        if(Physics.Raycast(viewRay, out viewRayHit, attackRange))
        {

        }

        Debug.DrawRay(viewRay.origin, viewRay.direction * viewRange, Color.red);

        if (isChasing == true)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    public override void TakeDamage(int damage)
    {
       
            health -= damage;
            OnTakeDamage?.Invoke( damage );
            Debug.Log( "Enemy health is: " + health );

            CheckDeath( ); 
        
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
