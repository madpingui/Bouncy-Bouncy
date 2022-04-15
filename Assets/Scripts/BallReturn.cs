using UnityEngine;

public class BallReturn : MonoBehaviour
{
    private BallLauncher ballLauncher;

    private void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ballLauncher.ReturnBall(collision.gameObject);
        collision.collider.gameObject.SetActive(false);
    }
}
