using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfTriggerDestroy : MonoBehaviour
{
    [SerializeField, Range(0,float.MaxValue)] private float delay;
    Collider thisCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    const string KILLMETHOD = "Kill";
    private void OnTriggerEnter(Collider other)
    {
        Invoke(KILLMETHOD, delay);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
