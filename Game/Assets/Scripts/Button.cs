using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [Tooltip("The layers of objects that can press the button")]
    [SerializeField] UnityEvent OnPressed;
    [SerializeField] UnityEvent WhilePressed;
    [SerializeField] UnityEvent OnReleased;
    [SerializeField] LayerMask PressableLayers;
    [SerializeField] Sprite UnpressedSprite;
    [SerializeField] Sprite PressedSprite;
    [SerializeField] SpriteRenderer Renderer;
    [SerializeField] Collider2D col;
    public bool isPressed {get; private set;} = false;
    void FixedUpdate()
    {
        bool newIsPressed = col.IsTouchingLayers(PressableLayers);
        if(newIsPressed && !isPressed)//On Pressed
        {
            OnPressed.Invoke();
            Renderer.sprite = PressedSprite;
        }
        if(newIsPressed)//While Pressed
        {
            WhilePressed.Invoke();
        }
        if(!newIsPressed && isPressed)//On Released
        {
            OnReleased.Invoke();
            Renderer.sprite = UnpressedSprite;
        }
        isPressed = newIsPressed;
    }
}
