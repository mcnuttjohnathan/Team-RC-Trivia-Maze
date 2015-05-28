﻿/**MazeGenerator walks through an empty map of a fixed size, and fills the map with rooms. And makes sure the rooms are inter connected
 * 
 * @author Zoe Baker
 **/

using System;
using System.Configuration;
using System.Drawing;
using System.Xml;
using TriviaMaze.com.teamrc.gameobjects;

public class MazeGenerator{

 /**generate creates a map and  of default size 4x4
 * @return      a fully filled maze with 4x4 rooms 
 **/
    public Map generate()
    {
        /*
        int mazeSize = 4;
        Map m = new Map(mazeSize);
        Point p = new Point(0, 0);

        for (int i = 0; i < mazeSize; i++){
            for (int j = 0; j < mazeSize; j++){
                int curExit = calcExits(i, j, mazeSize);
                Room r = new Room(curExit, p);
                m.setRoom(i, j, r);
                p.Y = p.Y + 128;
            }
            p.X = p.X + 128;
            p.Y = 0;
        }
        */

        Map m = new Map(3, 3);
        Console.WriteLine("Top Row: ");
        Console.WriteLine(m.getRoom(0, 0).toString());
        Console.WriteLine(m.getRoom(0, 1).toString());
        Console.WriteLine(m.getRoom(0, 2).toString());
       // Console.WriteLine(m.getRoom(0, 3).toString());
        Console.WriteLine("2 Row: ");
        Console.WriteLine(m.getRoom(1, 0).toString());
        Console.WriteLine(m.getRoom(1, 1).toString());
        Console.WriteLine(m.getRoom(1, 2).toString());
       // Console.WriteLine(m.getRoom(1, 3).toString());
        Console.WriteLine("3 Row: ");
        Console.WriteLine(m.getRoom(2, 0).toString());
        Console.WriteLine(m.getRoom(2, 1).toString());
        Console.WriteLine(m.getRoom(2, 2).toString());
       // Console.WriteLine(m.getRoom(2, 3).toString());
       /* Console.WriteLine("Bottom Row: ");
        Console.WriteLine(m.getRoom(3, 0).toString());
        Console.WriteLine(m.getRoom(3, 1).toString());
        Console.WriteLine(m.getRoom(3, 2).toString());
        Console.WriteLine(m.getRoom(3, 3).toString());
        */
        Console.Write(m.toString());
        return m;
    }

    /**generate creates a maze of the specified dimensions
     * @param h - the height of the maze to be created
     * @param w - the width of the maze to be created
     * @return      a fully filled map with rooms all interconnected
     **/
    public Map generate(int h, int w){
        Map m = new Map(h, w);

        return m;
    }

    /**calcExits takes the coordinates of the room and determines wich paths should exist without going out of bounds
     * @param i     the row number of the room coords
     * @param j     the column number of the room coords
     * @param s     how big the map is
     * @return      the int array of exits that need to exist in the room
     **/
    public int calcExits(int i, int j, int s)
    {
        int exits = 0;

        if (i != s - 1){
            exits += 1;
        }

        if (j != s - 1){
            exits += 2;
        }

        return exits;
    }


}