using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Linq;

namespace ConsoleApplication
{
    public class Painel
    {
        public Button[] buttons;
        public Color[] colors;
        public Position[] buttonsPosition;       

        public Painel()
        {
            colors = getVectorColor();
            buttonsPosition = getButtonsPosition();
            buttons = new Button[colors.Length];          
            for (int i = 0; i < colors.Length; i++)
            {
                buttons[i] = new Button(buttonsPosition.ElementAt(i), 1, colors.ElementAt(i));
            }
        }

        public void buildPainel()
        {
            GL.Disable(EnableCap.Texture2D);

            for (int i = 0; i < buttons.Length; i++)
            {
                Vector3 norm = new Vector3();
                norm.X = 0;
                norm.Y = 1;
                norm.Z = 0;
                double calx, calz;
                for (double j = 0; j < 10; j += 0.0007)
                {  
                    GL.Begin(PrimitiveType.Lines);
                    GL.Color3(buttons[i].color);
                    GL.Normal3(norm);

                    calx = Math.Cos(j) + buttons[i].position.x;
                    calz = Math.Sin(j) + buttons[i].position.z;
                  
                    GL.Vertex3(buttons[i].position.x, buttons[i].position.y, buttons[i].position.z);                 
                    GL.Vertex3(calx, buttons[i].position.y, calz);
                    GL.End();
                }
            }
            GL.Enable(EnableCap.Texture2D);
        }

        private Color[] getVectorColor()
        {
            Color[] result =
            {
                Color.WhiteSmoke,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Purple,
                Color.Pink,
                Color.Gray,
                Color.Brown,
                Color.Cyan,
                Color.Khaki
            };
            return result;
        }

        private Position[] getButtonsPosition()
        {
            Position[] result =
            {
                new Position(0,2.1,-5),
                new Position(-2.5,2.1,-2.5),
                new Position(0,2.1,-2.5),
                new Position(2.5,2.1,-2.5),
                new Position(-2.5,2.1,0),
                new Position(0,2.1,0),
                new Position(2.5,2.1,0),
                new Position(-2.5,2.1,2.5),
                new Position(0,2.1,2.5),
                new Position(2.5,2.1,2.5),
                new Position(0,2.1,5),
            };
            return result;
        }
    }
}