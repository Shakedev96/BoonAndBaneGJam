using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerUpUI : MonoBehaviour
{
    public static PowerUpUI instance;
    
    [Header("UI Elements")]
    public Image powerUpBar;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ActivatePowerUp(float duration)
    {
        StartCoroutine(FillBar(duration));
    }

    private IEnumerator FillBar(float duration)
    {
        float elapsedTime = 0f;
        powerUpBar.fillAmount = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            powerUpBar.fillAmount = 1f - (elapsedTime / duration);
            yield return null;
        }

        powerUpBar.fillAmount = 0f;
    }

}
/*
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    public static PowerUpUI instance;
    
    [Header("UI Elements")]
    public Image powerUpBar;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ActivatePowerUp(float duration)
    {
        StartCoroutine(FillBar(duration));
    }

    private IEnumerator FillBar(float duration)
    {
        float elapsedTime = 0f;
        powerUpBar.fillAmount = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            powerUpBar.fillAmount = 1f - (elapsedTime / duration);
            yield return null;
        }

        powerUpBar.fillAmount = 0f;
    }
}

*/
