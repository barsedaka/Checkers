using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGameLogic
{
    public enum eMoveType
    {
        Ilegal,
        IlegalNotSameColor,
        IlegalNotEmptyDestination,
        IlegalNeedToEat,
        IlegalNotValidEatMove,
        Regular,
        Eat,
        NeedToEatAgain,
    }
}