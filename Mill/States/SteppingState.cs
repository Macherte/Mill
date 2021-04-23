using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormElements;

namespace Mill.States
{
    class SteppingState : GameState
    {
        RoundButton firstClickedButton;
        public override void ChangeState(Mill mill)
        {
            mill.SetState(new GameOverState(mill));
        }

        public override void TakeAction(Mill mill, RoundButton button)
        {
            if (firstClickedButton == null)
            {
                if (!button.Invisible && mill.White == button.White)
                {
                    firstClickedButton = button;
                    button.BackColor = button.White ? Color.LightGray : Color.DarkGray;
                    button.FlatAppearance.MouseDownBackColor = button.White ? Color.LightGray : Color.DarkGray;
                    button.FlatAppearance.MouseOverBackColor = button.White ? Color.LightGray : Color.DarkGray;
                }
            }
            else
            {
                if (button == firstClickedButton)
                {
                    firstClickedButton.BackColor = firstClickedButton.White ? Color.White : Color.Black;
                    firstClickedButton.FlatAppearance.MouseDownBackColor = firstClickedButton.White ? Color.White : Color.Black;
                    firstClickedButton.FlatAppearance.MouseOverBackColor = firstClickedButton.White ? Color.White : Color.Black;
                    firstClickedButton = null;
                }
                else if (button.Invisible)
                {
                    if (mill.GetNumberOf(mill.White) == 3 || firstClickedButton.CheckNeighbour(button))
                    {
                        button.White = mill.White;
                        button.Invisible = false;
                        firstClickedButton.Invisible = true;
                        firstClickedButton = null;
                        int millCount = mill.CheckMill(button);
                        if (millCount > 0)
                            mill.ChangeToMillState(millCount);
                        else
                            mill.White = !mill.White;
                    }
                }
            }
        }
    }
}