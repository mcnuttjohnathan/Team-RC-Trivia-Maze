using System;

namespace TriviaMaze
{
    public class RoomTester
    {
        static void Main(string[] args)
        {
            Room testRoom = new Room();
            string testRoomStr = testRoom.tostring();
            Console.WriteLine(testRoomStr);

            int[] a = { 1, 0, 0, 0 };
            Room testRoom2 = new Room(a);
            string testRoomStr2 = testRoom2.tostring();
            Console.WriteLine(testRoomStr2);

            int[] b = { 1, 1, 0, 0 };
            Room testRoom3 = new Room(b);
            string testRoomStr3 = testRoom3.tostring();
            Console.WriteLine(testRoomStr3);

            int[] c = { 1, 1, 1, 0 };
            Room testRoom4 = new Room(c);
            string testRoomStr4 = testRoom4.tostring();
            Console.WriteLine(testRoomStr4);

            int[] d = { 1, 1, 1, 1 };
            Room testRoom5 = new Room(d);
            string testRoomStr5 = testRoom5.tostring();
            Console.WriteLine(testRoomStr5);

            int[] e = { 1, 0, 0, 1 };
            Room testRoom6 = new Room(e);
            string testRoomStr6 = testRoom6.tostring();
            Console.WriteLine(testRoomStr6);

            MazeGenerator mg = new MazeGenerator();
            Map m = mg.generate();
            String s = m.toString();
            Console.Write(s);
            Console.Read();

            Console.Read();
        }
    }
}