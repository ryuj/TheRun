using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    private const int ENEMY_POINT = 50;

    private GameObject gameManager;

    public LayerMask blockLayer;

    private Rigidbody2D rbody;

    private float moveSpeed = 1;

    public enum MOVE_DIR
    {
        LEFT,
        RIGHT,
    }

    private MOVE_DIR moveDirection = MOVE_DIR.LEFT;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        bool isBlock;

        switch (moveDirection)
        {
            case MOVE_DIR.LEFT:
                rbody.velocity = new Vector2(moveSpeed * -1, rbody.velocity.y);
                transform.localScale = new Vector2(1, 1);

                isBlock = Physics2D.Linecast(
                    new Vector2(transform.position.x, transform.position.y + .5f),
                    new Vector2(transform.position.x - .3f, transform.position.y + .5f),
                    blockLayer);

                if (isBlock)
                {
                    moveDirection = MOVE_DIR.RIGHT;
                }
                break;
            case MOVE_DIR.RIGHT:
                rbody.velocity = new Vector2(moveSpeed, rbody.velocity.y);
                transform.localScale = new Vector2(-1, 1);

                isBlock = Physics2D.Linecast(
                    new Vector2(transform.position.x, transform.position.y + .5f),
                    new Vector2(transform.position.x + .3f, transform.position.y + .5f),
                    blockLayer);

                if (isBlock)
                {
                    moveDirection = MOVE_DIR.LEFT;
                }
                break;
        }
    }

    public void DestroyEnemy()
    {
        gameManager.GetComponent<GameManager>().AddScore(ENEMY_POINT);

        rbody.velocity = new Vector2(0, 0);

        var circleCollider = GetComponent<CircleCollider2D>();
        var boxCollider = GetComponent<CircleCollider2D>();
        Destroy(circleCollider);
        Destroy(boxCollider);

        var animSet = DOTween.Sequence();
        animSet.Append(transform.DOLocalMoveY(.5f, .2f).SetRelative());
        animSet.Append(transform.DOLocalMoveY(-10f, 1f).SetRelative());

        Destroy(this.gameObject, .5f);
    }
}
