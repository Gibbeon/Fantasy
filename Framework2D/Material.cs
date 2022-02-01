using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Framework2D
{
    public class Material
    {
        public static Texture2D DefaultTexture2D;

        public TextureView ColorMap;
        public float Alpha = .5f;
        Vector3 DiffuseColor;
        Vector3 EmissiveColor;
        bool FogEnabled;
        Vector3 FogColor;
        float FogEnd;
        float FogStart;
        bool TextureEnabled;

        public Material(TextureView textureView) {
            ColorMap = textureView;
            FogEnabled = false;
            TextureEnabled = true;
        }

        public Material(Texture2D? texture2D = null): this (new TextureView(texture2D?? DefaultTexture2D)) {

        }

    }
}