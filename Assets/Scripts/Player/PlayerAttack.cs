using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Audio ")]
    [SerializeField] private AudioClip _punchSound;
    [SerializeField] private GameObject _armCollider;
    [SerializeField] private Animator _animator;
    
    private bool _isAttacking;
    private bool _gameFinished;
    private bool _gamePaused;
    public bool IsAttacking => _isAttacking;

    private void OnEnable()
    {
        TimerSystem.TimerFinished += OnTimerFinished;
        PauseSystem.GamePaused += OnGamePaused;
        GameManager.GameUnpaused += OnGameUnPaused;
    }

    private void OnDisable()
    {
        TimerSystem.TimerFinished -= OnTimerFinished;
        PauseSystem.GamePaused -= OnGamePaused;
        GameManager.GameUnpaused -= OnGameUnPaused;
    }

    private void OnGameUnPaused()
    {
        _gamePaused = false;
    }

    private void OnGamePaused()
    {
        _gamePaused = true;
    }

    private void OnTimerFinished()
    {
        _gameFinished = true;
    }

    private void Start()
    {
        _armCollider.SetActive(false);
    }

    public void OnAttack()
    {
        if (_isAttacking || _gameFinished || _gamePaused) return;

        _animator.SetTrigger("Attack");
        _isAttacking = true;
        _armCollider.SetActive(true);
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Sound, _punchSound);
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
