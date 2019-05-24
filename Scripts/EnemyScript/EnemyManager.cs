using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;                        // Enemy prefab to be spawned.
    public float spawnTime = 3f;                    // How long between each spawn.
    public Transform[] spawnPoints;                 // An array of the spawn points this enemy can spawn from.
    public PlayerController PController;            // To get the Health from PlayerController.

	// Use this for initialization
	void Start ()
    {
        // Call the spawn function after a delay of the spawnTime and then continue to call after set amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}
	
    void Spawn()
    {
        // If player has no health left:
        if (PController.PlayerHealth <= 0f)
        {
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn points' position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
