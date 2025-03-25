using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
     public static FadeManager instance;

    public Image fadeOverlay;
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        // Ensure fadeOverlay is assigned
        if (fadeOverlay == null)
        {
            Debug.LogError("FadeManager: fadeOverlay is not assigned!");
        }
    }

    public IEnumerator FadeOut()
    {
        if (fadeOverlay == null) yield break;

        fadeOverlay.gameObject.SetActive(true);
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeOverlay.color = new Color(0, 0, 0, 1);
    }

    public IEnumerator FadeIn()
    {
        if (fadeOverlay == null) yield break;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeOverlay.color = new Color(0, 0, 0, 0);
        fadeOverlay.gameObject.SetActive(false);
    }

    public void SetOverlayAlpha(float alpha)
    {
        if (fadeOverlay != null)
        {
            fadeOverlay.color = new Color(0, 0, 0, alpha);
        }
    }
}
