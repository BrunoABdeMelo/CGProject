using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ConsoleApplication
{
    public class Light
    {
        public void lightLoad()
        {
            GL.Enable(EnableCap.VertexArray);
            GL.Enable(EnableCap.ColorArray);
            GL.ClearColor(Color4.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);
            GL.ShadeModel(ShadingModel.Flat);
            GL.Enable(EnableCap.Texture2D);            

            float[] light_ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light_diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] light_specular = { 1.0f, 1.0f, 1.0f, 0.0f };
            float[] spotdirection = { 0.0f, 0.0f, -1.0f };
            float[] light_position = { 0.0f, 0.0f, 0.0f, 1.0f };

            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, light_specular);                        
        }

        public void enableLigh()
        {
            
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.ColorMaterial);
        }

        public void disableLight()
        {
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.Normalize);
            GL.Disable(EnableCap.ColorMaterial);
        }
    }
}