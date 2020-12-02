using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMap
{
    class Car
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        public Vector2 Position;
        public Vector2 Size;
        public int Speed;

        public Car(Vector2 position, Vector2 size, int speed)
        {
            Position = position;
            Size = size;
            Speed = speed;
        }

        public void MoveForward(Direction direction, KeyboardState ks)
        {
            switch (direction)
            {
                case Direction.Up:
                    while (ks.IsKeyDown(Keys.Space))
                    {
                        Position.Y -= Speed;
                    }
                    break;
                case Direction.Down:
                    while (ks.IsKeyDown(Keys.Space))
                    {
                        Position.Y += Speed;
                    }
                    break;
                case Direction.Left:
                    while (ks.IsKeyDown(Keys.Space))
                    {
                        Position.X -= Speed;
                    }
                    break;
                case Direction.Right:
                    while (ks.IsKeyDown(Keys.Space))
                    { 
                        Position.X += Speed;
                    }
                    break;
            }
        }
    }
}
