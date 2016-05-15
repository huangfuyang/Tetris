using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Tetris
{
    public class BlockMaster
    {
        public Random rand = new Random();

        public int rows, columns;

        public int point, speed, killed;

        public int[,] master;

        public Block block;
        public Block block1;
        public Block block2;



        public BlockMaster(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        #region 游戏开始
        public void Start()
        {
            master = new int[rows, columns];
            speed = 1;
            point = 0;
            killed = 0;

            block1 = new Block(rand);
            block2 = new Block(rand);

            AddBlock();

        }
        #endregion

        #region 新增一个Block
        public void AddBlock()
        {
            block = block1;
            block.x = columns / 2 - block.columns / 2;
            block.y = 0;

            block1 = block2;
            block2 = new Block(rand);
        }
        #endregion

        #region 是否结束
        public bool isDead
        {
            get
            {
                var result = false;
                for (var x = 0; x < columns; x++)
                {
                    if (master[0, x] == 1)
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            }
        }
        #endregion

        #region 是否有消行
        public int[] KilledRows()
        {
            var killedList = new List<int>();
            for (int y = block.top; y <= block.bottom && y>0; y++)
            {
                var isKilled = true;
                for (var x = 0; x < columns; x++)
                {
                    if (master[y, x] == 0)
                    {
                        isKilled = false;
                        break;
                    }
                }
                if (isKilled)
                {
                    killedList.Add(y);
                }
            }

            foreach(var row in killedList)
            {
                for (var y = row; y > 0; y--)
                {
                    for (var x = 0; x < columns; x++)
                    {
                        master[y, x] = master[y - 1, x];
                    }
                }
            }

            var killedArr = killedList.ToArray();
            //加分
            switch (killedArr.Length)
            {
                case 2:
                    point++;
                    break;
                case 3:
                    point += 2;
                    break;
                case 4:
                    point += 4;
                    break;
            }
            //记录消除行数
            killed += killedArr.Length;
            //设定速度 10分提速一次
            speed = point / 10 + 1;

            return killedArr;
        }

        #endregion

        #region 能否向下移动
        public bool canMovedown
        {
            get
            {
                var result = true;

                //判断是否到达底部 不再移动
                if (block.bottom == rows - 1)
                {
                    result = false;
                }
                else
                {
                    for (var y = 0; y < block.rows; y++)
                    {
                        for (var x = 0; x < block.columns && block.y + y > -1 && block.y + y + 1 < rows; x++)
                        {
                            if (block.arr[y, x] == 1 && master[y + block.y + 1, block.x + x] == 1)
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }

                if (result == false)
                    block.SetMasterValue(master);
                return result;
            }

        }

        #endregion

        #region 能否向左移动
        public bool canMoveLeft
        {
            get
            {
                var result = true;
                if (block.left == 0)
                {
                    return false;
                }
                else
                {
                    for (var y = 0; y < block.rows; y++)
                    {
                        for (var x = 0; x < block.columns && block.y + y > -1 && block.x + x - 1 > -1; x++)
                        {
                            if (block.arr[y, x] == 1 && master[y + block.y, block.x + x - 1] == 1)
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
                return result;
            }
        }
        #endregion

        #region 能否向右移动
        public bool canMoveRight
        {
            get
            {
                var result = true;
                if (block.right == columns - 1)
                {
                    return false;
                }
                else
                {
                    for (var y = 0; y < block.rows; y++)
                    {
                        for (var x = 0; x < block.columns && block.y + y > -1 && block.x + x + 1 < columns; x++)
                        {
                            if (block.arr[y, x] == 1 && master[y + block.y, block.x + x + 1] == 1)
                            {
                                result = false;
                                break;
                            }
                        }
                    }

                }
                return result;
            }
        }
        #endregion

        #region 移动
        //向下
        public void MoveDown()
        {
            if (block.bottom < rows - 1)
                block.y++;

            return;
        }
        //向左
        public void MoveLeft()
        {
            if (block.left > 0)
                block.x--;
        }

        //向右
        public void MoveRight()
        {
            if (block.right < columns - 1)
                block.x++;
        }
        #endregion



        #region 变形
        //若是变形会导致与周Master的值重叠，则不能变形
        //若变形导致Master越界，则调整block位置
        public bool canChange
        {
            get
            {
                var tmp = new Block(block.type, (block.direction + 1) % 4);
                tmp.x = block.x;
                tmp.y = block.y;
                for (var y = 0; y < tmp.rows; y++)
                {
                    for (var x = 0; x < tmp.columns && tmp.y + y > -1 && tmp.x + x < columns; x++)
                    {
                        if (master[tmp.y + y, tmp.x + x] == 1)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public void Change()
        {
            block.direction = (block.direction + 1) % 4;
            block.InitBlock();


            if (block.bottom >= rows)
            {
                block.y = rows - block.rows;
            }
            if (block.right >= columns)
            {
                block.x = columns - block.columns;
            }


        }
        #endregion
    }
}
