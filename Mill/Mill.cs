using Mill.States;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormElements;

namespace Mill
{
    public partial class Mill : Form
    {
        private GameState state;
        private List<RoundButton> buttons;

        public Mill(GameState state)
        {
            InitializeComponent();
            buttons = new List<RoundButton>()
            {
                btn_0t0, btn_0t1, btn_0t2, btn_0ml, btn_0mr, btn_0b0, btn_0b1, btn_0b2,
                btn_1t0, btn_1t1, btn_1t2, btn_1ml, btn_1mr, btn_1b0, btn_1b1, btn_1b2,
                btn_2t0, btn_2t1, btn_2t2, btn_2ml, btn_2mr, btn_2b0, btn_2b1, btn_2b2
            };
            this.state = state;
            White = true;
        }

        public bool White { get; set; }

        public void SetState(GameState state)
        {
            this.state = state;
        }

        public void ChangeState()
        {
            state.ChangeState(this);
        }

        public void ChangeToMillState(int millCount)
        {
            MessageBox.Show("Mill!");
            GameState currentState = state;
            state = new MillState(millCount, currentState);
        }

        public void ReturnFromMillState()
        {
            (state as MillState).ReturnFromMillState(this);
        }

        public int CheckMill(RoundButton button)
        {
            int millCount = 0;
            var locations = button.GetLocations();
            foreach (var item in locations)
            {
                var elements = (from butt in buttons
                                where (bool)butt.GetType().GetProperty(item).GetValue(butt) == true &&
                                !butt.Invisible &&
                                butt.White == button.White
                                select butt).ToList();
                if (elements.Count == 3)
                    millCount++;
            }

            if (state is MillState && millCount != 0)    //Millstate alatt lett gombra kattintva (vagyis törölni akarjuk azt a gombot) és a kattintott gomb malomban van; megnézem, hogy az összes malomban van-e
            {
                var opponentsAllButtons = (from butt in buttons
                                           where !butt.Invisible &&
                                           butt.White == button.White
                                           select butt).ToList();
                foreach (var item in opponentsAllButtons)
                {
                    byte count = 0;
                    var locs = item.GetLocations();
                    foreach (var subitem in locs)
                    {
                        var elements = (from butt in opponentsAllButtons
                                        where (bool)butt.GetType().GetProperty(subitem).GetValue(butt) == true
                                        select butt).ToList();
                        if (elements.Count == 3)
                            count++;
                    }
                    if (count == 0)
                        return 1;   //ha van olyan gomb, ami nincs malomban
                }
                return 0; //ha minden gomb malomban van
            }

            return millCount;
        }

        public int GetNumberOf(bool white)
        {
            return (from butt in buttons
                    where !butt.Invisible &&
                    butt.White == white
                    select butt).ToList().Count;
        }

        private bool HasValidMove()
        {
            var buttonsToCheck = (from butt in buttons
                                  where !butt.Invisible &&
                                  butt.White == !White
                                  select butt).ToList();

            foreach (var item in buttonsToCheck)
            {
                if (buttons.FirstOrDefault(b => b.Invisible && b.CheckNeighbour(item)) != null)
                    return true;
            }
            return false;
        }

        public void CheckWinner()
        {
            if (!HasValidMove())
                ChangeState();
            else if (!(state is PlaceButtonsState) && GetNumberOf(!White) < 3)
                ChangeState();
        }


        private void MillButtonClick(object sender, EventArgs e)
        {
            RoundButton button = (sender as RoundButton);
            state.TakeAction(this, button);
        }
    }
}
