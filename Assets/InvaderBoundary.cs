using Unity.Mathematics;
using UnityEngine;

public class InvaderBoundary : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        var invader = collision.GetComponent<Invader>();
        if (invader)
        {
            // If entering from the inside edge, send it back the way it came.
            if (math.abs(collision.transform.position.x) < math.abs(transform.position.x))
            {
                invader.Descend();
            }
        }
    }
}
