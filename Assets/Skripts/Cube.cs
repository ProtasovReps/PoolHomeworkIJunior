using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;

    public bool IsColorChanged { get; private set; }

    private void Awake() => _renderer = GetComponent<Renderer>();

    private void OnEnable() => SetDefaultColor();

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
        IsColorChanged = true;
    }

    private void SetDefaultColor()
    {
        _renderer.material.color = Color.white;
        IsColorChanged = false;
    }
}
