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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGameService" in both code and config file together.
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
