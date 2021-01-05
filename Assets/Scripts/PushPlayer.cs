using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    //Method used to create a slide movement in the player when touches a barrier
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            float direction = other.transform.position.x - transform.position.x;
            player.Slide((int)Mathf.Sign(direction));
        }
    }
}
