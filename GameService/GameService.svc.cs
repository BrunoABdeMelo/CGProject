using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GameService.Enums;
using GameService.Models;

namespace GameService
{

    // NOTE: In order to launch WCF Test Client for testing this service, 
    // please select GameService.svc or GameService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameService : IGameService
    {
        private Game _gameData;

        public GameService()
        {
            _gameData = new Game();
        }

        public Player EnterGame()
        {
            Player player = _gameData.AddPlayerInTheGame();
            return player;
        }

        public Game GetGameData()
        {
            return _gameData;
        }

        public void Play(Player player, int position)
        {
            _gameData.DoPlayerMovement(position,player);
        }

        public void ResetGame()
        {
            _gameData = new Game();
        }
    }
}
