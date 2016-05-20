﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication
{
    class Body
    {
        public float[] bodyVertex;
        public byte[] bodyTriangles;
        public Color[] buttonColors;
        public Color sideColor;
        public Color lineColor;
        public int colorNow;
        Texture2D texture;
        

        

        public Body(Color sideColor, Color lineColor)
        {
            bodyVertex = startVertex();
            bodyTriangles = startTriangles();
            setButtonColors();
            colorNow = 0;
            this.sideColor = sideColor;
            this.lineColor = lineColor;
            this.sideColor = Color.White;
            texture =  ContentPipe.LoadTexture("heman.jpg");

        }

        private void setButtonColors()
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

                Color.Black
            };

            buttonColors = result;
        }

        public void buildBody()
        {

            buildSides();
            buildLines();

        }

        private void buildSides()
        {
            for (int i = 0; i < bodyTriangles.Length; i = i + 3)
            {

                byte vertex1 = bodyTriangles.ElementAt(i);
                byte vertex2 = bodyTriangles.ElementAt(i + 1);
                byte vertex3 = bodyTriangles.ElementAt(i + 2);

                float vertex1x = bodyVertex.ElementAt(3 * vertex1);
                float vertex1y = bodyVertex.ElementAt(3 * vertex1 + 1);
                float vertex1z = bodyVertex.ElementAt(3 * vertex1 + 2);

                float vertex2x = bodyVertex.ElementAt(3 * vertex2);
                float vertex2y = bodyVertex.ElementAt(3 * vertex2 + 1);
                float vertex2z = bodyVertex.ElementAt(3 * vertex2 + 2);

                float vertex3x = bodyVertex.ElementAt(3 * vertex3);
                float vertex3y = bodyVertex.ElementAt(3 * vertex3 + 1);
                float vertex3z = bodyVertex.ElementAt(3 * vertex3 + 2);

                Vector3 a = new Vector3();
                a.X = vertex1x;
                a.Y = vertex1y;
                a.Z = vertex1z;

                Vector3 b = new Vector3();
                b.X = vertex2x;
                b.Y = vertex2y;
                b.Z = vertex2z;

                Vector3 c = new Vector3();
                c.X = vertex3x;
                c.Y = vertex3y;
                c.Z = vertex3z;

                var dir = Vector3.Cross(b - a, c - a);
                var norm = Vector3.Normalize(dir);


                /*
                 * 
                 *  GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.Red);

            GL.TexCoord2(0, 0);
            GL.Vertex3(10, 0, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex3(0, 10, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex3(0, 0, 10);

            GL.End();
                 * */
                GL.BindTexture(TextureTarget.Texture2D, texture.ID);
                GL.Begin(PrimitiveType.Triangles);
                GL.Color3(sideColor);

                GL.Normal3(-norm);
                GL.TexCoord2(0, 0);
                GL.Vertex3(vertex3x, vertex3y, vertex3z);
                GL.TexCoord2(1, 0);
                GL.Vertex3(vertex2x, vertex2y, vertex2z);
                GL.TexCoord2(1, 1);
                GL.Vertex3(vertex1x, vertex1y, vertex1z);



            }

            GL.End();

        }

        private void buildLines()
        {
            byte[] result =
            {
                0,1,
                10,11,
                0,3,
                3,9,
                9,10,
                1,2,
                2,8,
                8,11,
                0,5,
                5,6,
                6,10,
                1,4,
                4,7,
                7,11,
                5,4,
                9,8,
                3,2,
                3,5,
                2,4,
                9,6,
                8,7,
            };


            for (int i = 0; i < result.Length; i = i + 2)
            {
                byte vertex1 = result.ElementAt(i);
                byte vertex2 = result.ElementAt(i + 1);


                float vertex1x = bodyVertex.ElementAt(3 * vertex1);
                float vertex1y = bodyVertex.ElementAt(3 * vertex1 + 1);
                float vertex1z = bodyVertex.ElementAt(3 * vertex1 + 2);

                float vertex2x = bodyVertex.ElementAt(3 * vertex2);
                float vertex2y = bodyVertex.ElementAt(3 * vertex2 + 1);
                float vertex2z = bodyVertex.ElementAt(3 * vertex2 + 2);




                GL.Begin(PrimitiveType.Lines);
                GL.Color3(lineColor);
                GL.Vertex3(vertex1x, vertex1y, vertex1z);
                GL.Vertex3(vertex2x, vertex2y, vertex2z);
                GL.End();
            }




        }

        private byte[] startTriangles()
        {
            byte[] result =
            {
                //forma 1
              
                0,1,2,
                0,2,3,
                0,5,4,
                0,4,1,
                0,3,5,
                1,4,2,
             
                // forma 2
                2,3,5,
                2,4,5,
                8,9,6,
                8,7,6,
                2,9,3,
                2,8,9,
                4,5,6,
                4,6,7,
                3,6,5,
                3,9,6,
                2,4,7,
                2,7,8,
                
                //forma 3
                8,10,9,
                8,11,10,
                6,11,7,
                6,10,11,
                8,7,11,
                9,10,6,
                };

            return result;

        }

        private float[] startVertex()
        {
            float[] result =
            {
                // vertices de A a L dos triangulos
                0,6,0,
                9,6,0,
                9,4,9,
                0,4,9,
                9,0,9,
                0,0,9,
                0,0,23,
                9,0,23,
                9,4,23,
                0,4,23,
                0,0,32,
                9,0,32,
            };

            for (int i = 0; i < result.Length; i = i + 3)
            {
                result[i] = result.ElementAt(i) - 4.5f;
                result[i + 1] = result.ElementAt(i + 1) - 2;
                result[i + 2] = result.ElementAt(i + 2) - 16f;

            }

            return result;
        }

    }
}