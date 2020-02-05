public class Equipment_Visual_Player : Equipment_Visual
{
    Equipment equipment;
    public override void Start()
    {
        base.Start();
        equipment = Stats_Player.instance.gameObject.GetComponent<Equipment>();
        equipment.onEquipmentChange += UpdateVisuals;
    }
}