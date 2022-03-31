
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace UnityCommon
{
    public class AssetTextureArrayCreater : MonoBehaviour
    {
        public Vector2Int PixeSize;
        public bool KeepRatioFromIndex0;
        public TextureFormat Format = TextureFormat.DXT1;
        public Texture2D[] TexturesAlbedo;
        public Texture2D[] TexturesNormal;

        #region TEXFunc
        Texture2D DuplicateTexture(Texture2D source)
        {
            // Temporal Render Texture, Blit
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.ARGB32,
                        RenderTextureReadWrite.Default);
            Graphics.Blit(source, renderTex); // copy src to render tex
            RenderTexture previousTemp = RenderTexture.active;
            RenderTexture.active = renderTex; // set main

            // Generate dst Texture2D
            Texture2D dst = new Texture2D(source.width, source.height);
            dst.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0); // copy render to dst
            dst.Apply();

            // Finalze
            RenderTexture.active = previousTemp;
            RenderTexture.ReleaseTemporary(renderTex);

            return dst;
        }

        void ResizeTexture(Color[] src, int srcWidth, int srcHeight, Color[] dst, int dstWidth, int dstHeight)
        {
            if (src == null || srcWidth <= 0 || srcHeight <= 0 || dstWidth <= 0 || dstHeight <= 0)
            {
                dst = new Color[Mathf.Max(dstWidth,0) * Mathf.Max(dstHeight, 0)];
                return;
            }
            if (dst == null)
            {
                dst = new Color[dstWidth * dstHeight];
            }

            // 데스티네이션이 소스보다 크면 필터가 1이하이고 반대면 1보다 크다.
            // 데스티네이션이 소스보다 크면 소스의 인덱스를 0.x만큼 이동시킨다. (필터크기)
            // 데스티네이션이 소스보다 작으면 n만큼 이동시킨다. (필터크기)
            // 두 경우 모두 소스 이동량만큼 필터링한다. (좌우상하 4방위 각각 이동량/2 사이즈의 직사각형)

            float SrcDeltaX = (float)srcWidth / dstWidth;
            float SrcDeltaY = (float)srcHeight / dstHeight;
            float srcIndexX = 0;
            float srcIndexY = 0;

            for (int i = 0; i < dstWidth; ++i)
            {
                for (int j = 0; j < dstHeight; ++j)
                {
                    Color peek = AverageFiltering(src, srcWidth, srcHeight, srcIndexX, srcIndexY, SrcDeltaX, SrcDeltaY);

                    dst[Serial(i, j, dstWidth, dstHeight)] = peek;

                    srcIndexY += SrcDeltaY;
                }

                srcIndexX += SrcDeltaX;
            }
        }

        Color AverageFiltering(Color[] src, int srcWidth, int srcHeight, float centerX, float centerY, float filterX, float filterY)
        {
            // 인덱스가 사이즈 벗어날경우 repeat
            // 좌측 하단부터, 각 축에 대해 남은 필터 크기가 1보다 클경우 1씩 증가시키며 peek 한다.
            // 1보다 작은 필터 크기가 남았을경우 픽하고 cnt값을 소수점만큼 증가시켜서 peek하고 털어낸다.
            // 추후 고려 : clamp 모드? and 필터링 정밀도? (필터 축 몇등분할것인지 또는 얼마나 증가시키며 peek할것인지)

            Color peek = Color.black;
            float cnt = 0;

            // Y Init
            float posY = (centerY - filterY / 2);
            float lastMoveDeltaY = 1;
            float filterXOriginal = filterX;

            while (filterY >= 0)
            {
                // X Init
                filterX = filterXOriginal;
                float posX = (centerX - filterX / 2);
                float lastMoveDeltaX = 1;

                while (filterX >= 0)
                {
                    // 현위치 픽하고, (Repeat)
                    Color peekNow = Peek(src, posX, posY, srcWidth, srcHeight);

                    // 현위치 픽한값 결과에 더하고 (현위치 도달시까지 이동한 값이 비중)
                    float delta = lastMoveDeltaX * lastMoveDeltaY;
                    float lp = cnt / (cnt + delta);
                    float rp = delta / (cnt + delta);
                    peek = peek * lp + peekNow * rp;
                    cnt += delta;

                    // X Move to next
                    if (filterX >= 1)
                    {
                        posX += 1;
                        filterX -= 1;
                        lastMoveDeltaX = 1;
                    }
                    else if (0 < filterX)
                    {
                        posX += filterX;
                        lastMoveDeltaX = filterX;
                        filterX = 0;
                    }
                    else if (filterX <= 0)
                        break;
                }

                // Y Move to next
                if (filterY > 1)
                {
                    posY += 1;
                    filterY -= 1;
                    lastMoveDeltaY = 1;
                }
                else if (0 < filterY)
                {
                    posY += filterY;
                    lastMoveDeltaY = filterY;
                    filterY = 0;
                }
                else if (filterY <= 0)
                    break;
            }

            return peek;
        }

        // Repeat
        Color Peek(Color[] src, float x, float y, int width, int height)
        {
            while (x < 0) x += width;
            while (y < 0) y += height;

            int posXLeft = Mathf.FloorToInt(x) % width;
            int posXRigt = Mathf.CeilToInt(x) % width;
            int posYBot = Mathf.FloorToInt(y) % height;
            int posYTop = Mathf.CeilToInt(y) % height;

            float percentX = x - posXLeft;
            float percentY = y - posYBot;
            Color colorLeftBot = src[Serial(posXLeft, posYBot, width, height)];
            Color colorRigtBot = src[Serial(posXRigt, posYBot, width, height)];
            Color colorBot = Color.Lerp(colorLeftBot, colorRigtBot, percentX);
            Color colorLeftTop = src[Serial(posXLeft, posYTop, width, height)];
            Color colorRightTop = src[Serial(posXRigt, posYTop, width, height)];
            Color colorTop = Color.Lerp(colorLeftTop, colorRightTop, percentX);
            Color peek = Color.Lerp(colorBot, colorTop, percentY);

            //return src[Serial(Mathf.FloorToInt(x), Mathf.FloorToInt(y), width, height)];

            return peek;
        }

        int Serial(int x, int y, int xsize, int ysize)
        {
            return y * xsize + x;
        }
        #endregion TEXFunc

        private Texture2D UnpackDXTnm(Texture2D tex)
        {
            Vector2 xy = new Vector2();
            Color[] colors = tex.GetPixels();

            for (int i = 0; i < colors.Length; i++)
            {
                Color packed = colors[i];
                Color unpacked;

                xy.x = packed.a * 2 - 1;
                xy.y = packed.g * 2 - 1;
                unpacked.b = Mathf.Sqrt(1 - Mathf.Clamp01(Vector2.Dot(xy, xy))) * 0.5f + 0.5f;
                unpacked.r = xy.x * 0.5f + 0.5f;
                unpacked.g = xy.y * 0.5f + 0.5f;
                unpacked.a = 1;

                colors[i] = unpacked;
            }

            tex.SetPixels(colors); //apply pixels to the texture
            tex.Apply();

            return tex;

            /*
             half4 CustomUnpackNormal(half4 packednormal){  
                half4 normal;
                normal.xy = packednormal.wy * 2 - 1;
                normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                normal.xyz = normal.xyz*0.5+0.5;
                return half4(normal.xyz,1);
            }
            */
        }

        public Texture2DArray Create(Texture2D[] textures, int width, int height, TextureFormat format, bool isNormal)
        {
            if (textures.Length < 1) throw new InvalidOperationException("texlength<1");
            if (width < 1) throw new InvalidOperationException("width<1");
            if (height < 1) throw new InvalidOperationException("height<1");
            while (width % 4 != 0) ++width; // for fit compression dxt
            while (height % 4 != 0) ++height; // for fit compression dxt

            // generate new t2array
            Texture2DArray texture2DArray;
            texture2DArray = new Texture2DArray(width, height, textures.Length, format, false);          

            // default setting
            texture2DArray.filterMode = FilterMode.Bilinear;
            texture2DArray.wrapMode = TextureWrapMode.Repeat;

            // copy
            Color[] resizedColors = new Color[width * height];
            for (int i = 0; i < textures.Length; i++)
            {
                // duplicate to read
                Texture2D readableTex = DuplicateTexture(textures[i]);
                Color[] readingColors = readableTex.GetPixels();

                // resize
                ResizeTexture(readingColors, readableTex.width, readableTex.height, resizedColors, width, height);

                // new tex
                Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
                texture.SetPixels(resizedColors);
                texture.Apply();

                // unpack normal
                if (isNormal)
                {
                    //UnpackDXTnm(texture);
                }

                // compress
                EditorUtility.CompressTexture(texture, format, TextureCompressionQuality.Normal);

                // copy
                Graphics.CopyTexture(texture, 0, texture2DArray, i);
            }

            // apply
            texture2DArray.Apply();

            return texture2DArray;
        }

        public void SaveEditorAsset(UnityEngine.Object asset, string path = "Assets/New Asset.asset")
        {
#if UNITY_EDITOR
            path = AssetDatabase.GenerateUniqueAssetPath(path);
            AssetDatabase.CreateAsset(asset, path);
            Debug.Log("Saved asset to " + path);
#endif
        }

        public Texture2DArray CreateReturnAlbedo()
        {
            if (KeepRatioFromIndex0)
            {
                int max = Mathf.Max(TexturesAlbedo[0].width, TexturesAlbedo[0].height);
                float ratioX = (float)TexturesAlbedo[0].width / max;
                float ratioY = (float)TexturesAlbedo[0].height / max;

                int maxPixel = Mathf.Max(PixeSize.x, PixeSize.y);
                PixeSize.x = Mathf.CeilToInt(maxPixel * ratioX);
                PixeSize.y = Mathf.CeilToInt(maxPixel * ratioY);
            }

            var array = Create(TexturesAlbedo, PixeSize.x, PixeSize.y, Format, false);

            return array;
        }

        [InspectorButton("CreateAlbedo", "Create Albedo")] public bool a0;
        public void CreateAlbedo()
        {
            var array = CreateReturnAlbedo();
            SaveEditorAsset(array, string.Format("Assets/Texture2DArrayAlbedo({0})_{1}x{2}_{3}_0.asset", array.depth, array.width, array.height, array.format));
        }

        [InspectorButton("CreateNormalMaps", "Create Normal")] public bool a1;
        public void CreateNormalMaps()
        {
            if (KeepRatioFromIndex0)
            {
                int max = Mathf.Max(TexturesNormal[0].width, TexturesNormal[0].height);
                float ratioX = (float)TexturesNormal[0].width / max;
                float ratioY = (float)TexturesNormal[0].height / max;

                int maxPixel = Mathf.Max(PixeSize.x, PixeSize.y);
                PixeSize.x = Mathf.CeilToInt(maxPixel * ratioX);
                PixeSize.y = Mathf.CeilToInt(maxPixel * ratioY);
            }

            var array = Create(TexturesNormal, PixeSize.x, PixeSize.y, TextureFormat.DXT5, true);
            SaveEditorAsset(array, string.Format("Assets/Texture2DArrayNormal({0})_{1}x{2}_{3}_0.asset", array.depth, array.width, array.height, array.format));
        }
    }
}
#endif