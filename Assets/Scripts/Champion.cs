using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        if (Input.GetKey(KeyCode.S))
            transform.position -= Vector3.forward * Time.deltaTime * moveSpeed;

        transform.LookAt(Camera.main.ScreenPointToRay(Input.mousePosition).GetIntersectionPoint(transform.position.y));
        //transform.rotation = Quaternion.LookRotation(Camera.main.ScreenPointToRay(Input.mousePosition).GetIntersectionPoint() - transform.position, Vector3.up);
    }
}
