
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDebuggerActionExample : MonoBehaviour
{
    public InputAction exampleAction;

    private void OnEnable()
    {
        exampleAction.Enable();
    }
}
