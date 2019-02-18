using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class OrbManager : MonoBehaviour
{
    private const int ORB_POINT = 100;

    private GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        
    }

    public void GetOrb()
    {
        gameManager.GetComponent<GameManager>().AddScore(ORB_POINT);

        var circleCollider = GetComponent<CircleCollider2D>();
        Destroy(circleCollider);

        transform.DOScale(2.5f, .3f);
        var spriteRenderer = transform.GetComponent<SpriteRenderer>();
        DOTween.ToAlpha(() => spriteRenderer.color, a => spriteRenderer.color = a, .0f, .3f);

        Destroy(this.gameObject, .5f);
    }
}
