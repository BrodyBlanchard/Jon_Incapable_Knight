/*****************************************************************************
// File Name : SpawnerScript.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles the houses spawning enemies.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //where they will spawn on the houses
    [SerializeField] private Transform spawner;
    //this game object (i prefer to define it just for comprehension rather than this.gameObject)
    [SerializeField] private GameObject enemy;

    //determine amount spawned
    [SerializeField] private int spawnAmount;
    [SerializeField] private int spawnMin, spawnMax;

    [SerializeField] private GameManager gm;

    
    /// <summary>
    /// Sets the amount to a random value and begins spawning enemies
    /// </summary>
    void Start()
    {
        spawnAmount = Random.Range(spawnMin,spawnMax);
        StartCoroutine(Spawning());
        gm = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Spawns enemies based on the spawn amount on a random interval, and keeps a tally of how many enemies are spawned
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spawning()
    {
        for (var i = 0; i < spawnAmount; i++)
        {
            if(gm != null)
            {
                gm.enemyCount += 1;
            }
            Instantiate(enemy, spawner.position, spawner.rotation);
            yield return new WaitForSeconds(Random.Range(1,10));
        }
    }
}
