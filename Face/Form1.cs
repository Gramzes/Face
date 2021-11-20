using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        List<Face> faces = new List<Face>();
        Graphics gr;
        int x_cursor, y_cursor;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            bool check = false;
            if (e.Button==MouseButtons.Left)
            {
                foreach (Face face in faces)
                {
                    if (face.in_eye(e.X, e.Y))
                    {
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    int diam = rnd.Next(80, 150);
                    faces.Add(new Face(e.X - diam / 2, e.Y - diam / 2, diam));
                }
            }
            this.Invalidate();
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Face face in faces)
            {
                face.update(e.Graphics, x_cursor, y_cursor);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            x_cursor = e.X;
            y_cursor = e.Y;
            this.Refresh();
        }
    }
}
