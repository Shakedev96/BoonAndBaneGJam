using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActivateBane : MonoBehaviour
{
    [Header("Boons UI")]
    public Image speedBoonBar;
    public Image doubleJumpBoonBar;
    public Image dashBoonBar;

    [Header("Banes UI")]
    public Image blurryVisionBaneBar;
    public Image frictionlessBaneBar;
    public Image speedReductionBaneBar;

    private Dictionary<BoonType, Coroutine> activeBoons = new Dictionary<BoonType, Coroutine>();
    private Dictionary<BaneType, Coroutine> activeBanes = new Dictionary<BaneType, Coroutine>();

    public enum BoonType { Speed, DoubleJump, Dash }
    public enum BaneType { BlurryVision, Frictionless, SpeedReduction }

    // 🌟 *Boons System*
    public void ActivateBoon(BoonType boon, float duration)
    {
        if (activeBoons.ContainsKey(boon))
        {
            StopCoroutine(activeBoons[boon]);
            activeBoons.Remove(boon);
        }

        Coroutine boonCoroutine = StartCoroutine(BoonTimer(boon, duration));
        activeBoons[boon] = boonCoroutine;
    }

    private IEnumerator BoonTimer(BoonType boon, float duration)
    {
        Image boonBar = GetBoonBar(boon);
        if (boonBar == null) yield break;

        boonBar.gameObject.SetActive(true);
        float timer = duration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            boonBar.fillAmount = timer / duration;
            yield return null;
        }

        DeactivateBoon(boon);
    }

    public void DeactivateBoon(BoonType boon)
    {
        Image boonBar = GetBoonBar(boon);
        if (boonBar != null)
        {
            boonBar.gameObject.SetActive(false);
            boonBar.fillAmount = 1f; // Reset bar for next use
        }
        activeBoons.Remove(boon);
    }

    private Image GetBoonBar(BoonType boon)
    {
        switch (boon)
        {
            case BoonType.Speed: return speedBoonBar;
            case BoonType.DoubleJump: return doubleJumpBoonBar;
            case BoonType.Dash: return dashBoonBar;
            default: return null;
        }
    }

    //  *Banes System*
    public void ActivateBan(BaneType bane, float duration)
    {
        if (activeBanes.ContainsKey(bane))
        {
            StopCoroutine(activeBanes[bane]);
            activeBanes.Remove(bane);
        }

        Coroutine baneCoroutine = StartCoroutine(BaneTimer(bane, duration));
        activeBanes[bane] = baneCoroutine;
    }

    private IEnumerator BaneTimer(BaneType bane, float duration)
    {
        Image baneBar = GetBaneBar(bane);
        if (baneBar == null) yield break;

        baneBar.gameObject.SetActive(true);
        float timer = duration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            baneBar.fillAmount = timer / duration;
            yield return null;
        }

        baneBar.gameObject.SetActive(false);
        activeBanes.Remove(bane);
    }

    private Image GetBaneBar(BaneType bane)
    {
        switch (bane)
        {
            case BaneType.BlurryVision: return blurryVisionBaneBar;
            case BaneType.Frictionless: return frictionlessBaneBar;
            case BaneType.SpeedReduction: return speedReductionBaneBar;
            default: return null;
        }
    }

}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    
}

*/