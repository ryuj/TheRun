using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject gameManager;

    public LayerMask blockLayer;

    private Rigidbody2D rbody;

    private const float MOVE_SPEED = 3;
    private float moveSpeed;
    private float jumpPower = 400;
    private bool goJump = false;
    private bool canJump = false;
    private bool usingButtons = false;

    public enum MOVE_DIR
    {
        STOP,
        LEFT,
        RIGHT,
    };

    private MOVE_DIR moveDirection = MOVE_DIR.STOP;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        switch (moveDirection)
        {
            case MOVE_DIR.STOP:
                moveSpeed = 0;
                break;
            case MOVE_DIR.LEFT:
                moveSpeed = MOVE_SPEED * -1;
                transform.localScale = new Vector2(-1, 1);
                break;
            case MOVE_DIR.RIGHT:
                moveSpeed = MOVE_SPEED;
                transform.localScale = new Vector2(1, 1);
                break;
        }

        rbody.velocity = new Vector2(moveSpeed, rbody.velocity.y);

        if (goJump)
        {
            rbody.AddForce(Vector2.up * jumpPower);
            goJump = false;
        }

        if (!usingButtons)
        {
            float x = Input.GetAxisRaw("Horizontal");

            if (x == 0)
            {
                moveDirection = MOVE_DIR.STOP;
            }
            else
            {
                if (x < 0)
                {
                    moveDirection = MOVE_DIR.LEFT;
                }
                else
                {
                    moveDirection = MOVE_DIR.RIGHT;
                }
            }

            if (Input.GetKeyDown("space"))
            {
                PushJumpButton();
            }
        }
    }

    void Update()
    {
        canJump =
            Physics2D.Linecast(transform.position - (transform.right * .3f), transform.position - (transform.up * .1f), blockLayer) ||
            Physics2D.Linecast(transform.position + (transform.right * .3f), transform.position - (transform.up * .1f), blockLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.GetComponent<GameManager>().gameMode != GameManager.GAME_MODE.PLAY)
        {
            return;
        }

        if (collision.gameObject.tag == "Trap")
        {
            gameManager.GetComponent<GameManager>().GameOver();
            DestroyPlayer();
        }

        if (collision.gameObject.tag == "Goal")
        {
            gameManager.GetComponent<GameManager>().GameClear();
        }
    }

    private void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }

    public void PushLeftButton()
    {
        moveDirection = MOVE_DIR.LEFT;
        usingButtons = true;
    }

    public void PushRightButton()
    {
        moveDirection = MOVE_DIR.RIGHT;
        usingButtons = true;
    }

    public void PushJumpButton()
    {
        if (canJump)
        {
            goJump = true;
        }
    }

    public void ReleaseMoveButton()
    {
        moveDirection = MOVE_DIR.STOP;
        usingButtons = false;
    }

}
