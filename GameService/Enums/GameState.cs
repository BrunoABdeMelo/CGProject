using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GameService.Enums
{
    [DataContract]
    public enum GameState
    {
        [EnumMember]
        WaitingPlayer,

        [EnumMember]
        Running,

        [EnumMember]
        Finished

    }
}