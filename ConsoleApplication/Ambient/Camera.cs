﻿using OpenTK.Graphics.OpenGL;

namespace ConsoleApplication
{
    public class Camera
    {   
        public void cameraRotationYA()
        {
            GL.Rotate(-0.33, 0, 1, 0);    
        }

        public void cameraRotationYB()
        {
            GL.Rotate(0.33, 0, 1, 0);           
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
            GL.Rotate(-0.33, 0, 0, 1);
        }

        public void cameraRotationZB()
        {
            GL.Rotate(0.33, 0, 0, 1);
        }

        public void zoomIn()
        {            
            GL.Scale(1.005f, 1.005f, 1.005f);
        }

        public void zoomOut()
        {
            GL.Scale(0.995f, 0.995f, 0.995f);            
        }
    }
}