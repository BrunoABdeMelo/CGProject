using OpenTK.Graphics.OpenGL;

namespace ConsoleApplication
{
    public class Camera
    {   
        public void cameraRotationYA()
        {
            GL.Rotate(-0.5, 0, 1, 0);    
        }

        public void cameraRotationYB()
        {
            GL.Rotate(0.5, 0, 1, 0);           
        }

        public void cameraRotationXA()
        {
            GL.Rotate(0.5, 1, 0, 0);            
        }

        public void cameraRotationXB()
        {
            GL.Rotate(-0.5, 1, 0, 0);            
        }

        public void cameraRotationZA()
        {
            GL.Rotate(-0.5, 0, 0, 1);
        }

        public void cameraRotationZB()
        {
            GL.Rotate(0.5, 0, 0, 1);
        }

        public void zoomIn()
        {            
            GL.Scale(1.005f, 1.005f, 1.005f);
        }

        public void zoomOut()
        {
            GL.Scale(0.995f, 0.995f, 0.995f);            
        }

        public void mode1()
        {
            GL.Rotate(-0.5, 0, 1, 0);
            GL.Rotate(1, 1, 0, 0);
            GL.Scale(0.995f, 0.995f, 0.995f);
        }

        public void mode2()
        {
            GL.Scale(1.005f, 1.005f, 1.005f);
            GL.Rotate(-1, 1, 0, 0);
            GL.Rotate(0.5, 0, 1, 0);
        }
    }
}