using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;



namespace ConsoleApplication
{
    public enum MerlimState { GameTwo, GameOne, Start };

    class Merlim : GameWindow, IPlataform
    {
        Matrix4 matrixProjection, matrixModelview,cameraMatrix;
        Body body;
        Painel painel;
        Light light;
        
        Camera camera;
        ShellGame shellGame;
        TicTacToeGame ticTacToeGame;
        MerlimState merlimState;

        bool mouseClick;
        bool keyboardCameraMove;
        int keyboardCountEvent;
        Action rotation;

        Point pointerPosition;

        ButtonState btstate = ButtonState.Zero;

        Texture2D texture;

        public Merlim() : base(640, 480, GraphicsMode.Default, "Merlin", GameWindowFlags.Default, DisplayDevice.Default, 2, 1, GraphicsContextFlags.Debug)
        {
            body = new Body(Color.Red, Color.Black);
            painel = new Painel();
            light = new Light();
           
            camera = new Camera(matrixModelview);
            shellGame = new ShellGame(this);
            ticTacToeGame = new TicTacToeGame(this);
            merlimState = MerlimState.Start;

            Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Keyboard_KeyDown);
            Mouse.ButtonDown += new EventHandler<MouseButtonEventArgs>(Mouse_KeyDown);

            mouseClick = false;
            keyboardCountEvent = 0;
            rotation = null;

            pointerPosition = new Point();

           
           
        }

        public int numbOfButtons()
        {
            return painel.buttons.Length;
        }

        public void paintButton(int position, ButtonState buttonState)
        {
            if (position <= painel.buttons.Length && position >= 0)
            {
                painel.buttons[position].changeState(buttonState);
            }
            else
            {
                
            }
        }

        public void resetButton(int position)
        {
            if (position <= painel.buttons.Length && position >= 0)
            {
                painel.buttons[position].changeState(ButtonState.Zero);
            }
            else
            {
                
            }
        }

        public void paintAllButtons(ButtonState buttonstate)
        {
            for (int i = 0; i < painel.buttons.Length; i++)
            {
                painel.buttons[i].changeState(buttonstate);
            }

            /*
            Painel temp = new Painel();
            for(int i = 0; i < painel.buttons.Length; i++)
            {
                temp.buttons[i].changeState(buttonstate);
            }

            painel = temp;
            */
        }

