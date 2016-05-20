

using System;

namespace ConsoleApplication
{
    public class TicTacToeGame
    {
        private IPlataform plataform;
        private Player player;
        private int[] matrix;

        public TicTacToeGame(IPlataform plataform)
        {
            this.plataform = plataform;
            matrix = new int[plataform.numbOfButtons()];
            reset();
        }

        private void reset()
        {
            resetMatrix();
            player = Player.ONE;
            plataform.resetAllButtons();
        }

        private void resetMatrix()
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = 0;
            }
        }

        private Player changePlayer()
        {
            if (player == Player.ONE)
            {
                return Player.TWO;
            }
            return Player.ONE;
        }

        private int getNumberByPlayer()
        {
            if (player == Player.ONE)
            {
                return 1;
            }
            return 2;
        }

        private ButtonState getButtonStateByPlayer()
        {
            if (player == Player.ONE)
            {
                return ButtonState.One;
            }
            return ButtonState.Two;
        }

        public void play()
        {
            reset();
        }

        public bool isAWinner()
        {
            int p = getNumberByPlayer();

            if (matrix[0] == p && matrix[1] == p && matrix[2] == p)
            {
                return true;
            }
            else if (matrix[3] == p && matrix[4] == p && matrix[5] == p)
            {
                return true;
            }
            else if (matrix[6] == p && matrix[7] == p && matrix[8] == p)
            {
                return true;
            }
            else if (matrix[0] == p && matrix[3] == p && matrix[6] == p)
            {
                return true;
            }
            else if (matrix[1] == p && matrix[4] == p && matrix[7] == p)
            {
                return true;
            }
            else if (matrix[2] == p && matrix[5] == p && matrix[8] == p)
            {
                return true;
            }
            else if (matrix[0] == p && matrix[4] == p && matrix[8] == p)
            {
                return true;
            }
            else if (matrix[2] == p && matrix[4] == p && matrix[6] == p)
            {
                return true;
            }
            return false;
        }

        private bool isValidButton(int button)
        {
            if(button > 9 || button < 1)
            {
                return false;
            }
            return true; 
        }

        public void setMove(int button)
        {           
            if (isValidButton(button) == true)
            {
                matrix[(button-1)] = getNumberByPlayer();
                plataform.paintButton(button, getButtonStateByPlayer());
                if (isAWinner() == true)
                {
                    plataform.paintAllButtons(getButtonStateByPlayer());
                }
                else
                {
                    player = changePlayer();
                }
            }           
        }
    }
}