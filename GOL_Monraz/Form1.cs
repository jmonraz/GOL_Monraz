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
        bool[,] universe = new bool[25, 25];

        // The scratch pad array
        bool[,] scratchPad = new bool[25, 25];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        // seed number
        int seed = 100;

        // alive cells count
        int cellsAlive = 0;

        // Bool for enabling/disabling cell neighbor count
        bool isNeighborCountVisible = true;

        // Bool for grid on/off
        bool isGridVisible = true;

        // Bool for toroidal on/off
        bool isToroidal = true;

        // Bool for finite on/off
        bool isFinite = false;
        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            toolStripStatusLabelInterval.Text = "Interval = " + timer.Interval.ToString();
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            int count = 0;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // Apply Finite count neighbor method 
                    if(isFinite == true)
                    {
                        count = CountNeighborsFinite(x, y);
                    }
                    // Apply Toroidal count neighbor method 
                    else if (isToroidal == true)
                    {
                        count = CountNeighborsToroidal(x, y);
                    }
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

            CountAliveCells();

            aliveLabel.Text = "Alive = " + cellsAlive.ToString();
            bool[,] temp = scratchPad;
            scratchPad = universe;
            universe = temp;
            // Increment generation count
            generations++;

            // Update status strip generations

            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            graphicsPanel1.Invalidate();
        }

        private void CountAliveCells()
        {

            int yAxis = universe.GetLength(1);
            int xAxis = universe.GetLength(0);
            cellsAlive = yAxis * xAxis;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {

                    if (!universe[x, y])
                    {
                        --cellsAlive;
                    }
                }
            }

            aliveLabel.Text = "Alive = " + cellsAlive.ToString();
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

                    if (isGridVisible == true)
                    {
                        // Outline the cell with a pen
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
                    

                    if(isNeighborCountVisible == true)
                    {
                        // Sets font and size of neighbor count
                        Font font = new Font("Arial", 6f);

                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        int neighbors = 0;
                        // Gets number of neighbors for each cell in Finite mode
                        if(isFinite == true)
                        {
                            neighbors = CountNeighborsFinite(x, y);
                        }
                        // Gets number of neighbors for each cell in Toroidal mode
                        else if(isToroidal == true)
                        {
                            neighbors = CountNeighborsToroidal(x, y);
                        }

                        // If the neighbor count is zero do not display it
                        if (neighbors != 0)
                        {
                            e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Black, cellRect, stringFormat);
                        }
                    }
                    
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
                // Calls CountAliveCells to update the alive label in the statusStrip
                CountAliveCells();
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
        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0) continue;
                    // if xCheck is less than 0 then set to xLen - 1
                    if (xCheck < 0) xCheck = xLen - 1;
                    // if yCheck is less than 0 then set to yLen - 1
                    if (yCheck < 0) yCheck = yLen - 1;
                    // if xCheck is greater than or equal too xLen then set to 0
                    if (xCheck >= xLen) xCheck = 0;
                    // if yCheck is greater than or equal too yLen then set to 0
                    if (yCheck >= yLen) yCheck = 0;

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
            // Iterates through the universe array to clear the panel
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // Sets all cells to dead
                    universe[x, y] = false;
                }
            }

            // Resets the generations count
            generations = 0;
            // Resets the cellsAlive count
            cellsAlive = 0;

            aliveLabel.Text = "Alive = " + cellsAlive.ToString();
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

            if (DialogResult.OK == dlg.ShowDialog())
            {
                timer.Interval = dlg.Miliseconds;
                int y = dlg.CellHeight;
                int x = dlg.CellWidht;
                if (!(y == universe.GetLength(1) && x == universe.GetLength(0)))
                {
                    universe = new bool[x, y];
                    scratchPad = new bool[x, y];
                }

                toolStripStatusLabelInterval.Text = "Interval = " + timer.Interval.ToString();
                // Tells windows to repaint
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
            CountAliveCells();
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
            CountAliveCells();
        }

        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();


            dlg.Color = gridColor;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = graphicsPanel1.BackColor;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Turns on the neighbor count in the graphic panel for each cell
            if (neighborCountToolStripMenuItem.Checked == true)
            {
                isNeighborCountVisible = true;
            }
            //Turns off the neighbor count in the graphic panel for each cell
            else if (neighborCountToolStripMenuItem.Checked == false)
            {
                isNeighborCountVisible = false;
            }
            graphicsPanel1.Invalidate();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Turns on the grid on the graphics panel
            if(gridToolStripMenuItem.Checked == true)
            {
                isGridVisible = true;
            }
            // Turns off the grid on the graphics panel
            else if(gridToolStripMenuItem.Checked == false)
            {
                isGridVisible = false;
            }
            graphicsPanel1.Invalidate();
        }

        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Unchecks finite checkbox and sets finite count neighbors to false
            if (toroidalToolStripMenuItem.Checked == true)
            {
                isToroidal = true;
                isFinite = false;
                finiteToolStripMenuItem.Checked = false;
            }
            // Tells windows to repaint
            graphicsPanel1.Invalidate();
        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Unchecks toroidal checkbox and sets toroidal count neighbors to false
            if(finiteToolStripMenuItem.Checked == true)
            {
                isFinite = true;
                isToroidal = false;
                toroidalToolStripMenuItem.Checked = false;
            }
            // Tells windows to repaint
            graphicsPanel1.Invalidate();
        }
    }
}
