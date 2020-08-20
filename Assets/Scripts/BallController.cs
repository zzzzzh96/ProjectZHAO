using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;

    [Range(0.2f,2f)]
    public float moveSpeedModifier;

    float dirX, dirY;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.acceleration.x * moveSpeedModifier;
        dirY = Input.acceleration.y * moveSpeedModifier;
    }

    void FixedUpdate() {

        rb.velocity = new Vector2(rb.velocity.x + dirX, rb.velocity.y + dirY);
    }
}
