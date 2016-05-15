using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public System.Windows.Forms.Timer timer;
        public BlockMaster blockMaster;
        Block block { get { return blockMaster.block; } }
        BrushData brushData;
        private AI ai;
        Label[,] labs;
        Label[,] labs1;
        Label[,] labs2;
        int rows, columns;

        public Window1()
        {
            InitializeComponent();
            ConsoleManager.Show();
        }


        void InitMaster()
        {
            labs = new Label[rows, columns];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    Label lab = new Label();
                    lab.Width = 16;
                    lab.Height = 16;
                    lab.Background = brushData.none;
                    labs[i, j] = lab;
                    Grid.SetRow(lab, i);
                    Grid.SetColumn(lab, j);

                    gridMaster.Children.Add(lab);
                }
            }

            labs1 = new Label[4, 4];
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    Label lab = new Label();
                    lab.Width = 16;
                    lab.Height = 16;
                    lab.Background = brushData.none;
                    labs1[i, j] = lab;
                    Grid.SetRow(lab, i);
                    Grid.SetColumn(lab, j);
                    gridBlock1.Children.Add(labs1[i, j]);
                }
            }
            labs2 = new Label[4, 4];
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    Label lab = new Label();
                    lab.Width = 16;
                    lab.Height = 16;
                    lab.Background = brushData.none;
                    labs2[i, j] = lab;
                    Grid.SetRow(lab, i);
                    Grid.SetColumn(lab, j);
                    gridBlock2.Children.Add(labs2[i, j]);
                }
            }
        }

        public void BlockMoveDown()
        {
            if (block == null)
                return;

            if (blockMaster.isDead)
            {
                timer.Stop();
                ai.End();
                MessageBox.Show("Game Over!");
                return;
            }

            if (blockMaster.canMovedown)
            {
                BrushBlockLabel(-1);
                blockMaster.MoveDown();
                BrushBlockLabel(block.type);
            }
            else
            {
                KillRowsLabel(blockMaster.KilledRows());
                blockMaster.AddBlock();
                BrushBlock(blockMaster.block1, labs1);
                BrushBlock(blockMaster.block2, labs2);
            }






        }

        void KillRowsLabel(int[] killedRows)
        {
            foreach (var row in killedRows)
            {
                for (var y = row; y > 1; y--)
                {
                    for (var x = 0; x < columns; x++)
                    {
                        labs[y, x].Background = labs[y - 1, x].Background;
                    }
                }
            }
            labLevel.Content = "Level: " + blockMaster.speed.ToString();
            labPoint.Content = "Point: " + blockMaster.point;
            labRow.Content = "Row: " + blockMaster.killed;

            timer.Interval = 500 / (blockMaster.speed);
        }

        public void BlockMoveLeft()
        {
            if (blockMaster.canMoveLeft)
            {
                BrushBlockLabel(-1);
                blockMaster.MoveLeft();
                BrushBlockLabel(block.type);
            }
        }

        public void BlockMoveRight()
        {
            if (blockMaster.canMoveRight)
            {
                BrushBlockLabel(-1);
                blockMaster.MoveRight();
                BrushBlockLabel(block.type);
            }
        }

        public void BlockChange()
        {
            if (blockMaster.canChange)
            {
                BrushBlockLabel(-1);
                blockMaster.Change();
                BrushBlockLabel(block.type);
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "F2":
                    Start();
                    return;
                case "Space":
                case "Up":
                    BlockChange();
                    return;
                case "Down":
                    BlockMoveDown();
                    return;
                case "Left":
                    BlockMoveLeft();
                    return;
                case "Right":
                    BlockMoveRight();
                    return;
                default:
                    return;
            }
        }

        void BrushBlockLabel(int blockType)
        {
            for (var y = 0; y < block.rows; y++)
            {
                for (var x = 0; x < block.columns; x++)
                {
                    if (block.arr[y, x] == 1 && block.y + y >= 0)
                    {
                        labs[y + block.y, x + block.x].Background = brushData.getBrush(blockType);
                    }
                }
            }

        }

        void BrushBlock(Block b, Label[,] labels)
        {
            for (var y = 0; y < 4; y++)
            {
                for (var x = 0; x < 4; x++)
                {
                    if (y < b.rows && x < b.columns)
                        labels[y + b.y, x + b.x].Background = brushData.getBrush(b.arr[y, x] == 1 ? b.type : -1);
                    else
                        labels[y + b.y, x + b.x].Background = brushData.getBrush(-1);
                }
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

            rows = gridMaster.RowDefinitions.Count;
            columns = gridMaster.ColumnDefinitions.Count;
            blockMaster = new BlockMaster(rows, columns);
            brushData = new BrushData();
            ai = new AI(this);

            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(MoveDown_Tick);
            timer.Enabled = false;
            InitMaster();
        }

        private void Start()
        {
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    labs[i, j].Background = brushData.none;
                }
            }

            blockMaster.Start();

            timer.Interval = 500 / blockMaster.speed;
            timer.Start();
            labLevel.Content = "Level: 1";
            labPoint.Content = "Point: 0";
            labRow.Content = "Row: 0";

            ai.Start();
        }

        private void MoveDown_Tick(object sender, EventArgs e)
        {
            //BlockMoveDown();
        }



    }
}
