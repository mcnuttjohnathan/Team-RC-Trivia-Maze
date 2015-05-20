/**MazeGenerator walks through an empty map of a fixed size, and fills the map with rooms. And makes sure the rooms are inter connected
 * 
 * @author Zoe Baker
 **/

using System;
using TriviaMaze.com.teamrc.gameobjects;
using System.Drawing;


public class MazeGenerator{

    /**generate creates a blank map and walks through it space bu space setting rooms
     * @return      a fully filled map with rooms all interconnected
     **/
    public Map generate(){
        int mazeSize = 4;
        Map m = new Map(mazeSize);
        Point loc = new Point(0, 0);

        for (int i = 0; i < mazeSize; i++){
            for (int j = 0; j < mazeSize; j++){
                int curExit = calcExits(j, i, mazeSize);
                Room r = new Room(curExit, loc);
                m.setRoom(j, i, r);
                loc.X += 128;
            }
            loc.X = 0;
            loc.Y += 128;
        }

        return m;
    }

    /**calcExits takes the coordinates of the room and determines wich paths should exist without going out of bounds
     * @param i     the row number of the room coords
     * @param j     the column number of the room coords
     * @param s     how big the map is
     * @return      the int array of exits that need to exist in the room
     **/
    public int calcExits(int i, int j, int s){
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