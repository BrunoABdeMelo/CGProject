using GameService.Enums;
using GameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GameService
{
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        Player EnterGame();

        [OperationContract]
        Game GetGameData();

        [OperationContract]
        void Play(Player player, int position);
    }
}
