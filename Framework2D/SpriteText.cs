using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using Fantasy.Game;

namespace Fantasy.Framework2D
{
    
    public enum VerticalTextAlign
    {
        None,
        Top,
        Middle,
        Bottom
    }

    public enum HorizontalTextAlign
    {
        None,
        Left,
        Center,
        Right
    }

    /*class SpriteText : Sprite
    {
        public SpriteFont Font
        {
            get;
            set;
        }
        public VerticalTextAlign VerticalTextAlign
        {
            get;
            set;
        }
        public HorizontalTextAlign HorizontalTextAlign
        {
            get;
            set;
        }
        public string Text
        {
            get => _text;
            set => SetText(value);
        }
        private string _text = string.Empty;
        public SpriteText(string text, SpriteFont font, 
            VerticalTextAlign verticalTextAlign = VerticalTextAlign.None, 
            HorizontalTextAlign horizontalTextAlign = HorizontalTextAlign.None)
        {
            Font = font;

            HorizontalTextAlign = horizontalTextAlign;
            VerticalTextAlign = verticalTextAlign;

            AmbientColor = Color.Black;

            SetText(text);
        }

        public void SetText(string text)
        {
            _text = text;
            Size = Font.MeasureString(_text);
        }

        public override void DrawTo(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(!Visible) return;

            var yOffset = 0;
            var xOffset = 0;

            switch (VerticalTextAlign)
            {
                case VerticalTextAlign.None:
                case VerticalTextAlign.Top:
                    break;
                case VerticalTextAlign.Middle:
                    //yOffset = (int)(((GameEngine.Instance.GraphicsDevice.Viewport.Height - GameEngine.Instance.GraphicsDevice.Viewport.Y) / 2) - Height / 2);
                    break;
                case VerticalTextAlign.Bottom:
                    //yOffset = (int)((GameEngine.Instance.GraphicsDevice.Viewport.Height - GameEngine.Instance.GraphicsDevice.Viewport.Y) - Height);
                    break;
            }

            switch (HorizontalTextAlign)
            {
                case HorizontalTextAlign.None:
                case HorizontalTextAlign.Left:
                    break;
                case HorizontalTextAlign.Center:
                    //xOffset = (int)(((GameEngine.Instance.GraphicsDevice.Viewport.Width - GameEngine.Instance.GraphicsDevice.Viewport.X) / 2) - Width / 2);
                    break;
                case HorizontalTextAlign.Right:
                    //xOffset = (int)((GameEngine.Instance.GraphicsDevice.Viewport.Width - GameEngine.Instance.GraphicsDevice.Viewport.X) - Width);
                    break;
            }

            spriteBatch.DrawString(
                Font,
                _text, 
                Location + new Vector2(Size.X / 2, Size.Y / 2) * Scale,
                AmbientColor,
                Rotation,
                new Vector2(Size.X / 2, Size.Y / 2),
                Scale,
                SpriteEffects,
                Depth);
        }
    }*/
}
