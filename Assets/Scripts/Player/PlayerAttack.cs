using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _armCollider;
    [SerializeField] private Animator _animator;
    
    private bool _isAttacking;
    public bool IsAttacking => _isAttacking;

    private void Start()
    {
        _armCollider.SetActive(false);
    }

    public void OnAttack()
    {
        if (_isAttacking) return;

        _animator.SetTrigger("Attack");
        _isAttacking = true;
        _armCollider.SetActive(true);
    }

    public void OnAttackAnimationEnd()
    {
        _armCollider.SetActive(false);
        _isAttacking = false;
    }
    
    public void OnArmTriggerEnter(Collider otherCollider)
    {
        if (!otherCollider.CompareTag("Building")) return;
    }
}
