﻿using System.Collections.Generic;

namespace AutomataSharp
{
    public class AutomataRunner
    {
        public Cell[,] Grid { get; }
        public int Width { get; }
        public int Height { get; }

        public AutomataRunner(Cell[,] grid)
        {
            Grid = grid;
            Width = Grid.GetLength(0);
            Height = Grid.GetLength(1);
        }

        public void RunStep(int steps = 1)
        {
            var filterList = new List<Cell>();
            for (int i = 0; i < steps; i++)
                RunStep();

            void RunStep()
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        var cell = Grid[x, y];
                        filterList.Clear();
                        filterList.AddRange(cell.Filter.Filter(Grid, x, y));
                        cell.SetNextState(cell.Rule.RunRule(filterList));
                    }
                }

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Grid[x, y].StepState();
                    }
                }
            }
        }
    }
}
