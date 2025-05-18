using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration  = 1f;

    public void FadeIn() => StartCoroutine(Fade(0f, 1f));
    public void FadeOut() => StartCoroutine(Fade(1f, 0f));

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;             // 경과한 시간

        while (elapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        canvasGroup.interactable = (endAlpha == 1f);
        canvasGroup.blocksRaycasts = (endAlpha == 1f);

    }
}
