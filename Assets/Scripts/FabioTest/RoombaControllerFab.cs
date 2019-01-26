using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaControllerFab : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The straight moving speed of the roomba")]
    private float moveSpeed = 1.0f;

    [SerializeField]
    [Tooltip("The turning speed in radius per seconds of the roomba")]
    private float turnSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed, 0));
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            transform.Translate(new Vector3(Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed, 0, 0));
        }

        
    }
}
