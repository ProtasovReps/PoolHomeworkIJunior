using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Transform _floor;
    [SerializeField] private float _getDelay;

    private ObjectPool<Cube> _pool;
    private int _poolMaxSize = 10;
    private int _poolCapacity = 10;

    private void Awake() => CreatePool();

    private void Start() => StartCoroutine(GetCubesWithDelay());

    public void ReleaseCube(Cube cube) => StartCoroutine(ReleaseDelayed(cube));

    private void CreatePool()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cube),
        actionOnGet: (cube) => GetCube(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false),
        actionOnDestroy: (cube) => Destroy(cube),
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void GetCube(Cube cube)
    {
        float upPosition = 60f;

        cube.transform.position = new Vector3(SetRandomPosition(), upPosition, _floor.position.z);
        cube.gameObject.SetActive(true);
    }

    private float SetRandomPosition()
    {
        float floorStart = _floor.position.x - (_floor.lossyScale.x / 2);
        float floorEnd = _floor.position.x + (_floor.lossyScale.x / 2);

        return Random.Range(floorStart, floorEnd);
    }

    private IEnumerator GetCubesWithDelay()
    {
        bool isWorking = true;
        var delay = new WaitForSeconds(_getDelay);

        while (isWorking)
        {
            _pool.Get();
            yield return delay;
        }
    }

    private IEnumerator ReleaseDelayed(Cube cube)
    {
        int minDelay = 2;
        int maxDelay = 5;

        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        _pool.Release(cube);
    }
}
