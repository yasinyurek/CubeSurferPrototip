using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBox : MonoBehaviour
{
    public int count;
    int c;
    public GameObject particle;

    private void Start()
    {
        c = count;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (c>0)
        {
            if (other.tag=="cube")
            {
                PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                c--;
                if (playerController.cubecount==1)
                {
                    playerController.point = count;
                }
                playerController.RemoveCube();

                GameObject go= Instantiate(particle, other.gameObject.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                Destroy(go, 1);


            }
        }
    }
}
