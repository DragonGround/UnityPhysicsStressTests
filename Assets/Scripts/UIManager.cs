using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public int sphereCount = 500;
    public TextMeshProUGUI sphereCountText;
    public Spawner3D spawner3D;
    public Spawner2D spawner2D;
    public Camera camera2D;

    EntityManager EM;
    List<GameObject> _spawnedGOs = new List<GameObject>();
    Entity _physicsStepEntity;
    Entity _spawnerEntity;

    void Start() {
        EM = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    public void DoUnityPhysics() {
        CleanUp();
        GrabEntities();

        var ps = EM.GetComponentData<PhysicsStep>(_physicsStepEntity);
        ps.SimulationType = SimulationType.UnityPhysics;
        EM.SetComponentData(_physicsStepEntity, ps);

        var spawner = EM.GetComponentData<Spawner>(_spawnerEntity);
        spawner.Amount = sphereCount * 500;
        EM.SetComponentData(_spawnerEntity, spawner);
        EM.AddComponentData(_spawnerEntity, new Spawn());
    }

    public void DoHavokPhysics() {
        CleanUp();
        GrabEntities();

        var ps = EM.GetComponentData<PhysicsStep>(_physicsStepEntity);
        ps.SimulationType = SimulationType.HavokPhysics;
        EM.SetComponentData(_physicsStepEntity, ps);

        var spawner = EM.GetComponentData<Spawner>(_spawnerEntity);
        spawner.Amount = sphereCount * 500;
        EM.SetComponentData(_spawnerEntity, spawner);
        EM.AddComponentData(_spawnerEntity, new Spawn());
    }

    public void DoPhysx() {
        CleanUp();
        spawner3D.amount = sphereCount * 500;
        spawner3D.gameObject.SetActive(true);
    }

    public void DoBox2D() {
        CleanUp();
        camera2D.gameObject.SetActive(true);
        spawner2D.amount = sphereCount * 500;
        spawner2D.gameObject.SetActive(true);
    }

    public void OnSphereCountChanged(float count) {
        sphereCount = (int) count;
        sphereCountText.text = (sphereCount * 500) + " Spheres";
    }

    void GrabEntities() {
        var query = EM.CreateEntityQuery(typeof(PhysicsStep));
        _physicsStepEntity = query.GetSingletonEntity();
        query = EM.CreateEntityQuery(typeof(Spawner));
        _spawnerEntity = query.GetSingletonEntity();
    }

    void CleanUp() {
        var sphereQuery = EM.CreateEntityQuery(typeof(Sphere));
        EM.DestroyEntity(sphereQuery);
        _spawnedGOs.Clear();
        spawner3D.gameObject.SetActive(false);
        spawner2D.gameObject.SetActive(false);
        camera2D.gameObject.SetActive(false);
    }
}