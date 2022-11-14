using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    [Header("Gun Physical properties")]
    [SerializeField]
    private Transform gunOutput;
    [SerializeField]
    private GameObject gunVisuals;

    [Header("Gun Properties")]
    [SerializeField]
    [Range(1,100)]
    private int maxAmmo;
    private int currentAmmo;
    [SerializeField]
    [Range(0,10)]
    private float reloadTime;
    [SerializeField]
    [Range(0.1f,100)]
    private float rateOfFire;
    private float shootTimer = 0;
    [SerializeField]
    private bool automaticReload = true;
    private bool isReloading = false;
    [SerializeField,Tooltip("Every bullet shot requires a button press.")]
    private bool requireNewInput = false;
    private bool justShot = false;

    [Header("Resources")]
    public GameObject projectileTemplate;
    [SerializeField]
    private ParticleSystem muzzleFlash;

    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip noAmmoSound;
    public AudioClip reloadSound;
    private float noAmmoSoundPlayedTimer = 0;

    private void Awake()
    {
        if (gunOutput == null)
        {
            Debug.Log("gunOutput not assigned");
        }
        if (projectileTemplate == null)
        {
            Debug.Log("no projectile assigned");
        }
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        if (automaticReload)
        {
            if (currentAmmo <= 0)
            {
                Reload();
            }
        }
        if (isReloading)
        {
            ReloadAnim();
        }
        if (noAmmoSoundPlayedTimer <= 0)
        {
            noAmmoSoundPlayedTimer -= Time.deltaTime;
        }
    }

    public void TryShoot(string AxisName, Vector3 desiredPoint, bool raycastHit)
    {
        if (isReloading) return;

        // Check if input is valid
        if (Input.GetAxis(AxisName) > 0)
        {
            if (requireNewInput)
            {
                if (justShot)
                {
                    return;
                }
                justShot = true;
            }
        }
        else
        {
            justShot = false;
            return;
        }

        // Input is valid and out of ammo
        if (currentAmmo <= 0)
        {
            if (noAmmoSound)
            {
                if (noAmmoSoundPlayedTimer <= 0)
                {
                    audioSource.PlayOneShot(noAmmoSound);
                    noAmmoSoundPlayedTimer = noAmmoSound.length;
                }
            }
            return;
        }

        // Input is valid and enough time past
        if (shootTimer <= 0)
        {
            shootTimer = (1.0f / GetRateOfFire);

            if (raycastHit)
            {
                gunOutput.LookAt(desiredPoint);
            }
            else
            {
                //gunOutput.localRotation = Quaternion.identity;
            }
            Instantiate(projectileTemplate, gunOutput.position, gunOutput.rotation);
            Instantiate(muzzleFlash, gunOutput.position,gunOutput.rotation);
            --currentAmmo;
            if (shootSound)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }

    public void Reload()
    {
        if (isReloading) return;

        Invoke(nameof(InstantReload), reloadTime);
        isReloading = true;
        if (reloadSound)
        {
            audioSource.PlayOneShot(reloadSound);
        }
    }

    private void InstantReload()
    {
        currentAmmo = maxAmmo;
        shootTimer = 0;
        isReloading = false;
        transform.localRotation = Quaternion.identity;
    }

    private void ReloadAnim()
    {
        float rotationSpeed = 360.0f / reloadTime;
        transform.Rotate(rotationSpeed*Time.deltaTime, 0, 0, Space.Self);
    }

    #region ReadOnlyProps

    public int GetCurrentAmmo
    {
        get { return currentAmmo; }
    }
    public int GetMaxAmmo
    {
        get { return maxAmmo; }
    }
    public float GetReloadTime
    {
        get { return reloadTime; }
    }
    public float GetRateOfFire
    {
        get { return rateOfFire; }
    }
    public bool IsReloading
    {
        get { return isReloading; }
    }
    #endregion
}
