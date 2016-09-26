using ConsoleApplication.GameServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Game
{
    class TicTacToeGameSD
    {
        private IPlataform plataform;
        private Player player;
        private int[] matrix;
        private Player playerTurn;

        public TicTacToeGameSD(IPlataform plataform)
        {
            GameServiceClient game = new GameServiceClient();
            game.Open();
            IGameService gameService = game;

            
            this.plataform = plataform;
            matrix = new int[plataform.numbOfButtons()];
            start();
        }

        private void resetMatrix()
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = 0;
            }
        }


        private void showStartPlayer()
        {
            if (player == Player.ONE)
            {
                plataform.paintButton(0,ButtonState.One);
                plataform.paintButton(10, ButtonState.Two);
            }
            plataform.paintButton(0, ButtonState.Two);
            plataform.paintButton(10, ButtonState.One);
        }

        private void serviceStart()
        {
            // Service.start
            // player = getPlayerFromService();
            Thread thread = new Thread(start);
            thread.Start();
        }

        private void start()
        {
            resetMatrix();
            playerTurn = Player.ONE;
            plataform.resetAllButtons();
            serviceStart();
            showStartPlayer();
            
        }
                
        public void firstPlayerMove(int button)
        {
            if (isValidButton(button) == true && playerTurn == player)
            {
                plataform.paintButton(button, getButtonStateByPlayer());
                // service.play(button,player)
                playerTurn = opositePlayer();
                 //new thread => isMyturn() ou callback
                // secondPlayerMove();
            }
        }

        private Player opositePlayer()
        {
            if(player == Player.ONE)
            {
                return Player.TWO;
            }
            return Player.ONE;
        }

        private void secondPlayerMove()
        {
            // int serviceMatrix = serviceMatrix.getMatrix();

            // int diference = moveDiference(serviceMatrix,matrix)

            // plataform.paintButton(diference, opositePlayer());

            // playerTurn = player;
        }
        private bool isMyTurn()
        {
            //while( ask service == false){thread.sleep(2000);}
                
            return true;
        }
        

        private ButtonState getButtonStateByPlayer()
        {
            if (player == Player.ONE)
            {
                return ButtonState.One;
            }
            return ButtonState.Two;
        }


        private bool isValidButton(int button)
        {
            if (button > 9 || button < 1)
            {
                return false;
            }
            return true;
        }

    }
}
