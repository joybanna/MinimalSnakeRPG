﻿using System;
using System.Collections;
using UnityEngine;

public class BoxGuide : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Coroutine _coroutine;
    [SerializeField] private bool _isShowMoveableArea;
    private Color _originalColor;
    private Color _currentColor;

    private void Start()
    {
        _originalColor = spriteRenderer.color;
        _currentColor = _originalColor;
    }

    public void ShowMoveableArea(bool isShow)
    {
        _isShowMoveableArea = isShow;
        spriteRenderer.color = _originalColor;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ShowMoveableAreaCoroutine(Color.yellow));
    }

    private IEnumerator ShowMoveableAreaCoroutine(Color color)
    {
        while (_isShowMoveableArea)
        {
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = _originalColor;
            yield return new WaitForSeconds(0.5f);
        }

        spriteRenderer.color = _currentColor;
    }

    public void SetColor(BoxStatus status)
    {
        var color = status switch
        {
            BoxStatus.Empty => Color.white,
            BoxStatus.Hero => Color.cyan,
            BoxStatus.Enemy => Color.yellow,
            BoxStatus.Collectible => Color.green,
            BoxStatus.Obstacle => Color.red,
            BoxStatus.Blind => new Color(1, 1, 1, 0),
            _ => Color.white
        };
        spriteRenderer.color = color;
        _currentColor = color;
    }
}