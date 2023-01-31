using System;
using System.Collections.Generic;
using System.Threading;

namespace strategiapeli
{
    class Program
    {
        enum GameState
        {
            Title,
            Game,
            Loss,
            Victory,
            Quit
        }
        static void Main(string[] args)
        {
            GameState state = GameState.Title;
            while (state != GameState.Quit)
            {
                switch (state)
                {
                    case GameState.Title:
                        state = RunTitle();
                        break;
                    case GameState.Game:
                        state = RunGame();
                        break;
                    case GameState.Victory:
                        state = GameOverMenu(true);
                        break;
                    case GameState.Loss:
                        state = GameOverMenu(false);
                        break;
                }
            }
        }

        private static GameState RunTitle()
        {
            Console.WriteLine("HUMANS vs. SKELTALS");
            Console.WriteLine("Press any key to play");
            Console.ReadKey();
            Console.Clear();
            return GameState.Game;
        }

        private static GameState GameOverMenu(bool win)
        {
            bool quit = true;
            ConsoleKey key;
            
            while (true)
            {
                Console.Clear();
                if (win)
                {
                    Console.WriteLine("Game over. You won!");
                } else
                {
                    Console.WriteLine("Game over. You lost.");
                }
                Console.WriteLine("Would you like to quit or play again?");
                Console.WriteLine("Press Enter");
                Console.WriteLine("");
                if (quit)
                {
                    Console.WriteLine("[QUIT]");
                    Console.WriteLine(" PLAY AGAIN ");
                } else
                {
                    Console.WriteLine(" QUIT ");
                    Console.WriteLine("[PLAY AGAIN]");
                }
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow)
                {
                    if (quit)
                    {
                        quit = false;
                    } else
                    {
                        quit = true;
                    }
                } else if (key == ConsoleKey.Enter && quit)
                {
                    return GameState.Quit;
                } else if (key == ConsoleKey.Enter && !quit)
                {
                    Console.Clear();
                    return GameState.Title;
                } else
                {
                    continue;
                }
                
            }
        }

        private static GameState RunGame()
        {
            bool gameWon;
            bool unitDead = false;
            Random randomNumber = new Random();
            Unit chosenAttacker;
            Unit chosenTarget;

            List<Unit> humans = new List<Unit>();
            humans.Add(new Unit("Human 1", 10, 5));
            humans.Add(new Unit("Human 2", 12, 5));
            humans.Add(new Unit("Human 3", 10, 6));

            List<Unit> skeltals = new List<Unit>();
            skeltals.Add(new Unit("Skeltal 1", 10, 5));
            skeltals.Add(new Unit("Skeltal 2", 10, 6));
            skeltals.Add(new Unit("Skeltal 3", 10, 5));

            while (true)
            {

                WriteUnits(humans, skeltals);

                while (true)
                {
                    Console.WriteLine("Choose attacker");
                    try
                    {
                        chosenAttacker = humans[Convert.ToInt32(Console.ReadLine()) - 1];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Choice invalid");
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Choice invalid");
                        continue;
                    }

                    if (!chosenAttacker.alive)
                    {
                        Console.WriteLine(chosenAttacker.name + " is dead");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine(chosenAttacker.name + " chosen");


                while (true)
                {


                    Console.WriteLine("Choose target");
                    try
                    {
                        chosenTarget = skeltals[Convert.ToInt32(Console.ReadLine()) - 1];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Choice invalid");
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Choice invalid");
                        continue;
                    }

                    if (!chosenTarget.alive)
                    {
                        Console.WriteLine(chosenTarget.name + " is already dead");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                Console.WriteLine(chosenTarget.name + " chosen");

                Attack(chosenAttacker, chosenTarget);

                foreach (Unit skeltal in skeltals)
                {
                    if (skeltal.alive)
                    {
                        unitDead = false;
                        break;
                    }
                    else
                    {
                        unitDead = true;
                    }
                }

                if (unitDead)
                {
                    //Console.WriteLine("Game over. You won!");
                    gameWon = true;
                    break;
                }

                while (true)
                {
                    chosenAttacker = skeltals[randomNumber.Next(0, skeltals.Count)];
                    if (chosenAttacker.alive)
                    {
                        break;
                    }
                }

                while (true)
                {
                    chosenTarget = humans[randomNumber.Next(0, humans.Count)];
                    if (chosenTarget.alive)
                    {
                        break;
                    }
                }

                Attack(chosenAttacker, chosenTarget);

                foreach (Unit human in humans)
                {
                    if (human.alive)
                    {
                        unitDead = false;
                        break;
                    }
                    else
                    {
                        unitDead = true;
                    }
                }

                if (unitDead)
                {
                    //Console.WriteLine("Game over. You lost.");
                    gameWon = false;
                    break;
                }

                
                Console.Clear();
            }
            Console.Clear();
            if (gameWon)
            {
                return GameState.Victory;
            } else
            {
                return GameState.Loss;
            }
            
        }

        static void Attack(Unit attacker, Unit target)
        {
            if (target.alive && attacker.alive)
            {
                target.health = target.health - attacker.attack;
                Console.WriteLine(attacker.name + " attacks " + target.name + " dealing " + attacker.attack + " damage!");
                //Console.WriteLine(target.name + "'s health is " + target.health);
                if (target.health <= 0)
                {
                    target.alive = false;
                    Console.WriteLine(target.name + " is dead");
                }
                WaitForSeconds(3);
            } else if (!attacker.alive)
            {
                Console.WriteLine(attacker.name + " is dead");
            } else
            {
                Console.WriteLine(target.name + " is already dead");
            }
            Console.WriteLine("");
        }

        static void WriteUnits(List<Unit> humans, List<Unit> skeltals)
        {
            int unitNumber = 1;
            Console.SetCursorPosition(0, 0);
            Console.Write("Player");
            foreach (Unit human in humans)
            {
                Console.SetCursorPosition(0, unitNumber);
                if (!human.alive)
                {
                    Console.WriteLine(unitNumber + ": " + human.name + " +DEAD+");
                }
                else
                {
                    Console.WriteLine(unitNumber + ": " + human.name + "  Health: " + human.health);
                }
                unitNumber++;
            }
            unitNumber = 1;

            Console.SetCursorPosition(30, 0);
            Console.Write("Enemy");
            foreach (Unit skeltal in skeltals)
            {
                Console.SetCursorPosition(30, unitNumber);
                if (!skeltal.alive)
                {
                    Console.WriteLine(unitNumber + ": " + skeltal.name + " +DEAD+");
                }
                else
                {
                    Console.WriteLine(unitNumber + ": " + skeltal.name + "  Health: " + skeltal.health);
                }
                unitNumber++;
            }

            Console.SetCursorPosition(0, humans.Count + 2);
        }

        static void WaitForSeconds(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }
    }
}
