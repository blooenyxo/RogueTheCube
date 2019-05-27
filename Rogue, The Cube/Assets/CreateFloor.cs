using UnityEngine;

public class CreateFloor : MonoBehaviour
{
    [Header("Walls")]
    public GameObject wallTop;
    public GameObject wallBottom;
    public GameObject wallRight;
    public GameObject wallLeft;

    [Header("Checking Points")]
    public GameObject checkPointTop;
    public GameObject checkPointBottom;
    public GameObject checkPointRight;
    public GameObject checkPointLeft;

    void Start()
    {
        int r = Random.Range(0, 5);
        if (r == 0)
        {
            wallTop.SetActive(true);
            wallBottom.SetActive(false);
            wallRight.SetActive(false);
            wallLeft.SetActive(false);
        }
        if (r == 1)
        {
            wallTop.SetActive(false);
            wallBottom.SetActive(true);
            wallRight.SetActive(false);
            wallLeft.SetActive(false);
        }
        if (r == 2)
        {
            wallTop.SetActive(false);
            wallBottom.SetActive(false);
            wallRight.SetActive(true);
            wallLeft.SetActive(false);
        }
        if (r == 3)
        {
            wallTop.SetActive(false);
            wallBottom.SetActive(false);
            wallRight.SetActive(false);
            wallLeft.SetActive(true);
        }
        if (r == 4)
        {
            wallTop.SetActive(false);
            wallBottom.SetActive(false);
            wallRight.SetActive(false);
            wallLeft.SetActive(false);
        }
    }
}