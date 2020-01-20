using System.Drawing;
using System;

namespace SIGBOT.Components.War.Classroom
{
    public class J002 : Map
    {
        static class TEAM
        {
            public static byte PLANETFOG = 0;
            public static byte PLIPPLOP = 1;
            public static byte RIPPLE = 2;
            public static byte BATTLECARS = 3;
            public static byte OMBRES = 4;
            public static byte TOUGHCOOKIE = 5;
            public static byte HAULAWAY = 6;
            public static byte ROBOTS = 7;
            public static byte NEUTRAL = 8;
        }

        public J002(){
                    // Initializing every team
            teams.Add(new Team("A planet in the fog", TEAM.PLANETFOG, Color.WhiteSmoke, "Montserrat Light"));
            teams.Add(new Team("plip plop", TEAM.PLIPPLOP, Color.Salmon, "mini-wakuwaku"));
            teams.Add(new Team("Ripple", TEAM.RIPPLE, Color.LimeGreen, "Kaushan Script"));
            teams.Add(new Team("Battle cars", TEAM.BATTLECARS, Color.Gold, "Rubik Black"));
            teams.Add(new Team("Materia prima", TEAM.OMBRES, Color.Purple, "Bebas Neue"));
            teams.Add(new Team("Tough Kookie", TEAM.TOUGHCOOKIE, Color.Orange, "Baloo"));
            teams.Add(new Team("Haul away", TEAM.HAULAWAY, Color.Teal, "Satturday Collection"));
            teams.Add(new Team("Robots 'em up", TEAM.ROBOTS, Color.DodgerBlue, "Osaka-Sans Serif"));
            teams.Add(new Team("Equipe pédagogique", TEAM.NEUTRAL, Color.Gray, "Calibri"));

            CreateRegions();
            ResizeRegions();
            MoveRegions();
            MakeBridges();
        } 

        void MakeBridges(){
            // Plip plop
            regions["Archi"].ConnectWith(regions["L'Intervenant"]);
            regions["Héloise"].ConnectWith(regions["Matthias"]);
            regions["Louis P"].ConnectWith(regions["Tristan"]);
            regions["Dorian"].ConnectWith(regions["Corinne"]);

            // Battle Cars
            regions["Florian"].ConnectWith(regions["L'Intervenant"]);
            regions["Dylan"].ConnectWith(regions["Galva"]);
            regions["Anthony"].ConnectWith(regions["Martin P"]);

            // Planet fog
            regions["Adrien"].ConnectWith(regions["Paul"]);
            regions["Loup"].ConnectWith(regions["Galva"]);
            regions["Noé"].ConnectWith(regions["Quentin"]);
            regions["Hamish"].ConnectWith(regions["Alexandre"]);
            regions["Charlotte"].ConnectWith(regions["Etienne"]);
            regions["Charlotte"].ConnectWith(regions["Pierre"]);

            // Robot them up
            regions["Alexandre"].ConnectWith(regions["Adrien"]);
            regions["Jules"].ConnectWith(regions["Julie"]);
            regions["Aurélien"].ConnectWith(regions["Eric"]);
            regions["Matthieu R"].ConnectWith(regions["Louis C"]);
            regions["Romain"].ConnectWith(regions["Valentin"]);
            regions["Arthur"].ConnectWith(regions["Valentin"]);

            // Ripple
            regions["Marc"].ConnectWith(regions["Gaétan R"]);
            regions["Alessandro"].ConnectWith(regions["Fabrice"]);
            regions["Morgane"].ConnectWith(regions["Jim"]);
            regions["Marie"].ConnectWith(regions["Clément"]);

            // Haul Away
            regions["Clément"].ConnectWith(regions["Théophile"]);
            regions["Lys"].ConnectWith(regions["Eve"]);

            // Tough Kookie
                // Done already


            // Ombres
                // Done already
        }

