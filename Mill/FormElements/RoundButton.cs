using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace FormElements
{
    public class RoundButton : Button
    {
        private bool invisible;
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            Region = new Region(grPath);

            base.OnPaint(e);
        }

        public bool Invisible
        {
            get { return invisible; }
            set
            {
                if (value)
                {
                    FlatStyle = FlatStyle.Flat;
                    ForeColor = Color.Transparent;
                    BackColor = Color.Transparent;
                    FlatAppearance.MouseDownBackColor = Color.Transparent;
                    FlatAppearance.MouseOverBackColor = Color.Transparent;
                }
                else
                {
                    FlatStyle = FlatStyle.Flat;
                    BackColor = White ? Color.White : Color.Black;
                    ForeColor = White ? Color.White : Color.Black;
                    FlatAppearance.MouseOverBackColor = White ? Color.White : Color.Black;
                    FlatAppearance.MouseDownBackColor = White ? Color.White : Color.Black;
                }
                invisible = value;
            }
        }


        public bool White { get; set; }
        public bool T0 { get; set; }
        public bool T1 { get; set; }
        public bool T2 { get; set; }
        public bool B0 { get; set; }
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool L0 { get; set; }
        public bool L1 { get; set; }
        public bool L2 { get; set; }
        public bool R0 { get; set; }
        public bool R1 { get; set; }
        public bool R2 { get; set; }
        public bool TM { get; set; }
        public bool LM { get; set; }
        public bool BM { get; set; }
        public bool RM { get; set; }

        [DebuggerStepThrough]
        public List<string> GetLocations()
        {
            List<string> locations = new List<string>();
            if (T0) locations.Add("T0");
            if (T1) locations.Add("T1");
            if (T2) locations.Add("T2");
            if (B0) locations.Add("B0");
            if (B1) locations.Add("B1");
            if (B2) locations.Add("B2");
            if (L0) locations.Add("L0");
            if (L1) locations.Add("L1");
            if (L2) locations.Add("L2");
            if (R0) locations.Add("R0");
            if (R1) locations.Add("R1");
            if (R2) locations.Add("R2");
            if (TM) locations.Add("TM");
            if (LM) locations.Add("LM");
            if (BM) locations.Add("BM");
            if (RM) locations.Add("RM");

            if (locations.Count != 2)
                throw new Exception("Locations length must be 2");

            return locations;
        }

        public bool CheckNeighbour(RoundButton button)
        {
            var thisButtonLocation = GetLocations();
            var neighbourButtonLocation = button.GetLocations();

            var differences = thisButtonLocation.Except(neighbourButtonLocation).Concat(neighbourButtonLocation.Except(thisButtonLocation)).ToList();
            if (differences.Count == 2)
            {
                if (differences.FirstOrDefault(b => b.Contains("M")) != null)
                    return true;

                //ha nem tartalmaz M-t:
                byte first = Convert.ToByte(differences[0].Remove(0, 1));
                byte second = Convert.ToByte(differences[1].Remove(0, 1));
                if (Math.Abs((first - second)) == 1)
                    return true;
            }
            return false;
        }
    }
}