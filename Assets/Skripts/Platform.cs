using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            if (cube.IsColorChanged == false)
            {
                cube.SetRandomColor();
                _spawner.ReleaseCube(cube);
            }
        }
    }
}
