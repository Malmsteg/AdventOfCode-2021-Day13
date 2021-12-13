using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AdventOfCode_2021_Day13
{
    public static class Program
    {
        public static void Main()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(currentDirectory, @"..\..\..\input.txt");
            string path = Path.GetFullPath(file);
            string[] text = File.ReadAllText(path).Replace("\r", "").Split("\n");

            int xlen =0, ylen=0, lineBreak=0;

            for(int i = 0; i < text.Length; i++)
            {
                if(string.IsNullOrEmpty(text[i]))
                {
                    lineBreak=i;
                    break;
                }
                else
                {
                    string[] points = text[i].Split(',');
                    int x = Convert.ToInt32(points[0])+1;
                    int y = Convert.ToInt32(points[1])+1;
                    xlen = x > xlen ? x : xlen;
                    ylen = y > ylen ? y : ylen;
                }
            }
            string[,] coordinates = new string [xlen,ylen];

            for(int i = 0; i < lineBreak; i++)
            {
                string[] point = text[i].Split(',');
                int x = Convert.ToInt32(point[0]);
                int y = Convert.ToInt32(point[1]);
                coordinates[x,y] = ".";
            }

            List<string> foldingInstructions = new();

            for(int i = lineBreak+1; i < text.Length;i++)
            {
                string[] temp = text[i].Split(' ');
                foldingInstructions.Add(temp[2]);
            }

            foreach(string item in foldingInstructions)
            {
                string[] temp = item.Split("=");
                if(temp[0].Equals("x"))
                {
                    int distance = Convert.ToInt32(temp[1]);
                    for(int i = distance; i < coordinates.GetLength(0);i++)
                    {
                        for(int j = 0; j < coordinates.GetLength(1);j++)
                        {
                            if(!string.IsNullOrEmpty(coordinates[i,j]) && coordinates[i,j].Equals("."))
                            {
                                coordinates[i,j] = "";
                                coordinates[distance-(i - distance),j] = ".";
                            }
                        }
                    }
                }
                else
                {
                    int distance = Convert.ToInt32(temp[1]);
                    for(int i = 0; i < coordinates.GetLength(0);i++)
                    {
                        for(int j = distance; j < coordinates.GetLength(1);j++)
                        {
                            if(!string.IsNullOrEmpty(coordinates[i,j]) && coordinates[i,j].Equals("."))
                            {
                                coordinates[i,j] = "";
                                coordinates[i,distance - (j - distance)] = ".";
                            }
                        }
                    }
                }
            }
            int count = 0;
            for(int i = 0; i < coordinates.GetLength(0);i++)
            {
                for(int j = 0; j < coordinates.GetLength(1);j++)
                {
                    if(!string.IsNullOrEmpty(coordinates[i,j]) && coordinates[i,j].Equals("."))
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
            int newX = 0, newY = 0;
            for(int i = 0; i < coordinates.GetLength(0);i++)
            {
                for(int j = 0; j < coordinates.GetLength(1);j++)
                {
                    if(!string.IsNullOrEmpty(coordinates[i,j]))
                    {
                        newX = i > newX ? i : newX;
                        newY = j > newY ? j : newY;
                    }
                }
            }
            string[,] coordinates2 = new string[newX+1,newY+1];
            for(int i = 0; i < coordinates2.GetLength(0);i++)
            {
                for(int j = 0; j < coordinates2.GetLength(1);j++)
                {
                    if(!string.IsNullOrEmpty(coordinates[i,j]))
                    {
                        coordinates2[i,j]="#";
                    }
                    else
                    {
                        coordinates2[i,j]=".";
                    }
                }
            }

            for(int j = 0; j < coordinates2.GetLength(1);j++)
            {
                for(int i = 0; i < coordinates2.GetLength(0);i++)
                {
                    Console.Write(coordinates2[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
