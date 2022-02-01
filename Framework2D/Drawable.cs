using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Framework2D
{
    public abstract class Drawable : IDrawable
    {
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public bool Visible
        {
            get => _visible;
            set => SetVisible(value);
        }
        public int DrawOrder
        {
            get => _drawOrder;
            set => SetDrawOrder(value);
        }
        private bool _visible;
        private int _drawOrder;

        public Drawable()
        {
            _visible = true;
            _drawOrder = 0;
        }   

        protected void SetVisible(bool value)
        {
            if (_visible != value)
            {
                _visible = value;
                if (VisibleChanged != null)
                {
                    VisibleChanged(this, EventArgs.Empty);
                }
            }
        }
        protected void SetDrawOrder(int value)
        {
            if (_drawOrder != value)
            {
                _drawOrder = value;
                if (DrawOrderChanged != null)
                {
                    DrawOrderChanged(this, EventArgs.Empty);
                }
            }
        }


        public abstract void Draw(GameTime gameTime);
    }
}
