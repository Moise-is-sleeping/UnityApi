using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movmentS2 : MonoBehaviour
{
    private float timer = 0.0f;
    private int pos = -1;
    private bool moveF = true;
    public float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pos == 3)
            {
                pos = 4;
            }
            else
            {
                pos = 0;
            }
        }
        
        switch (pos)
        {

            case 0:
                if (transform.position != new Vector3(-9.85f, 7f, -10.88f))
                {
                    MovePosition(new Vector3(-9.85f, 7f, -10.88f));
                }
                else
                {
                    pos += 1;
                }
                break;
            case 1:
                if (transform.position != new Vector3(5.85f, 7f, -10.88f))
                {
                    MovePosition(new Vector3(5.85f, 7f, -10.88f));
                }
                else
                {
                    pos += 1;
                }
                break;
            case 2:
                if (transform.position != new Vector3(5.85f, 1.57f, -10.88f))
                {
                    MovePosition(new Vector3(5.85f, 1.57f, -10.88f));
                }
                else
                {
                    pos += 1 ;
                }
                break;
            case 4:
                if (transform.position != new Vector3(-9.85f, 1.57f, -10.88f))
                {
                    MovePosition(new Vector3(-9.85f, 1.57f, -10.88f));
                }
                else
                {
                    pos -= 5;
                }
                break;
        }

   
            
        void MovePosition(Vector3 target )
        {
            transform.position = Vector3.MoveTowards(transform.position,target, speed * Time.deltaTime);
            
        }
    }
}
