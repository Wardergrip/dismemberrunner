using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField] private Transform gunSocket;
    [SerializeField] private GameObject mainCamera;

    [Header("Gun")]
    [SerializeField]
    private GameObject gunTemplate;

    private GameObject gun;
    private BasicGun gunScript;

    private const string SHOOT_BUTTON = "Fire1";
    private const string RELOAD_BUTTON = "Reload";

    private void Start()
    {
        if (gunTemplate == null)
        {
            Debug.Log("gunTemplate Missing");
            return;
        }
        if (gunSocket == null)
        {
            Debug.Log("gunSocket Missing");
            return;
        }

        gun = Instantiate(gunTemplate, gunSocket);
        gunScript = gun.GetComponent<BasicGun>();
    }

    private void Update()
    {
        RaycastHit hitInfo;
        bool raycastHit = false;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo))
        {
            raycastHit = true;
        }
        gunScript.TryShoot(SHOOT_BUTTON,hitInfo.point,raycastHit);
        if (Input.GetAxis(RELOAD_BUTTON) > 0)
        {
            gunScript.Reload();
        }
    }
}
