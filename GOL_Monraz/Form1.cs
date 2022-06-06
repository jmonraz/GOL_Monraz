﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL_Monraz
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[5, 5];

        // The scratch pad array
        bool[,] scratchPad = new bool[5,5];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        // seed number
        int seed = 100;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int count = CountNeighborsFinite(x, y);

                    // Apply the rules
                    if (universe[x, y])
                    {
                        if (count < 2)
                        {
                            scratchPad[x, y] = false;
                        }
                        else if (count > 3)
                        {
                            scratchPad[x, y] = false;
                        }
                        else if (count == 2 || count == 3)
                        {
                            scratchPad[x, y] = true;
                        }
                    }
                    // Turn on/off the scratchPad
                    else
                    {
                        if (count == 3)
                        {
                            scratchPad[x, y] = true;
                        }
                        else
                        {
                            scratchPad[x, y] = false;
                        }
                    }
                }
            }

            bool[,] temp = scratchPad;
            scratchPad = universe;
            universe = temp;
            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private int CountNeighborsFinite(int x, int y)
        {
            // keeps count of neighbors
            int count = 0;
            // Gets the 2-d array length
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    if (xOffset == 0 && yOffset == 0) continue;
                    if (xCheck < 0) continue;
                    if (yCheck < 0) continue;
                    if (xCheck >= xLen) continue;
                    if (yCheck >= yLen) continue;

                    if (universe[xCheck, yCheck] == true) count++;

                }
            }

            return count;
        }

        private void PauseStripButton_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
        }

        private void PlayStripButton_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = true;
        }

        private void ForwardStripButton_Click(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int y = 0; y < universe.GetLength(1); y++)
            {
                for(int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }

            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            this.timer.Enabled = false;
            graphicsPanel1.Invalidate();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options_Dialog dlg = new Options_Dialog();

            dlg.Miliseconds = timer.Interval;
            dlg.CellWidht = universe.GetLength(1);
            dlg.CellHeight = universe.GetLength(0);
            
            if(DialogResult.OK == dlg.ShowDialog())
            {
                timer.Interval = dlg.Miliseconds;
                int y = dlg.CellHeight;
                int x = dlg.CellWidht;

                universe = new bool[x, y];
                scratchPad = new bool[x, y];
                graphicsPanel1.Invalidate();
            }           
        }

        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();


            dlg.Color = cellColor;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
            
        }

        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeedDialog dlg = new SeedDialog();

            dlg.seedNumber = seed;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                seed = dlg.seedNumber;
  
                Random randy = new Random(seed);

                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    for(int x = 0; x < universe.GetLength(0); x++)
                    {
                        int num = randy.Next(0, 2);
                        if(num == 0)
                        {
                            universe[x, y] = true;
                            graphicsPanel1.Invalidate();
                        }
                        else
                        {
                            universe[x, y] = false;
                            graphicsPanel1.Invalidate();
                        }
                    }
                }
            }
        }

        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random randy = new Random();

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int num = randy.Next(0, 2);
                    if (num == 0)
                    {
                        universe[x, y] = true;
                        graphicsPanel1.Invalidate();
                    }
                    else
                    {
                        universe[x, y] = false;
                        graphicsPanel1.Invalidate();
                    }
                }
            }
        }
    }
}
