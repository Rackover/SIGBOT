using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace SIGBOT.Components.War
{
    class Map
    {
        public enum TEAM { PLIPPLOP, PLANETFOG, RIPPLE, BATTLECARS, OMBRES, TOUGHCOOKIE, HAULAWAY, ROBOTS }
        public Regions regions = new Regions();
        public Dictionary<TEAM, Team> teams = new Dictionary<TEAM, Team>();

        public Map()
        {
            // Initializing every team
            teams.Add(TEAM.PLANETFOG, new Team("A planet in the fog", Color.WhiteSmoke));
            teams.Add(TEAM.PLIPPLOP, new Team("Plip plop", Color.Pink));
            teams.Add(TEAM.RIPPLE, new Team("Ripple", Color.Lime));
            teams.Add(TEAM.BATTLECARS, new Team("Battle cars", Color.Yellow));
            teams.Add(TEAM.OMBRES, new Team("Ombres", Color.Purple));
            teams.Add(TEAM.TOUGHCOOKIE, new Team("Tough Kookie", Color.Orange));
            teams.Add(TEAM.HAULAWAY, new Team("Haul away", Color.Teal));
            teams.Add(TEAM.ROBOTS, new Team("Robots them up", Color.Cyan));

            // Creating regions ("Tables")

            // HAUL AWAY
            regions.Add(new Region("Manuela", teams[TEAM.HAULAWAY]));
            regions.Add(new Region("Lys", teams[TEAM.HAULAWAY]));
            regions.Add(new Region("Juliette", teams[TEAM.HAULAWAY]));
            regions.Add(new Region("Gaétan R", teams[TEAM.HAULAWAY]));
            regions.Add(new Region("Jim", teams[TEAM.HAULAWAY]));
            regions.Add(new Region("Fabrice", teams[TEAM.HAULAWAY]));
            regions.Add(new Region("Clément", teams[TEAM.HAULAWAY]));
            regions.Add(new Region("Noémie", teams[TEAM.HAULAWAY]));
            regions["Juliette"].ConnectWith(regions["Manuela"]);
            regions["Juliette"].ConnectWith(regions["Lys"]);
            regions["Juliette"].ConnectWith(regions["Jim"]);
            regions["Clément"].ConnectWith(regions["Lys"]);
            regions["Clément"].ConnectWith(regions["Jim"]);
            regions["Fabrice"].ConnectWith(regions["Manuela"]);
            regions["Fabrice"].ConnectWith(regions["Gaétan R"]);
            regions["Fabrice"].ConnectWith(regions["Jim"]);
            regions["Noémie"].ConnectWith(regions["Gaétan R"]);
            regions["Noémie"].ConnectWith(regions["Manuela"]);

            // PLIP PLOP
            regions.Add(new Region("Dorian", teams[TEAM.PLIPPLOP]));
            regions.Add(new Region("Rack", teams[TEAM.PLIPPLOP]));
            regions.Add(new Region("Archi", teams[TEAM.PLIPPLOP]));
            regions.Add(new Region("Héloise", teams[TEAM.PLIPPLOP]));
            regions.Add(new Region("Gaétan C", teams[TEAM.PLIPPLOP]));
            regions.Add(new Region("Louis P", teams[TEAM.PLIPPLOP]));
            regions["Rack"].ConnectWith(regions["Héloise"]);
            regions["Rack"].ConnectWith(regions["Louis P"]);
            regions["Rack"].ConnectWith(regions["Gaétan C"]);
            regions["Archi"].ConnectWith(regions["Gaétan C"]);
            regions["Archi"].ConnectWith(regions["Héloise"]);
            regions["Dorian"].ConnectWith(regions["Héloise"]);
            regions["Dorian"].ConnectWith(regions["Louis P"]);

            // PLANET IN THE FOG
            regions.Add(new Region("Charlotte", teams[TEAM.PLANETFOG]));

            // RIPPLE 
            regions.Add(new Region("Marc", teams[TEAM.RIPPLE]));
            regions.Add(new Region("Morgane", teams[TEAM.RIPPLE]));
            regions.Add(new Region("Alessandro", teams[TEAM.RIPPLE]));
            regions.Add(new Region("Marie", teams[TEAM.RIPPLE]));
            regions.Add(new Region("Simon", teams[TEAM.RIPPLE]));
            regions.Add(new Region("Etienne", teams[TEAM.RIPPLE]));
            regions.Add(new Region("Pierre", teams[TEAM.RIPPLE]));
            regions.Add(new Region("Inès", teams[TEAM.RIPPLE]));
            regions["Marie"].ConnectWith(regions["Pierre"]);
            regions["Marie"].ConnectWith(regions["Alessandro"]);
            regions["Marie"].ConnectWith(regions["Simon"]);
            regions["Inès"].ConnectWith(regions["Marc"]);
            regions["Inès"].ConnectWith(regions["Etienne"]);
            regions["Etienne"].ConnectWith(regions["Alessandro"]);
            regions["Etienne"].ConnectWith(regions["Pierre"]);
            regions["Etienne"].ConnectWith(regions["Jim"]);
            regions["Morgane"].ConnectWith(regions["Pierre"]);
            regions["Morgane"].ConnectWith(regions["Simon"]);

            // BATTLE CARS
            regions.Add(new Region("Matthias", teams[TEAM.BATTLECARS]));
            regions.Add(new Region("Dylan", teams[TEAM.BATTLECARS]));
            regions.Add(new Region("Léa L", teams[TEAM.BATTLECARS]));
            regions.Add(new Region("Léa G", teams[TEAM.BATTLECARS]));
            regions.Add(new Region("Florian", teams[TEAM.BATTLECARS]));
            regions.Add(new Region("Louis B", teams[TEAM.BATTLECARS]));
            regions.Add(new Region("Anthony", teams[TEAM.BATTLECARS]));
            regions["Léa L"].ConnectWith(regions["Léa G"]);
            regions["Léa L"].ConnectWith(regions["Dylan"]);
            regions["Léa L"].ConnectWith(regions["Florian"]);
            regions["Anthony"].ConnectWith(regions["Dylan"]);
            regions["Anthony"].ConnectWith(regions["Léa G"]);
            regions["Louis B"].ConnectWith(regions["Dylan"]);
            regions["Louis B"].ConnectWith(regions["Florian"]);
            regions["Matthias"].ConnectWith(regions["Louis B"]);
            regions["Matthias"].ConnectWith(regions["Florian"]);

            // OMBRES
            regions.Add(new Region("Eve", teams[TEAM.OMBRES]));
            regions.Add(new Region("Théophile", teams[TEAM.OMBRES]));
            regions.Add(new Region("Geoffrey", teams[TEAM.OMBRES]));
            regions.Add(new Region("Amélie", teams[TEAM.OMBRES]));
            regions.Add(new Region("Valentin", teams[TEAM.OMBRES]));
            regions.Add(new Region("Joffrey", teams[TEAM.OMBRES]));
            regions.Add(new Region("Loan", teams[TEAM.OMBRES]));
            regions.Add(new Region("Matthieu A", teams[TEAM.OMBRES]));
            regions.Add(new Region("Lug", teams[TEAM.OMBRES]));
            regions["Valentin"].ConnectWith(regions["Joffrey"]);
            regions["Valentin"].ConnectWith(regions["Amélie"]);
            regions["Amélie"].ConnectWith(regions["Joffrey"]);
            regions["Amélie"].ConnectWith(regions["Geoffrey"]);
            regions["Joffrey"].ConnectWith(regions["Loan"]);
            regions["Geoffrey"].ConnectWith(regions["Loan"]);
            regions["Loan"].ConnectWith(regions["Matthieu A"]);
            regions["Geoffrey"].ConnectWith(regions["Théophile"]);
            regions["Théophile"].ConnectWith(regions["Matthieu A"]);
            regions["Théophile"].ConnectWith(regions["Eve"]);
            regions["Lug"].ConnectWith(regions["Eve"]);
            regions["Lug"].ConnectWith(regions["Matthieu A"]);

            // TOUGH KOOKIE
            regions.Add(new Region("Loup", teams[TEAM.TOUGHCOOKIE]));
            regions.Add(new Region("Adrien", teams[TEAM.TOUGHCOOKIE]));
            regions.Add(new Region("Julie", teams[TEAM.TOUGHCOOKIE]));
            regions.Add(new Region("Eric", teams[TEAM.TOUGHCOOKIE]));
            regions.Add(new Region("Louis C", teams[TEAM.TOUGHCOOKIE]));
            regions.Add(new Region("Tristan", teams[TEAM.TOUGHCOOKIE]));
            regions.Add(new Region("Corinne", teams[TEAM.TOUGHCOOKIE]));
            regions.Add(new Region("Laura", teams[TEAM.TOUGHCOOKIE]));
            regions["Loup"].ConnectWith(regions["Laura"]);
            regions["Laura"].ConnectWith(regions["Corinne"]);
            regions["Corinne"].ConnectWith(regions["Tristan"]);
            regions["Tristan"].ConnectWith(regions["Louis C"]);
            regions["Louis C"].ConnectWith(regions["Eric"]);
            regions["Eric"].ConnectWith(regions["Julie"]);
            regions["Julie"].ConnectWith(regions["Adrien"]);
            regions["Adrien"].ConnectWith(regions["Loup"]);
            regions["Julie"].ConnectWith(regions["Laura"]);
            regions["Eric"].ConnectWith(regions["Corinne"]);

            // HAUL AWAY


            // Size corrections
            // Horizontal teams
            regions.FindAll(o => o.owner == teams[TEAM.OMBRES]).ForEach(o => { o.SetSize(4, 3); });
            regions.FindAll(o => o.owner == teams[TEAM.PLIPPLOP]).ForEach(o => { o.SetSize(4, 3); });
            regions.FindAll(o => o.owner == teams[TEAM.PLANETFOG]).ForEach(o => { o.SetSize(4, 3); });
            // Special desks
            regions["Valentin"].SetSize(3, 5);
            regions["Charlotte"].SetSize(3, 5);
            regions["Matthias"].SetSize(5, 3);

            // Positions
            // The grid is 55x32 sized
            regions["Noémie"].SetPosition(2, 3);

            regions["Marc"].SetPosition(10, 3);
            regions["Inès"].PutRightOf(regions["Marc"]);
            regions["Alessandro"].PutBelow(regions["Marc"]);
            regions["Marie"].PutBelow(regions["Alessandro"]);
            regions["Simon"].PutBelow(regions["Marie"]);
            regions["Etienne"].PutBelow(regions["Inès"]);
            regions["Pierre"].PutBelow(regions["Etienne"]);
            regions["Morgane"].PutBelow(regions["Pierre"]);

            regions["Eve"].SetPosition(2, 21);

            regions["Charlotte"].SetPosition(18, 7);

            regions["Léa G"].SetPosition(39, 6);

            regions["Dorian"].SetPosition(36, 22);

            regions["Adrien"].SetPosition(28, 16);

            //regions["???"].SetPosition(20, 16);
        }


        public void Draw()
        {
            int h = 32;
            int w = 55;

            var bitmap = new Bitmap(w, h); // 55x32 is the resolution of the bitmap (pixels)
            var image = Graphics.FromImage(bitmap);
            var brush = new SolidBrush(Color.DarkBlue);
            var pen = new Pen(Color.Black, 1f);
            image.FillRectangle(brush, new Rectangle(0, 0, w, h));

            foreach (var region in regions){
                if (region.position.X == 0) continue;

                brush.Color = Color.White;
                image.FillRectangle(
                    brush,
                    new Rectangle(
                        (int)region.position.X,
                        (int)region.position.Y,
                        (int)region.size.X-1,
                        (int)region.size.Y-1
                    )
                );

                // Stroke
                image.DrawRectangle(
                    pen,
                    new Rectangle(
                        (int)region.position.X,
                        (int)region.position.Y,
                        (int)region.size.X - 1,
                        (int)region.size.Y - 1
                    )
                );
            }

            brush.Dispose();
            bitmap.Save("test.png", ImageFormat.Png);
            Console.ReadLine();
        }
    }

}
