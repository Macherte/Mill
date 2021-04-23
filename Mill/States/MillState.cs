using FormElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mill.States
{
    class MillState : GameState
    {
        private int millCount;
        private GameState currentState;
        public MillState(int millCount, GameState currentState)
        {
            this.millCount = millCount;
            this.currentState = currentState;
        }
        public override void ChangeState(Mill mill)
        {
            mill.SetState(new GameOverState(mill));
        }

        public void ReturnFromMillState(Mill mill)
        {
            mill.SetState(currentState);
            if (currentState is PlaceButtonsState)
            {
                if ((currentState as PlaceButtonsState).ClickCounter == 18)
                {
                    mill.ChangeState();
                    mill.CheckWinner();
                }
            }
        }

        public override void TakeAction(Mill mill, RoundButton button)
        {
            if (!button.Invisible && mill.White != button.White)
            {
                if (mill.CheckMill(button) == 0)    //kattintott gomb nincs malomban
                {
                    button.Invisible = true;
                    if (--millCount == 0)
                    {
                        mill.ReturnFromMillState();
                        mill.CheckWinner();
                        mill.White = !mill.White;
                    }
                }
            }
        }
    }
}
