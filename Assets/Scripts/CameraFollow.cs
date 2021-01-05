using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.transform.position;
    }


    private void LateUpdate()
    {
        transform.position = new Vector3(0, Player.position.y + offset.y, Player.position.z + offset.z);
    }
}
