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
            var brush = new SolidBrush(Color.White);
            var pen = new Pen(Color.Black, STROKE);
            image.FillRectangle(brush, new Rectangle(0, 0, W*SCALE, H*SCALE));


            foreach(var team in teams){

                // Regions
                var points = new List<Vector2>();
                foreach (var region in team.territory){
                    pen.Color = Color.Black;
                    pen.Width = 1;
                    points.Add(region.position);
                    DrawRegion(image, region, brush, pen);
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

                var fontRedux = 4F;
                var minFontSize = 1.3f;
                var format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                var width = Math.Max(points.Count*2f, 7f);
                var height = Math.Max(points.Count*2f, 7f);
                
                brush.Color = Color.Black;
                image.DrawString(team.name, new Font("Droid Serif", Math.Max(minFontSize, points.Count/fontRedux)*SCALE), brush, new RectangleF(
                    (x - width/2)*SCALE, 
                    (y - height/2)*SCALE,
                    width * SCALE,
                    height * SCALE
                ), format);
            }            

                /* 
            foreach (var region in regions){
                if (region.position.X == 0) continue;
                pen.Color = Color.Black;
                pen.Width = 1;
                DrawRegion(image, region, brush, pen);
                
                // For debugging only
                foreach(var neighbor in region.neighbors){
                    pen.Color = Color.Red;
                    pen.Width = 1;
                    DrawLink(image, region, neighbor, pen);
                }
            }
                */

            brush.Dispose();

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

                brush.Color = region.owner.color;
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
                    (int)Math.Floor(region.owner.color.R/darknessFactor),
                    (int)Math.Floor(region.owner.color.G/darknessFactor),
                    (int)Math.Floor(region.owner.color.B/darknessFactor)
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

        public void WriteToDisk(Bitmap map){
            map.Save("test.png", ImageFormat.Png);
        }
    }
}
