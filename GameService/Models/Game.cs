using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using GameService.Enums;

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

        [DataMember]
        public int? LastMovement { get; set; }

        public Player AddPlayerInTheGame()
        {
            Player player = Player;

            ResetGameIfNecessary();

            if (State != GameState.WaitingPlayer)
            {
                throw new FaultException("Players can't join while game is running!");
            }

            UpdateWaitingStatus();

            return player;
        }

        public void DoPlayerMovement(int position,Player player)
        {
            if (BoardPositionIsValid(position) && PlayerCanPlay(player))
            {
                Board[position] = (int)Player;
                LastMovement = position;
                CheckGameOver();
            }
        }

        #region private

        private void StartGame()
        {
            Random random = new Random();
            Player randomPlayer = Player.One;
            Player = randomPlayer;
            State = GameState.Running;
        }

        private void UpdateWaitingStatus()
        {
            switch (Player)
            {
                case Player.One:
                    Player = Player.Two;
                    break;
                case Player.Two:
                    StartGame();
                    break;
            }
        }

        private void ResetGameIfNecessary()
        {
            if (State == GameState.Finished)
            {
                ResetBoard();
                LastMovement = null;
                Player = Player.One;
                State = GameState.WaitingPlayer;
            }
        }

        private void ResetBoard()
        {
            Board = new int[9];
        }

        private bool PlayerCanPlay(Player player)
        {
            return player == Player && State == GameState.Running;
        }

        private bool BoardPositionIsValid(int position)
        {
            return PositionIsInBoardRange(position) && BoardPositionIsClear(position);
        }

        private bool PositionIsInBoardRange(int position)
        {
            return position >= 0 && position < Board.Length;
        }

        private bool BoardPositionIsClear(int position)
        {
            return Board[position] == (int)Player.None;
        }

        private void CheckGameOver()
        {
            if (ActualPlayerIsAWinner())
            {
                State = GameState.Finished;
            }
            else if (IsADraw())
            {
                State = GameState.Finished;
                Player = Player.None;
            }
            else
            {
                ChangePlayer();
            }
        }

        private void ChangePlayer()
        {
            if (Player == Player.One)
            {
                Player = Player.Two;
            }
            else
            {
                Player = Player.One;
            }
        }

        private bool IsADraw()
        {
            return Board.All(t => t != (int)Player.None);
        }

        private bool ActualPlayerIsAWinner()
        {
            int p = (int)Player;

            if (Board[0] == p && Board[1] == p && Board[2] == p)
            {
                return true;
            }
            if (Board[3] == p && Board[4] == p && Board[5] == p)
            {
                return true;
            }
            if (Board[6] == p && Board[7] == p && Board[8] == p)
            {
                return true;
            }
            if (Board[0] == p && Board[3] == p && Board[6] == p)
            {
                return true;
            }
            if (Board[1] == p && Board[4] == p && Board[7] == p)
            {
                return true;
            }
            if (Board[2] == p && Board[5] == p && Board[8] == p)
            {
                return true;
            }
            if (Board[0] == p && Board[4] == p && Board[8] == p)
            {
                return true;
            }
            if (Board[2] == p && Board[4] == p && Board[6] == p)
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}