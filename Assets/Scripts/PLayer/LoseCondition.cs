using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCondition : MonoBehaviour
{
    public static string player = "Player";

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == player)
        {
            SceneManager.LoadScene("GAME_OVER");
            Destroy(other.gameObject);
            Debug.Log("GameOver");
        }
    }
}
