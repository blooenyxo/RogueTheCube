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

    private bool bButtomEdge = false;
    private bool bTopEdge = false;
    private bool bLeftEdge = false;
    private bool bRightEdge = false;



    void Start()
    {
        RandomWalls();
        AllFour();
        EndOfMap();
        //Edges();
    }

    private void RandomWalls()
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

    private void EndOfMap()
    {
        if (checkPointBottom.GetComponent<CheckNeighbour>().Check() == false)
        {
            wallBottom.SetActive(true);
            bButtomEdge = true;
        }
        if (checkPointLeft.GetComponent<CheckNeighbour>().Check() == false)
        {
            wallLeft.SetActive(true);
            bLeftEdge = true;
        }
        if (checkPointRight.GetComponent<CheckNeighbour>().Check() == false)
        {
            wallRight.SetActive(true);
            bRightEdge = true;
        }
        if (checkPointTop.GetComponent<CheckNeighbour>().Check() == false)
        {
            wallTop.SetActive(true);
            bTopEdge = true;
        }
    }

    private void Edges()
    {


        if (bTopEdge && bRightEdge)
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
                wallBottom.SetActive(false);
            if (ran == 1)
                wallLeft.SetActive(false);
        }
        else if (bRightEdge && bButtomEdge)
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
                wallTop.SetActive(false);
            if (ran == 1)
                wallLeft.SetActive(false);
        }
        else if (bButtomEdge && bLeftEdge)
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
                wallTop.SetActive(false);
            if (ran == 1)
                wallRight.SetActive(false);
        }
        else if (bLeftEdge && bTopEdge)
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
                wallBottom.SetActive(false);
            if (ran == 1)
                wallRight.SetActive(false);
        }
        else if (bTopEdge)
        {
            int ran = Random.Range(0, 3);
            if (ran == 0)
                wallBottom.SetActive(false);
            if (ran == 1)
                wallRight.SetActive(false);
            if (ran == 2)
                wallLeft.SetActive(false);
        }
        else if (bButtomEdge)
        {
            int ran = Random.Range(0, 3);
            if (ran == 0)
                wallTop.SetActive(false);
            if (ran == 1)
                wallRight.SetActive(false);
            if (ran == 2)
                wallLeft.SetActive(false);
        }
        else if (bLeftEdge)
        {
            int ran = Random.Range(0, 3);
            if (ran == 0)
                wallBottom.SetActive(false);
            if (ran == 1)
                wallRight.SetActive(false);
            if (ran == 2)
                wallTop.SetActive(false);
        }
        else if (bRightEdge)
        {
            int ran = Random.Range(0, 3);
            if (ran == 0)
                wallBottom.SetActive(false);
            if (ran == 1)
                wallTop.SetActive(false);
            if (ran == 2)
                wallLeft.SetActive(false);
        }
    }

    private void AllFour()
    {
        if (wallBottom && wallLeft && wallRight && wallTop)
        {
            int ran = Random.Range(0, 4);
            if (ran == 0)
                wallBottom.SetActive(false);
            if (ran == 1)
                wallTop.SetActive(false);
            if (ran == 2)
                wallRight.SetActive(false);
            if (ran == 3)
                wallLeft.SetActive(false);
        }
    }
}