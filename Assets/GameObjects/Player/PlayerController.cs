using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bar;


    float HorzPos;
    bool bWalking = false;
    float Speed =5f;

    // Start is called before the first frame update
    void Start()
    {
        HorzPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyUp(KeyCode.RightArrow) && (!Input.GetKey(KeyCode.LeftArrow)) ) ||
            (Input.GetKeyUp(KeyCode.LeftArrow) && (!Input.GetKey(KeyCode.RightArrow)) ))
        {
            bWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            bWalking = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bWalking = true;
        }

        if (bWalking)  //not walking
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                HorzPos += Speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorzPos -= Speed * Time.deltaTime;
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            fallLeft();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            fallRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
        }
        */

        HorzPos = Mathf.Clamp(HorzPos, -7f, 7f);

        transform.position = new Vector2(HorzPos, transform.position.y);

    }




}
