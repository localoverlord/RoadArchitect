#region "Imports"
using UnityEngine;
using System.Collections.Generic;
#endregion


namespace RoadArchitect
{
    [ExecuteInEditMode]
    public class GSDTerrain : MonoBehaviour
    {
#if UNITY_EDITOR
        #region "Vars"
        [SerializeField]
        [HideInInspector]
        [UnityEngine.Serialization.FormerlySerializedAs("mGSDID")]
        private int uID = -1;

        public int UID { get { return uID; } }

        [HideInInspector]
        [UnityEngine.Serialization.FormerlySerializedAs("tTerrain")]
        public Terrain terrain;

        //Splat map:
        [UnityEngine.Serialization.FormerlySerializedAs("SplatResoWidth")]
        public int splatResoWidth = 1024;
        [UnityEngine.Serialization.FormerlySerializedAs("SplatResoHeight")]
        public int splatResoHeight = 1024;
        [UnityEngine.Serialization.FormerlySerializedAs("SplatBackground")]
        public Color splatBackground = new Color(0f, 0f, 0f, 1f);
        [UnityEngine.Serialization.FormerlySerializedAs("SplatForeground")]
        public Color splatForeground = new Color(1f, 1f, 1f, 1f);
        [UnityEngine.Serialization.FormerlySerializedAs("SplatWidth")]
        public float splatWidth = 30f;
        [UnityEngine.Serialization.FormerlySerializedAs("SplatSkipBridges")]
        public bool isSplatSkipBridges = false;
        [UnityEngine.Serialization.FormerlySerializedAs("SplatSkipTunnels")]
        public bool isSplatSkipTunnels = false;
        [UnityEngine.Serialization.FormerlySerializedAs("SplatSingleRoad")]
        public bool isSplatSingleRoad = false;
        [UnityEngine.Serialization.FormerlySerializedAs("SplatSingleChoiceIndex")]
        public int splatSingleChoiceIndex = 0;
        [UnityEngine.Serialization.FormerlySerializedAs("RoadSingleChoiceUID")]
        public string roadSingleChoiceUID = "";
        #endregion


        private void OnEnable()
        {
            CheckID();
            if (!terrain)
            {
                terrain = transform.gameObject.GetComponent<Terrain>();
            }
        }


        public void CheckID()
        {
            if (Application.isEditor)
            {
                if (uID < 0)
                {
                    uID = GetNewID();
                }
                if (!terrain)
                {
                    terrain = transform.gameObject.GetComponent<Terrain>();
                }
            }
        }


        private int GetNewID()
        {
            Object[] allTerrainObjs = GameObject.FindObjectsOfType(typeof(GSDTerrain));
            List<int> allIDS = new List<int>();
            foreach (GSDTerrain Terrain in allTerrainObjs)
            {
                if (Terrain.UID > 0)
                {
                    allIDS.Add(Terrain.UID);
                }
            }

            bool isNotDone = true;
            int spamChecker = 0;
            int spamCheckerMax = allIDS.Count + 64;
            int random;
            while (isNotDone)
            {
                if (spamChecker > spamCheckerMax)
                {
                    Debug.LogError("Failed to generate terrainID");
                    break;
                }
                random = Random.Range(1, 2000000000);
                if (!allIDS.Contains(random))
                {
                    isNotDone = false;
                    return random;
                }
                spamChecker += 1;
            }

            return -1;
        }
#endif


        private void Start()
        {
#if UNITY_EDITOR
            this.enabled = true;
            CheckID();
            if (!terrain)
            {
                terrain = transform.gameObject.GetComponent<Terrain>();
            }
#else
		this.enabled = false;
#endif
        }
    }
}