using UnityEngine;

public class CreateWorld : MonoBehaviour
{
    public GameObject floor;

    private void Start()
    {
        for (int x = 0; x < 200; x += 20)
        {
            for (int y = 0; y < 200; y += 20)
            {
                CreateFloorTile(x, y);
            }
        }
    }

    private void CreateFloorTile(int x, int y)
    {
        GameObject _tmp = Instantiate(floor, this.transform.position + new Vector3(x, 0, y), this.transform.rotation);
        _tmp.transform.SetParent(this.transform);
    }

}
