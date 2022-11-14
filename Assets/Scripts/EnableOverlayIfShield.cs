using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class EnableOverlayIfShield : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Health playerHealth;

    [SerializeField] private Image overlay;
    [SerializeField] private TextMeshProUGUI overlayText;

    private void Start()
    {
        playerHealth = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth)
        {
            overlay.enabled = playerHealth.IsShieldActive;
            if (overlayText) overlayText.enabled = playerHealth.IsShieldActive;
        }
    }
}
