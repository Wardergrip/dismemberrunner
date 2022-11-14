using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnableImageTemp : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField, Range(float.Epsilon, 1)] private float disableAfterTime;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            image.enabled = false;
        }
    }

    public void ShowAndStartTimer()
    {
        timer = disableAfterTime;
        image.enabled = true;
    }
}
