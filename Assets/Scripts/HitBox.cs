using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private HP _hP;

    public bool TakeDamage(int damage, Team team)
    {
        if(_hP.Team != team)
        {
            _hP.TakeDamage(damage);
            return true;
        }
        return false;

    }
}
