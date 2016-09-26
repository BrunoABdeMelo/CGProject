﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GameService.Enums
{
    [DataContract]
    public enum Player
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        One = 1,
        [EnumMember]
        Two = 2
    }
}