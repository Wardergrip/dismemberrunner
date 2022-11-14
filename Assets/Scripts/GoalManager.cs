using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    [SerializeField] private string sceneToBeLoaded;
    [SerializeField] private float delay;
    private bool goalAssigned = false;

    // Start is called before the first frame update
    void Start()
    {
        if (goal != null)
        {
            goalAssigned = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!goalAssigned) return;
        if (goal == null)
        {
            Invoke(nameof(LoadScene),delay);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToBeLoaded);
    }

    public GameObject Goal
    {
        get { return goal; }
        set { goal = value; }
    }
}
