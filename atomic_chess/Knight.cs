using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atomic_chess
{
    class Knight : Piece, IPiece
    {
        public static int[,] legalMoves = new int[,] {
                                            { -2, -1 }, //stanga sus-sus
                                            { -1, -2 }, //stanga stanga-sus
                                            { 1, -2}, //stanga stanga-jos
                                            { 2, -1}, //stanga jos-jos
                                            { -2, 1}, //dreapta sus-sus
                                            { -1, 2}, //dreapta dreapta-sus
                                            { 1, 2}, //dreapta dreapta-jos
                                            { 2, 1} //dreapta jos-jos
                                            };
        public Knight(int x, int y, Piece.Color color, string name) : base(x, y, color, name)
        {
            points = 3;
        }

        public override bool canMove(int newX, int newY, Piece.Color color)
        {
            string key = Convert.ToInt32(newX) + Convert.ToString(newY);

            if (color == Piece.Color.white)
            {
                if (atomicChess.panelDict[key].isFree || atomicChess.panelDict[key].imageName.Contains("black"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (newX == this.x + legalMoves[i, 0] && newY == this.y + legalMoves[i, 1])
                        {
                            return true;
                        }
                    }
                }
            }

            if (color == Piece.Color.black)
            {

                if (atomicChess.panelDict[key].isFree || atomicChess.panelDict[key].imageName.Contains("white"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (newX == this.x + legalMoves[i, 0] && newY == this.y + legalMoves[i, 1])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
