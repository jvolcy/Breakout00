using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMaker : MonoBehaviour
{
    public GameObject BlockPrefab;
    public int NumOddRows = 5;
    public int NumOddCols = 11;
    public float ColSpacing = 1f;
    public float RowSpacing = 0.75f;
    public Vector2 CenterBlockPos = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        MakeBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeBlocks()
    {

        GameObject gameObject;

        for (int j = -NumOddRows / 2; j <= NumOddRows / 2; j++)
        {

            for (int i = -NumOddCols / 2; i <= NumOddCols / 2; i++)
            {
                float XPos = CenterBlockPos.x + i * ColSpacing;
                float YPos = CenterBlockPos.y + j * RowSpacing;
                gameObject = Instantiate(BlockPrefab);
                gameObject.transform.position = new Vector2(XPos, YPos);
            }
        }
    }
}
