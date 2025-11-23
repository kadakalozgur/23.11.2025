using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float detectRange = 50f;
    public float attackRange = 0.5f;

    private Animator anim;
    private Rigidbody2D rb;
    private bool isMoving = false;
    private bool isFacingRight = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Player'ý tag'den bul
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectRange)
        {
            // Yön belirle
            Vector2 direction = (player.position - transform.position).normalized;

            // Flip (doðru yöne baksýn)
            if (direction.x < 0 && !isFacingRight)
                Flip();
            else if (direction.x > 0 && isFacingRight)
                Flip();

            // Player’a doðru hareket
            if (distance > attackRange)
            {
                Vector2 newPos = Vector2.MoveTowards(rb.position, player.position, moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                // Yürüyorsa animasyonu tetikle
                if (!isMoving)
                {
                    anim.ResetTrigger("AttackTrigger");
                    anim.SetTrigger("MoveTrigger");
                    isMoving = true;
                }
            }
            else
            {
                // Saldýrý animasyonu
                if (isMoving)
                {
                    anim.ResetTrigger("MoveTrigger");
                    anim.SetTrigger("AttackTrigger");
                    isMoving = false;
                }
            }
        }

        else
        {
            // Player uzaksa dur
            rb.velocity = Vector2.zero;
            if (isMoving)
            {
                anim.ResetTrigger("MoveTrigger");
                isMoving = false;
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
