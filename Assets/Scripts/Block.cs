using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int hitsRemaining = 5;

    private Renderer mat;
    private TextMeshPro text;

    private BallLauncher bS;

    private void Awake()
    {
        bS = FindObjectOfType<BallLauncher>();
        mat = GetComponent<Renderer>();
        text = GetComponentInChildren<TextMeshPro>();
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        text.SetText(hitsRemaining.ToString());
        mat.material.color = Color.Lerp(Color.white, Color.red, hitsRemaining/10f);
        mat.material.SetColor("_EmissionColor", Color.Lerp(Color.white, Color.red, hitsRemaining / 10f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitsRemaining--;
        if(hitsRemaining > 0)
        {
            UpdateVisualState();
        }
        else
        {
            Destroy(gameObject);
            bS.AddScore();
        }
    }

    internal void SetHits(int hits)
    {
        hitsRemaining = hits;
        UpdateVisualState();
    }
}
