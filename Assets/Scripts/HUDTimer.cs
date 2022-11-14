using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

using System;

public class HUDTimer : MonoBehaviour
{
    static private Dictionary<string, float> timePerLevel;

    private TextMeshProUGUI tmp;
    private float totalSeconds;
    private TimeSpan timeSpan;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        totalSeconds += Time.deltaTime;
        timeSpan = TimeSpan.FromSeconds(totalSeconds);
        tmp.text = timeSpan.ToString("mm':'ss':'ff");
    }

    private void OnDestroy()
    {
#if false
        if (timePerLevel.ContainsKey(SceneManager.GetActiveScene().name))
        {
            if (timePerLevel.TryGetValue(SceneManager.GetActiveScene().name, out float time))
            {
                if (time < totalSeconds)
                {
                    timePerLevel[SceneManager.GetActiveScene().name] = time;
                }
            }
        }
        else 
        { 
            timePerLevel.Add(SceneManager.GetActiveScene().name, totalSeconds);
        }
#endif
    }

    public float TotalSeconds
    {
        get { return totalSeconds; }
    }

    public Dictionary<string, float> TimePerLevel
    {
        get { return timePerLevel; }
    }
}
