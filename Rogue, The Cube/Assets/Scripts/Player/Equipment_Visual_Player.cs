public class Equipment_Visual_Player : Equipment_Visual
{
    Equipment equipment;
    public override void Start()
    {
        base.Start();
        equipment = Stats_Player.instance.gameObject.GetComponent<Equipment>();
        equipment.onEquipmentChange += UpdateVisuals;
    }

    private void LateUpdate()
    {
        if (equipment == null)
        {
            equipment = Stats_Player.instance.gameObject.GetComponent<Equipment>();
            equipment.onEquipmentChange += UpdateVisuals;
        }
    }

    public override void UpdateVisuals(Item newItem, Item oldItem)
    {
        base.UpdateVisuals(newItem, oldItem);
    }
}