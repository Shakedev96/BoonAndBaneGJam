using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private RunTimer runTime;

    public float currentTime, bestTime;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        runTime = FindAnyObjectByType<RunTimer>();
        currentTime = runTime.elapsedTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHighScore()
    {
        if(currentTime < bestTime)
        {
            bestTime = currentTime;
        }
    }
}
