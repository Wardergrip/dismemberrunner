using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int startHealth = 10;
    [SerializeField] private int currentHealth = 0;

    [Header("Shield")]
    [SerializeField] private bool hasShield = false;
    private bool shieldActive = false;
    private bool shieldOverloaded = false;
    [SerializeField] private float shieldMaxEnergy = 1.5f;
    private float shieldEnergy;
    [SerializeField][Range(0, 10)] private float shieldDrainSpeed = 1;
    [SerializeField][Range(0, 10)] private float shieldGainSpeed = 1;

    [Header("Feedback")]
    [SerializeField] GameObject damageOverlay;
    EnableImageTemp damageFeedback;

    public float HealthPercentage
    {
        get { return ((float)currentHealth / startHealth); }
    }

    public bool IsShieldActive
    {
        get { return shieldActive; }
    }

    public float ShieldPercentage
    {
        get { return (shieldEnergy / shieldMaxEnergy); }
    }

    private void Awake()
    {
        currentHealth = startHealth;
        shieldEnergy = shieldMaxEnergy;
        if (damageOverlay)
            damageFeedback = damageOverlay.GetComponent<EnableImageTemp>();
    }

    private void Update()
    {
        if (!hasShield) return;

        if (Input.GetAxis("Shield") > 0)
        {
            if (shieldEnergy > 0)
            {
                shieldActive = true;
                shieldEnergy -= Time.deltaTime * shieldDrainSpeed;
            }
            else
            {
                shieldActive = false;
                shieldOverloaded = true;
            }
        }
        else
        {
            shieldActive = false;
            shieldOverloaded = false;
        }

        if (!shieldActive && !shieldOverloaded)
        {
            if (shieldEnergy < shieldMaxEnergy)
            {
                shieldEnergy += Time.deltaTime * shieldGainSpeed;
            }
            else shieldEnergy = shieldMaxEnergy;
        }
    }

    public void Damage(int amount)
    {
        if (shieldActive) return;
        currentHealth -= amount;

        if (damageFeedback)
            damageFeedback.ShowAndStartTimer();

        if (currentHealth <= 0)
            Kill();
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
