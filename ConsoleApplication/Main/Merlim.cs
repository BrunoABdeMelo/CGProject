using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;



namespace ConsoleApplication
{
    public enum MerlimState { GameTwo, GameOne, Start };

    public class Merlim : GameWindow, IPlataform
    {
        private Matrix4 matrixProjection;
        private Body body;
        private Painel painel;
        private Light light;
        private Camera camera;
        private ShellGame shellGame;
        private TicTacToeGame ticTacToeGame;
        private MerlimState merlimState;

        private bool updateDraw;
        private bool mouseClick;
        private bool keyboardCameraMove;
        private int numberRotateFrames;
        private int countRotateFrames;
        private int count = 0;
        private Action rotation;
        private Point pointerPosition;
        private Microsoft.VisualBasic.Devices.Audio audio;
        int key = 1;

        public Merlim() : base(640, 480, GraphicsMode.Default, "Merlin", GameWindowFlags.Default, DisplayDevice.Default, 2, 1, GraphicsContextFlags.Debug)
        {
            body = new Body(Color.Red, Color.Black);
            painel = new Painel();
            light = new Light();
            camera = new Camera();
            shellGame = new ShellGame(this);
            ticTacToeGame = new TicTacToeGame(this);
            merlimState = MerlimState.Start;

            Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Keyboard_KeyDown);
            Mouse.ButtonDown += new EventHandler<MouseButtonEventArgs>(Mouse_KeyDown);

            updateDraw = true;
            mouseClick = false;
            rotation = null;
            numberRotateFrames = 30;
            countRotateFrames = 0;

            pointerPosition = new Point();

            audio = new Microsoft.VisualBasic.Devices.Audio();
        }

        // Interface
        public int numbOfButtons()
        {
            return painel.buttons.Length;
        }

        public void paintButton(int position, ButtonState buttonState)
        {
            if (position <= painel.buttons.Length && position >= 0)
            {
                painel.buttons[position].changeState(buttonState);
                updateDraw = true;
            }
        }

        public void resetButton(int position)
        {
            if (position <= painel.buttons.Length && position >= 0)
            {
                painel.buttons[position].changeState(ButtonState.Zero);
                updateDraw = true;
            }

        }

        public void paintAllButtons(ButtonState buttonstate)
        {
            for (int i = 0; i < painel.buttons.Length; i++)
            {
                painel.buttons[i].changeState(buttonstate);
            }
            updateDraw = true;
        }

        public void resetAllButtons()
        {
            for (int i = 0; i < painel.buttons.Length; i++)
            {
                painel.buttons[i].changeState(ButtonState.Zero);
            }
            updateDraw = true;
        }
        //--------------------------------//

        // Events
        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {


            if (key == 2)
            {
                key = 2;
            }
            switch (e.Key)
            {
                case Key.Escape:
                    Exit();
                    break;
                case Key.Left:
                    keyboardCameraMove = true;
                    rotation = camera.cameraRotationYB;
                    break;
                case Key.Right:
                    keyboardCameraMove = true;
                    rotation = camera.cameraRotationYA;
                    break;
                case Key.Up:
                    keyboardCameraMove = true;
                    rotation = camera.cameraRotationXA;
                    break;
                case Key.Down:
                    rotation = camera.cameraRotationXB;
                    keyboardCameraMove = true;
                    break;
                case Key.Z:
                    rotation = camera.zoomIn;
                    keyboardCameraMove = true;
                    break;
                case Key.X:
                    rotation = camera.zoomOut;
                    keyboardCameraMove = true;
                    break;
                case Key.A:
                    rotation = camera.cameraRotationZA;
                    keyboardCameraMove = true;
                    break;
                case Key.S:
                    rotation = camera.cameraRotationZB;
                    keyboardCameraMove = true;
                    break;
                case Key.Number1:
                    merlimState = MerlimState.GameOne;
                    shellGame.play();
                    break;
                case Key.Number2:
                    merlimState = MerlimState.GameTwo;
                    ticTacToeGame.play();
                    break;
                case Key.Space:
                    merlimState = MerlimState.Start;
                    resetGames();
                    break;
                case Key.H:
                    string path1 = "Content/isayhey.wav";
                    audio.Play(path1);
                    body.changeTexture(1);
                    updateDraw = true;
                    break;
                case Key.N:
                    audio.Stop();
                    body.changeTexture(0);
                    updateDraw = true;
                    break;
                case Key.I:
                    string path2 = "Content/italoSong.wav";
                    audio.Play(path2);
                    body.changeTexture(2);
                    updateDraw = true;
                    break;
                default:
                    break;
            }
            key++;
        }

        private void Mouse_KeyDown(object sender, MouseButtonEventArgs e)
        {
            if (e.IsPressed == true)
            {
                pointerPosition = e.Position;
                mouseClick = true;
                updateDraw = true;
            }
        }
        //--------------------------------//

        // GameWindow Override
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            rotateEvent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            light.lightLoad();

            matrixProjection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1f, 100f);

            GL.Viewport(0, 0, this.Width, this.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref matrixProjection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            Vector3 m_eye = new Vector3(0f, 0, 25);
            Vector3 target = new Vector3(0f, 0f, 0f);
            Vector3 up = new Vector3(0f, 1f, 0f);

            matrixProjection = Matrix4.LookAt(m_eye, target, up);
            GL.LoadMatrix(ref matrixProjection);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
            matrixProjection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1f, 100f);
            updateDraw = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            rollingGameEvent();

            if (updateDraw == true)
            {
                drawEvent();             
            }
          

        }
        //--------------------------------//

        // Actions for events
        private void rotateEvent()
        {
            if (keyboardCameraMove == true && countRotateFrames < numberRotateFrames)
            {
                rotation.Invoke();
                countRotateFrames++;
                updateDraw = true;
            }
            else
            {
                keyboardCameraMove = false;
                countRotateFrames = 0;
            }
        }

        private void rollingGameEvent()
        {
            if (merlimState == MerlimState.GameOne && shellGame.isRunning())
            {
                shellGame.play();
            }
        }

        private void mouseClickEvent()
        {
            Color cor = getColor(pointerPosition);

            if (isValidButtonColor(cor))
            {
                if (merlimState == MerlimState.GameOne && shellGame.isRunning() == false)
                {
                    shellGame.setAnswer(getButtonFromColor(cor));
                }
                else if (merlimState == MerlimState.GameTwo)
                {
                    ticTacToeGame.setMove(getButtonFromColor(cor));
                }
            }
        }

        private void drawEvent()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (mouseClick == true)
            {
                light.disableLight();
                draw();
                mouseClickEvent();
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                light.enableLigh();
                mouseClick = false;
            }           
            
            draw();
            updateDraw = false;
            SwapBuffers();          
        }

        private void draw()
        {
            body.buildBody();
            painel.buildPainel();
        }
        //--------------------------------//

        // States,Checks,Parsers,Validation
        private void resetGames()
        {
            shellGame.reset();
        }

        private ButtonState switchButtonZeroOne(ButtonState buttonState)
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
        //--------------------------------//
    }
}
