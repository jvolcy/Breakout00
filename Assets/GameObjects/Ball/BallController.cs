using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    public GameObject GameOverText;
    public BlockMaker blockMaker;
    public TMP_Text TxtScore;
    public TMP_Text TxtBlocks;

    int NumBlocks;

    const int BLOCK_HIT_PTS = 10;
    const int BAR_HIT_PTS = 20;

    int Score = 0;
    int numBlocksHit = 0;

    float HorzSpeed;// = 4.2f;
    float VertSpeed; // = -2.8f;

    float BallSpeed = 7f;
    float BallAngle = -45f * Mathf.Deg2Rad;
    float BallAngleRandomAmp = 10 * Mathf.Deg2Rad;
    float BallMinAngle = 30 * Mathf.Deg2Rad;
    float BallMaxAngle = 60 * Mathf.Deg2Rad;

    int avoidBackToBackCollison = 0;
    bool bGameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        NumBlocks = blockMaker.NumOddCols * blockMaker.NumOddRows;

        HorzSpeed = BallSpeed * Mathf.Cos(BallAngle);
        VertSpeed = BallSpeed * Mathf.Sin(BallAngle);

        UpdateBlocks();
        UpdateScore();

 
        GameOverText.SetActive(false);
    }

    // Update is called once per frame
    Color32 color1 = Color.white;
    Color32 color2 = Color.white;
    void Update()
    {
        if (bGameOver)
        {
            return;
        }

        transform.Translate(new Vector2(HorzSpeed*Time.deltaTime, VertSpeed*Time.deltaTime));
        if (avoidBackToBackCollison > 0) avoidBackToBackCollison--;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        
        if (avoidBackToBackCollison > 0)
        {
            return;
        }

        if (HorzSpeed > 0 && VertSpeed > 0)
        {
            BallAngle = Mathf.Atan2(VertSpeed, HorzSpeed);
            BallAngle += Random.Range(-BallAngleRandomAmp, BallAngleRandomAmp);
            BallAngle = Mathf.Clamp(BallAngle, BallMinAngle, BallMaxAngle);
            HorzSpeed = BallSpeed * Mathf.Cos(BallAngle);
            VertSpeed = BallSpeed * Mathf.Sin(BallAngle);
            //Debug.Log("Angle: " + BallAngle * Mathf.Rad2Deg);
        }

        if (other.name == "LeftWall" || other.name == "RightWall")
        {
            HorzSpeed = -HorzSpeed;
        }
        else if (other.name == "TopWall" || other.name == "BottomWall")
        {
            VertSpeed = -VertSpeed;
        }
        else if (other.name == "BlockLeft" || other.name == "BlockRight")
        {
            HorzSpeed = -HorzSpeed;
            Destroy(other.transform.parent.gameObject);
            BlockHit();

        }
        else if (other.name == "BlockTop" || other.name == "BlockBottom")
        {
            VertSpeed = -VertSpeed;
            Destroy(other.transform.parent.gameObject);
            BlockHit();
        }
        else if (other.name == "Bar")
        {
            VertSpeed = -VertSpeed;
            Score += BAR_HIT_PTS;
        }

        UpdateBlocks();
        UpdateScore();
    }

    void BlockHit()
    {
        numBlocksHit++;
        if (numBlocksHit == NumBlocks)
        {
            bGameOver = true;
            GameOverText.SetActive(true);
            BallSpeed = 0f;
            VertSpeed = 0f;
            HorzSpeed = 0f;
        }
        avoidBackToBackCollison = 1;
        Score += BLOCK_HIT_PTS;
    }

    void UpdateScore()
    {
        TxtScore.text = "Score: " + Score.ToString("00000");
    }

    void UpdateBlocks()
    {
        TxtBlocks.text = "Blocks: " + (NumBlocks - numBlocksHit).ToString();
    }


}
