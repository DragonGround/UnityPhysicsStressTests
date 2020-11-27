using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner2D : MonoBehaviour {
    public int amount = 500;
    public GameObject prefab;

    List<GameObject> _instantiated = new List<GameObject>();

    void OnEnable() {
        var count = 0;
        for (int i = 0; i < amount; i++) {
            var go = Instantiate(prefab);
            go.transform.position = Random.insideUnitCircle * 50f + new Vector2(0, 60f);
            _instantiated.Add(go);
        }
    }

    void OnDisable() {
        foreach (var go in _instantiated) {
            Destroy(go);
        }
    }
}