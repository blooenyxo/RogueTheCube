using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startgametest : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;
    public bool playerExists = false;

    private void Update()
    {
        if (!playerExists && Input.GetButton("Jump"))
        {
            GameObject p = Instantiate(player, spawnPoint.position, spawnPoint.rotation);
            p.transform.SetParent(spawnPoint);
            p.name = "Player";
            playerExists = true;
        }
    }
}
