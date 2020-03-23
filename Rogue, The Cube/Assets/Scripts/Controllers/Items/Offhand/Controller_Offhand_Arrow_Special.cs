using UnityEngine;

public class Controller_Offhand_Arrow_Special : Controller_Offhand
{
    private Arrow arrow;
    private GameObject visualModel;

    public override void Start()
    {
        base.Start();

        arrow = GetComponentInParent<Controller_Equipment>().currentEquipment[3].arrow;
        visualModel = arrow.visualModel;
    }

    public override void UseOffhand()
    {
        base.UseOffhand();
    }

    public override void ReleaseOffhand()
    {
        base.ReleaseOffhand();
    }

    private void CastSpecialArrow()
    {
        //Instantiate(visualModel, );
    }
}
