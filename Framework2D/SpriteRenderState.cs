using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Framework2D
{
    public class SpriteRenderState 
    {
        public SpriteSortMode SpriteSortMode
        {
            get;
            set;
        }
        public BlendState BlendState
        {
            get;
            set;
        }
        public SamplerState SamplerState
        {
            get;
            set;
        }
        public DepthStencilState DepthStencilState
        {
            get;
            set;
        }
        public RasterizerState RasterizerState
        {
            get;
            set;
        }
        public Effect Effect
        {
            get;
            set;
        }

        public SpriteRenderState() 
        {
            SpriteSortMode = SpriteSortMode.Immediate;
        }
    }
}
