using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHideAfterEnable : MonoBehaviour
{
    
    [SerializeField] private float _hideAfter = 2f;
    [SerializeField] private float _hideRate = 2f;
    
    void OnEnable()
    {
        StartCoroutine(HideCoroutine());
    }

    IEnumerator HideCoroutine()
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.alpha = 1f;

        yield return new WaitForSeconds(_hideAfter);

        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * _hideRate;
            group.alpha = alpha;
            yield return null;
        }
    }

}
