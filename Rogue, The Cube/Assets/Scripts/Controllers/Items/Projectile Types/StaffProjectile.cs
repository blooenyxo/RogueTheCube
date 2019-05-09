using UnityEngine;

public class StaffProjectile : Controller_Projectile
{
    private Stats_Player stats_Player;

    public override void Start()
    {
        stats_Player = Stats_Player.instance;

        speed = stats_Player.INTELIGENCE.GetValue();

        base.Start();
    }
}
