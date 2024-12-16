using BepInEx.Unity.IL2CPP.Utils.Collections;
using CrimsonDice.Services;
using ProjectM.Physics;
using System;
using System.Collections;
using System.Linq;
using Unity.Entities;
using UnityEngine;

namespace CrimsonDice;

internal static class Core
{
    public static World Server { get; } = GetServerWorld() ?? throw new Exception("There is no Server world (yet)...");
    public static EntityManager EntityManager => Server.EntityManager;

    public static DiceService DiceService { get; internal set; }
    public static PlayerService PlayerService { get; internal set; }

    public static bool hasInitialized = false;
    static MonoBehaviour monoBehaviour;

    public static void Initialize()
    {
        if (hasInitialized) return;

        DiceService = new DiceService();
        PlayerService = new PlayerService();

        hasInitialized = true;
    }

    static World GetServerWorld()
    {
        return World.s_AllWorlds.ToArray().FirstOrDefault(world => world.Name == "Server");
    }

    public static Coroutine StartCoroutine(IEnumerator routine)
    {
        if (monoBehaviour == null)
        {
            var go = new GameObject("CrimsonDice");
            monoBehaviour = go.AddComponent<IgnorePhysicsDebugSystem>();
            UnityEngine.Object.DontDestroyOnLoad(go);
        }

        return monoBehaviour.StartCoroutine(routine.WrapToIl2Cpp());
    }

    public static void StopCoroutine(Coroutine coroutine)
    {
        if (monoBehaviour == null)
        {
            return;
        }

        monoBehaviour.StopCoroutine(coroutine);
    }
}