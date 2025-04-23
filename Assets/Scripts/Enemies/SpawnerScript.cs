/*****************************************************************************
// File Name : SpawnerScript.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles enemy spawning and waves
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //defined in editor
    [SerializeField] private int spawnType;
    [SerializeField] private int wave;

    //where they will spawn
    [SerializeField] private Transform spawner;

    //enemy type
    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject civilian;

    //determine amount spawned
    [SerializeField] private int spawnAmount;
    [SerializeField] private int spawnMin, spawnMax;

    [SerializeField] private bool spawning;
    private GameManager gm;

    
    /// <summary>
    /// Sets the amount to a random value and begins spawning enemies
    /// </summary>
    void Start()
    {
        spawnAmount = Random.Range(spawnMin, spawnMax) * wave;
        gm = FindObjectOfType<GameManager>();
        spawner = this.transform;
    }

    private void Update()
    {
        if(gm.wave == wave && spawning == false)
        {
            StartCoroutine(Spawning());
            spawning = true;
        }
    }

    /// <summary>
    /// Spawns enemies based on the spawn amount on a random interval, and keeps a tally of how many 
    /// civilians are spawned
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spawning()
    {
        for (var i = 0; i < spawnAmount; i++)
        {
            if (gm != null && spawnType == 2)
            {
                gm.enemyCount += 1;
                Instantiate(civilian, spawner.position, spawner.rotation);
            }
            else if(spawnType == 1)
            {
                Instantiate(guard, spawner.position, spawner.rotation);
            }
            yield return new WaitForSeconds(Random.Range(1,10));
        }
        wave = 0;
        spawning = false;
    }
}
