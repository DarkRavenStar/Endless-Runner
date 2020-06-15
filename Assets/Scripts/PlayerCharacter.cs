using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    GameObject runCollision;

    [SerializeField]
    GameObject slideCollision;

    [SerializeField]
    GameObject jumpCollision;

    [SerializeField]
    GameObject deathCollision;

    public bool isDead = false;
    public float speed = 1.0f;

    [SerializeField]
    bool isJumping = false;
    public float jumpForce = 10.0f;

    public float deathForce;
    public float deathTime = 3.0f;

    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody2D;

    [SerializeField]
    private bool isSliding = false;

    [SerializeField]
    private float slidingTimer;
    public float slidingTime = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = this.GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        runCollision.SetActive(true);
        slideCollision.SetActive(false);
        jumpCollision.SetActive(false);
        deathCollision.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            if (!isJumping && !isSliding)
            {
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
                {
                    Jump();
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartSliding();
                }
            }
            else if (isSliding)
            {
                slidingTimer += Time.deltaTime;
                if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) || slidingTimer >= slidingTime)
                {
                    ResetSliding();
                    ResetCollision();
                }
            }
        }
    }

    void Jump()
    {
        isJumping = true;
        playerAnimator.SetBool("IsJumping", isJumping);

        jumpCollision.SetActive(true);

        runCollision.SetActive(false);
        slideCollision.SetActive(false);

        playerRigidbody2D.AddForce(new Vector2(0f, jumpForce));
    }

    void ResetJump()
    {
        isJumping = false;
        playerAnimator.SetBool("IsJumping", isJumping);
    }

    void StartSliding()
    {
        isSliding = true;
        playerAnimator.SetBool("IsSliding", isSliding);

        slideCollision.SetActive(true);

        jumpCollision.SetActive(false);
        runCollision.SetActive(false);
    }

    void ResetSliding()
    {
        isSliding = false;
        slidingTimer = 0.0f;
        playerAnimator.SetBool("IsSliding", isSliding);
    }

    void ResetCollision()
    {
        runCollision.SetActive(true);

        slideCollision.SetActive(false);
        jumpCollision.SetActive(false);
    }

    void PlayerDeathCollision()
    {
        runCollision.SetActive(false);
        deathCollision.SetActive(true);
    }

    void PlayerDeath()
    {
        isDead = true;
        GameManager.Instance.isPlayerDead = isDead;

        playerAnimator.SetBool("IsDead", isDead);

        playerRigidbody2D.velocity = Vector2.zero;
        playerRigidbody2D.AddForce(new Vector2(-deathForce, 0.0f));

        StartCoroutine(SceneRestart(deathTime));
    }

    IEnumerator SceneRestart(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isDead)
        {
            if (isJumping && col.gameObject.CompareTag("Ground"))
            {
                ResetJump();
                ResetCollision();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                ResetJump();
                ResetSliding();
                ResetCollision();
                PlayerDeathCollision();
                PlayerDeath();
                Debug.Log("Player Died");
            }
        }
    }
}
