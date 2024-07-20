using Pathfinding;
using System.Collections;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform _target; // Цель для противника
    [SerializeField]
    private float _retreatDuration = 1f; // Время отступления
    [SerializeField]
    private float _attackRange = 1f; // Дистанция атаки
    [SerializeField]
    private float _attackSpeed = 5f; // Скорость при атаке
    [SerializeField]
    private float _normalSpeed = 2f; // Обычная скорость передвижения
    [SerializeField]
    private float _afterAttackSpeed = 5f; // Cкорость отступления
    [SerializeField]
    private int _attackDamage = 10; // Урон при атаке
    [SerializeField]
    private float _attackCooldown = 2f; // Время между атаками
    [SerializeField]
    private float _attackDuration = 1f;
    [SerializeField]
    private float _viewRange = 1f;
    [SerializeField]
    private ZoneOfView _zoneOfView;
    [SerializeField]
    private StartAttackZone _startAttackZone;

    private AIPath _aiPath;
    private bool _isStopAttack = false; // Флаг для проверки видимости цели
    private bool _isRetreating = false;
    private float _lastAttackTime;
    private Rigidbody2D _rigidbody;
    private Damage _damage;
    private IEnumerator _retreater;

    void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _aiPath.maxSpeed = _normalSpeed;
        GetComponent<AIDestinationSetter>().target = _target;
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = GetComponent<Damage>();
        _aiPath.canMove = false;
        _aiPath.maxSpeed = _normalSpeed;
        _zoneOfView.SetZone(_target, _viewRange);
        _startAttackZone.SetZone(_target, _attackRange);
        _damage.SetDamage(_attackDamage);
    }

    private void OnEnable()
    {
        _startAttackZone.AttackStarted += CheckForAttack;
        _zoneOfView.TargetInZone += StartSearch;
        _damage.DamageGiven += TakeDamage;
    }

    private void OnDisable()
    {
        _startAttackZone.AttackStarted -= CheckForAttack;
        _zoneOfView.TargetInZone -= StartSearch;
        _damage.DamageGiven -= TakeDamage;
    }


    private void FixedUpdate()
    {
        if(_isRetreating)
        {
            _retreater.MoveNext();
        }
    }

    private void CheckForAttack()
    {
        if (Time.time - _lastAttackTime > _attackCooldown)
        {
            StartCoroutine(Attack());
        }
    }

    public void StartSearch()
    {
        _aiPath.canMove = true;
        _damage.AllowDamage();
    }

    private IEnumerator Attack()
    {
        _aiPath.maxSpeed = _attackSpeed;
        _aiPath.canMove = true;
        var attackTime = 0.0f;
        while (attackTime < _attackDuration && !_isStopAttack)
        {
            attackTime += Time.deltaTime;
            yield return null;
        }
        _isStopAttack = false;
        _aiPath.maxSpeed = _normalSpeed;
        _lastAttackTime = Time.time;
    }

    public void TakeDamage()
    {
        _isStopAttack = true;
        _damage.DenyDamage();
        _isRetreating = true;
        _retreater = Retreat(_target.position);
    }

    private IEnumerator Retreat(Vector2 attackPosition)
    {
        _aiPath.enabled = false;;
        Vector2 retreatVector = (Vector2)transform.position - attackPosition;
        Vector2 retreatDirection = retreatVector.normalized;

        float retreatTime = 0f;
        while (retreatTime < _retreatDuration)
        {
            _rigidbody.MovePosition(_rigidbody.position + retreatDirection * _afterAttackSpeed * Time.fixedDeltaTime);
            retreatTime += Time.fixedDeltaTime;
            yield return null;
        }

        _aiPath.enabled = true;
        _isRetreating = false;
        _retreater = null;
        _damage.AllowDamage();
    }
}

