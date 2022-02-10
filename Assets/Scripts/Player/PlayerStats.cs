using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject deathCanvas;

    public float health = 100;
    public sInventory inventory;
    public Vector3 spawnPoint = Vector3.zero;

    private void Start()
    {
        deathCanvas.SetActive(false);
        inventory?.SetSlotCount( 16 );
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        deathCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Respawn()
    {
        health = 100;
        deathCanvas.SetActive(false);
        transform.position = spawnPoint;
    }

}
