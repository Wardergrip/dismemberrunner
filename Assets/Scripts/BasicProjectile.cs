using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 30.0f;
    [SerializeField]
    private float lifeTime = 10.0f;

    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private ParticleSystem hitExplosion;

    [SerializeField]
    private AudioClip hitSound;

    private bool isQuitting = false;

    // Start is called before the first frame update
    void Awake()
    {
        Invoke(KILL_METHODNAME, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    const string KILL_METHODNAME = "Kill";
    void Kill()
    {
        Destroy(gameObject);
    }

    public int GetDamage
    {
        get { return damage; }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Health targetHealth = collider.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.Damage(damage);
        }
        Kill();
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            if (hitExplosion)
            {
                Instantiate(hitExplosion, transform.position,transform.rotation);
                if (hitSound)
                    AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }
        }
       
    }
}
