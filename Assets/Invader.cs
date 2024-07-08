using Unity.Mathematics;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public enum Direction
    {
        LEFT = -1,
        RIGHT = 1,
    }

    public static float Speed { get; set; } = 3.0f;
    public static float Acceleration { get; set; } = 0.08f;

    public Direction direction = Direction.RIGHT;
    public GameObject explosionPrefab;
    public Score score;

    private Rigidbody2D body;
    private float targetY;

    public void Descend()
    {
        this.targetY -= 1;

        if (this.direction == Direction.LEFT)
        {
            this.direction = Direction.RIGHT;
        }
        else
        {
            this.direction = Direction.LEFT;
        }
    }

    void OnDestroy()
    {
        score.Value++;
    }

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();
        this.targetY = this.body.position.y;
    }

    void FixedUpdate()
    {
        var deltaY = targetY - this.body.position.y;

        if (math.abs(deltaY) > 0.1f)
        {
            this.body.velocityX = 0;
            this.body.velocityY = Speed * math.sign(deltaY);
        }
        else
        {
            this.body.velocityX = Speed * (int)this.direction;
            this.body.velocityY = 0;
        }
    }

    async void OnCollisionEnter2D(Collision2D collision)
    {
        await InstantiateAsync(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
