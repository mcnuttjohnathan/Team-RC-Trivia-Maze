/**Map consists of a 2d array of Rooms to populate the map, and keeps track of the start and exit coordinates.
 * 
 * @author Zoe Baker
 **/

using System;

public class Map{
    Room[,] m;
    int[] start;
    int[] finish;

    /**The Default Constructor
     **/
    public Map(){
    }

    /**The Explicite Value Constructor
     * @param s     s is the size of the map
     **/
    public Map(int s){
        this.m = new Room[s, s];
        this.start = new int[2] { 0, 0 };
        this.finish = new int[2] { s - 1, s - 1 };
    }

    /**Set Room assigns a specific room to a place in the map
     * @param i         i is the row number of the coords
     * @param j         j is the column number of the coors
     * @param r         r is the room that needs to be placed in those coords
     **/
    public void setRoom(int i, int j, Room r){
        m[i, j] = r;
    }

    /**getRoom returns the room at specific coords
    * @param i         i is the row number of the coords
    * @param j         j is the column number of the coors
    * @return          returns the room stored at the value of the inputted coords
    **/
    public Room getRoom(int i, int j){
        return m[i, j];
    }

    /**Set Start assigns start the coordinates entered in the array
    * @param i         i is the array that stores the coords of start
    **/
    public void setStart(int[] i){
        this.start = i;
    }

    /**getStart returns the location of start
    * @return        returns the coords of start in an int array
    **/
    public int[] getStart() {
        return this.start;
    }

    /**setFinish assigns finish the coordinates entered in the array
    * @param i         i is the array that stores the coords of finish
    **/
    public void setFinish(int[] i){
        this.finish = i;
    }

    /**getFinish returns the location of finish
    * @return        returns the coords of finish in an int array
    **/
    public int[] getFinish(){
        return this.finish;
    }

    /** toString
    * @return      the map 2d array in a string format showing linked passages
    **/
    public string toString(){
        string result = "";

        for (int i = 0; i < this.m.GetLength(0); i++){
            for (int j = 0; j < this.m.GetLength(1); j++){

                int[] e = m[i, j].getExits();

                if (start[0] == i && start[1] == j){
                    result += 'S';
                }
                else if (finish[0] == i && finish[1] == j){
                    result += 'E';
                }
                else{
                    result += 'O';
                }

                if (e[1] == 1){
                    result += '-';
                }
                else{
                    result += ' ';
                }
            }

            if (i != 3){
                result += "\n| | | | \n";
            }

        }

        return result;
    }

}