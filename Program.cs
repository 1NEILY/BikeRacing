using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

struct Object
{
    public int x;
    public int y;
    public char a;
    public ConsoleColor color;
}

    class Program
{
    static void PositionPrint(int x, int y, char c, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }
    static void PrintOnDesk(int x, int y, string str, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }

    static void Main()
    {
        int z = 0;
        int l1 = 0;
        int l2 = 0;

        PrintOnDesk(15, 1, "Добро пожаловать в гонки!", ConsoleColor.Green);
        PrintOnDesk(15, 2, "Выберите длинну дороги:", ConsoleColor.Green);
        PrintOnDesk(15, 3, "1 - 10 трасс", ConsoleColor.Green);
        PrintOnDesk(15, 4, "2 - 5 трасс", ConsoleColor.Green);
        PrintOnDesk(15, 5, "3 - 2 трассы (хардкор)", ConsoleColor.Green);
        Console.Write("\n\n               Ваш выбор?  ");
        int n = Convert.ToInt32(Console.ReadLine());

        PrintOnDesk(15, 8, "Также уровень сложности:", ConsoleColor.Green);
        PrintOnDesk(15, 9, "1 - легко", ConsoleColor.Green);
        PrintOnDesk(15, 10, "2 - средне", ConsoleColor.Green);
        PrintOnDesk(15, 11, "3 - сложно", ConsoleColor.Green);
        PrintOnDesk(15, 12, "4 - мастер", ConsoleColor.DarkYellow);
        Console.Write("\n\n               Ваш выбор?  ");
        int v = Convert.ToInt32(Console.ReadLine());

        if (n == 1) n = 10;
        if (n == 2) n = 5;
        if (n == 3) n = 2;

        if (v == 1)
        {
            v = 75;
            z = 5;
            l1 = 15;
            l2 = 25;
        }
        if (v == 2)
        {
            v = 150;
            z = 3;
            l1 = 12;
            l2 = 20;
        }
        if (v == 3)
        {
            v = 200;
            z = 2;
            l1 = 10;
            l2 = 15;
        }
        if (v == 4)
        {
            v = 350;
            z = 1;
            l1 = 6;
            l2 = 10;
        }

        double speed = v;
        double uscorenie = 1.0;
        int playfieldWidth = n + 10;
        int Zizny = z;

        Console.BufferHeight = Console.WindowHeight = 20;
        Console.BufferWidth = Console.WindowWidth = 60;

        Object MotoBike = new Object();
        MotoBike.a = '╩';
        MotoBike.x = 10;
        MotoBike.y = Console.WindowHeight - 1;
        MotoBike.color = ConsoleColor.DarkMagenta;

        Random randomno = new Random();
        List<Object> objects = new List<Object>();


        while (true)
        {
            speed += uscorenie;
            if (speed > 600)
            {
                speed = 600;
            }

            bool hit = false;
            {
                int Luck = randomno.Next(0, 100);

                Object fence1 = new Object();
                fence1.color = ConsoleColor.Yellow;
                fence1.x = 9;
                fence1.y = 0;
                fence1.a = '|';
                objects.Add(fence1);

                Object fence2 = new Object();
                fence2.color = ConsoleColor.Yellow;
                fence2.x = n + 10;
                fence2.y = 0;
                fence2.a = '|';
                objects.Add(fence2);

                if (Luck < 40)
                {
                    Object Tree = new Object();
                    Tree.color = ConsoleColor.DarkGreen;
                    Tree.x = randomno.Next(0, 8);
                    Tree.y = 0;
                    Tree.a = '¶';
                    objects.Add(Tree);

                    Object Tree1 = new Object();
                    Tree1.color = ConsoleColor.DarkGreen;
                    Tree1.x = randomno.Next(n + 12, 30);
                    Tree1.y = 0;
                    Tree1.a = '¶';
                    objects.Add(Tree1);
                }

                if (Luck < l1)
                {
                    Object Bonus = new Object();
                    Bonus.color = ConsoleColor.Red;
                    Bonus.a = '♥';
                    Bonus.x = randomno.Next(10, playfieldWidth);
                    Bonus.y = 0;
                    objects.Add(Bonus);     
                }
                else if (Luck < l2)
                {
                    Object Risovka = new Object();
                    Risovka.color = ConsoleColor.DarkYellow;
                    Risovka.a = '↓';
                    Risovka.x = randomno.Next(10, playfieldWidth);
                    Risovka.y = 0;
                    objects.Add(Risovka);  
                }
                else
                {
                    Object Breaker = new Object();
                    Breaker.color = ConsoleColor.White;
                    Breaker.x = randomno.Next(10, playfieldWidth);
                    Breaker.y = 0;
                    Breaker.a = '¤';
                    objects.Add(Breaker);
                }
            }



            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressbutton = Console.ReadKey(true);
               
                if (pressbutton.Key == ConsoleKey.LeftArrow)
                {
                    if (MotoBike.x - 1 >= 10)
                    {
                        MotoBike.x = MotoBike.x - 1;
                    }
                }
                else if (pressbutton.Key == ConsoleKey.RightArrow)
                {
                    if (MotoBike.x + 1 < playfieldWidth)
                    {
                        MotoBike.x = MotoBike.x + 1;
                    }
                }
            }


            List<Object> newList = new List<Object>();

            for (int i = 0; i < objects.Count; i++)
            {
                Object Risovka2 = objects[i];
                Object Risovka = new Object();
                Risovka.x = Risovka2.x;
                Risovka.y = Risovka2.y + 1;
                Risovka.a = Risovka2.a;
                Risovka.color = Risovka2.color;

                if (Risovka.a == '¤' && Risovka.y == MotoBike.y && Risovka.x == MotoBike.x)
                {
                    Zizny--;
                    hit = true;
                    speed += 50;
                    if (speed > 700)
                    {
                        speed = 700;
                    }
                    if (Zizny <= 0)
                    {
                        PrintOnDesk(32, 10, "ИГРА ОКОНЧЕНА '-'", ConsoleColor.Red);
                        PrintOnDesk(32, 12, "Нажмите ENTER, чтобы выйти", ConsoleColor.Red);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                if (Risovka.a == '♥' && Risovka.y == MotoBike.y && Risovka.x == MotoBike.x)
                {
                    Zizny++;
                }
                if (Risovka.y < Console.WindowHeight)
                {
                    newList.Add(Risovka);
                }
                if (Risovka.a == '↓' && Risovka.y == MotoBike.y && Risovka.x == MotoBike.x)
                {
                    speed -= 20;
                }
            }

            objects = newList;
            Console.Clear();
            if (hit)
            {
                objects.Clear();
                PositionPrint(MotoBike.x, MotoBike.y, '☺', ConsoleColor.Red);
            }
            else
            {
                PositionPrint(MotoBike.x, MotoBike.y, MotoBike.a, MotoBike.color);
            }
            foreach (Object bike in objects)
            {
                PositionPrint(bike.x, bike.y, bike.a, bike.color);
            }

            if (speed <= 0) speed = 0;
            PrintOnDesk(32, 1, "Ваши жизни: " + Zizny, ConsoleColor.Green);
            PrintOnDesk(32, 2, "Скорость байка: " + speed, ConsoleColor.Green);
            PrintOnDesk(32, 3, "Ускорение: " + uscorenie, ConsoleColor.Green);

            Thread.Sleep((int)(600 - speed));
        }
    }
}