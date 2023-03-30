using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice.FileManager
{
    public class FileReader
    {
        public static List<NewSateliteRequest> ReadNewSatelite(string filePath)
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                List<NewSateliteRequest> newSateliteRequests= new List<NewSateliteRequest>();
                // Read the lines of the file
                string[] fileLines = File.ReadAllLines(filePath);
                int numberOfNewSAtelites = int.Parse(fileLines[0]);
                int NUMBER_OF_SATELITE_PARAMS = 4;

                // save each satalite defined in the file
                for (int lineIndex = 1; lineIndex < numberOfNewSAtelites * NUMBER_OF_SATELITE_PARAMS; lineIndex = lineIndex+ NUMBER_OF_SATELITE_PARAMS)
                {
                    //next 4 rows define all satelite params
                    NewSateliteRequest sateliteRequest = new NewSateliteRequest(fileLines[lineIndex], fileLines[lineIndex+1],
                        int.Parse(fileLines[lineIndex + 2]), int.Parse(fileLines[lineIndex + 3]));
                    newSateliteRequests.Add(sateliteRequest);
                }
                return newSateliteRequests;
            }
            else
            {
                Console.WriteLine("File does not exist.");
                throw new FileNotFoundException();
            }
        }

        //I wanted to make it more genery, using delegates but in order for faster development i created a similar code
        public static List<NewActionRequest> ReadNewRequest(string filePath)
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                List<NewActionRequest> newSateliteRequests = new List<NewActionRequest>();
                // Read the lines of the file
                string[] fileLines = File.ReadAllLines(filePath);
                int numberOfNewSAtelites = int.Parse(fileLines[0]);
                int NUMBER_OF_SATELITE_PARAMS = 3;

                // save each satalite defined in the file
                for (int lineIndex = 1; lineIndex < numberOfNewSAtelites * NUMBER_OF_SATELITE_PARAMS; lineIndex = lineIndex + NUMBER_OF_SATELITE_PARAMS)
                {
                    string currentLine = fileLines[lineIndex];
                    //next 3 rows define all satelite params
                    NewActionRequest sateliteRequest = new NewActionRequest(fileLines[lineIndex], fileLines[lineIndex + 1],
                        int.Parse(fileLines[lineIndex + 2]));
                    newSateliteRequests.Add(sateliteRequest);
                }
                return newSateliteRequests;
            }
            else
            {
                Console.WriteLine("File does not exist.");
                throw new FileNotFoundException();
            }
        }
    }
}
