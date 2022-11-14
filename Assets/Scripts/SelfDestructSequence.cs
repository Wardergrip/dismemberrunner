using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructSequence : MonoBehaviour
{
    [Header("Dismember Manager")]
    [SerializeField]
    private DismemberManager dismemberManager;

    [Header("Self Destruct Sequence")]
    [SerializeField, Range(0.1f, 6.0f)]
    private float sequenceDuration = 3.0f;
    private float sequenceTimer;
    [SerializeField, Range(0.1f, 100.0f)]
    private float flashingFrequency = 1.0f;
    private float currentFlashingFrequency = 0;
    [SerializeField, Range(0.01f, 1)]
    private float speedingUpFactor = 0.9f;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField, Range(0,20)]
    private float explosionRange = 6.0f;
    [SerializeField, Range(0, 10000)]
    private float explosionForce = 10;
    [SerializeField, Range(0, 100)]
    private int damage = 1;
    [SerializeField]
    private GameObject blastSphere;
    [SerializeField]
    private GameObject maxBlastSphere;
    private Vector3 originalSphereScale;
    [SerializeField]
    private AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        flashingFrequency = 1.0f / flashingFrequency;
        currentFlashingFrequency = flashingFrequency;
        originalSphereScale = blastSphere.transform.localScale;
        sequenceTimer = sequenceDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (dismemberManager.AmountOfLimbs == 0)
        {
            sequenceTimer -= Time.deltaTime;
            currentFlashingFrequency -= Time.deltaTime;
            blastSphere.SetActive(true);
            maxBlastSphere.SetActive(true);
            float lerpValue = 1 - (sequenceTimer / sequenceDuration);
            blastSphere.transform.localScale = new Vector3(Mathf.Lerp(originalSphereScale.x, explosionRange, lerpValue), Mathf.Lerp(originalSphereScale.y, explosionRange, lerpValue), Mathf.Lerp(originalSphereScale.z, explosionRange, lerpValue));
            maxBlastSphere.transform.localScale = originalSphereScale * explosionRange;
            if (currentFlashingFrequency <= 0)
            {
                dismemberManager.SwitchFlashingColor();
                flashingFrequency *= speedingUpFactor;
                currentFlashingFrequency = flashingFrequency;
            }
            if (sequenceTimer <= 0)
            {
                if (particles) Instantiate(particles,dismemberManager.heart.transform.position,Quaternion.identity);
                
                Collider[] overlaps = Physics.OverlapSphere(dismemberManager.heart.transform.position, explosionRange);
                foreach (Collider coll in overlaps)
                {
                    Health healthComp = coll.GetComponent<Health>();
                    if (healthComp)
                    {
                        healthComp.Damage(damage);
                        Rigidbody rb = coll.gameObject.GetComponent<Rigidbody>();
                        if (rb)
                        {
                            rb.AddForce(((coll.gameObject.transform.position) - dismemberManager.heart.transform.position).normalized * explosionForce, ForceMode.Impulse);
                        }
                    }
                }
                if (explosionSound)
                    AudioSource.PlayClipAtPoint(explosionSound,transform.position);
                Destroy(gameObject);
            }
        }
    }

    public DismemberManager GetDismemberManager
    {
        get { return dismemberManager; }
    }
}
