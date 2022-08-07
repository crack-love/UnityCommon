#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;

/// <summary>
/// 2021-04-20 화 오후 3:25:17, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
#if UNITY_EDITOR
    public class MeshAssetGenerator : MonoBehaviour
    {
        [InspectorButton("GenerateGrassMesh")]
        public bool btn0;

        void GenerateGrassMesh()
        {
            var yconst = -0.15f;

            // generate quadmesh * 2
            var mesh = new Mesh();
            mesh.name = UnityEngine.Random.Range(0, 10000).ToString();
            mesh.vertices = new Vector3[]
            {
                    new Vector3(-0.5f,yconst,0), new Vector3(0.5f,yconst,0), new Vector3(-0.5f,1+yconst,0), new Vector3(0.5f,1+yconst,0),
                    new Vector3(0,yconst,0.5f), new Vector3(0,yconst,-0.5f), new Vector3(0,1+yconst,0.5f), new Vector3(0,1+yconst,-0.5f),
            };
            mesh.triangles = new int[]
            {
                    0, 1, 3, 0, 3, 2,
                    4, 5, 7, 4, 7, 6,
            };
            mesh.uv = new Vector2[]
            {
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1),
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1),
            };


            AssetDatabase.CreateAsset(mesh, "assets/grassMesh.asset");
        }

        [InspectorButton("GenerateSimpleGrassMesh")]
        public bool btn1;
        public void GenerateSimpleGrassMesh()
        {
            var mesh = GenerateSimpleGrassMeshResult();
            AssetDatabase.CreateAsset(mesh, "assets/grassMesh.asset");
        }


        public Mesh GenerateSimpleGrassMeshResult()
        {
            var yconst = -0f;

            // generate quadmesh both side faced
            var mesh = new Mesh();
            mesh.name = UnityEngine.Random.Range(0, 10000).ToString();
            mesh.vertices = new Vector3[]
            {
                    new Vector3(-0.5f,yconst,0), new Vector3(0.5f,yconst,0), new Vector3(-0.5f,1+yconst,0), new Vector3(0.5f,1+yconst,0),
            };
            mesh.triangles = new int[]
            {
                    0, 1, 3, 0, 3, 2,
                    0, 2, 3, 0, 3, 1,
            };
            mesh.uv = new Vector2[]
            {
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1),
            };

            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();

            return mesh;
        }
    }
#endif
}