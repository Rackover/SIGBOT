using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace SIGBOT.Components.War
{
    public class Display
    {
        public readonly int H = 32;
        public readonly int W = 55;
        public readonly int STROKE = 1;
        public readonly int SCALE = 1;

        public Display(int scale){
            this.SCALE = scale;
        }

        public Bitmap DrawMap(Regions regions, List<Team> teams){

            var bitmap = new Bitmap(W * SCALE, H * SCALE); // 55x32 is the resolution of the bitmap (pixels)
            var image = Graphics.FromImage(bitmap);
            var brush = new SolidBrush(Color.LightGray);
            var pen = new Pen(Color.Black, STROKE);
            image.FillRectangle(brush, new Rectangle(0, 0, W*SCALE, H*SCALE));

            // Draw bridges
            foreach(var region in regions)
            {
                foreach (var neighbor in region.neighbors) {
                    var neighborRegion = regions.Find(o => o.id == neighbor);
                    pen.Color = Color.Gray;
                    DrawLink(image, region, neighborRegion, pen);
                }
            }

            foreach (var team in teams)
            {

                if (team.territory.Count == 0) continue; // Team eliminated

                var a = team.GetTerritory();
                if (team.territory.Count != a.Count)
                {
                    Console.WriteLine("Territory count mismatch for " + team.name + ", expected " + team.territory.Count + ", got " + a.Count);
                    Console.WriteLine(team.territory[0]);
                    Console.WriteLine(regions[team.territory[0]]);
                    throw new Exception();
                }

                // Regions
                foreach (var region in team.GetTerritory())
                {
                    pen.Color = Color.Black;
                    pen.Width = 1;
                    DrawRegion(image, region, brush, pen);
                }
            }

            foreach (var team in teams){

                var points = new List<Vector2>();
                foreach (var region in team.GetTerritory())
                {
                    points.Add(region.position + region.size / 2f);
                }

                // Team tag
                float x = 0f;
                float y = 0f;
                foreach(var point in points){
                    x += point.X;
                    y += point.Y;
                }
                x /= points.Count;
                y /= points.Count;

                var fontRedux = 5F;
                var minFontSize = 1.3f;
                var format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                var width = Math.Max(points.Count*2f, 7f);
                var height = Math.Max(points.Count*2f, 7f);

                brush.Color = Color.Black;
                image.DrawString(team.name, new Font(team.font ?? "Impact", Math.Max(minFontSize, points.Count/fontRedux)*SCALE), brush, new RectangleF(
                    Math.Max(0, x - width/2) * SCALE,
                    Math.Max(0, y - height/2) * SCALE,
                    Math.Min(W, width) * SCALE,
                    Math.Min(H, height) * SCALE
                ), format);
            }
            
            /*
            foreach (var region in regions){
                image.DrawString(region.name, new Font(FontFamily.GenericSansSerif, SCALE), brush, (region.position.X + region.size.X) * SCALE, (region.position.Y + region.size.Y) * SCALE);
            }
            */

            brush.Dispose();
            image.Dispose();

            return bitmap;
        }

        public void DrawLink(Graphics image, Region regionA, Region regionB, Pen pen){
            image.DrawLine(
                pen, 
                new Point((int)Math.Floor(regionA.position.X + regionA.size.X/2f)*SCALE, (int)Math.Floor(regionA.position.Y + regionA.size.Y/2f)*SCALE),
                new Point((int)Math.Floor(regionB.position.X + regionB.size.X/2f)*SCALE, (int)Math.Floor(regionB.position.Y + regionB.size.Y/2f)*SCALE)
            );
        }

        public void DrawRegion(Graphics image, Region region, SolidBrush brush, Pen pen){

                brush.Color = region.GetOwner().color;
                image.FillRectangle(
                    brush,
                    new Rectangle(
                        (int)(region.position.X)*SCALE,
                        (int)(region.position.Y)*SCALE,
                        (int)(region.size.X-1)*SCALE,
                        (int)(region.size.Y-1)*SCALE
                    )
                );

                // Stroke
                var darknessFactor = 2f;
                pen.Color = Color.FromArgb(
                    (int)Math.Floor(region.GetOwner().color.R/darknessFactor),
                    (int)Math.Floor(region.GetOwner().color.G/darknessFactor),
                    (int)Math.Floor(region.GetOwner().color.B/darknessFactor)
                );
                image.DrawRectangle(
                    pen,
                    new Rectangle(
                        (int)(region.position.X)*SCALE,
                        (int)(region.position.Y)*SCALE,
                        (int)(region.size.X - 1)*SCALE,
                        (int)(region.size.Y - 1)*SCALE
                    )
                );
        }

        public void WriteToDisk(Bitmap map, string name="test.png")
        {
            map.Save(name, ImageFormat.Png);
            map.Dispose();
        }
    }
}
