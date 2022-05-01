using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float HorzPos;

    // Start is called before the first frame update
    void Start()
    {
        HorzPos = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            HorzPos += 0.05f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            HorzPos -= 0.05f;
        }

        HorzPos = Mathf.Clamp(HorzPos, -8f, 8f);

        transform.position = new Vector2(HorzPos, transform.position.y);

    }
}
