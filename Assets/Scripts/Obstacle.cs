using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isPlayerDead)
        {
            transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
