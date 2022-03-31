using UnityEngine;

namespace Common
{
    public static class ComputeShaderExtension
    {
        /// <summary>
        /// Result could be more one time of integer division (dataSize/threadgroupSize)
        /// </summary>
        /// <returns>How many batch calls need</returns>
        public static Vector3Int GetBatchSize(this ComputeShader shader, int kernelIndex, Vector3Int dataSize)
        {
            var (x, y, z) = GetBatchSize(shader, kernelIndex, dataSize.x, dataSize.y, dataSize.z);
            return new Vector3Int(x, y, z);
        }

        public static (int x, int y, int z) GetBatchSize(this ComputeShader shader, int kernelIndex, int dataSizeX, int dataSizeY, int dataSizeZ)
        {
            shader.GetKernelThreadGroupSizes(kernelIndex, out uint gsizex, out uint gsizey, out uint gsizez);

            int batchX = Mathf.CeilToInt((float)dataSizeX / gsizex);
            int batchY = Mathf.CeilToInt((float)dataSizeY / gsizey);
            int batchZ = Mathf.CeilToInt((float)dataSizeZ / gsizez);

            batchX = Mathf.Max(batchX, 1);
            batchY = Mathf.Max(batchY, 1);
            batchZ = Mathf.Max(batchZ, 1);

            return (batchX, batchY, batchZ);
        }

        /// <summary>
        /// Dispatch batch size automatic calculated from Kernel's thread groupsize and dataSize
        /// </summary>
        public static void DispatchAll(this ComputeShader shader, int kernelIndex, Vector3Int dataSize)
        {
            var batch = GetBatchSize(shader, kernelIndex, dataSize);

            shader.Dispatch(kernelIndex, batch.x, batch.y, batch.z);
        }

        public static void DispatchAll(this ComputeShader shader, int kernelIndex, int dataSizeX, int dataSizeY, int dataSizeZ)
        {
            var batch = GetBatchSize(shader, kernelIndex, dataSizeX, dataSizeY, dataSizeZ);

            shader.Dispatch(kernelIndex, batch.x, batch.y, batch.z);
        }
    }
}
