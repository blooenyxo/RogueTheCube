using UnityEngine;

public class Arrow : Controller_Projectile
{
    private Stats_Player stats_Player;
    public override void Start()
    {
        stats_Player = Stats_Player.instance;

        speed = Mathf.RoundToInt(stats_Player.AGILITY.GetValue() * 1.5f);
        base.Start();
    }
}
