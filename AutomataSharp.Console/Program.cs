using System;
using System.Text;

namespace AutomataSharp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 79;
            int steps = 200;
            var cells = new Cell[width, 1];
            var rule = new WolframRule(90);
            var filter = new NeighborFilter1D(EdgeWrap.Zero);
            var imageName = "automata.png";

            for(int x = 0; x < width; x++)
            {
                var cell = new Cell(0, rule, filter);
                cell.StateHistory = new HistoryQueue<int>(steps);
                cells[x, 0] = cell;
            }
            cells[width / 2, 0].SetNextState(1);
            cells[width / 2, 0].StepState();

            var runner = new AutomataRunner(cells);
            runner.RunStep(steps);
            Output1DCellHistoriesAsPng(cells, imageName);
            Output1DCellHistoriesAsText(cells);
        }

        static void Output1DCellHistoriesAsPng(Cell[,] cells, string imagePath)
        {
            var stateImage = new StateImage();
            stateImage.AddNewFrameFrom1DCellHistory(cells);
            stateImage.SaveAsPng(imagePath);
        }

        static void Output1DCellHistoriesAsText(Cell[,] cells)
        {
            var stateImage = new StateImage();
            stateImage.AddNewFrameFrom1DCellHistory(cells);
            var textFrames = stateImage.FramesAsText();

            foreach (var frame in textFrames)
                PrintTextFrame(frame);
        }

        static void PrintTextFrame(char[,] frame)
        {
            int width = frame.GetLength(0);
            int height = frame.GetLength(1);
            var sb = new StringBuilder(width * height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    sb.Append(frame[x, y]);
                }
                sb.AppendLine();
            }

            Console.Write(sb.ToString());
        }
    }
}
