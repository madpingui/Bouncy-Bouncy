using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour {

    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private BlockSpawner blockSpawner;

    public LineRenderer lineRend;

    public Transform launchPoint;

    public GameObject returnBall;

    [SerializeField]
    private Transform bolasParent;

    private int ballsReady;

    private int nroBolas;

    [SerializeField]
    private GameObject ballPref;

    private bool listoLanzamiento;

    public Text scoreText;
    public int score;
    public Text hiscoreText;

    private Vector3 posicionLanzamiento;


    private void Awake()
    {
        hiscoreText.text = PlayerPrefs.GetInt("Hiscore").ToString();
        blockSpawner = FindObjectOfType<BlockSpawner>();
    }

    public void ReturnBall(GameObject bolaChocada)
    {
        ballsReady++;
        if(ballsReady == 1)
        {
            posicionLanzamiento = bolaChocada.transform.position;
        }

        if (ballsReady == bolasParent.childCount)
        {
            returnBall.SetActive(false);
            listoLanzamiento = false;
            transform.parent.parent.position = new Vector3(posicionLanzamiento.x,transform.parent.parent.position.y,transform.parent.parent.position.z); 
            blockSpawner.SpawnRowOfBlocks();

            foreach (Transform ball in bolasParent)
            {
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    public void returnBalls()
    {
        Invoke("returnBalls2", 0.3f);
    }

    public void returnBalls2()
    {
        foreach (Transform ball in bolasParent)
        {
            ball.gameObject.SetActive(false);
            ReturnBall(ball.gameObject);
        }
    }

    private void CreateBall()
    {
        GameObject ball = Instantiate(ballPref, launchPoint.position,Quaternion.identity);
        ball.transform.parent = bolasParent.transform;
    }

    private void Update ()
    {
        if(listoLanzamiento == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startDragPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                endDragPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                
                float dist = Vector3.Distance(startDragPosition, endDragPosition);

                lineRend.SetPosition(0, new Vector3(0, dist*15, 0));

                float angle = Mathf.Atan2(startDragPosition.y - endDragPosition.y, startDragPosition.x - endDragPosition.x) * 180 / Mathf.PI;

                if(angle > 160)
                {
                    angle = 160;
                }
                else if(angle < 20)
                {
                    angle = 20;
                }

                transform.localRotation = Quaternion.Euler(new Vector3(-angle+90,0,0));
            }
            else if (Input.GetMouseButtonUp(0))
            {
                lineRend.SetPosition(0, Vector3.zero);
                EndDrag();
            }
        }
	}

    private void EndDrag()
    {
        returnBall.SetActive(true);
        CreateBall();
        StartCoroutine(LaunchBalls());
        listoLanzamiento = true;
    }

    private IEnumerator LaunchBalls()
    {
        foreach (Transform ball in bolasParent)
        {
            ball.transform.position = launchPoint.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody>().AddForce(launchPoint.up);
            yield return new WaitForSeconds(0.1f);
        }

        ballsReady = 0;
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
