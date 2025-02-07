using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    public static string player = "Player";

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == player)
        {
            Destroy(other.gameObject);
            Debug.Log("GameOver");
        }
    }
}
