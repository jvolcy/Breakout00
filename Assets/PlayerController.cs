using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TwinCtrl LeftTwin;
    public TwinCtrl RightTwin;
    public GameObject Bar;

    Vector2 BarTransformPosition;
    Quaternion BarTransformRotation;

    float HorzPos;

    // Start is called before the first frame update
    void Start()
    {
        HorzPos = transform.position.x;
        BarTransformPosition = Bar.transform.localPosition;
        BarTransformRotation = Bar.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LeftTwin.forward();
            RightTwin.reverse();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftTwin.reverse();
            RightTwin.forward();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            idle();
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

        HorzPos = Mathf.Clamp(HorzPos, -6f, 6f);

        transform.position = new Vector2(HorzPos, transform.position.y);

    }

    public void fallLeft()
    {
        idle();
        LeftTwin.fall();
    }

    public void fallRight()
    {
        idle();
        RightTwin.fall();
    }

    public void idle()
    {
        LeftTwin.idle();
        RightTwin.idle();
    }

    public void Reset()
    {
        HorzPos = 0;
        LeftTwin.Reset();
        RightTwin.Reset();
        Bar.transform.localPosition = BarTransformPosition;
        Bar.transform.localRotation = BarTransformRotation;
    }



    public void  CopyTransform(Transform source, Transform destination)
    {
        destination.localPosition = transform.localPosition;
        destination.localRotation = transform.localRotation;
        destination.localScale = transform.localScale;
    }


}
