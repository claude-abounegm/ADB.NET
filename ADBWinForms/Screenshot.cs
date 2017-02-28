/*
 * Screenshot.cs
 * Written by Claude Abounegm
 */

using System.Drawing;
using System.Windows.Forms;

namespace ADBWinForms
{
    public partial class Screenshot : Form
    {
        public Screenshot()
        {
            InitializeComponent();
        }

        public void SetImage(Image image)
        {
            pictureBox.Image = image;
        }
    }
}
