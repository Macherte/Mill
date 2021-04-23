using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormElements;

namespace Mill.States
{
    class PlaceButtonsState : GameState
    {
        public override void ChangeState(Mill mill)
        {
            mill.SetState(new SteppingState());
        }
        public byte ClickCounter { get; set; }

        public override void TakeAction(Mill mill, RoundButton button)
        {
            if (button.Invisible)
            {
                button.White = mill.White;
                button.Invisible = false;
                ClickCounter++;
                int millCount = mill.CheckMill(button);
                if (millCount > 0)
                    mill.ChangeToMillState(millCount);
                else
                {
                    if (ClickCounter == 18)
                    {
                        mill.ChangeState();
                        mill.CheckWinner();
                    }
                    mill.White = !mill.White;
                }
            }
        }
    }
}
