using ConsoleApplication.GameServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication.GameServiceReference;

namespace ConsoleApplication
{
    class TicTacToeGameSD
    {
        private IPlataform plataform;
        private Player player;
        private int[] matrix;
        private Player playerTurn;
        private Game gameState;
        private bool drawFlag = false;
        

        private IGameService gameService;
        public TicTacToeGameSD(IPlataform plataform)
        {
            GameServiceClient game = new GameServiceClient();
            game.Open();
            gameService = game;

            player = gameService.EnterGame();
            //gameService.EnterGame();
            
            this.plataform = plataform;
            matrix = new int[plataform.numbOfButtons()];
           
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
            if (player == Player.One)
            {
                plataform.paintButton(0,ButtonState.One);
                plataform.paintButton(10, ButtonState.Two);
            }
            else
            {
                plataform.paintButton(0, ButtonState.Two);
                plataform.paintButton(10, ButtonState.One);
            }      
        }
        

        public void start()
        {
            resetMatrix();
            plataform.resetAllButtons();
            gameState = gameService.GetGameData();
            showStartPlayer();
            
        }

        public bool isFinished()
        {
            gameState = gameService.GetGameData();
            if(gameState.State == GameState.Finished)
            {
                return true;
            }
            return false;
        }

        public bool isMyTurn()
        {
            if (gameState.Player == player && drawFlag == true)
            {
                drawFlag = false;
                return true;
            }
            return false;
        }
                
        public void firstPlayerMove(int button)
        {
            gameState = gameService.GetGameData();
            if (isValidButton(button) == true && gameState.State == GameState.Running && gameState.Player == player)
            {
                plataform.paintButton(button, getButtonStateByPlayer(player));
                gameService.Play(player, button-1);
                if(isFinished() == false)
                {
                    playerTurn = opositePlayer();
                    Thread secondThread = new Thread(secondPlayerMove);
                    secondThread.Start();
                }        
            }
        }

        private Player opositePlayer()
        {
            if(player == Player.One)
            {
                return Player.Two;
            }
            return Player.One;
        }

        private void endOfTheGame()
        {

        }

        private void secondPlayerMove()
        {
            waitingSecondPlayer();          
            drawFlag = true;
        }


        public void finish()
        {

            if(gameState.Player != Player.None)
            {
                plataform.paintAllButtons(getButtonStateByPlayer(gameState.Player));
            }        
           
        }

        public void updateImage()
        {
            if (gameState.LastMovement.HasValue)
            {
                int position = gameState.LastMovement.Value;
                plataform.paintButton(position+1, getButtonStateByPlayer(opositePlayer()));
            }
        }

        private void waitingSecondPlayer()
        {            
            do
            {
                gameState = gameService.GetGameData();
            } while (gameState.Player != player);
        }
        

        private ButtonState getButtonStateByPlayer(Player player)
        {
            if (player == Player.One)
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
