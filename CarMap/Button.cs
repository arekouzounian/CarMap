using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CarMap
{

    class Button
    {

        public enum State
        {
            None,
            Pressed,
            Hover,
            Released
        }

        private Rectangle _rectangle;
        private State _state;
        public State buttonState
        {
            get { return _state; }
            set { _state = value; } //events go here
        }

        private Dictionary<State, Texture2D> _textures;

        public Button(Rectangle rectangle, Texture2D noneTexture, Texture2D hoverTexture, Texture2D pressedTexture)
        {
            _rectangle = rectangle;
            _textures = new Dictionary<State, Texture2D>
            {
                { State.None, noneTexture },
                { State.Hover, hoverTexture },
                { State.Pressed, pressedTexture },
                { State.Released, noneTexture }
            };
        }

        public void Update(MouseState mouseState)
        {
            if (_rectangle.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    buttonState = State.Pressed;
                else
                    buttonState = buttonState == State.Pressed ? State.Released : State.Hover;
            }
            else
            {
                buttonState = State.None;
            }
        }

        // Make sure Begin is called on s before you call this function
        public void Draw(SpriteBatch s)
        {
            s.Draw(_textures[buttonState], _rectangle, Color.White);
        }

    }
}