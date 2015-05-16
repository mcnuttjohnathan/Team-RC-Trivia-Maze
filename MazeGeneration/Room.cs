/** Room.cs is a room object that stores a char 2d array representation 
 * of the room layout, with X being walls and O being traversable floors
 * 
 * @author Zoe Baker
 **/

using System;


public class Room
{
    char[,] r;
    int[] exits;

    /** This is the default constructor for Room
     **/
    public Room()
    {
        this.r = new char[5, 5]{{'X','X','X','X','X'},
                                   {'X','O','O','O','X'},
                                   {'X','O','O','O','X'},
                                   {'X','O','O','O','X'},
                                   {'X','X','X','X','X'}};
    }

    /**This is the explicit value constructor for Room
     * @param e     This parameter is the array that contains what exits exist
     **/
    public Room(int[] e)
    {
        this.r = new char[5, 5]{{'X','X','X','X','X'},
                                   {'X','O','O','O','X'},
                                   {'X','O','O','O','X'},
                                   {'X','O','O','O','X'},
                                   {'X','X','X','X','X'}};

        this.exits = e;
        makeExits();
    }

    /** getExits returns the array of exits
     * @return      The array containing the exits existing in the room
     **/
    public int[] getExits()
    {
        return this.exits;
    }

    /** setExits sets the exits in the room
    * @param e     The array containing the exits existing in the room
    **/
    public void setExits(int[] e)
    {
        this.exits = e;
        makeExits();
    }

    /** toString
    * @return      the room 2d array in a string format
    **/
    public string toString()
    {
        string result = "";

        for (int i = 0; i < r.GetLength(0); i++)
        {
            for (int j = 0; j < r.GetLength(1); j++)
            {
                result += r[i, j];
            }
            result += "\n";
        }

        return result;
    }

    /** makeExits uses the 2d array of exits to go through the room 
     * and make sure that the chars reflected the open paths.
     **/
    public void makeExits()
    {
        if (this.exits[0] == 1)
        {
            this.r[0, 2] = 'O';
        }

        if (this.exits[1] == 1)
        {
            this.r[2, 4] = 'O';
        }

        if (this.exits[2] == 1)
        {
            this.r[4, 2] = 'O';
        }

        if (this.exits[3] == 1)
        {
            this.r[2, 0] = 'O';
        }
    }
}