using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    private Health playerHealth;
    [SerializeField] private DismemberManager dismemberManager;
    [SerializeField] private EnemyMovement enemyMovement;

    [Header("Melee attack")]
    [SerializeField] private GameObject meleeRangeObj;
    [SerializeField] private int damage = 1;
    [SerializeField] private float meleeRange = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        meleeRangeObj.SetActive(enemyMovement.IsArmsRotationComplete && dismemberManager.HasArms);
        if (!dismemberManager.HasArms) return;

        if ((player.transform.position - transform.position).sqrMagnitude <= (meleeRange * meleeRange))
        {
            if (enemyMovement.IsArmsRotationComplete)
            {
                enemyMovement.StartArmsRotation();
                enemyMovement.allowMovement = false;
                playerHealth.Damage(damage);
            }
        }
        enemyMovement.allowMovement = enemyMovement.IsArmsRotationComplete;        
    }
}
