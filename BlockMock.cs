using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class BlockMock
    {
        public int rows, columns;
        public int[,] master;
        private double height = -0.03;
        private double clear = 8;
        private double hole = -7.5;

        public int SumOfHeight
        {
            get
            {
                int r = rows;
                for (int y = rows - 1; y >= 0; y--)
                {
                    for (var x = 0; x < columns; x++)
                    {
                        if (master[y, x] == 1)
                        {
                            r = y;
                            break;
                        }
                    }
                }
                return rows - r;
            }
        }

        public int RowsCleared
        {
            get
            {
                int r = 0;
                for (int y = rows - 1; y >= 0; y--)
                {
                    bool clear = true;
                    for (var x = 0; x < columns; x++)
                    {
                        if (master[y, x] == 0)
                        {
                            clear = false;
                            break;
                        }
                    }
                    if (clear)
                    {
                        r++;
                    }
                }
                return r;
            }
        }

        public int Holes
        {
            get
            {
                int r = 0;
                var killedList = new List<int>();
                for (int y = rows - 1; y >= 0; y--)
                {
                    bool clear = true;
                    for (var x = 0; x < columns; x++)
                    {
                        if (master[y, x] == 0)
                        {
                            clear = false;
                            break;
                        }
                    }
                    if (clear)
                    {
                        killedList.Add(y);
                    }
                }


                foreach (var row in killedList)
                {
                    for (var y = row; y > 0; y--)
                    {
                        for (var x = 0; x < columns; x++)
                        {
                            master[y, x] = master[y - 1, x];
                        }
                    }
                }
                //count hole
                for (int x = 0; x < columns; x++)
                {
                    int firstCell = -1;
                    for (int y = 0; y < rows; y++)
                    {
                        if (firstCell == -1)
                        {
                            if (master[y, x] == 1)
                            {
                                firstCell = y;
                            }
                        }
                        else
                        {
                            if (master[y, x] == 0)
                            {
                                r++;
                            }
                        }

                    }
                }
                return r;
            }
        }
        public int Movedown(Block block)
        {
            if (block.left < 0 || block.right >= columns )
            {
                return -1;
            }
            for (int i = 0; i < rows; i++)
            {
                block.y = i;
                for (var y = 0; y < block.rows; y++)
                {
                    for (var x = 0; x < block.columns && 
                        block.y + y > -1 && block.y + y < rows; x++)
                    {
                        if (block.arr[y, x] == 1 && master[y + block.y, block.x + x] == 1)
                        {
                            block.y = i-1;
                            return i - 1;
                        }
                    }
                }
                if (block.bottom == rows-1)
                {
                    block.y = i;
                    return i;
                }
            }
            return -1;
        }

        private double GetScore()
        {
            return height*SumOfHeight + clear*RowsCleared + hole*Holes;
        }

        public BlockMock(int rows, int columns, int[,] data)
        {
            this.rows = rows;
            this.columns = columns;
            master = (int[,])data.Clone();
        }

        public double AddBlock(Block b)
        {
            if (Movedown(b) != -1)
            {
                b.SetMasterValue(master);
                return GetScore();
            }
            else
            {
                return double.MinValue;
            }
        }

        public void SetData(int[,] data)
        {
            master = (int[,])data.Clone();
        }

    }
}
