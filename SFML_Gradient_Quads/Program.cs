using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Window;
using SFML.Graphics;

namespace SFML_Gradient_Quads
{
    class Program
    {
        static void Main(string[] args)
        {
            GradientRect g = new GradientRect(0, 0, 800, 600, Color.Red, Color.Black, 2);
            RenderWindow win = new RenderWindow(new VideoMode(800, 600), "");
            Color gradientPrimary = new Color(255, 0, 0);
            Color gradientSecondary = new Color(0, 0, 0);

            win.Closed += (sender, e) => win.Close();
            win.MouseMoved += (sender, e) =>
            {
                if (e.X < 400)
                {
                    gradientPrimary.R = PercentColor(e.Y, 0.0);
                    gradientPrimary.G = PercentColor(e.Y, 0.0);
                    gradientPrimary.B = PercentColor(e.Y, 600.0);
                }
                else
                {
                    gradientSecondary.R = (byte)(Math.Abs(255 - PercentColor(e.Y, 600.0)));
                    gradientSecondary.G = (byte)(Math.Abs(255 - PercentColor(e.Y, 600.0)));
                    gradientSecondary.B = (byte)(Math.Abs(255 - PercentColor(e.Y, 60S0.0)));
                }
            };
            
            while (win.IsOpen())
            {
                win.DispatchEvents();
                win.Clear();
                g.Draw(win);
                win.Display();                
                g.ChangePrimaryColor(gradientPrimary);
                g.ChangeSecondaryColor(gradientSecondary);
            }
        }

        static public byte PercentColor(double x, double y)
        {
            return (byte)(255 * ((x / y) * 100) / 100);
        }
    }

    class GradientRect
    {
        Vertex[] rect;

        public GradientRect(int x, int y, int w, int h, Color c1, Color c2, int gradients)
        {
            rect = new Vertex[4];
            Vertex p1 = new Vertex(new Vector2f(x, y), c1);
            Vertex p2 = new Vertex(new Vector2f(w, y), c1);            
            Vertex p3 = new Vertex(new Vector2f(w, h), c2);
            Vertex p4 = new Vertex(new Vector2f(x, h), c2);

            rect[0] = p1;
            rect[1] = p2;
            rect[2] = p3;
            rect[3] = p4;
        }

        public void ChangePrimaryColor(Color c)
        {
            rect[0].Color = c;
            rect[1].Color = c;
        }

        public void ChangeSecondaryColor(Color c)
        {
            rect[2].Color = c;
            rect[3].Color = c;
        }

        public void Draw(RenderWindow r)
        {
            r.Draw(rect, PrimitiveType.Quads);
        }
    }
}
