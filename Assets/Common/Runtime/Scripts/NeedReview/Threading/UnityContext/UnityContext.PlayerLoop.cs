using UnityEngine;
using System;
using UnityEngine.LowLevel;
using System.Linq;
using System.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 2020-06-01 월 오후 6:08:04, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    // PlayerLoop Section

    public static partial class UnityContext
    {
        static YieldRunner[] m_yieldRunners;
        static UpdateRunner[] m_updateRunners;

        static void InitializePlayerLoopStructure()
        {
            // Initialize Queue List
            m_yieldRunners = new YieldRunner[7];
            m_updateRunners = new UpdateRunner[7];

            for (int i = 0; i < 7; ++i)
            {
                m_yieldRunners[i] = new YieldRunner();
                m_updateRunners[i] = new UpdateRunner();
            }
        }

        static void InitializePlayerLoopRegistrationRuntime()
        {
            // Get PlayerLoopSystem
            var system = PlayerLoop.GetCurrentPlayerLoop();
            var subSystem = system.subSystemList;

            AddRunnerToSystem(typeof(Initialization), typeof(InitializationYield), ref subSystem[0], 0);
            AddRunnerToSystem(typeof(EarlyUpdate), typeof(EarlyUpdateYield), ref subSystem[1], 1);
            AddRunnerToSystem(typeof(FixedUpdate), typeof(FixedUpdateYield), ref subSystem[2], 2);
            AddRunnerToSystem(typeof(PreUpdate), typeof(PreUpdateYield), ref subSystem[3], 3);
            AddRunnerToSystem(typeof(Update), typeof(UpdateYield), ref subSystem[4], 4);
            AddRunnerToSystem(typeof(PreLateUpdate), typeof(PreLateUpdateYield), ref subSystem[5], 5);
            AddRunnerToSystem(typeof(PostLateUpdate), typeof(PostLateUpdateYield), ref subSystem[6], 6);

            system.subSystemList = subSystem;
            PlayerLoop.SetPlayerLoop(system);
        }

        /// <summary>
        /// add idx runners to system
        /// </summary>
        static void AddRunnerToSystem(Type updateName, Type yieldName, ref PlayerLoopSystem system, int idx)
        {
            // create Adding system
            var addingUpdate = new PlayerLoopSystem();
            addingUpdate.type = updateName;
            addingUpdate.updateDelegate = m_updateRunners[idx].Run;

            var addingYield = new PlayerLoopSystem();
            addingYield.type = yieldName;
            addingYield.updateDelegate = m_yieldRunners[idx].Run;

            // copy system except duplication
            var ssSrc = Enumerable.Where(system.subSystemList, (x) => x.type != updateName && x.type != yieldName).ToArray();
            var ssDst = new PlayerLoopSystem[ssSrc.Length + 2];
            Array.Copy(ssSrc, 0, ssDst, 2, ssSrc.Length);

            // add adding
            ssDst[0] = addingYield;
            ssDst[1] = addingUpdate;

            // set system
            system.subSystemList = ssDst;
        }

        public static void QueueYield(PlayerLoopType type, Action yield)
        {
            m_yieldRunners[(int)type].Queue(yield);
        }

        /// <summary>
        /// Function returns false when finished
        /// </summary>
        public static void QueueUpdate(PlayerLoopType type, Func<bool> update)
        {
            m_updateRunners[(int)type].Queue(LoopableFunction.Create(update));
        }

        public static void QueueUpdate(PlayerLoopType type, ILoopable update)
        {
            m_updateRunners[(int)type].Queue(update);
        }

        public static void DequeueUpdate(PlayerLoopType type, ILoopable update)
        {
            m_updateRunners[(int)type].Dequeue(update);
        }

        public static void DequeueUpdate(PlayerLoopType type, Func<bool> update)
        {
            m_updateRunners[(int)type].Dequeue(update);
        }

        public static void QueueUpdate(PlayerLoopType type, Action update)
        {
            // TODO Gabage generated
            m_updateRunners[(int)type].Queue(LoopableAction.Create(update));
        }

        public static void DequeueUpdate(PlayerLoopType type, Action update)
        {
            m_updateRunners[(int)type].Dequeue(update);
        }

        // For naming on profile view
        class Initialization { }
        class InitializationYield { }
        class EarlyUpdate { }
        class EarlyUpdateYield { }
        class FixedUpdate { }
        class FixedUpdateYield { }
        class PreUpdate { }
        class PreUpdateYield { }
        class Update { }
        class UpdateYield { }
        class PreLateUpdate { }
        class PreLateUpdateYield { }
        class PostLateUpdate { }
        class PostLateUpdateYield { }

        public static float DeltaTime
        {
            get
            {
                if (Application.isPlaying)
                {
                    return Time.deltaTime;
                }
                else
                {
                    return m_editorDeltaTime;
                }
            }
        }

        public static float UnscaleDeltaTime
        {
            get
            {
                if (Application.isPlaying)
                {
                    return Time.unscaledDeltaTime;
                }
                else
                {
                    return m_editorDeltaTime;
                }
            }
        }

        static float m_editorDeltaTime;
        static Stopwatch m_editorStopwatch;

        static void InitializePlayerLoopRegistrationEditor()
        {
            m_editorStopwatch = new Stopwatch();

            EditorApplication.update += EditorPlayerLoopUpdate;
        }

        static void EditorPlayerLoopUpdate()
        {
            if (!Application.isPlaying)
            {
                // calculate delta time
                m_editorDeltaTime = m_editorStopwatch.ElapsedMilliseconds / 1000f;
                m_editorStopwatch.Restart();

                for (int i = 0; i < 7; ++i)
                {
                    m_yieldRunners[i].Run();
                    m_updateRunners[i].Run();
                }
            }
        }
    }
}