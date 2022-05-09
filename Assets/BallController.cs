using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public AudioClip BlockHitSound;
    public AudioClip WallHitSound;
    public AudioClip PlayerHitSound;
    public CameraController cameraController;
    public GameObject StreamerPrefab;
    public GameObject GameOverText;
    public int NumBlocks = 55;

    const int StreamerLength = 50;

    GameObject[] Streamer = new GameObject[StreamerLength];

    int numBlocksHit = 0;

    float HorzSpeed = 0.06f;
    float VertSpeed = -0.04f;
    float HorzSpeedRandomAmp = 0.01f;
    float VertSpeedRandomAmp = 0.01f;
    float MaxHorzSpeedAmp = 0.07f;
    float MaxVertSpeedAmp = 0.07f;

    int avoidBackToBackCollison = 0;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
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
        transform.Translate(new Vector2(HorzSpeed, VertSpeed));

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
            audioSource.clip = PlayerHitSound;
        }

        audioSource.Play();

        HorzSpeed = Mathf.Clamp(HorzSpeed, -MaxHorzSpeedAmp, MaxHorzSpeedAmp);
        VertSpeed = Mathf.Clamp(VertSpeed, -MaxVertSpeedAmp, MaxVertSpeedAmp);
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
    }
}
