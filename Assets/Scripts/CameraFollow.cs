using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;
    public Vector3 minCamPos;
    public Vector3 maxCamPos;

    public bool bound;

    public float offsetX;
    //public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        

    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        //store camera's position in temp
        Vector3 temp = transform.position;
        //camera's position is equal to player's position
        temp.x = playerTransform.position.x;
        temp.x += offsetX;
        //temp.x += offsetX;
        temp.y = playerTransform.position.y;
        //temp.y += offsetY;


        //set camera's temp position to its current position
        transform.position = temp;

        if (bound)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamPos.x, maxCamPos.x), 
                Mathf.Clamp(transform.position.y, minCamPos.y, maxCamPos.y),
                Mathf.Clamp(transform.position.z, minCamPos.z, maxCamPos.z));
            //print("Working");
        }    
        
    }

    private void LateUpdate()
    {
        

    }


}
