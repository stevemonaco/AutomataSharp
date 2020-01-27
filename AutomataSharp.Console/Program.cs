using System;
using System.Text;

namespace AutomataSharp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 19;
            var cells = new Cell[width, 1];
            var rule = new WolframRule(90);
            var filter = new NeighborFilter1D(EdgeWrap.Zero);

            for(int x = 0; x < width; x++)
            {
                cells[x, 0] = new Cell(0, rule, filter);
            }
            cells[width / 2, 0].SetNextState(1);
            cells[width / 2, 0].StepState();

            var runner = new AutomataRunner(cells);

            PrintCells(cells);

            for (int i = 0; i < 100; i++)
            {
                runner.RunStep();
                PrintCells(cells);
            }
        }

        static void PrintCells(Cell[,] cells)
        {
            int width = cells.GetLength(0);
            int height = cells.GetLength(1);
            var sb = new StringBuilder(width * height);

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    sb.Append(cells[x, y].State == 0 ? '.' : '#');
                }
                sb.AppendLine();
            }

            Console.Write(sb.ToString());
        }
    }
}
