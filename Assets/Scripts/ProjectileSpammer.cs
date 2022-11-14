using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpammer : MonoBehaviour
{
    [SerializeField] private GameObject projectileTemplate;

    [SerializeField, Range(0.1f, 100)] private float rateOfFire;
    private float shootTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = (1 / rateOfFire);
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = (1 / rateOfFire);
            Instantiate(projectileTemplate, transform.position, transform.rotation);
        }
    }
}
