using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnPrefab;
    public float spawnInterval = 0.6f;
    public int maxSpawnCount = 50;
    public Score score;

    private float lastSpawnTime = 0;
    private int spawnCounter = 0;

    void FixedUpdate()
    {
        var timeSinceLastSpawn = Time.fixedTime - lastSpawnTime;

        if (spawnCounter < maxSpawnCount && timeSinceLastSpawn >= spawnInterval)
        {
            lastSpawnTime = Time.fixedTime;
            spawnCounter++;
            var invader = Instantiate(spawnPrefab, transform.position, transform.rotation);
            invader.GetComponent<Invader>().score = score;
        }

        AccelerateInvaders(Time.fixedDeltaTime);
    }

    void AccelerateInvaders(float timeDelta)
    {
        Invader.Speed += Invader.Acceleration * timeDelta;
    }
}
