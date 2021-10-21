using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject template)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(template, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }    

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(t => t.activeSelf == false);
        return result != null;
    }    

    public void ResetPool()
    {
        foreach (GameObject item in _pool)
        {
            item.SetActive(false);
        }
    }
}
