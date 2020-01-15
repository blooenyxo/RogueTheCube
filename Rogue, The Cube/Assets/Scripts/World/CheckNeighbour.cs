using UnityEngine;

public class CheckNeighbour : MonoBehaviour
{
    public float _radius = 5f;
    public LayerMask _layerMask;

    public bool Check()
    {
        Collider[] col = Physics.OverlapSphere(this.transform.position, _radius, _layerMask);
        //Debug.Log(col);

        if (col.Length > 0)
        {
            //Debug.Log("true");
            return true;
        }
        else
        {
            //Debug.Log("false");
            return false;
        }
    }
}