using ConsoleApplication.GameServiceReference;
using System.Threading;

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
        Thread secondThread;

        GameServiceClient game;

        public TicTacToeGameSD(IPlataform plataform)
        {
            game = new GameServiceClient();
            game.Open();
            gameService = game;

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

        private Player opositePlayer()
        {
            if(player == Player.One)
            {
                return Player.Two;
            }
            return Player.One;
        }
        
        private void secondPlayerMove()
        {
            waitingSecondPlayer();          
            drawFlag = true;
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
     
        public void start()
        {
            
            resetMatrix();
            plataform.resetAllButtons();
            player = gameService.EnterGame();
            gameState = gameService.GetGameData();
            showStartPlayer();
            if (player == Player.Two)
            {
                secondThread = new Thread(secondPlayerMove);
                secondThread.Start();
            }

        }

        public void firstPlayerMove(int button)
        {
            gameState = gameService.GetGameData();
            if (isValidButton(button) == true && gameState.State == GameState.Running && gameState.Player == player)
            {
                plataform.paintButton(button, getButtonStateByPlayer(player));
                gameService.Play(player, button - 1);
                gameState = gameService.GetGameData();
                
                if (isFinished() == false)
                {
                    playerTurn = opositePlayer();
                    secondThread = new Thread(secondPlayerMove);
                    secondThread.Start();
                }
            }
        }

        public bool isFinished()
        {
            gameState = gameService.GetGameData();
            if (gameState.State == GameState.Finished)
            {
                return true;
            }
            return false;
        }

        public void finish()
        {
            if (gameState.Player != Player.None)
            {
                plataform.paintAllButtons(getButtonStateByPlayer(gameState.Player));
            }
        }

        public bool isMyTurn()
        {
            if (gameState.Player == player && drawFlag == true)
            {
                drawFlag = false;
                // verificar isso com guri
                secondThread.Abort();
                return true;
            }
            return false;
        }

        public void updateImage()
        {
            if (gameState.LastMovement.HasValue)
            {
               
                int position = gameState.LastMovement.Value;
                plataform.paintButton(position + 1, getButtonStateByPlayer(opositePlayer()));
            }
        }

    }
}
