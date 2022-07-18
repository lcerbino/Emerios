using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace EmeriosChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {

        }

        private static List<char> GetLongestFromTypeOfMatrix(int length, List<char> listOfMatrix, bool isDiagonal = false)
        {
            var longests = new List<List<char>>();
            if (!isDiagonal)
            {
                int r = 0;
                while (r  < listOfMatrix.Count)
                {
                    var largest = GetLongestRepetitionFromAList(listOfMatrix.GetRange(r, length));
                    longests.Add(largest);
                    r += length;
                }

                return longests.OrderByDescending(r => r.Count).First();
            }
            else
            {
                //looping from top to main diagonal
                longests.AddRange(GetLongestRepititionFromDiagonal(listOfMatrix, length));

                //looping from bottom to main diagonal 
                var listReverse = new List<char>();
                listReverse.AddRange(listOfMatrix);
                listReverse.Reverse();

                longests.AddRange(GetLongestRepititionFromDiagonal(listReverse, length));

                return longests.OrderByDescending(r => r.Count).First();
            }
        }

        public static List<char> GetLongestRepetitionFromAList(List<char> list)
        {
            var stringJoined = new string(list.ToArray());
            var pattern = @"(\w)\1*";
            var regExp = new Regex(pattern);
            var matches = regExp.Matches(stringJoined);
            var query = matches.OrderByDescending(r => r.Value.Length).First();
            var value = Convert.ToChar(query.Value.First());
            var lenght = query.Value.Length;
            var response = new List<char>();
            response.AddRange(Enumerable.Repeat(value, lenght));

            return response;
        }


        public static List<List<char>> GetLongestRepititionFromDiagonal(List<char> listOfMatrix, int length)
        {
            int r = 0; int j = 0;
            var longests = new List<List<char>>();
            while (r < listOfMatrix.Count)
            {
                var amountOfElements = j + 1;
                var largest = GetLongestRepetitionFromAList(listOfMatrix.GetRange(r, amountOfElements));
                longests.Add(largest);

                j++;
                r = j * (j + 1) / 2;

                if (j == length)
                    break;
            }

            return longests;
        }


        public static List<char> GetMaxOcurrance(string pathDireactory)
        {
            string[] fileLines = File.ReadAllLines(pathDireactory, Encoding.UTF8);
            char[,] mat = new char[fileLines.Length, fileLines.Length];
            for (int i = 0; i < fileLines.Length; ++i)
            {
                string line = fileLines[i];
                for (int j = 0; j < fileLines.Length; ++j)
                {
                    string[] split = line.Split(',');
                    mat[i, j] = Convert.ToChar(split[j].Trim());
                }
            }

             var length = mat.GetLength(0);

            (List<char> horizontal, List<char> vertical, List<char> diagonal, List<char> SecondDiagonal) = MapToLists(length, mat);


            var maxListOcurrencesByType = new List<List<char>>
            {
                GetLongestFromTypeOfMatrix(length, horizontal),
                GetLongestFromTypeOfMatrix(length, vertical),
                GetLongestFromTypeOfMatrix(length, diagonal, true),
                GetLongestFromTypeOfMatrix(length, SecondDiagonal, true)
            };

             return maxListOcurrencesByType.OrderByDescending(r => r.Count).ToList().First();
        }


        private static (List<char> horizontal, List<char> vertical, List<char> diagonal, List<char> secondDiagonal) MapToLists(int length, char[,] mat)
        {
            var horizontal = new List<char>();
            var vertical = new List<char>();
            var diagonal = new List<char>();
            var diagona2 = new List<char>();

            //fill horizontal & vertical
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    horizontal.Add(mat[i, j]);
                    vertical.Add(mat[j, i]);
                }
            }

            //fill diagonal 1
            for (int i = -length + 1; Math.Abs(i) < length; i++)
            {
                for (int j = 0; j <= length - Math.Abs(i) - 1; j++)
                {
                    int row = i < 0 ? j : i + j;
                    int col = i > 0 ? j : (Math.Abs(i) + j);
                    diagonal.Add(mat[row, col]);
                }
            }

            // fill diagonal 2 from top left to bottom right

            // Store elements in desired order
            List<List<char>> v = new List<List<char>>();
            for (int i = 0; i < 2 * length - 1; i++)
            {
                v.Add(new List<char>());
            }

            // Store every element on the basis
            // of sum of index (i + j)
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(0); j++)
                {
                    v[i + j].Add(mat[i, j]);
                }
            }

            // fill second diagonal
            for (int i = 0; i < v.Count; i++)
            {
                // print in reverse order
                for (int j = v[i].Count - 1; j >= 0; j--)
                {
                    diagona2.Add(v[i][j]);
                }
            }

            return (horizontal, vertical, diagonal, diagona2);
        }
    }

}
