public class Equipment_Visual_Player : Equipment_Visual
{
    Equipment equipment;
    public override void Start()
    {
        base.Start();
        equipment = Equipment.instance;
        equipment.onEquipmentChange += UpdateVisuals;
    }
}