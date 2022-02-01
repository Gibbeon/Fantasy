using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Framework2D
{   
    public struct TextureView
    {
        public Texture2D Texture2D;
        public Rectangle View;

        public TextureView(Texture2D texture) {
            Texture2D = texture;
            View = texture.Bounds;
        }
    }
}