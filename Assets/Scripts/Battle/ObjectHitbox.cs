public class ObjectHitbox: HitBox
{
    public override bool TakeDamage(int damage, Team team)
    {
        return true;
    }
}

