using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{
    public int minDamage = 25;
    public int maxDamage = 50;
    public float weaponrange = 3.5f;

    public Camera FPSCamera;

    //private TreeHealth treeHealth;
    //private BasicAI basicAIHealth;
    private BasicAI2 basicAI2Health;


    private void Update()
    {
        Ray ray = FPSCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hitInfo;

        Debug.DrawRay(ray.origin, ray.direction * weaponrange, Color.green);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.collider.tag == "Tree")
                {
                    //treeHealth = hitInfo.collider.GetComponent<TreeHealth>();
                    AttackTree();
                }

                else if (hitInfo.collider.tag == "Zombie")
                {
                    //basicAIHealth = hitInfo.collider.GetComponent<BasicAI>();
                    basicAI2Health = hitInfo.collider.GetComponent<BasicAI2>();
                    AttackZombie();
                }
            }
        }
    }

    private void AttackTree() // we don't have a way to access the tree script lol
    {
        int damage = Random.Range(minDamage, maxDamage);
        //treeHealth.health -= damage;
    }

    private void AttackZombie()
    {
        int damage = Random.Range(minDamage, maxDamage);
        //basicAIHealth.TakeDamage(damage);
        basicAI2Health.TakeDamage(damage);
    }


}


