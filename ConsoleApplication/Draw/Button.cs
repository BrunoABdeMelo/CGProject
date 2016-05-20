using System.Drawing;

namespace ConsoleApplication
{
    public class Button
    {
        public Color color;
        public Color baseColor;
        public Position position;
        public double raio;      

        public Button(Position position, double raio, Color color)
        {
            this.position = position;
            this.raio = raio;
            this.color = color;
            baseColor = color;           
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
