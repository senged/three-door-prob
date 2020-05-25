using System;


namespace Senged.ThreeDoors
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Random r = new Random();


            int numTries = 10000;   //10
            System.Collections.Generic.Dictionary<int, int> actuals = new System.Collections.Generic.Dictionary<int, int>()
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0}
            };

            int countCorrectGuessRandomeFromThree = 0;
            int countCorrectGuessRandomFromTwoRemaining = 0;
            int countCorrectGuessSwitchFromTwoRemaining = 0;

            for (int i = 0; i < numTries; i++)
            {
                System.Collections.Generic.List<int> doorNums = new System.Collections.Generic.List<int>() { 0, 1, 2 };


                int doorActual = getRandOneTwoThree(r);

                actuals[doorActual]++;



                //--------------------------------------------------------------------------
                //Random guess from 3
                int guessFromThree = getRandOneTwoThree(r);
                if (guessFromThree == doorActual)
                    countCorrectGuessRandomeFromThree++;



                //--------------------------------------------------------------------------
                //Random guess from 2 remaining after removing 1 known wrong answer
                int[] doorsNotSelected = new int[2];
                int count = 0;
                foreach (var d in doorNums)
                {
                    if (d != guessFromThree)
                    {
                        doorsNotSelected[count] = d;
                        count++;
                    }
                }

                int idxToKeep = -1;

                //If remaining doors both false, pick randomly. Otherwise have to keep the true door
                if (doorsNotSelected[0] != doorActual && doorsNotSelected[1] != doorActual)
                {
                    //Both false, pick randomly
                    idxToKeep = r.Next(0, 2);
                }
                else if (doorsNotSelected[0] == doorActual)
                {
                    idxToKeep = 0;
                }
                else
                {
                    idxToKeep = 1;
                }

                int[] twoDoors = new int[2];
                twoDoors[0] = doorsNotSelected[idxToKeep];
                twoDoors[1] = guessFromThree;


                //new random guess from two
                int r2 = r.Next(0, 2);
                int guessFromTwo = twoDoors[r2];

                if (guessFromTwo == doorActual)
                    countCorrectGuessRandomFromTwoRemaining++;


                //switch guess
                int switchGuessDoor = twoDoors[0];  //known from above initial guess put in idx=1
                if (switchGuessDoor == doorActual)
                    countCorrectGuessSwitchFromTwoRemaining++;
            }

            Console.WriteLine($"Actual results of {numTries} tries:");
            foreach (var doors in actuals)
            {
                Console.WriteLine($"    Door {doors.Key}: {(doors.Value / (double)numTries).ToString("P3")}");
            }

            Console.WriteLine($"Success rate guessing randomly from 3 choices = {(countCorrectGuessRandomeFromThree / (double)numTries).ToString("P3")}");
            Console.WriteLine($"Success rate guessing randomly from 2 remaining after removing one known false = {(countCorrectGuessRandomFromTwoRemaining / (double)numTries).ToString("P3")}");
            Console.WriteLine($"Success rate switching guess to new choice from 2 remaining after removing one known false = {(countCorrectGuessSwitchFromTwoRemaining / (double)numTries).ToString("P3")}");

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }


        private static int getRandOneTwoThree(Random r)
        {
            int doorNum = 0;

            double rVal = r.NextDouble();

            if (rVal < (1.0 / 3.0))
            {
                doorNum = 0;
            }
            else if (rVal > (1.0 / 3.0) && rVal <= (2.0 / 3.0))
            {
                doorNum = 1;
            }
            else
            {
                doorNum = 2;
            }

            return doorNum;
        }

    }


}
