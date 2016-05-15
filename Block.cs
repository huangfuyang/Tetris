using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Tetris
{

    public class Block
    {
        //类型ID  T  Z !Z  7  !7  _ 田
        public int type { get; set; }

        //方向
        public int direction { get; set; } // 0 1 2 3

        //当前坐标
        public int x { get; set; }
        public int y { get; set; }
        //长宽
        public int rows { get; set; }
        public int columns { get; set; }
        //形状
        public int[,] arr { get; set; }


        public int top { get { return y; } }
        public int bottom { get { return y + rows - 1; } }
        public int left { get { return x; } }
        public int right { get { return x + columns - 1; } }

        public Block(Random rand)
        {
            type = rand.Next(7);
            direction = rand.Next(3);
            int r1 = rand.Next(100);

            // "一" 的概率 7%(
            // 其他的概率相等 
            if (r1 < 30 && type == 6)
            {
                type = rand.Next(6);
            }

            InitBlock();
        }
        public Block(int type, int direction)
        {
            this.type = type;
            this.direction = direction;
            InitBlock();
        }

        public void InitBlock()
        { 

            arr = BlockData.GetBlockType(type, direction);
            rows = arr.GetLength(0);
            columns = arr.GetLength(1);
            
        }
    }
}
