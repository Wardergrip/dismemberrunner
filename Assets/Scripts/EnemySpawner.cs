using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField, Range(0, 10)] private float delay;
    private float timer;
    [SerializeField] private bool needsLineOfSight;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (!needsLineOfSight)
            {
                SpawnAndResetTimer();
            }
            else if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out RaycastHit hit))
            {
                if (hit.transform.gameObject == player)
                {
                    SpawnAndResetTimer();
                }
            }
        }
    }

    void SpawnAndResetTimer()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        EnemyMovement enemyMov = enemy.GetComponent<EnemyMovement>();
        enemyMov.Target = player;
        timer = delay;
    }
}
