using FormElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mill.States
{
    class GameOverState : GameState
    {
        public GameOverState(Mill mill)
        {
            MessageBox.Show(string.Format("GAME OVER! Winner: {0}", mill.White ? "White" : "Black"));
        }

        public override void ChangeState(Mill mill)
        {
            throw new NotImplementedException();
        }

        public override void TakeAction(Mill mill, RoundButton button)
        {
            throw new NotImplementedException();
        }
    }
}
