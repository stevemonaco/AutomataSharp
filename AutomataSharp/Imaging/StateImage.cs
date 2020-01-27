using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace AutomataSharp
{
    public class StateImage
    {
        private List<int[,]> _images = new List<int[,]>();

        public void AddNewFrameFromCellStates(Cell[,] cells)
        {
            int width = cells.GetLength(0);
            int height = cells.GetLength(1);
            var stateFrame = new int[width, height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    stateFrame[x, y] = cells[x, y].State;

            _images.Add(stateFrame);
        }

        public void AddNewFrameFrom1DCellHistory(Cell[,] cells)
        {
            int width = cells.GetLength(0);
            int height = 0;

            for (int x = 0; x < width; x++)
            {
                var count = cells[x, 0].StateHistory?.Count ?? 0;
                height = Math.Max(height, count);
            }
            height++; // Accounts for the current cell state

            var frame = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                int y = 0;
                foreach(var state in cells[x, 0].StateHistory)
                {
                    frame[x, y] = state;
                    y++;
                }
                frame[x, y] = cells[x, 0].State;
            }

            _images.Add(frame);
        }

        public List<char[,]> FramesAsText()
        {
            var frames = new List<char[,]>();

            foreach(var image in _images)
            {
                var width = image.GetLength(0);
                var height = image.GetLength(1);
                var frame = new char[width, height];

                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        frame[x, y] = MapStateText(image[x, y]);

                frames.Add(frame);
            }

            return frames;
        }

        public void SaveAsPng(string imagePath)
        {
            if (_images.Count == 0)
                throw new InvalidOperationException($"{nameof(SaveAsPng)} contains no image frames to save");

            int width = _images[0].GetLength(0);
            int height = _images[0].GetLength(1);
            using var outputImage = new Image<Rgba32>(width, height);

            for(int y = 0; y < height; y++)
            {
                var span = outputImage.GetPixelRowSpan(y);
                for (int x = 0; x < width; x++)
                {
                    span[x] = MapStateColor(_images[0][x, y]);
                }
            }

            outputImage.SaveAsPng(new FileStream(imagePath, FileMode.Create));
        }

        private Rgba32 MapStateColor(int state)
        {
            switch(state)
            {
                case 0:
                    return new Rgba32(255, 255, 255);
                case 1:
                    return new Rgba32(0, 0, 0);
                default:
                    return new Rgba32(255, 0, 0);
            }
        }

        private char MapStateText(int state)
        {
            switch (state)
            {
                case 0:
                    return '.';
                case 1:
                    return '#';
                default:
                    return ' ';
            }
        }

        private int MaxFrameWidth =>
            _images.Max(x => x.GetLength(0)) + 1;

        private int MaxFrameHeight =>
            _images.Max(x => x.GetLength(1)) + 1;
    }
}
