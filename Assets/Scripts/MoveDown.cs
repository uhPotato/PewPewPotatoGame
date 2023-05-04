using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.y -= moveSpeed * Time.fixedDeltaTime;

        if (pos.y < 7){
            moveSpeed = 0;
        }

        transform.position = pos;
    }
}
