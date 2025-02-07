using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunTimer : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI timerText; // Assign a UI Text component in the Inspector

    [Header("Player & End Condition")]
    public Transform player; // Assign the player object
    public Transform endPoint; // Assign the end-of-maze object
    private PlayerMove pMove;

    public float elapsedTime = 0f;
    [SerializeField] private bool isTimerRunning = false;
    private Vector3 lastPlayerPosition;
    private bool playerHasMoved = false;

    private void Start()
    {
        lastPlayerPosition = player.position;
        UpdateTimerUI();
        pMove = FindAnyObjectByType<PlayerMove>();
    }

    private void Update()
    {
        // Detect if player has moved
        if (!playerHasMoved && Vector3.Distance(player.position, lastPlayerPosition) > 0.1f)
        {
            playerHasMoved = true;
            isTimerRunning = true;
        }

        // Run the timer only if it has started
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();

            // Stop the timer if player reaches the end
            if (Vector3.Distance(player.position, endPoint.position) < 1f)
            {
                isTimerRunning = false;
                TimerComplete();
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText)
        {
            timerText.text = "Time: " + elapsedTime.ToString("F2") + "s";
        }
    }

    public void PlayerDied()
    {
        isTimerRunning = false;
        Debug.Log("Player died! Timer stopped at " + elapsedTime.ToString("F2") + " seconds.");
    }

    void TimerComplete()
    {
        Debug.Log("Player reached the end! Time taken: " + elapsedTime.ToString("F2") + " seconds.");
    }




}


/*
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("UI Elements")]
    public Text timerText; // Assign a UI Text component in the Inspector

    [Header("Player & End Condition")]
    public Transform player; // Assign the player object
    public Transform endPoint; // Assign the end-of-maze object

    private float elapsedTime = 0f;
    private bool isTimerRunning = false;
    private bool playerHasMoved = false;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        lastPlayerPosition = player.position;
        UpdateTimerUI();
    }

    private void Update()
    {
        // Detect if player has moved
        if (!playerHasMoved && Vector3.Distance(player.position, lastPlayerPosition) > 0.1f)
        {
            playerHasMoved = true;
            isTimerRunning = true;
        }

        // Run the timer only if it has started
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();

            // Stop the timer if player reaches the end
            if (Vector3.Distance(player.position, endPoint.position) < 1f)
            {
                isTimerRunning = false;
                TimerComplete();
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText)
        {
            timerText.text = "Time: " + elapsedTime.ToString("F2") + "s";
        }
    }

    public void PlayerDied()
    {
        isTimerRunning = false;
        Debug.Log("Player died! Timer stopped at " + elapsedTime.ToString("F2") + " seconds.");
    }

    void TimerComplete()
    {
        Debug.Log("Player reached the end! Time taken: " + elapsedTime.ToString("F2") + " seconds.");
    }
}

*/
