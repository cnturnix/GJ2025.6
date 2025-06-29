using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    public bool CanMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (CanMove)
        {
            float moveX = Input.GetAxisRaw("Horizontal"); // 获取水平输入（-1, 0, 1）
            float moveY = Input.GetAxisRaw("Vertical");   // 获取垂直输入（-1, 0, 1）
            if (moveX < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
            
            Vector2 movement = new Vector2(moveX, moveY).normalized; // 防止对角线速度过快
            rb.velocity = movement * moveSpeed;
            if(rb.velocity.magnitude!=0)anim.SetBool("isMove", true);
            else anim.SetBool("isMove", false);
        }

    }
}
