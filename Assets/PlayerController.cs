using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject LeftTwin;
    public GameObject RightTwin;
    public GameObject LeftTwinHead;
    public GameObject RightTwinHead;
    public GameObject LeftTwinFeet;
    public GameObject RightTwinFeet;

    Animator LeftTwinAnimator;
    Animator RightTwinAnimator;

    float HorzPos;

    // Start is called before the first frame update
    void Start()
    {
        HorzPos = transform.position.x;
        LeftTwinAnimator = LeftTwin.GetComponent<Animator>();
        RightTwinAnimator = RightTwin.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LeftTwinAnimator.SetBool("Forward", true);
            RightTwinAnimator.SetBool("Reverse", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftTwinAnimator.SetBool("Reverse", true);
            RightTwinAnimator.SetBool("Forward", true);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            LeftTwinAnimator.SetBool("Forward", false);
            RightTwinAnimator.SetBool("Reverse", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            LeftTwinAnimator.SetBool("Reverse", false);
            RightTwinAnimator.SetBool("Forward", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            HorzPos += 0.05f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            HorzPos -= 0.05f;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L");
            LeftTwinAnimator.SetTrigger("Fall");

            LeftTwinAnimator.SetBool("Reverse", false);
            LeftTwinAnimator.SetBool("Forward", false);
            RightTwinAnimator.SetBool("Reverse", false);
            RightTwinAnimator.SetBool("Forward", false);

            LeftTwinFeet.GetComponent<Collider2D>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RightTwinAnimator.SetTrigger("Fall");

            LeftTwinAnimator.SetBool("Reverse", false);
            LeftTwinAnimator.SetBool("Forward", false);
            RightTwinAnimator.SetBool("Reverse", false);
            RightTwinAnimator.SetBool("Forward", false);

            RightTwinFeet.GetComponent<Collider2D>().enabled = false;

        }

        HorzPos = Mathf.Clamp(HorzPos, -8f, 8f);

        transform.position = new Vector2(HorzPos, transform.position.y);

    }
}
