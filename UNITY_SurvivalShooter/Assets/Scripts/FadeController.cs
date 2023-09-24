using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float fadeTime = 0.5f;
    public float waitTime = 1f;
    private float accumTime = 0f;
    private Coroutine fadeCor;

    public bool isFadeInDone { get; private set; } = false;
    public bool isFadeOutDone { get; private set; } = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartFade()
    {
        if(fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        accumTime = 0f;
        while(accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0f,1f,accumTime / fadeTime);
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = 1f;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() // �� ���� �����ص� fadeout�Ƿ��� ��� �ؾ�����?
    {
        yield return new WaitForSeconds(waitTime);
        isFadeInDone = true;
        accumTime = 0f;
        while( accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = 0f;
        isFadeOutDone = true;
    }
}