        void MoveRegions(){
                        // Positions
            // The grid is 55x32 sized

            // Haul Away
            regions["Noémie"].SetPosition(2, 3);
            regions["Manuela"].PutSouthOf(regions["Noémie"]);
            regions["Juliette"].PutSouthOf(regions["Manuela"]);
            regions["Lys"].PutSouthOf(regions["Juliette"]);
            regions["Gaétan R"].PutEastOf(regions["Noémie"]);
            regions["Fabrice"].PutSouthOf(regions["Gaétan R"]);
            regions["Jim"].PutSouthOf(regions["Fabrice"]);
            regions["Clément"].PutSouthOf(regions["Jim"]);

            // Ripple
            regions["Marc"].SetPosition(10, 3);
            regions["Inès"].PutEastOf(regions["Marc"]);
            regions["Alessandro"].PutSouthOf(regions["Marc"]);
            regions["Etienne"].PutSouthOf(regions["Inès"]);
            regions["Pierre"].PutSouthOf(regions["Etienne"]);
            regions["Simon"].PutSouthOf(regions["Pierre"]);
            regions["Morgane"].PutWestOf(regions["Pierre"]);
            regions["Marie"].PutSouthOf(regions["Morgane"]);

            // Ombres
            regions["Eve"].SetPosition(2, 21);
            regions["Théophile"].PutEastOf(regions["Eve"]);
            regions["Geoffrey"].PutEastOf(regions["Théophile"]);
            regions["Amélie"].PutEastOf(regions["Geoffrey"]);
            regions["Valentin"].PutEastOf(regions["Amélie"]);
            regions["Lug-Owein"].PutSouthOf(regions["Eve"]);
            regions["Matthieu A"].PutEastOf(regions["Lug-Owein"]);
            regions["Loan"].PutEastOf(regions["Matthieu A"]);
            regions["Joffrey"].PutEastOf(regions["Loan"]);

            // Planet fog
            regions["Charlotte"].SetPosition(18, 7);
            regions["Martin B"].PutEastOf(regions["Charlotte"]);
            regions["Maxence"].PutEastOf(regions["Martin B"]);
            regions["Raphael"].PutEastOf(regions["Maxence"]);
            regions["Martin P"].PutEastOf(regions["Raphael"]);
            regions["Noé"].PutSouthOf(regions["Martin B"]);
            regions["Hamish"].PutEastOf(regions["Noé"]);
            regions["Paul"].PutEastOf(regions["Hamish"]);
            regions["Galva"].PutEastOf(regions["Paul"]);

            // Battle Cars
            regions["Anthony"].SetPosition(39, 6);
            regions["Léa G"].PutEastOf(regions["Anthony"]);
            regions["Léa L"].PutSouthOf(regions["Léa G"]);
            regions["Florian"].PutSouthOf(regions["Léa L"]);
            regions["Dylan"].PutWestOf(regions["Léa L"]);
            regions["Louis B"].PutWestOf(regions["Florian"]);
            regions["Matthias"].PutSouthOf(regions["Louis B"]);

            // Plip Plop
            regions["Dorian"].SetPosition(36, 22);
            regions["Louis P"].PutSouthOf(regions["Dorian"]);
            regions["Héloise"].PutEastOf(regions["Dorian"]);
            regions["Archi"].PutEastOf(regions["Héloise"]);
            regions["Gaétan C"].PutSouthOf(regions["Archi"]);
            regions["Rack"].PutSouthOf(regions["Héloise"]);

            // Tough K
            regions["Adrien"].SetPosition(28, 16);
            regions["Loup"].PutEastOf(regions["Adrien"]);
            regions["Julie"].PutSouthOf(regions["Adrien"]);
            regions["Eric"].PutSouthOf(regions["Julie"]);
            regions["Louis C"].PutSouthOf(regions["Eric"]);
            regions["Tristan"].PutEastOf(regions["Louis C"]);
            regions["Corinne"].PutEastOf(regions["Eric"]);
            regions["Laura"].PutEastOf(regions["Julie"]);

            // Robots
            regions["Quentin"].SetPosition(20, 16);
            regions["Alexandre"].PutEastOf(regions["Quentin"]);
            regions["Romain"].PutSouthOf(regions["Quentin"]);
            regions["Arthur"].PutSouthOf(regions["Romain"]);
            regions["Balaji"].PutSouthOf(regions["Arthur"]);
            regions["Jules"].PutEastOf(regions["Romain"]);
            regions["Aurélien"].PutEastOf(regions["Arthur"]);
            regions["Matthieu R"].PutEastOf(regions["Balaji"]);

            // Neutral
            regions["L'Intervenant"].SetPosition(48, 16);
        }

