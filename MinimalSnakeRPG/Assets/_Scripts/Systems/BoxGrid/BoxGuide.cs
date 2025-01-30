using System;
using System.Collections;
using UnityEngine;

public class BoxGuide : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Coroutine _coroutine;
    private bool _isShowMoveableArea;
    private Color _originalColor;

    private void Start()
    {
        _originalColor = spriteRenderer.color;
    }

    public void ShowMoveableArea(bool isShow)
    {
        if (_isShowMoveableArea == isShow)
        {
            return;
        }

        _isShowMoveableArea = isShow;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ShowMoveableAreaCoroutine());
    }

    private IEnumerator ShowMoveableAreaCoroutine()
    {
        while (_isShowMoveableArea)
        {
            spriteRenderer.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = _originalColor;
            yield return new WaitForSeconds(0.5f);
        }

        spriteRenderer.color = _originalColor;
    }
}