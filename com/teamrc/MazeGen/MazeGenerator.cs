/**MazeGenerator walks through an empty map of a fixed size, and fills the map with rooms. And makes sure the rooms are inter connected
 * 
 * @author Zoe Baker
 **/

using System;
using System.Drawing;
using TriviaMaze.com.teamrc.gameobjects;

public class MazeGenerator{

    /**generate creates a blank map and walks through it space bu space setting rooms
     * @return      a fully filled map with rooms all interconnected
     **/
    public Map generate(){
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