using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloor : MonoBehaviour
{
    public GameObject wallTop;
    public GameObject wallBottom;
    public GameObject wallRight;
    public GameObject wallLeft;

    void Start()
    {
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            wallTop.SetActive(false);
            wallBottom.SetActive(true);
            wallRight.SetActive(true);
            wallLeft.SetActive(true);
        }
        if (r == 1)
        {
            wallTop.SetActive(true);
            wallBottom.SetActive(false);
            wallRight.SetActive(true);
            wallLeft.SetActive(true);
        }
        if (r == 2)
        {
            wallTop.SetActive(true);
            wallBottom.SetActive(true);
            wallRight.SetActive(false);
            wallLeft.SetActive(true);
        }
        if (r == 3)
        {
            wallTop.SetActive(true);
            wallBottom.SetActive(true);
            wallRight.SetActive(true);
            wallLeft.SetActive(false);
        }
    }
}
