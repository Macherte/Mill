using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormElements;

namespace Mill.States
{
    public abstract class GameState
    {
        public abstract void ChangeState(Mill mill);
        public abstract void TakeAction(Mill mill, RoundButton button);
    }
}
