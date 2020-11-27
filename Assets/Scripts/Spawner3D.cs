using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner3D : MonoBehaviour {
    public int amount = 500;
    public GameObject prefab;

    List<GameObject> _instantiated = new List<GameObject>();

    void OnEnable() {
        var count = 0;
        for (int i = 0; i < amount; i++) {
            var go = Instantiate(prefab);
            go.transform.position = Random.insideUnitSphere * 50f + new Vector3(0, 60f, 0);
            _instantiated.Add(go);
        }
    }

    void OnDisable() {
        foreach (var go in _instantiated) {
            Destroy(go);
        }
    }
}