using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float CurrentPower
    {
        get { return currentPower; }
        set
        {
            currentPower = math.min(value, maxPower);
        }
    }

    public float acceleration = 0.1f;
    public float maxSpeed = 3.0f;

    public float maxPower = 1.0f;
    public float projectilePowerConsumption = 0.2f;
    public float powerRechargeRate = 0.3f;
    public float projectileFireInterval = 0.2f;

    public GameObject projectilePrefab;
    public GameObject explosionPrefab;

    private Rigidbody2D body;
    private Vector2 inputVector = Vector2.zero;
    private float currentPower;
    private bool isFiring = false;
    private float lastShotTime = 0;

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();
        this.currentPower = maxPower;
    }

    void FixedUpdate()
    {
        this.body.velocityX = math.min(this.body.velocityX + inputVector.x * this.acceleration, this.maxSpeed);
        this.CurrentPower += this.powerRechargeRate * Time.fixedDeltaTime;
    }

    async void Update()
    {
        if (isFiring)
        {
            await Fire();
        }
    }

    public async Task Fire()
    {
        var timeSinceLastShot = Time.fixedTime - lastShotTime;
        if (timeSinceLastShot < projectileFireInterval)
        {
            return;
        }

        if (currentPower < projectilePowerConsumption)
        {
            return;
        }

        currentPower -= projectilePowerConsumption;
        lastShotTime = Time.fixedTime;
        await InstantiateAsync(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
    }

    void OnMove(InputValue input)
    {
        this.inputVector = input.Get<Vector2>();
    }

    void OnAttack(InputValue input)
    {
        isFiring = input.isPressed;
    }

    void OnExit(InputValue input)
    {
        if (input.isPressed)
        {
            Application.Quit();
        }
    }

    async void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("alien"))
        {
            await InstantiateAsync(explosionPrefab, this.transform);
            Destroy(gameObject);
        }
    }
}
