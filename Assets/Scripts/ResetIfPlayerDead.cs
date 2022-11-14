using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetIfPlayerDead : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private int activeSceneBuildIdx;

    // Start is called before the first frame update
    void Start()
    {
        activeSceneBuildIdx = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            SceneManager.LoadScene(activeSceneBuildIdx);
        }
    }
}
