using GameService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace GameService.Models
{
    [DataContract]
    public class Game
    {
        public Game()
        {
            State = GameState.WaitingPlayer;
            Player = Player.One;
            Board = new int[9];
        }

        [DataMember]
        public GameState State { get; set; }

        [DataMember]
        public Player Player { get; set; }

        [DataMember]
        public int [] Board { get; set; }


        public Player AddPlayerInTheGame()
        {
            Player player = Player;

            switch (Player)
            {
                case Player.One:
                    Player = Player.Two;
                    break;
                case Player.Two:
                    StartGame();
                    break;
                default:
                    break;
            }

            return player;
        }

        private void StartGame()
        {
            Random random = new Random();
            Player randomPlayer = (Player)random.Next(1,2);
            State = GameState.Running;
        }



    }
}