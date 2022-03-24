using UnityEngine;

/// <summary>
/// 2021-01-12 화 오후 5:19:38, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public static class Texture2DTool
    {
        public static Texture2D CreateTexture(int sizeX, int sizeY, Color32 color)
        {
            var t = new Texture2D(sizeX, sizeY, TextureFormat.RGBA32, false);
            var px = new Color32[sizeX * sizeY];

            for (int i = 0; i < sizeX; ++i)
                for (int j = 0; j < sizeY; ++j)
                {
                    px[i + j * sizeX] = color;
                }

            t.SetPixels32(px);
            t.Apply();
            return t;
        }
    }
}