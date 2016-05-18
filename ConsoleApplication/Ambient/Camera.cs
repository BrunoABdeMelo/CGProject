using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace ConsoleApplication
{


    internal class Camera
    {
        double zEye = 20;
        double xEye = 0;
        double yEye = 20;
        double raio = 20;
        double angleXZ = 0;
       
        Matrix4 matrixModelview;
        
        public Camera(Matrix4 matrixModelview)
        {
            this.matrixModelview = matrixModelview;
        }
        public void cameraRender()
        {
            Matrix4.CreateRotationY(0, out matrixModelview);

            Vector3 eye = new Vector3((float)xEye, (float)yEye, (float)zEye);
            Vector3 target = new Vector3(0f, 0f, 0f);
            Vector3 up = new Vector3(0f, 1f, 0f);

            matrixModelview *= Matrix4.LookAt(eye, target, up);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref matrixModelview);

        }

        public void cameraRotationXZ1()
        {
            angleXZ = angleXZ + 0.01;
            xEye = raio * Math.Sin(angleXZ);
            zEye = raio * Math.Cos(angleXZ);
        }

        public void cameraRotationXZ2()
        {
            angleXZ = angleXZ - 0.01;
            xEye = raio * Math.Sin(angleXZ);
            zEye = raio * Math.Cos(angleXZ);
        }

        public void ZoomIn()
        {
            xEye = 0.99 * xEye;
            yEye = 0.99 * yEye;
            zEye = 0.99 * zEye;
            raio = 0.99 * raio;
        }

        public void ZoomOut()
        {
            xEye = 1.01 * xEye;
            yEye = 1.01 * yEye;
            zEye = 1.01 * zEye;
            raio = 1.01 * raio;
        }

        public void up()
        {
            yEye += 0.5;
        }

        public void down()
        {
            yEye -= 0.5;
        }
    }
}