using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 1.0f;
    Material mat;
    Vector2 offset = new Vector2(1.0f, 0.0f);

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (!GameManager.Instance.isPlayerDead)
        {
            mat.mainTextureOffset += offset * speed * Time.deltaTime;
        }
    }
}
