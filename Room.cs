using System;

public class Room
{
    char[][] r;
    int[] exits;

    //DVC
	public void Room(){
        this.r = new char[5,5]{{'X','X','X','X','X'},
                               {'X','O','O','O','X'},
                               {'X','O','O','O','X'},
                               {'X','O','O','O','X'},
                               {'X','X','X','X','X'}};
	}

    //EVC
    public void Room(int[] e){
        this.r = new char[5,5]{{'X','X','X','X','X'},
                               {'X','O','O','O','X'},
                               {'X','O','O','O','X'},
                               {'X','O','O','O','X'},
                               {'X','X','X','X','X'}};

        this.exits = e;
        makeExits();
    }

    public int[] getExits(){
        return this.e;
    }

    public void setExits(int[] e){
        this.exits = e;
    }

    public string tostring(){
        string result= "";
        for(int i = 0; i < r.length; i++){
            for(int j = 0; j < r[i].length; j++){
                result += r[i][j];
            }
            result += "/n";
        }
        return result;
    }

    public 
}
