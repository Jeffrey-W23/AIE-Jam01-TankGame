// Using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// SpawnPoint object. Inheriting from MonoBehaviour.Used for spawning the enemies.
//--------------------------------------------------------------------------------------
public class SpawnController : MonoBehaviour
{
    // PUBLIC VALUES //
    //--------------------------------------------------------------------------------------
    // public gameobject for the enemy spawn blueprint
    public GameObject m_gEnemyBlueprint;

    // public int for array size
    public int m_nPoolSize;

    // public float for spawn rate
    public float m_fSpawnRate;
    //--------------------------------------------------------------------------------------

    // PRVIATE VALUES //
    //--------------------------------------------------------------------------------------
    // prviate dynamic array for enemy list
    private List<GameObject> m_agEnemyList;

    // prviate float for the spawn timer
    private float m_fSpawnTimer;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // initialize enemylist with size
        m_agEnemyList = new List<GameObject>();

        // loop through each enemy
        for (int i = 0; i < m_nPoolSize; ++i)
        {
            // Instantiate and set active state.
            GameObject tmp = Instantiate(m_gEnemyBlueprint);
            tmp.SetActive(false);
            m_agEnemyList.Add(tmp);
        }

        // Set default value for timer
        m_fSpawnTimer = 0.0f;
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // Start the timer // Update by deltatime
        m_fSpawnTimer += Time.deltaTime;

        // if spawn timer is greater than the spawn rate
        if (m_fSpawnTimer > m_fSpawnRate)
        {
            // reset timer
            m_fSpawnTimer = 0.0f;

            // Allocate an enemy to the pool
            GameObject gEnemy = AllocateEnemy();

            // If a valid enemy
            if (gEnemy)
            {
                // put enemy at a random position in the map
                Vector2 pos = Random.insideUnitCircle.normalized * 500.0f;
                gEnemy.transform.position = pos;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // AllocateEnemy: Allocate enemy to the object pool.
    //
    // Return:
    //      GameObject: Return the allocated gameobject.
    //--------------------------------------------------------------------------------------
    GameObject AllocateEnemy()
    {
        // for each in the pool
        for (int i = 0; i < m_nPoolSize; ++i)
        {
            // Check if active
            if (!m_agEnemyList[i].activeInHierarchy)
            {
                // Set active state
                m_agEnemyList[i].SetActive(true);

                // Return the enemy
                return m_agEnemyList[i];
            }
        }

        // If all fail return null;
        return null;
    }

    //--------------------------------------------------------------------------------------
    // AddNewEnemy: Add a new enemy to the object pool.
    //--------------------------------------------------------------------------------------
    void AddNewEnemy()
    {
        GameObject tmp = Instantiate(m_gEnemyBlueprint);
        tmp.SetActive(false);
        m_agEnemyList.Add(tmp);
    }

    //--------------------------------------------------------------------------------------
    // SetPoolSize: Set the pool size of the SpawnPoint.
    //
    // Param:
    //      nSize: int for setting pool size
    //--------------------------------------------------------------------------------------
    void SetPoolSize(int nSize)
    {
        // Set pool size
        m_nPoolSize = nSize;

        // Add new enemy to the list
        while (m_nPoolSize < m_agEnemyList.Count) AddNewEnemy();
    }

    //--------------------------------------------------------------------------------------
    // SetHealth: Set the health of the enemies in the object pool.
    //
    // Param:
    //      nHealth: int for setting enemy health
    //--------------------------------------------------------------------------------------
    void SetHealth(int nHealth)
    {
        //; Loop through each enemy
        for (int i = 0; i < m_nPoolSize; ++i)
        {
            // set health
            m_agEnemyList[i].GetComponent<Enemy>().SetHealth(nHealth);
        }
    }

    //--------------------------------------------------------------------------------------
    // SetDamage: Set the damage of the enemies in the object pool.
    //
    // Param:
    //      nDamage: int for setting enemy damage
    //--------------------------------------------------------------------------------------
    void SetDamage(int nDamage)
    {
        // Loop through each enemy
        for (int i = 0; i < m_nPoolSize; ++i)
        {
            // set damage
            m_agEnemyList[i].GetComponent<Enemy>().SetHealth(nDamage);
        }
    }
}
