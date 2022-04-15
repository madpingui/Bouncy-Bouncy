using UnityEngine;

public class Walls : MonoBehaviour {

    private BlockSpawner bS;

    public float colDepth = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;
    public Transform topCollider;
    public Transform bottomCollider;
    public Transform leftCollider;
    public Transform rightCollider;
    private Vector3 cameraPos;

    public Material wallMat;
    // Use this for initialization
    void Awake()
    {
        bS = FindObjectOfType<BlockSpawner>();

        ////Generate our empty objects
        //topCollider = new GameObject().transform;
        //bottomCollider = new GameObject().transform;
        //rightCollider = new GameObject().transform;
        //leftCollider = new GameObject().transform;

        //Name our objects 
        topCollider.name = "TopCollider";
        bottomCollider.name = "BottomCollider";
        rightCollider.name = "RightCollider";
        leftCollider.name = "LeftCollider";

        ////Add the colliders
        //topCollider.gameObject.AddComponent<BoxCollider>();
        //bottomCollider.gameObject.AddComponent<BoxCollider>();
        bottomCollider.gameObject.AddComponent<BallReturn>();
        //rightCollider.gameObject.AddComponent<BoxCollider>();
        //leftCollider.gameObject.AddComponent<BoxCollider>();

        topCollider.GetComponent<MeshRenderer>().material = wallMat;
        bottomCollider.GetComponent<MeshRenderer>().material = wallMat;
        leftCollider.GetComponent<MeshRenderer>().material = wallMat;
        rightCollider.GetComponent<MeshRenderer>().material = wallMat;

        ////Make them the child of whatever object this script is on, preferably on the Camera so the objects move with the camera without extra scripting
        //topCollider.parent = transform;
        //bottomCollider.parent = transform;
        //rightCollider.parent = transform;
        //leftCollider.parent = transform;

        //Generate world space point information for position and scale calculations
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        BlockSpawner.playWidth = (int)screenSize.x*2;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        bS.transform.position = new Vector3(-screenSize.x+0.5f, screenSize.y-1.5f, 0);
        bS.SpawnRowOfBlocks();

        //Change our scale and positions to match the edges of the screen...   
        rightCollider.localScale = new Vector3(colDepth, screenSize.y * 2.5f, colDepth);
        rightCollider.position = new Vector3(cameraPos.x + screenSize.x + (rightCollider.localScale.x * 0.5f), cameraPos.y, zPosition);
        leftCollider.localScale = new Vector3(colDepth, screenSize.y * 2.5f, colDepth);
        leftCollider.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.localScale.x * 0.5f), cameraPos.y, zPosition);
        topCollider.localScale = new Vector3(screenSize.x * 2.5f, colDepth, colDepth);
        topCollider.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y-1 + (topCollider.localScale.y * 0.5f), zPosition);
        bottomCollider.localScale = new Vector3(screenSize.x * 2.5f, colDepth, colDepth);
        bottomCollider.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y+0.5f - (bottomCollider.localScale.y * 0.5f), zPosition);

        rightCollider.localRotation = Quaternion.Euler(new Vector3(0, 0, 0.1f));
        leftCollider.localRotation = Quaternion.Euler(new Vector3(0, 0, -0.1f));
    }
    private void Start()
    {
        Camera.main.orthographic = false;
    }
}
