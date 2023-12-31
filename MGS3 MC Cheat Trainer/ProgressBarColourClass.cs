using System.Drawing;
using System.Windows.Forms;

namespace MGS3_MC_Cheat_Trainer
{
    internal class ColouredProgressBar : ProgressBar
    {
        public Color ProgressBarColour { get; set; } = Color.Red; // Default color

        public ColouredProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            float percentage = (float)Value / Maximum;
            rect.Width = (int)(rect.Width * percentage);

            using (Brush brush = new SolidBrush(ProgressBarColour))
            {
                e.Graphics.FillRectangle(brush, 0, 0, rect.Width, rect.Height);
            }
        }
    }
}