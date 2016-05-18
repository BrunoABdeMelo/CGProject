using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace ConsoleApplication
{
    

    class Button
    {
        public Color color;
        public Color baseColor;
        public Position position;
        public double raio;
        public ButtonState buttonState;

        public Button(Position position, double raio, Color color)
        {
            this.position = position;
            this.raio = raio;
            this.color = color;
            baseColor = color;
            this.buttonState = ButtonState.Zero;
        }

        public void changeState(ButtonState state)
        {

            if (state == ButtonState.One)
            {
                color = Color.Lime;

            }
            else if (state == ButtonState.Two)
            {
                color = Color.Magenta;
            }
            else
            {
                color = baseColor;

            }
        }
    }
}
