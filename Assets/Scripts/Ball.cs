using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update ()
    {
        rb.velocity = rb.velocity.normalized * moveSpeed;
    }
}
