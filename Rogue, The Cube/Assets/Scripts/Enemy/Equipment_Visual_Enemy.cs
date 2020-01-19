public class Equipment_Visual_Enemy : Equipment_Visual
{
    public Controller_Enemy _Controller_Enemy;

    public override void Start()
    {
        _Controller_Enemy = GetComponent<Controller_Enemy>();

        if (_Controller_Enemy != null)
        {
            if (_Controller_Enemy.enemyWeapon != null)
                UpdateVisuals(_Controller_Enemy.enemyWeapon, null);
            if (_Controller_Enemy.enemyArmor != null)
                UpdateVisuals(_Controller_Enemy.enemyArmor, null);
        }
    }

    public override void UpdateVisuals(Item newItem, Item oldItem)
    {
        base.UpdateVisuals(newItem, oldItem);
    }
}