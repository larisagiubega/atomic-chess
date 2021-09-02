using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atomic_chess
{
    interface IPiece
    {
        //metode "generale"
        bool canMove(int newX, int newY, Piece.Color color);
        bool canBeCaptured(int x, int y, Piece.Color color);
        bool inBounds(int i, int j);

        void explodePieces(int x, int y);

        //metode de cautare folosite pentru capturare
        bool lookForPawn(int x, int y, Piece.Color color); //pion
        bool lookForRook(int x, int y, Piece.Color color); //tura + regina
        bool lookForBishop(int x, int y, Piece.Color color); //nebun + regina
        bool lookForKnight(int x, int y, Piece.Color color); //cal

    }
}
