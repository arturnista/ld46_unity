using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    
    private CanvasGroup _group;

    private bool _isRunning = false;

    void Awake()
    {
        _group = GetComponent<CanvasGroup>();
    }

    public void Show(float time = 1f, float delay = 0f, bool force = false)
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(ShowCoroutine(time, delay, force));
    }

    public IEnumerator ShowCoroutine(float time = 1f, float delay = 0f, bool force = false)
    {
        _group = GetComponent<CanvasGroup>();

        if (!_isRunning)
        {
            _isRunning = true;

            float alpha = force ? 0f : _group.alpha;
            _group.alpha = alpha;

            yield return new WaitForSeconds(delay);

            float rate = 1f / time;
            while (alpha < 1f)
            {
                alpha += rate * Time.deltaTime;
                _group.alpha = alpha;
                yield return null;
            }
            
            _isRunning = false;
        }
    }

    public void Hide(float time = 1f, float delay = 0f, bool force = false, bool keepActive = false)
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(HideCoroutine(time, delay, force, keepActive));
    }

    public IEnumerator HideCoroutine(float time = 1f, float delay = 0f, bool force = false, bool keepActive = false)
    {
        _group = GetComponent<CanvasGroup>();

        float alpha = force ? 1f : _group.alpha;
        _group.alpha = alpha;

        yield return new WaitForSeconds(delay);

        float rate = 1f / time;
        while (alpha > 0f)
        {
            alpha -= rate * Time.deltaTime;
            _group.alpha = alpha;
            yield return null;
        }

        if (!keepActive) gameObject.SetActive(false);
    }

}