        public void resetAllButtons()
        {
            for (int i = 0; i < painel.buttons.Length; i++)
            {
                painel.buttons[i].changeState(ButtonState.Zero);
            }
            /*
            painel = new Painel();
            */
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {

            switch (e.Key)
            {

                case Key.Escape:
                    Exit();
                    break;

                case Key.Left:
                    keyboardCameraMove = true;
                    rotation = camera.cameraRotationXZ2;
                    break;

                case Key.Right:
                    keyboardCameraMove = true;
                    rotation = camera.cameraRotationXZ1;
                    break;

                case Key.Up:
                    keyboardCameraMove = true;
                    rotation = camera.ZoomIn;
                    break;

                case Key.Down:
                    rotation = camera.ZoomOut;
                    keyboardCameraMove = true;
                    break;
                case Key.PageUp:
                    rotation = camera.up;
                    keyboardCameraMove = true;
                    break;
                case Key.PageDown:
                    rotation = camera.down;
                    keyboardCameraMove = true;
                    break;
                case Key.Q:
                    merlimState = MerlimState.GameOne;
                    shellGame.play();
                    break;
                case Key.W:
                    merlimState = MerlimState.GameTwo;
                    ticTacToeGame.play();
                    break;
                case Key.Space:
                    merlimState = MerlimState.Start;
                    resetGames();
                    break;
                case Key.T:
                    GL.Translate(0, 1, 0);
                    break;
                case Key.R:
                    GL.Rotate(1, 1, 0, 0);
                    break;
                case Key.N:
                    GL.Enable(EnableCap.Texture2D);
                    break;
                case Key.M:
                    GL.Disable(EnableCap.Texture2D);
                    break;
                default:
                    break;
            }


        }

        void Mouse_KeyDown(object sender, MouseButtonEventArgs e)
        {
            if (e.IsPressed == true)
            {
                pointerPosition = e.Position;
                mouseClick = true;
                light.disableLight();
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //texture = ContentPipe.LoadTexture("tiles.jpg");
            light.lightLoad();

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            

            float aspect = this.Width / Convert.ToSingle(this.Height);
            matrixProjection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect, 0.1f, 1000f);

            GL.Viewport(0, 0, this.Width, this.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref matrixProjection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();


           Vector3 m_eye = new Vector3(0f, 0, 25);
           Vector3 target = new Vector3(0f, 0f, 0f);
           Vector3 up = new Vector3(0f, 1f, 0f);

           matrixProjection = Matrix4.LookAt(m_eye,target, up);

           GL.LoadMatrix(ref matrixProjection);
           
           
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
           
            

           



            
            if (merlimState == MerlimState.GameOne && shellGame.isRunning())
            {
                shellGame.play();
            }

            draw();

            if (mouseClick == true) 
            {
                Color cor = getColor(pointerPosition);

                if (isValidButtonColor(cor))
                {
                    if (merlimState == MerlimState.GameOne && shellGame.isRunning() == false)
                    {     
                        shellGame.setAnswer(getButtonFromColor(cor));
                    }
                    else if(merlimState == MerlimState.GameTwo)
                    {
                        ticTacToeGame.setMove(getButtonFromColor(cor));
                    }
                }
                SwapBuffers();
                light.enableLigh();
                
            }
            mouseClick = false;



            if (keyboardCameraMove == true && keyboardCountEvent < 15)
            {
                rotation.Invoke();
                keyboardCountEvent++;
            }
            else
            {
                keyboardCameraMove = false;
                keyboardCountEvent = 0;
            }

            
            SwapBuffers();


        }

        public void resetGames()
        {
            shellGame.reset();
        }

        public void draw()
        {
            body.buildBody();
            painel.buildPainel();

        }
        /*
        public void draw2()
        {

            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.Red);

            GL.TexCoord2(0, 0);
            GL.Vertex3(10, 0, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex3(0, 10, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex3(0, 0, 10);

            GL.End();
        }
        */
        public void selectActionFromClick()
        {
            shellGame.play();
            /*
            paintAllButtons(btstate);
            btstate = switchButtonZeroOne(btstate);
            */
            /*
           Color cor = getColor(pointerPosition);
           int button = getButtonFromColor(cor);
           paintButton(button, ButtonState.One);
           Console.WriteLine(cor);
           */
        }

        public ButtonState switchButtonZeroOne(ButtonState buttonState)
        {
            if (buttonState == ButtonState.Zero)
            {
                return ButtonState.One;
            }

            return ButtonState.Zero;
        }

        private Color getColor(Point point)
        {



            byte[] color = new byte[1024];
            int[] viewport = new int[4];

            GL.GetInteger(GetPName.Viewport, viewport);
            GL.ReadPixels(point.X, viewport[3] - point.Y - 4, 10, 10, PixelFormat.Rgb, PixelType.UnsignedByte, color);
            Color pixelColor = Color.FromArgb(color[0], color[1], color[2]);
            pixelColor = parseToKnowColor(pixelColor);



            return pixelColor;

        }

        private Color parseToKnowColor(Color color)
        {

            int a = color.A;
            int r = color.R;
            int g = color.G;
            int b = color.B;

            Color result = Color.CornflowerBlue;

            if (Color.Red.A == a && Color.Red.R == r && Color.Red.G == g && Color.Red.B == b)
            {

                result = Color.Red;

            }
            else if (Color.Khaki.A == a && Color.Khaki.R == r && Color.Khaki.G == g && Color.Khaki.B == b)
            {

                result = Color.Khaki;

            }
            else if (Color.WhiteSmoke.A == a && Color.WhiteSmoke.R == r && Color.WhiteSmoke.G == g && Color.WhiteSmoke.B == b)
            {

                result = Color.WhiteSmoke;

            }
            else if (Color.Orange.A == a && Color.Orange.R == r && Color.Orange.G == g && Color.Orange.B == b)
            {

                result = Color.Orange;

            }
            else if (Color.Yellow.A == a && Color.Yellow.R == r && Color.Yellow.G == g && Color.Yellow.B == b)
            {

                result = Color.Yellow;

            }
            else if (Color.Green.A == a && Color.Green.R == r && Color.Green.G == g && Color.Green.B == b)
            {

                result = Color.Green;

            }
            else if (Color.Blue.A == a && Color.Blue.R == r && Color.Blue.G == g && Color.Blue.B == b)
            {

                result = Color.Blue;

            }
            else if (Color.Purple.A == a && Color.Purple.R == r && Color.Purple.G == g && Color.Purple.B == b)
            {

                result = Color.Purple;

            }
            else if (Color.Pink.A == a && Color.Pink.R == r && Color.Pink.G == g && Color.Pink.B == b)
            {

                result = Color.Pink;

            }
            else if (Color.Gray.A == a && Color.Gray.R == r && Color.Gray.G == g && Color.Gray.B == b)
            {

                result = Color.Gray;

            }
            else if (Color.Brown.A == a && Color.Brown.R == r && Color.Brown.G == g && Color.Brown.B == b)
            {

                result = Color.Brown;

            }
            else if (Color.Cyan.A == a && Color.Cyan.R == r && Color.Cyan.G == g && Color.Cyan.B == b)
            {

                result = Color.Cyan;

            }
            else if (Color.Lime.A == a && Color.Lime.R == r && Color.Lime.G == g && Color.Lime.B == b)
            {

                result = Color.Lime;

            }
            else if (Color.Magenta.A == a && Color.Magenta.R == r && Color.Magenta.G == g && Color.Magenta.B == b)
            {

                result = Color.Magenta;

            }


            return result;

        }

        private int getButtonFromColor(Color color)
        {
            if (color != Color.Magenta && color != Color.Lime)
            {
                for (int i = 0; i < painel.buttons.Length; i++)
                {
                    if (painel.buttons[i].color == color)
                    {
                        return i;
                    }
                }
            }            
            return -1;
        }

        private bool isValidButtonColor(Color color)
        {
            if (color != Color.Magenta && color != Color.Lime)
            {
                for (int i = 0; i < painel.buttons.Length; i++)
                {
                    if (painel.buttons[i].color == color)
                    {
                        return true; 
                    }
                }
            }

            return false;
        }
    }
}
