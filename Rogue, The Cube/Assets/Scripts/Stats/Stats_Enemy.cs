using UnityEngine;

public class Stats_Enemy : Stats
{
    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
    }

    public override void Die()
    {
        base.Die();

        int r = 0;

        if (GetComponent<Equipment_Visual>().lootBox && r == 0)
        {
            int numberOfItemsToDrop = Random.Range(0, GetComponent<Equipment_Visual>().itemsToDrop.Length + 1);

            GameObject lb = Instantiate(GetComponent<Equipment_Visual>().lootBox, this.transform.position, this.transform.rotation);

            for (int i = 0; i < numberOfItemsToDrop; i++)
            {
                lb.GetComponent<LootBox_Controller>().items.Add(GetComponent<Equipment_Visual>().itemsToDrop[i]);
            }
        }
    }
}