using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    public AudioClip BlockHitSound;
    public AudioClip WallHitSound;
    public AudioClip BarHitSound;
    public AudioClip TwinHitSound;
    public CameraController cameraController;
    public GameObject StreamerPrefab;
    public GameObject GameOverText;
    public BlockMaker blockMaker;
    public TMP_Text TxtScore;
    public TMP_Text TxtBlocks;

    int NumBlocks;

    const int StreamerLength = 50;

    GameObject[] Streamer = new GameObject[StreamerLength];

    const int BLOCK_HIT_PTS = 10;
    const int BAR_HIT_PTS = 20;
    const int TWIN_HIT_PTS = -100;

    int Score = 0;
    int numBlocksHit = 0;

    float HorzSpeed = 4.2f;
    float VertSpeed = -2.8f;
    float HorzSpeedRandomAmp = 0.7f;
    float VertSpeedRandomAmp = 0.7f;
    float MaxHorzSpeedAmp = 4.9f;
    float MaxVertSpeedAmp = 4.9f;

    int avoidBackToBackCollison = 0;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        NumBlocks = blockMaker.NumOddCols * blockMaker.NumOddRows;

        UpdateBlocks();
        UpdateScore();

        audioSource = GetComponent<AudioSource>();
        for (int i=0; i< StreamerLength; i++)
        {
            Streamer[i] = Instantiate(StreamerPrefab);
            Streamer[i].transform.position = transform.position;
            Streamer[i].transform.localScale = Streamer[i].transform.localScale * (StreamerLength-i) / StreamerLength;  //Streamer 0 is full size, streamer StreamerLength-1 is 1/StreamerLength full size.
        }

        GameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(HorzSpeed*Time.deltaTime, VertSpeed*Time.deltaTime));

        //stream
        for (int i= StreamerLength-1; i>0; i--)
        {
            Streamer[i].transform.position = Streamer[i - 1].transform.position;
        }
        Streamer[0].transform.position = transform.position;

        if (avoidBackToBackCollison > 0) avoidBackToBackCollison--;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        
        if (avoidBackToBackCollison > 0)
        {
            return;
        }
        
        if (other.name == "LeftWall" || other.name == "RightWall")
        {
            HorzSpeed = -HorzSpeed;
            HorzSpeed += Random.Range(-HorzSpeedRandomAmp, HorzSpeedRandomAmp);
            audioSource.clip = WallHitSound;
        }
        else if (other.name == "TopWall" || other.name == "BottomWall")
        {
            VertSpeed = -VertSpeed;
            VertSpeed += Random.Range(-VertSpeedRandomAmp, VertSpeedRandomAmp);
        }

        if (other.name == "BlockLeft" || other.name == "BlockRight")
        {
            HorzSpeed = -HorzSpeed;
            HorzSpeed += Random.Range(-HorzSpeedRandomAmp, HorzSpeedRandomAmp);
            Destroy(other.transform.parent.gameObject);
            BlockHit();
        }
        else if (other.name == "BlockTop" || other.name == "BlockBottom")
        {
            VertSpeed = -VertSpeed;
            VertSpeed += Random.Range(-VertSpeedRandomAmp, VertSpeedRandomAmp);
            Destroy(other.transform.parent.gameObject);
            BlockHit();
        }

        if (other.name == "Bar")
        {
            VertSpeed = -VertSpeed;
            VertSpeed += Random.Range(-VertSpeedRandomAmp, VertSpeedRandomAmp);
            audioSource.clip = BarHitSound;
            Score += BAR_HIT_PTS;
        }

        if (other.name == "Head")
        {
            Score += TWIN_HIT_PTS;
            audioSource.clip = TwinHitSound;
        }

        audioSource.Play();

        HorzSpeed = Mathf.Clamp(HorzSpeed, -MaxHorzSpeedAmp, MaxHorzSpeedAmp);
        VertSpeed = Mathf.Clamp(VertSpeed, -MaxVertSpeedAmp, MaxVertSpeedAmp);

        UpdateBlocks();
        UpdateScore();

    }

    void BlockHit()
    {
        audioSource.clip = BlockHitSound;
        cameraController.Shake(0.02f, 2, 0.4f);
        numBlocksHit++;
        if (numBlocksHit == NumBlocks)
        {
            GameOverText.SetActive(true);
            VertSpeed = 0f;
            HorzSpeed = 0f;
        }
        avoidBackToBackCollison = 3;
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
