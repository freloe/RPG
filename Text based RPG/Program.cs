using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_based_RPG
{
    public class MainMenu
    {
        
        static void Main(string[] args)
        {
            bool Answer = false;
            Character Player = new Character();
            while (Answer == false)
            {
                String Startup = "Do you want to load the game? Yes|No";
                Console.WriteLine(Startup);
                String AnswerText = Console.ReadLine();
                if (AnswerText == "Yes" || AnswerText == "No")
                {
                    switch (AnswerText)
                    {
                        case "Yes":
                            string[] Load = System.IO.File.ReadAllLines(@"C:\Users\frede\Documents\My Games\TextbasedRPG-SaveGame.txt");
                            Player.CharacterLoad(Load[0], Int32.Parse(Load[1]), Int32.Parse(Load[2]), Int32.Parse(Load[3]), Int32.Parse(Load[4]), Int32.Parse(Load[5]), Int32.Parse(Load[6]), Int32.Parse(Load[7]), Int32.Parse(Load[8]), Int32.Parse(Load[9]));
                            Console.WriteLine("Successfully loaded the Character, welcome back {0}!", Player.Name);
                            break;
                        case "No":
                            Player.Write("What is your Name?");
                            Player.Answer = Console.ReadLine();
                            Player.Name = Player.Answer;
                            break;
                    }
                    Answer = true;
                }
                else
                {
                    String PA = "Please Answer Yes or No";
                    Console.WriteLine(PA);
                }


            }
            Player.MainMenu();
        }
    }
    public class Character
    {
        public String Name;
        public int Health = 20;
        public int MaxHealth = 20;
        public int Attack = 10;
        public int Intelligence = 10;
        public int Mana = 20;
        public int MaxMana = 20;
        public int Speed = 5;
        public int Level = 1;
        public int Exp = 0;
        public int ExpCap;
        public int LevelPoints = 0;
        public int SpellPoints = 0;
        public String Answer;
        public Character()
        {

        }
        public void CharacterLoad(String Name,int Health,int MaxHealth,int Attack, int Intelligence,int Mana,int MaxMana,int Speed,int Level,int Exp)
        {
            this.Name = Name;
            this.Health = Health;
            this.MaxHealth = MaxHealth;
            this.Attack = Attack;
            this.Intelligence = Intelligence;
            this.Mana = Mana;
            this.MaxMana = MaxMana;
            this.Speed = Speed;
            this.Level = Level;
            this.Exp = Exp;

        }
        List<Spell> SpellList = new List<Spell>()
        {
            


        };
        public void getExpCap()

        {
            if (Level <= 10)
            {
                ExpCap = Level * 10;

            }
            if (Level <= 20 && Level > 10)
            {
                ExpCap = Level * 15;


            }
            if (Level <= 40 && Level > 20)
            {
                ExpCap = Level * 20;


            }
            if (Level < 50 && Level > 40)
            {
                ExpCap = Level * 35;

            }
        }
        public void LevelUP()
        {
            if (Exp >= ExpCap && Level != 50)
            {
                Level++;
                int ExtraExp = Exp -= ExpCap;
                Exp = 0;
                Exp += ExtraExp;
                LevelPoints += 3;
                SpellPoints += 10;
                getExpCap();
                String LevelUP = "You leveled up! You are now Level "+Level+"\nYou gained 3 Level Points\nYou gained 10 Spell Points";
                Write(LevelUP);
            }
        }
        public void SpendLevelPoints()
        {
            Boolean Ask = true;
            if (LevelPoints > 0)
            {
                String SpendPoints1 = "Choose a Stat to upgrade, you have " + LevelPoints + " Points left\nAttack|Intelligence|Mana|Speed|Health";
                Write(SpendPoints1);
                Answer = Console.ReadLine();
                switch (Answer)
                {
                    case "Attack":
                        Attack++;
                        String AttackUP = "Attack is now " + Attack;
                        Write(AttackUP);
                        break;
                    case "Intelligence":
                        Intelligence++;
                        String IntelligenceUP = "Intelligence is now " + Intelligence;
                        Write(IntelligenceUP);
                        break;
                    case "Mana":
                        Mana += 2;
                        MaxMana += 2;
                        String ManaUP = "Max Mana is now " + Mana;
                        Write(ManaUP);
                        break;
                    case "Speed":
                        Speed += 2;
                        String SpeedUP = "Speed is now " + Speed;
                        Write(SpeedUP);
                        break;
                    case "Health":
                        Health += 5;
                        MaxHealth += 5;
                        String HealthUP = "Max Health is now " + Health;
                        Write(HealthUP);
                        break;
                }
                LevelPoints--;
                while (Ask == true)
                {
                    String SpendPoints2 = "Spend more Points? Yes|No";
                    Write(SpendPoints2);
                    Answer = Console.ReadLine();
                    switch (Answer)
                    {
                        case "Yes":
                            SpendLevelPoints();
                            Ask = false;
                            break;
                        case "No":
                            MainMenu();
                            Ask = false;
                            break;

                    }
                }

            }
            else
            {
                String NoPoints = "You have no Points left";
                Write(NoPoints);
                MainMenu();
            }



        } 
        public void SaveGame()
        {
            string[] Save = 
            {
                Name,
                ""+Health,
                ""+MaxHealth,
                ""+Attack,
                ""+Intelligence,
                ""+Mana,
                ""+MaxMana,
                ""+Speed,
                ""+Level,
                ""+Exp,
            };
            System.IO.File.WriteAllLines(@"C:\Users\frede\Documents\My Games\TextbasedRPG-SaveGame.txt", Save);
            String Saved = "Successfully saved the Game!";
            Write(Saved);
            MainMenu();
        }
        public void Write(String text)
        {

            foreach (char c in text)
            {

                Console.Write(c);
                System.Threading.Thread.Sleep(60);

            }
            Console.WriteLine("");

        }
        public void MainMenu()
        {
            Boolean MainMenu = true;
            while (MainMenu == true)
            {
                Health = MaxHealth;
                Mana = MaxMana;
                String Main = "What do you want to do?\nFight|Level up|Create Spells|See existing Spells|Stats|Save";
                Write(Main);
                Answer = Console.ReadLine();
                
                switch (Answer)
                {
                    case "Fight":
                        MainMenu = false;
                        Fight(this);
                        break;
                    case "Level up":
                        MainMenu = false;
                        SpendLevelPoints();
                        break;
                    case "Create Spells":
                        MainMenu = false;
                        CreateSpells();
                        break;
                    case "See existing Spells":
                        MainMenu = false;
                        SeeSpells();
                        break;
                    case "Stats":
                        MainMenu = false;
                        SeeStats();
                        break;
                    case "Save":
                        MainMenu = false;
                        SaveGame();
                        break;

                }
                
            }

        }
        public void Adventure()
        {
            int rooms = 0;
            Random r = new Random();
            int roomstoend = r.Next(12, 20);
            String Beginning = "You went to an entrance of a Maze because of a myterious treasure hidden inside it but there are rumors that something evil is in there";
            String Scenario1 = "There are three ways to go left, right and straight\nWhich one do you go?";
            String Scenario2 = "You could go left or straight\nWhich one do you go?";
            String Scenario3 = "You could go right or left\nWhich one do you go?";
            String Scenario4 = "You could go straight or right\nWhich one do you go?";
            String AnswerR = "You went the right way and you are a step closer to the treasure";
            String AnswerS = "You went straight and you are a step closer to the treasure";
            String AnswerL = "You went left and you are a step closer to the treasure";
            String Encounter = "Something is infront of you you can't really see what it is";
            Write(Beginning);
            while (rooms != roomstoend)
            {
                Answer = Console.ReadLine();
                switch(Answer)
                {
                    

                }



        } }
        
        
        public void SeeStats()
        {
            String Stats = "MaxHealth: "+MaxHealth+ "\nHealth: " + Health + "\nMaxMana: " + MaxMana + "\nMana: " + Mana + "\nSpeed: " + Speed + "\nIntelligence: " + Intelligence + "\nAttack: " + Attack+"\nExp: "+Exp+"\nExpCap: "+ExpCap+"\nLevel: "+Level;
            Write(Stats);
            MainMenu();

        }
        public void SeeSpells()
        {
            if (SpellList.Count() > 0)
            {
                foreach (Spell s in SpellList)
                {
                    Write("----------------");
                    Write(s.GetSpellInfo());
                    Write("----------------");
                    Console.WriteLine();

                }
            }
            else Write("You have no Spells");
            MainMenu();


        }
        public void CreateSpells()
        {
            int Status = 0;
            String Statusstring;
            int Spellcost = 0;
            Boolean Integer = false;
            int Damage = 0;
            int Manacost = 0;
            String NameText = "What is the Name of the Spell?";
            Write(NameText);
            String Name = Console.ReadLine();
            while (Integer == false)
            {
                String DamageText = "How much Damage does the Spell do?";
                Write(DamageText);
                String Damagestring = Console.ReadLine();
                if (Int32.TryParse(Damagestring, out Damage) == false)
                {

                    continue;
                }
                else if (Int32.TryParse(Damagestring, out Damage) == true && Damage > 0)
                {
                    Int32.TryParse(Damagestring, out Damage);
                    Integer = true;
                    continue;
                }
                String DamageWrong = "This needs to be a positive number";
                Write(DamageWrong);


            }
            Integer = false;
            while (Integer == false)
            {
                String ManaText = "How much Mana does the Spell cost?";
                Write(ManaText);
                String Manastring = Console.ReadLine();
                if (Int32.TryParse(Manastring, out Manacost) == false)
                {

                    continue;

                }
                else if (Int32.TryParse(Manastring, out Manacost) == true && Manacost > 0)
                {
                    Int32.TryParse(Manastring, out Manacost);
                    Integer = true;
                    continue;
                }
                String ManaWrong = "This needs to be a positive number";
                Write(ManaWrong);



            }
            Statusstring = "What Status should the Spell apply? Fire|Ice|Nothing";
            Write(Statusstring);
            Answer = Console.ReadLine();
            switch(Answer)
            {
                case "Nothing":
                    Status = 0;
                    break;
                case "Fire":
                    Status = 1;
                    break;
                case "Ice":
                    Status = 2;
                    break;
            }
            String Actiontext = "Describe what the Spell does/What your are doing to cast the Spell";
            Write(Actiontext);
            String Action = Console.ReadLine();
            if (Manacost > Damage)
            {
                Spellcost = (Manacost - Damage) * 3 + Status*150;
            }
            else if (Manacost < Damage)
            {
                Spellcost = (Damage - Manacost) * 5+ Status * 150;
            }
            else if (Manacost == Damage)
            {
                Spellcost = Damage * 5+ Status * 150;

            }


            String spellcosts = "This Spell costs " + Spellcost + " Spell Points, you have " + SpellPoints + " rightnow";
            Write(spellcosts);
            String Buyit = "Do you want to create it for " + Spellcost + " Spell Points?\nYes|No";
            Write(Buyit);
            Answer = Console.ReadLine();
            switch (Answer)
            {
                case "Yes":
                    if (SpellPoints > Spellcost)
                    {
                        SpellPoints -= Spellcost;
                        String SpellConfirmed = "You have succesfully created the Spell " + Name;
                        Write(SpellConfirmed);
                    }
                    else
                    {
                        String SpellNOTConfirmed = "You don't have enough Spell Points";
                        Write(SpellNOTConfirmed);

                    }
                    break;
                case "No":
                    MainMenu();
                    break;



            }
            Spell spell = new Spell(Name, Damage, Manacost, Action,Status);
            SpellList.Add(spell);
            MainMenu();
        }
        public void Fight(Character n)
        {
            Character Player = n;
            int b = 2;
            Boolean YourTurn = true;
            Boolean InBattle;
            int timerEnemie = 0;
            int ManaDifference = MaxMana - Mana;
            Boolean FirePlayer = false;
            Boolean FireTimePlayer = false;
            Boolean IceTimePlayer = false;
            Boolean IcePlayer = false;
            Boolean FireEnemie = false;
            Boolean FireTimeEnemie = false;
            Boolean IceTimeEnemie = false;
            Boolean IceEnemie = false;
            int timerPlayer = 0;
            Enemie e = new Enemie();
            int f;
            Random Enemy = new Random();
            if (Level < 6)
            {
                switch (Enemy.Next(9))
                {
                    case 0:
                        e.Rat();
                        break;
                    case 1:
                        e.Rat();
                        break;
                    case 2:
                        e.Rat();
                        break;
                    case 3:
                        e.Rat();
                        break;
                    case 4:
                        e.Rat();
                        break;
                    case 5:
                        e.Rat();
                        break;
                    case 6:
                        e.Rat();
                        break;
                    case 7:
                        e.Rat();
                        break;
                    case 8:
                        e.Dragon();
                        break;
                }
            }
            if (Level < 15 && Level >= 6)
            {
                switch (Enemy.Next(9))
                {
                    case 0:
                        e.Dragon();
                        break;
                    case 1:
                        e.Dragon();
                        break;
                    case 2:
                        e.Dragon();
                        break;
                    case 3:
                        e.Dragon();
                        break;
                    case 4:
                        e.Dragon();
                        break;
                    case 5:
                        e.Dragon();
                        break;
                    case 6:
                        e.Dragon();
                        break;
                    case 7:
                        e.Dragon();
                        break;
                    case 8:
                        e.Demon();
                        break;
                }
            }
            if (Level < 30 && Level >= 15)
            {
                switch (Enemy.Next(5))
                {
                    case 0:
                        e.Rat();
                        break;
                    case 1:
                        e.Dragon();
                        break;
                    case 2:
                        e.Dragon();
                        break;
                    case 3:
                        e.Demon();
                        break;
                    case 4:
                        e.Demon();
                        break;
                }
            }
            if (e.Level < Level)
            {
                for (int i = Level; i > 0; i--)
                {
                    e.Level++;
                }
                e.getLevel();
            }
        
            Console.Clear();
            String Encounter = "An Enemie approaches you!\n...  ...  " + e.Name + " battles you!";
            Write(Encounter);
            InBattle = true;
            if (Speed > e.Speed)
            {
                String YouFaster = "You are faster than "+ e.Name;
                Write(YouFaster);
                YourTurn = true;

            }
            if (Speed < e.Speed)
            {
                String EnemieFaster = e.Name+" is faster than you";
                Write(EnemieFaster);
                YourTurn = false;

            }
            Random r = new Random();
            while (InBattle == true)
            {
                if (YourTurn == true)
                {
                    ManaDifference = MaxMana - Mana;
                    String Status = "You have " + Health + "Health left\nYou have " + Mana + "Mana left\n\n" + e.Name + " has " + e.Health + " Health left\n" + e.Mana + " Mana left";
                    Write("----------------");
                    Write(Status);
                    Write("----------------");
                    if ( FirePlayer == true && FireTimePlayer == true && IcePlayer == false )
                    {
                        Health--;
                        timerPlayer++;
                        String FireDamage = "You lost 1 Health because of Fire";
                        Write(FireDamage);
                        FireTimePlayer = false;
                    }
                    if(timerPlayer == 4)
                    {
                        String FireOver = "You stopped burning";
                        Write(FireOver);
                        timerPlayer = 0;
                        FirePlayer = false;
                    }
                    if (IcePlayer == true && IceTimePlayer == true && FirePlayer == false)
                    {
                        timerPlayer++;
                        if (timerPlayer == 2)
                        {
                            String IceOver = "You aren't frozen anymore";
                            Write(IceOver);
                            timerPlayer = 0;
                            IcePlayer = false;
                            continue;
                        }
                        String IceDamage = "You are frozen and you can't move";
                        Write(IceDamage);
                        IceTimePlayer = false;
                        YourTurn = false;
                        continue;
                    }
                    FireTimeEnemie = true;
                    IceTimeEnemie = true;

                    String Menu = "What do you want to do?\nAttack|Spells|Inspect";
                    Write(Menu);
                    Answer = Console.ReadLine();
                    switch (Answer)
                    {
                        case "Attack":
                            String Attack = "You attack the Enemie with Brute Force!\nYou dealed " + Player.Attack + " Damage";
                            Write(Attack);
                            e.Health -= Player.Attack;
                            YourTurn = false;
                            if (e.Health <= 0)
                            {
                                System.Threading.Thread.Sleep(100);
                                Console.Clear();
                                String EnemyKilled = "You killed " + e.Name + "\nYou gained " + e.ExpGain + " EXP";
                                Write(EnemyKilled);
                                Exp += e.ExpGain;
                                LevelUP();
                                InBattle = false;
                                continue;
                            }
                            if((ManaDifference) >= b + Player.Level)
                            {

                                Mana += b + Player.Level;

                            }
                            else if((ManaDifference <= b + Player.Level))
                            {
                                Mana += ManaDifference;
                            }
                            FireTimePlayer = true;
                            continue;
                        case "Spells":
                            if (SpellList.Count == 0)
                            {
                                String NoSpells = "You don't know any Spells!";
                                Write(NoSpells);
                                continue;
                            }
                            String Spells = "These are your spells: ";
                            Write(Spells);
                            int place = 0;
                            foreach (Spell s in SpellList)
                            {

                                place++;
                                Write("Place: " + place + "\nName: " + s.Name + "\nDamage: " + s.Damage + "\nMana Cost: " + s.ManaCost + "\n");

                            }
                            place = 0;
                            String ChooseOne = "Which one do you want to cast? Number of the Spell|Go Back";
                            Write(ChooseOne);
                            Answer = Console.ReadLine();
                            if (Int32.TryParse(Answer, out f) == true)
                            {
                                int SpellNumber = Int32.Parse(Answer);
                                SpellNumber--;
                                Spell Chosen = SpellList.ElementAt(SpellNumber);
                                if (Chosen.ManaCost < Mana)
                                {
                                    
                                    e.Health -= Chosen.Damage;
                                    String Hit = "You hit your Enemie with " + Chosen.Name + ", it dealed " + Chosen.Damage + " Damage";
                                    String Fire = "Your Enemy burns now";
                                    String Ice = "Your Enemy is frozen now";
                                    Write(Chosen.Action);
                                    Write(Hit);
                                    switch (Chosen.Status)
                                    {
                                        case 0:
                                            break;
                                        case 1:
                                            Write(Fire);
                                            FireEnemie = true;
                                            break;
                                        case 2:
                                            Write(Ice);
                                            IceEnemie = true;
                                            break;
                                    }
                                    Mana -= Chosen.ManaCost;
                                    YourTurn = false;
                                    if (e.Health <= 0)
                                    {
                                        System.Threading.Thread.Sleep(100);
                                        Console.Clear();
                                        String EnemyKilled = "You killed " + e.Name + "\nYou gained " + e.ExpGain+" EXP";
                                        Exp += e.ExpGain;
                                        Write(EnemyKilled);
                                        LevelUP();
                                        InBattle = false;
                                        continue;
                                    }
                                    if ((ManaDifference) >= b + Player.Level)
                                    {
                                        Mana += b + Player.Level;

                                    }
                                    FireTimePlayer = true;
                                    continue;
                                }
                                else
                                {
                                    String NoMana = "You don't have enough Mana left";
                                    Write(NoMana);
                                    if ((MaxMana -= Mana) >= b + Player.Level)
                                    {
                                        Mana += b + Player.Level;

                                    }
                                    continue;
                                }
                            }
                            if (Answer.Equals("Go Back"))
                            {
                                continue;
                            }
                            break;

                        case "Inspect":
                            String EnemieStats = e.Name + " Stats\n" + "Health: "+e.Health + "\nMana: " + e.Mana+ "\nSpeed: " + e.Speed+ "\nAttack: " + e.Attack + "\nIntelligence: " + e.Intelligence + "\nLevel: " + e.Level;
                            Write("----------------");
                            Write(EnemieStats);
                            String KnownSpells = "Your Enemie knows these Spells:";
                            Write(KnownSpells);
                            foreach(Spell s in e.SpellListEnemie)
                            {
                                Write(s.GetSpellInfo());
                            }
                            Write("----------------");
                            break;
                    }

                }
                if (YourTurn == false)
                {
                    FireTimePlayer = true;
                    IceTimePlayer = true;
                    if (FireEnemie == true && FireTimeEnemie == true && IceEnemie == false)
                    {
                        e.Health--;
                        timerEnemie++;
                        String FireDamage = e.Name+" lost 1 Health because of Fire";
                        Write(FireDamage);
                        FireTimeEnemie = false;
                    }
                    if (timerEnemie == 4)
                    {
                        String FireOver = e.Name + " stopped burning";
                        Write(FireOver);
                        timerEnemie = 0;
                        FireEnemie = false;
                    }
                    if (IceEnemie == true && IceTimeEnemie == true && FireEnemie == false)
                    {
                        timerEnemie++;
                        if (timerEnemie == 2)
                        {
                            String IceOver = e.Name + " isn't frozen anymore";
                            Write(IceOver);
                            timerEnemie = 0;
                            IceEnemie = false;
                            continue;
                        }
                        String Ice = e.Name + " is frozen and can't move";
                        Write(Ice);
                        IceTimeEnemie = false;
                        YourTurn = true;
                        continue;
                    }
                    
                    if (Health <= MaxHealth)
                    {

                        
                        if (r.Next(100) < 24)
                        {
                            Spell Chosen = e.SpellListEnemie.ElementAt(r.Next(SpellList.Count));
                            if(Chosen.ManaCost > Mana)
                            {
                                continue;
                            }
                            String Spell = e.Name + " used the Spell " + Chosen.Name + " it dealed " + Chosen.Damage + " Damage";
                            Write(Chosen.Action);
                            Write(Spell);
                            String Fire = "You burn now";
                            String Ice = "You are frozen now";
                            switch (Chosen.Status)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Write(Fire);
                                    FirePlayer = true;
                                    break;
                                case 2:
                                    Write(Ice);
                                    IcePlayer = true;
                                    break;
                            }
                            Player.Health -= Chosen.Damage;
                            e.Mana -= Chosen.ManaCost;
                            YourTurn = true;
                            if (Health <= 0)
                            {
                                System.Threading.Thread.Sleep(100);
                                Console.Clear();
                                String Death = "You died, " + e.Name + " killed you\n\nThank you for playing!\n\nPlease restart the Game";
                                Write(Death);
                                System.Threading.Thread.Sleep(1000000000);


                            }
                            if ((e.MaxMana -= e.Mana) >= b + e.Level)
                            {
                                e.Mana += b + e.Level;

                            }
                            continue;
                        }
                        else
                        {
                            String EnemyAttack = "The " + e.Name + " scratches you in the face and dealed " + e.Attack + " Damage";
                            Write(EnemyAttack);
                            Player.Health -= e.Attack;
                            YourTurn = true;
                            if (Health <= 0)
                            {
                                System.Threading.Thread.Sleep(100);
                                Console.Clear();
                                String Death = "You died, " + e.Name + " killed you\n\nThank you for playing!\n\nPlease restart the Game";
                                Write(Death);
                                System.Threading.Thread.Sleep(1000000000);

                            }
                            if ((e.MaxMana -= e.Mana) >= b + e.Level)
                            {
                                e.Mana += b + e.Level;

                            }
                            continue;
                        }


                    }
                    if(Health <= MaxHealth/2)
                    {
                        if (r.Next(100) < 49)
                        {
                            Spell Chosen = e.SpellListEnemie.ElementAt(r.Next(SpellList.Count));
                            if (Chosen.ManaCost > Mana)
                            {
                                continue;
                            }
                            String Spell = e.Name + " used the Spell " + Chosen.Name + " it dealed " + Chosen.Damage + " Damage";
                            Write(Chosen.Action);
                            Write(Spell);
                            String Fire = "You burn now";
                            String Ice = "You are frozen now";
                            switch (Chosen.Status)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Write(Fire);
                                    FirePlayer = true;
                                    break;
                                case 2:
                                    Write(Ice);
                                    IcePlayer = true;
                                    break;
                            }
                            Player.Health -= Chosen.Damage;
                            e.Mana -= Chosen.ManaCost;
                            YourTurn = true;
                            if (Health <= 0)
                            {
                                System.Threading.Thread.Sleep(100);
                                Console.Clear();
                                String Death = "You died, " + e.Name + " killed you\n\nThank you for playing!\n\nPlease restart the Game";
                                Write(Death);
                                System.Threading.Thread.Sleep(1000000000);

                            }
                            if ((e.MaxMana -= e.Mana) >= b + e.Level)
                            {
                                e.Mana += b + e.Level;

                            }
                            continue;
                        }
                        else
                        {
                            String EnemyAttack = "The " + e.Name + " scratches you in the face and dealed " + e.Attack + " Damage";
                            Write(EnemyAttack);
                            Player.Health -= e.Attack;
                            YourTurn = true;
                            if (Health <= 0)
                            {
                                System.Threading.Thread.Sleep(100);
                                Console.Clear();
                                String Death = "You died, " + e.Name + " killed you\n\nThank you for playing!\n\nPlease restart the Game";
                                Write(Death);
                                System.Threading.Thread.Sleep(1000000000);

                            }
                            if ((e.MaxMana -= e.Mana) >= b + e.Level)
                            {
                                e.Mana += b + e.Level;

                            }
                            continue;
                        }

                    }
                    if (Health <= MaxHealth / 4)
                    {
                        if (r.Next(100) < 74)
                        {
                            Spell Chosen = e.SpellListEnemie.ElementAt(r.Next(SpellList.Count));
                            if (Chosen.ManaCost > Mana)
                            {
                                continue;
                            }
                            String Spell = e.Name + " used the Spell " + Chosen.Name + " it dealed " + Chosen.Damage + " Damage";
                            Write(Chosen.Action);
                            Write(Spell);
                            String Fire = "You burn now";
                            String Ice = "You are frozen now";
                            switch (Chosen.Status)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Write(Fire);
                                    FirePlayer = true;
                                    break;
                                case 2:
                                    Write(Ice);
                                    IcePlayer = true;
                                    break;
                            }
                            Player.Health -= Chosen.Damage;
                            e.Mana -= Chosen.ManaCost;
                            YourTurn = true;
                            if (Health <= 0)
                            {
                                System.Threading.Thread.Sleep(100);
                                Console.Clear();
                                String Death = "You died, " + e.Name + " killed you\n\nThank you for playing!\n\nPlease restart the Game";
                                Write(Death);
                                System.Threading.Thread.Sleep(1000000000);

                            }
                            if ((e.MaxMana -= e.Mana) >= b + e.Level)
                            {
                                e.Mana += b + e.Level;

                            }
                            continue;
                        }
                        else
                        {
                            String EnemyAttack = "The "+e.Name+" scratches you in the face and dealed " + e.Attack + " Damage";
                            Write(EnemyAttack);
                            Player.Health -= e.Attack;
                            YourTurn = true;
                            if (Health <= 0)
                            {
                                System.Threading.Thread.Sleep(100);
                                Console.Clear();
                                String Death = "You died, " + e.Name + " killed you\n\nThank you for playing!\n\nPlease restart the Game";
                                Write(Death);
                                System.Threading.Thread.Sleep(1000000000);

                            }
                            if ((e.MaxMana -= e.Mana) >= b + e.Level)
                            {
                                e.Mana += b + e.Level;

                            }
                            continue;
                        }

                    }

                }

                
            }
            MainMenu();

        }
    }






    class Enemie
    {
        public String Name;
        public int MaxMana;
        public int Health;
        public int Attack;
        public int Intelligence;
        public int Mana;
        public int Speed;
        public int Level = 1;
        public int ExpGain;
        public List<Spell> SpellListEnemie = new List<Spell>(){};
        public void Rat()
        {
            Name = "Rat";
            Health = 13;
            Attack = 7;
            Intelligence = 3;
            Mana = 7;
            MaxMana = 7;
            Speed = 15;
            ExpGain = 15;
            SpellListEnemie.Add(new Spell("Ice Claws", 9, 6, "The Rat runs at you and scratches you with its ice Claws ", 2));
            SpellListEnemie.Add(new Spell("Ice Block", 9, 6, "The Rat summons an Ice Block above you that falls down on you ", 2));
        }
        public void Dragon()
        {
            Name = "Dragon";
            Health = 25;
            Attack = 13;
            Intelligence = 10;
            Mana = 10;
            MaxMana = 25;
            Speed = 10;
            ExpGain = 60;
            Level = 7;
            SpellListEnemie.Add(new Spell("Big Fireball",Intelligence,15,"The Dragon takes a deep breath and a big fireball starts to form at his mouth, then he blows the fireball at you",1));
            SpellListEnemie.Add(new Spell("Small Fireball", Intelligence/2, 15, "The Dragon takes a deep breath and a small fireball starts to form at his mouth, then he blows the fireball at you", 1));
            SpellListEnemie.Add(new Spell("Lightning Strike", Intelligence*2, 25, "The Dragon starts to say words that you don't understand they sound something like Dovahkiin,Dovakhiin naal ok zin los vahriin, then a Lightning strikes down on you", 0));
        }
        public void Demon()
        {
            Name = "Demon";
            Health = 40;
            Attack = 20;
            Intelligence = 17;
            Mana = 0;
            MaxMana = 16;
            Speed = 5;
            ExpGain = 120;
            Level = 10;
            SpellListEnemie.Add(new Spell("Chaos", Intelligence+10, 16, "The Demon starts to transform everything around you in Chaos", 0));
            SpellListEnemie.Add(new Spell("Hell Flames", Intelligence * 2, 26, "The Demon summons Flames that come directly from the Hell", 1));
            SpellListEnemie.Add(new Spell("Chaos Bomb", Intelligence * 4, 40, "The Demon creates an enourmes Ball of Chaos and throws it at you", 0));


        }





        public void getLevel()
        {
            
            for (int i = Level; i > 0; i--)
            {
                if (Name.Equals("Rat"))
                {
                    Health += 2;
                    Attack += 1;
                    Intelligence += 2;
                    Mana += 2;
                    MaxMana += 2;
                    Speed += 1;
                    ExpGain += 3;
                }
                if(Name.Equals("Dragon"))
                {
                    Health += 2;
                    Attack += 2;
                    Intelligence += 2;
                    Mana += 5;
                    MaxMana += 5;
                    Speed += 3;
                    ExpGain += 10;

                }
                if (Name.Equals("Demon"))
                {
                    Health += 5;
                    Attack += 3;
                    Intelligence += 3;
                    MaxMana += 2;
                    Speed += 1;
                    ExpGain += 30;

                }


            }
        }





    }
        class Spell
        {
            public String Name;
            public String Action;
            public int Damage;
            public int ManaCost;
            public int Status;
            //Status 0 = Nothing; 1 = Fire; 2 = Ice; 
            public Spell(String Name, int Damage, int ManaCost, String Action,int Status)
            {
                this.Name = Name;
                this.Damage = Damage;
                this.ManaCost = ManaCost;
                this.Action = Action;
                this.Status = Status;

            }
            public String GetSpellInfo()
            {
                return "Name: " + Name + "\nAction: " + Action + "\nDamage: " + Damage + "\nMana Cost: " + ManaCost + "\nStatus: " + Status + "\n";
            }
        }
    
}
