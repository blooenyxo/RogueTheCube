using UnityEngine;

public class Controller_Arrow : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Stats stats;
    [HideInInspector] public string parentTag;
    [HideInInspector] public Buff buff;

    public float maxDestroyTimer;
    public float speed;

    private float timer;
    private bool canFall = true;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, Random.Range(5, maxDestroyTimer));

        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse); // this has to be here, the speed variable in set only one row above
        timer = Time.time + .5f; // timer for acrivating gravity, so that the arrow can fall, but only after a given time
    }

    private void Update()
    {
        if (Time.time > timer && canFall)
        {
            GetComponent<Rigidbody>().useGravity = true;
            canFall = false;
        }
    }
}