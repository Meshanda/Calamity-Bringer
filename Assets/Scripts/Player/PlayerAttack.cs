using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _armCollider;

    private bool _isAttacking;
    public bool IsAttacking => _isAttacking;

    private void Start()
    {
        _armCollider.SetActive(false);
    }

    public void OnAttack()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        _armCollider.SetActive(true);
        Debug.Log("ATTAAAAAAQUE!!");

        // TODO: to replace with anim event
        StartCoroutine(Utils.DelayRoutine(.5f, () =>
        {
            _armCollider.SetActive(false);
            _isAttacking = false;
            Debug.Log("Stop attack.");
        }));
    }
    
    public void OnArmTriggerEnter(Collider otherCollider)
    {
        if (!otherCollider.CompareTag("Building")) return;
        
        Debug.Log($"Trigger with: {otherCollider.gameObject.name}");
    }
}
