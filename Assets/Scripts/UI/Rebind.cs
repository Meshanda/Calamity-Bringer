using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rebind : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;
    
    [Header("Rebind Settings")]
    [SerializeField] private string _actionName;

    [Header("Borders")]
    [SerializeField] private GameObject _blueBorder;
    [SerializeField] private GameObject _greenBorder;

    [Header("Bind Text")]
    [SerializeField] private TextMeshProUGUI _bindText;

    [Header("Buttons")] 
    [SerializeField] private GameObject _resetButton;

    private InputAction _action;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    
    private void Start()
    {
        _action = _inputActions.FindAction(_actionName);
        
        _resetButton.SetActive(false);
        _blueBorder.SetActive(true);
        _greenBorder.SetActive(false);
        
        UpdateText();
    }


    private void UpdateText()
    {
        var controlBindingIndex = _action.GetBindingIndexForControl(_action.controls[0]);
        var currentBindingInput = InputControlPath.ToHumanReadableString(_action.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        _bindText.text = currentBindingInput;
    }

    public void ClickRebind()
    {
        StartRebinding();
    }

    private void StartRebinding()
    {
        _blueBorder.SetActive(false);
        _greenBorder.SetActive(true);
        _bindText.text = "Binding...";
        
        rebindOperation = _action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(_ => RebindCompleted());
    
        rebindOperation.Start();
    }

    private void RebindCompleted()
    {
        rebindOperation.Dispose();
        rebindOperation = null;
        
        _blueBorder.SetActive(true);
        _greenBorder.SetActive(false);
        _resetButton.SetActive(true);
        
        UpdateText();
    }
    
    public void ClickReset()
    {
        ResetBinding();
        _resetButton.SetActive(false);
    }

    private void ResetBinding()
    {
        _action.RemoveAllBindingOverrides();
        UpdateText();
    }
}
