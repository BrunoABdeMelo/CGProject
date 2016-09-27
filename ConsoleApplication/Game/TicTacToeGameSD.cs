using ConsoleApplication.GameServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApplication
{
    class TicTacToeGameSD
    {
        private IPlataform plataform;
        private GameServiceReference.Player player;
        private int[] matrix;
        private GameServiceReference.Player playerTurn;

        private IGameService gameService;
        public TicTacToeGameSD(IPlataform plataform)
        {
            GameServiceClient game = new GameServiceClient();
            game.Open();
            gameService = game;
            player = gameService.EnterGame();
            gameService.EnterGame();
            
            this.plataform = plataform;
            matrix = new int[plataform.numbOfButtons()];
            //start();
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
            if (player == GameServiceReference.Player.One)
            {
                plataform.paintButton(0,ButtonState.One);
                plataform.paintButton(10, ButtonState.Two);
            }
            plataform.paintButton(0, ButtonState.Two);
            plataform.paintButton(10, ButtonState.One);
        }

        

        private GameServiceReference.Player getTurnFromService()
        {
            Game game = gameService.GetGameData();

            if(game.Player == GameServiceReference.Player.One)
            {
                return GameServiceReference.Player.One;
            }else
            {
                return GameServiceReference.Player.Two;
            }
        }

        public void start()
        {
            resetMatrix();
            playerTurn = getTurnFromService();
            plataform.resetAllButtons();
            showStartPlayer();
            
        }
                
        public void firstPlayerMove(int button)
        {
            if (isValidButton(button) == true && playerTurn == player)
            {
                plataform.paintButton(button, getButtonStateByPlayer());
                gameService.Play(player, button);// service.play(button,player)
                playerTurn = opositePlayer();
                 //new thread => isMyturn() ou callback
                // secondPlayerMove();
            }
        }

        private GameServiceReference.Player opositePlayer()
        {
            if(player == GameServiceReference.Player.One)
            {
                return GameServiceReference.Player.Two;
            }
            return GameServiceReference.Player.One;
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
            if (player == GameServiceReference.Player.One)
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
