using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Face
{
    class Eye
    {
        private int _x;
        private int _y;
        public int diam;
        Pupil pupil;
        bool kill = false;
        Image IMG = Image.FromFile("C:\\Users\\alexe\\source\\repos\\Face\\eye_dye.png");

        public int X
        {
            get { return _x + diam / 2; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y + diam / 2; }
            set { _y = value; }
        }
        public Eye(int x, int y, int diam)
        {
            X = x;
            Y = y;
            this.diam = diam;
            pupil = new Pupil(_x, _y, diam);
        }

        public void update(Graphics gr, Pen pen, Brush brush, int x_cursor, int y_cursor)
        {
            if (kill) gr.DrawImage(IMG, _x, _y, diam, Convert.ToInt32(diam * 1.5));
            gr.DrawEllipse(pen, _x, _y, diam, diam);
            pupil.update(gr, brush, x_cursor, y_cursor);
        }

        public bool in_eye(int x_cursor, int y_cursor)
        {
            bool check;
            check = pupil.in_eye(x_cursor, y_cursor);
            if (!kill) kill = check; 
            return check;
        }
        class Pupil
        {
            private int _x;
            private int _y;
            public int diam;
            public int x_pupil;
            public int y_pupil;
            bool kill = false;

            public int X_eye
            {
                get { return _x + diam / 2; }
                set { _x = value; }
            }

            public int Y_eye
            {
                get { return _y + diam / 2; }
                set { _y = value; }
            }
            public Pupil(int X_eye, int Y_eye, int diam)
            {
                this.X_eye = X_eye;
                this.Y_eye = Y_eye;
                this.diam = diam;
            }

            public void update(Graphics gr, Brush brush, int x_cursor, int y_cursor)
            {
                if (Math.Pow(x_cursor - X_eye, 2) + Math.Pow(y_cursor - Y_eye, 2) <= Math.Pow(diam / 2, 2) && !kill)
                {
                    x_pupil = x_cursor;
                    y_pupil = y_cursor;
                }
                else if (!kill)
                {
                    int L = x_cursor - X_eye;
                    int H = Y_eye - y_cursor;
                    int R = diam / 2;
                    int x_popravka = 0;
                    int y_popravka = 0;
                    if (L > 0 && H < 0)
                    {
                        x_popravka = 8;
                        y_popravka = 8;
                    }
                    else if (L > 0 && H > 0)
                    {
                        x_popravka = 8;
                        y_popravka = 0;
                    }
                    else if (L < 0 && H > 0)
                    {
                        x_popravka = 0;
                        y_popravka = 0;
                    }
                    else if (L > 0 && H > 0)
                    {
                        x_popravka = 8;
                        y_popravka = 0;
                    }
                    else if (L < 0 && H < 0)
                    {
                        x_popravka = 0;
                        y_popravka = 8;
                    }

                    if (H != 0) x_pupil = X_eye + Convert.ToInt32((R * H / Math.Sqrt(L * L + H * H)) * L / H) - x_popravka;
                    y_pupil = Y_eye - Convert.ToInt32(R * H / Math.Sqrt(L * L + H * H)) - y_popravka;
                }
                if (!kill) gr.FillEllipse(brush, x_pupil, y_pupil, 8, 8);
            }

            public bool in_eye(int x_cursor, int y_cursor)
            {
                if (Math.Pow(x_cursor - X_eye, 2) + Math.Pow(y_cursor - Y_eye, 2) <= Math.Pow(diam / 2, 2))
                {
                    kill = true;
                    return kill;
                }
                else return false;
            }
        }
    }
}
