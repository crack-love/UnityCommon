using System.Collections.Generic;
using UnityEngine;

namespace UnityCommon
{
    public static class CubeDirection
    {
        /// <summary>
        /// Pivot(left-bot) 기준으로 8개 큐브 점 델타 (pivot 포함)
        /// </summary>
        public static readonly IReadOnlyList<Vector3Int> CubeCtor = new Vector3Int[8]
        {
            // order is Voxel(Marching-Cube) numbering rule

            new Vector3Int(0, 0, 0),
            new Vector3Int(0, 0, 1),
            new Vector3Int(1, 0, 1),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, 1, 1),
            new Vector3Int(1, 1, 1),
            new Vector3Int(1, 1, 0),
        };

        /// <summary>
        /// Pivot(center) 기준 이웃한 26 큐브 점 델타 (pivot 제외)
        /// </summary>
        public static readonly IReadOnlyList<Vector3Int> CubeNeighbor = new Vector3Int[27]
        {
            new Vector3Int(-1, -1, -1), new Vector3Int(-1, -1,  0), new Vector3Int(-1, -1,  1),
            new Vector3Int(-1,  0, -1), new Vector3Int(-1,  0,  0), new Vector3Int(-1,  0,  1),
            new Vector3Int(-1,  1, -1), new Vector3Int(-1,  1,  0), new Vector3Int(-1,  1,  1),
            new Vector3Int( 0, -1, -1), new Vector3Int( 0, -1,  0), new Vector3Int( 0, -1,  1),
            new Vector3Int( 0,  0, -1), new Vector3Int( 0,  0,  0), new Vector3Int( 0,  0,  1),
            new Vector3Int( 0,  1, -1), new Vector3Int( 0,  1,  0), new Vector3Int( 0,  1,  1),
            new Vector3Int( 1, -1, -1), new Vector3Int( 1, -1,  0), new Vector3Int( 1, -1,  1),
            new Vector3Int( 1,  0, -1), new Vector3Int( 1,  0,  0), new Vector3Int( 1,  0,  1),
            new Vector3Int( 1,  1, -1), new Vector3Int( 1,  1,  0), new Vector3Int( 1,  1,  1),
        };

        /// <summary>
        /// Delta distance 3 제외 큐브 델타 (pivot 제외)
        /// </summary>
        public static readonly IReadOnlyList<Vector3Int> CubeNeighborExceptD3 = new Vector3Int[18]
        {
                                        new Vector3Int(-1, -1,  0),
            new Vector3Int(-1,  0, -1), new Vector3Int(-1,  0,  0), new Vector3Int(-1,  0,  1),
                                        new Vector3Int(-1,  1,  0),
            new Vector3Int( 0, -1, -1), new Vector3Int( 0, -1,  0), new Vector3Int( 0, -1,  1),
            new Vector3Int( 0,  0, -1),                             new Vector3Int( 0,  0,  1),
            new Vector3Int( 0,  1, -1), new Vector3Int( 0,  1,  0), new Vector3Int( 0,  1,  1),
                                        new Vector3Int( 1, -1,  0),
            new Vector3Int( 1,  0, -1), new Vector3Int( 1,  0,  0), new Vector3Int( 1,  0,  1),
                                        new Vector3Int( 1,  1,  0),
        };

        public static void GetVoxelVertexIndex(Vector3Int pivotCell, Vector3Int[] idx8)
        {
            idx8[0] = pivotCell + CubeCtor[0];
            idx8[1] = pivotCell + CubeCtor[1];
            idx8[2] = pivotCell + CubeCtor[2];
            idx8[3] = pivotCell + CubeCtor[3];
            idx8[4] = pivotCell + CubeCtor[4];
            idx8[5] = pivotCell + CubeCtor[5];
            idx8[6] = pivotCell + CubeCtor[6];
            idx8[7] = pivotCell + CubeCtor[7];
        }

        public static void GetVoxelVertexSquareDistance(Vector3 distance, float[] dst8)
        {
            float dx = Mathf.Pow(distance.x, 2);
            float dy = Mathf.Pow(distance.y, 2);
            float dz = Mathf.Pow(distance.z, 2);
            float dix = Mathf.Pow(1 - distance.x, 2);
            float diy = Mathf.Pow(1 - distance.y, 2);
            float diz = Mathf.Pow(1 - distance.z, 2);

            dst8[0] = dx + dy + dz;
            dst8[1] = dx + dy + diz;
            dst8[2] = dix + dy + diz;
            dst8[3] = dix + dy + dz;
            dst8[4] = dx + diy + dz;
            dst8[5] = dx + diy + diz;
            dst8[6] = dix + diy + diz;
            dst8[7] = dix + diy + dz;
        }
    }
}
