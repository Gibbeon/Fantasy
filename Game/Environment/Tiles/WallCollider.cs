using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Aseprite.Graphics;
using MonoGame.Aseprite.Documents;
using Fantasy.Framework2D;
using MonoGame.Extended.Collisions;
using System.Linq;

namespace Fantasy.Game
{
    public class WallCollider : ICollisionActor
    {
        public static int count;
        public IShapeF Bounds { get; set; }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            count++;
        }

        public WallCollider(RectangleF rectangleF)
        {
            Bounds = rectangleF;
        }
    }

    public static class WallColliderExtensions
    {
        public static List<ICollisionActor> GetCollisionActors(this MonoGame.Extended.Tiled.TiledMap map)
        {
            List<ICollisionActor> Result = new List<ICollisionActor>();

            int tiles_x = map.Width;
            int tiles_y = map.Height;
            int pixel_x = map.WidthInPixels;
            int pixel_y = map.HeightInPixels;

            List<(TiledMapTilesetTile, int start)> TileDefition = new List<(TiledMapTilesetTile, int start)>();

            int globalCount = 1;

            foreach (var tileset in map.Tilesets)
            {
                foreach (var tile in tileset.Tiles)
                {
                    TileDefition.Add((tile, globalCount));
                }
                globalCount += tileset.TileCount;
            }

            foreach (var layer in map.TileLayers)
            {
                foreach (var tile in layer.Tiles)
                {
                    var defintion = TileDefition
                        .Where(x => x.Item1.LocalTileIdentifier + x.start == tile.GlobalIdentifier)
                        .ToList();

                    foreach (var d in defintion)
                    {
                        foreach (var o in d.Item1.Objects)
                        {
                            if (o.Type == "WallCollider" && o is TiledMapRectangleObject)
                            {
                                var def = o as TiledMapRectangleObject;
                                RectangleF rect = new RectangleF(
                                    (int)(tile.X * layer.TileWidth + def.Position.X),
                                    (int)(tile.Y * layer.TileHeight + def.Position.Y),
                                    (int)def.Size.Width,
                                    (int)def.Size.Height
                                    );

                                Result.Add(new WallCollider(rect));
                            }
                        }
                    }
                }
            }
            return Result;
        }
    }
}