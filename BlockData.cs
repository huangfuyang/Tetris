using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public static class BlockData
    {
        public static int[,] SetMasterValue(this Block block, int[,] master)
        {
            if (block != null)
            {
                for (var y = 0; y < block.rows; y++)
                {
                    for (var x = 0; x < block.columns && block.y + y > -1; x++)
                    {
                        if (block.arr[y, x] == 1)
                            master[block.y + y, block.x + x] = block.arr[y, x];
                    }
                }
            }
            return master;
        }

        public static int[,] GetBlockType(int type, int direction)
        {
            switch (type)
            {
                case 0://T  
                    switch (direction)
                    {
                        default:
                        case 0:
                            return new int[,] { 
                                {1,1,1},
                                {0,1,0}
                            };
                        case 1:
                            return new int[,] { 
                                {0,1},
                                {1,1},
                                {0,1}
                            };
                        case 2:
                            return new int[,] { 
                                {0,1,0},
                                {1,1,1}
                            };
                        case 3:
                            return new int[,] { 
                                {1,0},
                                {1,1},
                                {1,0}
                            };
                    }
                default:
                case 1://Z  
                    switch (direction)
                    {
                        default:
                        case 0:
                        case 2:
                            return new int[,] { 
                                {1,1,0},
                                {0,1,1}
                            };
                        case 1:
                        case 3:
                            return new int[,] { 
                                {0,1},
                                {1,1},
                                {1,0}
                            };
                    }
                case 2://S 
                    switch (direction)
                    {
                        default:
                        case 0:
                        case 2:
                            return new int[,] { 
                                {0,1,1},
                                {1,1,0}
                            };
                        case 1:
                        case 3:
                            return new int[,] { 
                                {1,0},
                                {1,1},
                                {0,1}
                            };
                    }
                case 3://L 
                    switch (direction)
                    {
                        default:
                        case 0:
                            return new int[,] { 
                                {1,0},
                                {1,0},
                                {1,1}
                            };
                        case 1:
                            return new int[,] { 
                                {1,1,1},
                                {1,0,0}
                            };
                        case 2:
                            return new int[,] { 
                                {1,1},
                                {0,1},
                                {0,1}
                            };
                        case 3:
                            return new int[,] { 
                                {0,0,1},
                                {1,1,1}
                            };
                    }
                case 4://Ld
                    switch (direction)
                    {
                        default:
                        case 0://┏
                            return new int[,] { 
                                {1,1},
                                {1,0},
                                {1,0}
                            };
                        case 1://┗
                            return new int[,] { 
                                {1,0,0},
                                {1,1,1}
                            };
                        case 2://┛
                            return new int[,] { 
                                {0,1},
                                {0,1},
                                {1,1}
                            };
                        case 3://┓
                            return new int[,] { 
                                {1,1,1},
                                {0,0,1}
                            };
                    }
                case 5://田
                    return new int[,] { 
                                {1,1},
                                {1,1}
                            };
                case 6: // 
                    switch (direction)
                    {
                        default:
                        case 0://一 
                        case 2:
                            return new int[,] {
                                {1,1,1,1}
                            };
                        case 1://|
                        case 3:
                            return new int[,] {
                                {1},
                                {1},
                                {1},
                                {1}
                            };
                    }
            }
        }

    }
}
