using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

    [SerializeField]
    private Block blockPrefab;

    public static int playWidth;
    private float distandeBetweenBlocks = 0.85f;
    private int rowsSpawned;

    private BallLauncher bS;

    private List<Block> blocksSpawned = new List<Block>();

    private void Awake()
    {
        bS = FindObjectOfType<BallLauncher>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < 1; i++)
        {
            SpawnRowOfBlocks();
        }
    }

    public void SpawnRowOfBlocks()
    {
        foreach (var block in blocksSpawned)
        {
            if(block != null)
            {
                block.transform.position += Vector3.down * distandeBetweenBlocks;
                if(block.transform.position.y < bS.transform.position.y + 0.575f)
                {
                    PlayerPrefs.SetInt("Hiscore", bS.score);
                    GameManager.Instance.perder();
                }
            }
        }

        for (int i = 0; i < playWidth*1.15f; i++)
        {
            if(UnityEngine.Random.Range(0,100) <= 30)
            {
                Block go = Instantiate(blockPrefab, GetPosition(i), Quaternion.identity);
                int hits = UnityEngine.Random.Range(1, 3) + rowsSpawned;

                go.SetHits(hits);

                blocksSpawned.Add(go);
            }
        }

        rowsSpawned++;
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * distandeBetweenBlocks;
        return position;
    }
}
