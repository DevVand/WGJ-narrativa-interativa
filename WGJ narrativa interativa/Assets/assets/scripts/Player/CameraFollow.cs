using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smooth=10;
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Lerp(transform.position.x, player.position.x, smooth * Time.deltaTime);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
