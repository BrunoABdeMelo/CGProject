
using System;

namespace ConsoleApplication
{
    public enum ShellGameState { Zero, One, Two, Three };
    class ShellGame
    {
        IPlataform plataform;
        public ShellGameState state;
        int countLoopStateOne;
        int answer;
        int countFrame = 0;
        int actionperFrame = 0;



        public ShellGame(IPlataform plataform)
        {
            this.plataform = plataform;
            reset();
        }

        public void play()
        {
            if (state == ShellGameState.Zero)
            {
                initialize();
            }
            else if (state == ShellGameState.One)
            {
                shellGameOne();
            }
            else if (state == ShellGameState.Two)
            {
                shellGameTwo();
            }
            else
            {
                // erro
            }
        }

        public bool isRunning()
        {
            if (state == ShellGameState.One || state == ShellGameState.Two)
            {
                return true;
            }
            return false;
        }

        public void reset()
        {
            plataform.paintAllButtons(ButtonState.Zero);
            state = ShellGameState.Zero;
            countLoopStateOne = -1;
        }

        private void initialize()
        {
            plataform.paintAllButtons(ButtonState.Zero);
            state = ShellGameState.One;
            countLoopStateOne = 0;
        }

        private void shellGameOne()
        {
            answer = getRandomPainelNumber();
            plataform.paintButton(answer, ButtonState.One);
            state = ShellGameState.Two;
            Console.WriteLine(answer);
        }

        private void shellGameTwo()
        {
            if(countFrame > actionperFrame)
            {
                plataform.resetButton(answer);
                countLoopStateOne++;
                if (countLoopStateOne > 10)
                {
                    state = ShellGameState.Three;
                }
                else
                {
                    state = ShellGameState.One;
                }
                countFrame = 0;
            }else
            {
                countFrame++;
            }
           
        }

        public void setAnswer(int button)
        {
            if (state == ShellGameState.Three)
            {
                if (button == answer)
                {
                    plataform.paintAllButtons(ButtonState.One);
                    Console.WriteLine("one win");
                    Console.WriteLine(button);
                }
                else
                {
                    plataform.paintAllButtons(ButtonState.Two);
                    Console.WriteLine("one lose");
                    Console.WriteLine(button);
                }
            }

            state = ShellGameState.Zero;

        }

        private int getRandomPainelNumber()
        {
            Random rnd = new Random();
            return rnd.Next(0, 11);
        }
    }
}