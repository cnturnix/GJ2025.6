using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public bool CanMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (CanMove)
        {
            float moveX = Input.GetAxisRaw("Horizontal"); // 获取水平输入（-1, 0, 1）
            float moveY = Input.GetAxisRaw("Vertical");   // 获取垂直输入（-1, 0, 1）

            Vector2 movement = new Vector2(moveX, moveY).normalized; // 防止对角线速度过快
            rb.velocity = movement * moveSpeed;
        }

    }
}
