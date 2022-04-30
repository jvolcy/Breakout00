using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public AudioClip BlockHitSound;
    public AudioClip WallHitSound;
    public AudioClip PlayerHitSound;
       
    float HorzSpeed = 0.05f;
    float VertSpeed = -0.05f;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(HorzSpeed, VertSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);

        if (other.name == "LeftWall")
        {
            HorzSpeed = -HorzSpeed;
            audioSource.clip = WallHitSound;
        }
        else if (other.name == "RightWall")
        {
            HorzSpeed = -HorzSpeed;
            audioSource.clip = WallHitSound;
        }
        else if (other.name == "TopWall")
        {
            VertSpeed = -VertSpeed;
            audioSource.clip = WallHitSound;
        }
        else if (other.name == "BottomWall")
        {
            VertSpeed = -VertSpeed;
            audioSource.clip = WallHitSound;
        }
        else if (other.name == "Player")
        {
            VertSpeed = -VertSpeed;
            audioSource.clip = PlayerHitSound;
        }
        else    //we hit a block
        {
            VertSpeed = -VertSpeed;
            Destroy(other.gameObject);
            audioSource.clip = BlockHitSound;
        }

        audioSource.Play();

    }

}
