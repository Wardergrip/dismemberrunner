using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PercentageBar : MonoBehaviour
{
    [SerializeField] Image healthBar;

    [SerializeField] private GameObject player;
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (player) playerHealth = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar && playerHealth)
        {
            healthBar.transform.localScale = new Vector3(playerHealth.ShieldPercentage, 1.0f, 1.0f);
        }
    }
}