        void ResizeRegions(){
            // Size corrections
            // Horizontal teams
            regions.FindAll(o => o.owner == TEAM.OMBRES).ForEach(o => { o.SetSize(HORIZONTAL_DESK); });
            regions.FindAll(o => o.owner == TEAM.PLIPPLOP).ForEach(o => { o.SetSize(HORIZONTAL_DESK); });
            regions.FindAll(o => o.owner == TEAM.PLANETFOG).ForEach(o => { o.SetSize(HORIZONTAL_DESK); });
            // Special desks
            regions["Valentin"].SetSize(LONG_VERTICAL_DESK);
            regions["Charlotte"].SetSize(LONG_VERTICAL_DESK);
            regions["Matthias"].SetSize(LARGE_HORIZONTAL_DESK);
            regions["L'Intervenant"].SetSize(LARGE_HORIZONTAL_DESK);
        }

        void CreateRegions(){

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
            regions.Add(new Region("Noé", teams[TEAM.PLANETFOG]));
            regions.Add(new Region("Hamish", teams[TEAM.PLANETFOG]));
            regions.Add(new Region("Martin P", teams[TEAM.PLANETFOG]));
            regions.Add(new Region("Martin B", teams[TEAM.PLANETFOG]));
            regions.Add(new Region("Paul", teams[TEAM.PLANETFOG]));
            regions.Add(new Region("Galva", teams[TEAM.PLANETFOG]));
            regions.Add(new Region("Maxence", teams[TEAM.PLANETFOG]));
            regions.Add(new Region("Raphael", teams[TEAM.PLANETFOG]));
            regions["Charlotte"].ConnectWith(regions["Noé"]);
            regions["Charlotte"].ConnectWith(regions["Martin B"]);
            regions["Noé"].ConnectWith(regions["Martin B"]);
            regions["Noé"].ConnectWith(regions["Hamish"]);
            regions["Maxence"].ConnectWith(regions["Hamish"]);
            regions["Maxence"].ConnectWith(regions["Martin B"]);
            regions["Maxence"].ConnectWith(regions["Raphael"]);
            regions["Paul"].ConnectWith(regions["Hamish"]);
            regions["Paul"].ConnectWith(regions["Raphael"]);
            regions["Paul"].ConnectWith(regions["Galva"]);
            regions["Martin P"].ConnectWith(regions["Galva"]);
            regions["Martin P"].ConnectWith(regions["Maxence"]);
            regions["Martin P"].ConnectWith(regions["Raphael"]);

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
            regions["Etienne"].ConnectWith(regions["Inès"]);
            regions["Morgane"].ConnectWith(regions["Pierre"]);
            regions["Morgane"].ConnectWith(regions["Simon"]);
            regions["Marc"].ConnectWith(regions["Alessandro"]);

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
            regions.Add(new Region("Lug-Owein", teams[TEAM.OMBRES]));
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
            regions["Lug-Owein"].ConnectWith(regions["Eve"]);
            regions["Lug-Owein"].ConnectWith(regions["Matthieu A"]);

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

            // ROBOT THEM UP
            regions.Add(new Region("Quentin", teams[TEAM.ROBOTS]));
            regions.Add(new Region("Alexandre", teams[TEAM.ROBOTS]));
            regions.Add(new Region("Romain", teams[TEAM.ROBOTS]));
            regions.Add(new Region("Jules", teams[TEAM.ROBOTS]));
            regions.Add(new Region("Arthur", teams[TEAM.ROBOTS]));
            regions.Add(new Region("Aurélien", teams[TEAM.ROBOTS]));
            regions.Add(new Region("Balaji", teams[TEAM.ROBOTS]));
            regions.Add(new Region("Matthieu R", teams[TEAM.ROBOTS]));
            regions["Quentin"].ConnectWith(regions["Alexandre"]);
            regions["Quentin"].ConnectWith(regions["Romain"]);
            regions["Jules"].ConnectWith(regions["Alexandre"]);
            regions["Jules"].ConnectWith(regions["Romain"]);
            regions["Jules"].ConnectWith(regions["Aurélien"]);
            regions["Matthieu R"].ConnectWith(regions["Aurélien"]);
            regions["Matthieu R"].ConnectWith(regions["Balaji"]);
            regions["Arthur"].ConnectWith(regions["Romain"]);
            regions["Arthur"].ConnectWith(regions["Aurélien"]);
            regions["Arthur"].ConnectWith(regions["Balaji"]);

            // Neutral
            regions.Add(new Region("L'Intervenant", teams[TEAM.NEUTRAL]));
        }
    }
}
