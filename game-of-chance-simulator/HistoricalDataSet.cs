using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameOfChanceSimulator
{
    public class HistoricalDataSet
    {
        GameSimulator game = new GameSimulator();
        public int Size { get; private set; } // read-only property to expose the number of the underlying data
        private List<HistoricalDataPoint> __DataPoints = new List<HistoricalDataPoint>();
        public IReadOnlyList<HistoricalDataPoint> Datapoints { get { return __DataPoints.AsReadOnly(); } }

         internal void AddDataPoint(HistoricalDataPoint data)
        {
            // adding data to the Datapoint list
            __DataPoints.Add(data);
        }



       List<string> GetTurtles()
        {
            // get all the turtles from the csv
            string[] turtlelines = File.ReadAllLines("turtle.csv");
            List<string> datas;
            List<string> tekik = new List<string>();
            List<Turtle> turtles = new List<Turtle>();


            for (int i = 0; i < turtlelines.Length; i++)
            {
                List<string> data = new List<string>();
                data.Add(turtlelines[i]);
                for (int j = 0; j < data.Count; j++)
                {
                    datas = data[j].Split(',').ToList();
                    tekik.Add(datas[0]);
                    
                }

            }
            return tekik;


        }




        public HistoricalDataSet(ILogger logger) 
        {
            // list the actual fighter turtles
            int counter = 1;
            logger.Info("The fighter turtles are: \n");
            foreach (var turtle in GetTurtles())
            {
                Console.WriteLine(counter + ". " + turtle + "\n");
                counter++;
               
            }
            
        }

       public void Generate()
        {

            /* calling the method generates a single new (randomized) HistoricalDataPoint instance,
             * it adds this to the list of available DataPoints
             * stored by the class and finally appends a new entry to history.csv. */

            HistoricalDataPoint data = new HistoricalDataPoint(game.Simulator());
            AddDataPoint(data);

            string file = "history.csv";

            if (!File.Exists(file))
            {
                File.WriteAllText(file, data.Storinglist + "\n");
            }
            else
            {
                File.AppendAllText(file, data.Storinglist + "\n");
            }

        }





        public void Load() 
        {
            /* calling the method reads already generated data points from history.csv,
             * it creates an instance of HistoricalDataPoint for each entry in the CSV file. */
            string file = "history.csv";
            string[] data = File.ReadAllLines(file);

            foreach (var item in data)
            {
                HistoricalDataPoint DataFollowup = new HistoricalDataPoint(item);
                AddDataPoint(DataFollowup);
            }
            
        }
    }
}

    

