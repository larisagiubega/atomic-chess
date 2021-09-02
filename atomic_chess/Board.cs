using System;

namespace atomic_chess
{
    class Board
    {
        //ALB
        public static Rook W_ROOK1;
        public static Rook W_ROOK2;
        public static Knight W_KNIGHT1;
        public static Knight W_KNIGHT2;
        public static Bishop W_BISHOP1;
        public static Bishop W_BISHOP2;
        public static Queen W_QUEEN;
        public static King W_KING;

        public static Pawn W_PAWN1;
        public static Pawn W_PAWN2;
        public static Pawn W_PAWN3;
        public static Pawn W_PAWN4;
        public static Pawn W_PAWN5;
        public static Pawn W_PAWN6;
        public static Pawn W_PAWN7;
        public static Pawn W_PAWN8;


        //NEGRU
        public static Rook B_ROOK1;
        public static Rook B_ROOK2;
        public static Knight B_KNIGHT1;
        public static Knight B_KNIGHT2;
        public static Bishop B_BISHOP1;
        public static Bishop B_BISHOP2;
        public static Queen B_QUEEN;
        public static King B_KING;

        public static Pawn B_PAWN1;
        public static Pawn B_PAWN2;
        public static Pawn B_PAWN3;
        public static Pawn B_PAWN4;
        public static Pawn B_PAWN5;
        public static Pawn B_PAWN6;
        public static Pawn B_PAWN7;
        public static Pawn B_PAWN8;

        public static Piece[,] board;

        //boolene care retin daca alb, respectiv negru, sunt in sah
        public static bool isWhiteInCheck = false;
        public static bool isBlackInCheck = false;

        //boolene care retin castigatorul
        public static bool gameOverForWhite;
        public static bool gameOverForBlack;
        public static bool draw;
        public static bool gameOver;

        //variabila binara in care se retine jucatorul al carui rand este;
        //consideram 0 - white; 1 - black
        public static Piece.Color beginningTurn { set; get; }
        public static Piece.Color turn { set; get; }

        public Board()
        {

        }

        public Board(int turn) : this((Piece.Color)turn)
        {

        }
        public Board(Piece.Color turn)
        {
            gameOverForWhite = false;
            gameOverForBlack = false;
            draw = false;
            gameOver = false;

            if ((int)turn < 0 || (int)turn > 1)
            {
                Random random = new Random();
                turn = (Piece.Color)random.Next(0, 2);
            }

            beginningTurn = turn;
            Board.turn = turn;

            switch (beginningTurn)
            {
                case Piece.Color.black:
                    {
                        InitializePiecesForBlackTurn();
                        board = new Piece[8, 8]
                             {
                            { W_ROOK1, W_KNIGHT1, W_BISHOP1, W_KING, W_QUEEN, W_BISHOP2, W_KNIGHT2, W_ROOK2},
                            { W_PAWN1, W_PAWN2, W_PAWN3, W_PAWN4, W_PAWN5, W_PAWN6, W_PAWN7, W_PAWN8},
                            { null,    null,    null,    null,    null,    null,    null,    null },
                            { null,    null,    null,    null,    null,    null,    null,    null },
                            { null,    null,    null,    null,    null,    null,    null,    null },
                            { null,    null,    null,    null,    null,    null,    null,    null },
                            { B_PAWN1, B_PAWN2, B_PAWN3,  B_PAWN4, B_PAWN5, B_PAWN6, B_PAWN7, B_PAWN8},
                            { B_ROOK1, B_KNIGHT1, B_BISHOP1, B_KING, B_QUEEN, B_BISHOP2, B_KNIGHT2, B_ROOK2}
                             };

                        break;
                    }
                case Piece.Color.white:
                    {
                        InitializePiecesForWhiteTurn();
                        board = new Piece[8, 8]
                        {
                            { B_ROOK1, B_KNIGHT1, B_BISHOP1, B_QUEEN, B_KING, B_BISHOP2, B_KNIGHT2, B_ROOK2},
                            { B_PAWN1, B_PAWN2, B_PAWN3, B_PAWN4, B_PAWN5, B_PAWN6, B_PAWN7, B_PAWN8 },
                            { null,    null,    null,    null,    null,    null,    null,    null },
                            { null,    null,    null,    null,    null,    null,    null,    null },
                            { null,    null,    null,    null,    null,    null,    null,    null },
                            { null,    null,    null,    null,    null,    null,    null,    null },
                            { W_PAWN1, W_PAWN2, W_PAWN3, W_PAWN4, W_PAWN5, W_PAWN6, W_PAWN7, W_PAWN8},
                            { W_ROOK1, W_KNIGHT1, W_BISHOP1, W_QUEEN, W_KING, W_BISHOP2, W_KNIGHT2, W_ROOK2}
                        };
                        break;
                    }

                    //irelevant, nu va exista niciodata caz default
                    //default:
                    //    break;
            }


        }

        public static King findKing(Piece.Color color)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null && board[i, j].name.ToLower().Contains("king") && board[i, j].name.ToLower().Contains(Convert.ToString(color)))
                        return (King)board[i, j];
                }
            }
            return null;
        }

        public static void PrintBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board.board[i, j] == null)
                    {
                        Console.Write("0   ");
                    }
                    else
                    {
                        if (Board.board[i, j].name[0] == 'w')
                            Console.Write("w");
                        if (Board.board[i, j].name[0] == 'b')
                            Console.Write("b");
                        if (Board.board[i, j].name.ToLower().Contains("pawn"))
                        {
                            Console.Write("p  ");
                        }
                        if (Board.board[i, j].name.ToLower().Contains("rook"))
                        {
                            Console.Write("r  ");
                        }
                        if (Board.board[i, j].name.ToLower().Contains("knight"))
                        {
                            Console.Write("n  ");
                        }
                        if (Board.board[i, j].name.ToLower().Contains("bishop"))
                        {
                            Console.Write("b  ");
                        }
                        if (Board.board[i, j].name.ToLower().Contains("king"))
                        {
                            Console.Write("k  ");
                        }
                        if (Board.board[i, j].name.ToLower().Contains("queen"))
                        {
                            Console.Write("q  ");
                        }

                    }
                }
                Console.WriteLine();
            }
        }

        private void InitializePiecesForBlackTurn()
        {
            //ALB
            W_ROOK1 = new Rook(0, 0, Piece.Color.white, "whiteRook1");
            W_ROOK2 = new Rook(0, 7, Piece.Color.white, "whiteRook2");
            W_KNIGHT1 = new Knight(0, 1, Piece.Color.white, "whiteKnight1");
            W_KNIGHT2 = new Knight(0, 6, Piece.Color.white, "whiteKnight2");
            W_BISHOP1 = new Bishop(0, 2, Piece.Color.white, "whiteBishop1");
            W_BISHOP2 = new Bishop(0, 5, Piece.Color.white, "whiteBishop2");
            W_QUEEN = new Queen(0, 4, Piece.Color.white, "whiteQueen");
            W_KING = new King(0, 3, Piece.Color.white, "whiteKing");

            W_PAWN1 = new Pawn(1, 0, Piece.Color.white, "whitePawn1");
            W_PAWN2 = new Pawn(1, 1, Piece.Color.white, "whitePawn2");
            W_PAWN3 = new Pawn(1, 2, Piece.Color.white, "whitePawn3");
            W_PAWN4 = new Pawn(1, 3, Piece.Color.white, "whitePawn4");
            W_PAWN5 = new Pawn(1, 4, Piece.Color.white, "whitePawn5");
            W_PAWN6 = new Pawn(1, 5, Piece.Color.white, "whitePawn6");
            W_PAWN7 = new Pawn(1, 6, Piece.Color.white, "whitePawn7");
            W_PAWN8 = new Pawn(1, 7, Piece.Color.white, "whitePawn8");


            //NEGRU
            B_ROOK1 = new Rook(7, 0, Piece.Color.black, "blackRook1");
            B_ROOK2 = new Rook(7, 7, Piece.Color.black, "blackRook2");
            B_KNIGHT1 = new Knight(7, 1, Piece.Color.black, "blackKnight1");
            B_KNIGHT2 = new Knight(7, 6, Piece.Color.black, "blackKnight2");
            B_BISHOP1 = new Bishop(7, 2, Piece.Color.black, "blackBishop1");
            B_BISHOP2 = new Bishop(7, 5, Piece.Color.black, "blackBishop2");
            B_QUEEN = new Queen(7, 4, Piece.Color.black, "blackQueen");
            B_KING = new King(7, 3, Piece.Color.black, "blackKing");

            B_PAWN1 = new Pawn(6, 0, Piece.Color.black, "blackPawn1");
            B_PAWN2 = new Pawn(6, 1, Piece.Color.black, "blackPawn2");
            B_PAWN3 = new Pawn(6, 2, Piece.Color.black, "blackPawn3");
            B_PAWN4 = new Pawn(6, 3, Piece.Color.black, "blackPawn4");
            B_PAWN5 = new Pawn(6, 4, Piece.Color.black, "blackPawn5");
            B_PAWN6 = new Pawn(6, 5, Piece.Color.black, "blackPawn6");
            B_PAWN7 = new Pawn(6, 6, Piece.Color.black, "blackPawn7");
            B_PAWN8 = new Pawn(6, 7, Piece.Color.black, "blackPawn8");
        }

        private void InitializePiecesForWhiteTurn()
        {
            //NEGRU
            B_ROOK1 = new Rook(0, 0, Piece.Color.black, "blackRook1");
            B_ROOK2 = new Rook(0, 7, Piece.Color.black, "blackRook2");
            B_KNIGHT1 = new Knight(0, 1, Piece.Color.black, "blackKnight1");
            B_KNIGHT2 = new Knight(0, 6, Piece.Color.black, "blackKnight2");
            B_BISHOP1 = new Bishop(0, 2, Piece.Color.black, "blackBishop1");
            B_BISHOP2 = new Bishop(0, 5, Piece.Color.black, "blackBishop2");
            B_QUEEN = new Queen(0, 3, Piece.Color.black, "blackQueen");
            B_KING = new King(0, 4, Piece.Color.black, "blackKing");

            B_PAWN1 = new Pawn(1, 0, Piece.Color.black, "blackPawn1");
            B_PAWN2 = new Pawn(1, 1, Piece.Color.black, "blackPawn2");
            B_PAWN3 = new Pawn(1, 2, Piece.Color.black, "blackPawn3");
            B_PAWN4 = new Pawn(1, 3, Piece.Color.black, "blackPawn4");
            B_PAWN5 = new Pawn(1, 4, Piece.Color.black, "blackPawn5");
            B_PAWN6 = new Pawn(1, 5, Piece.Color.black, "blackPawn6");
            B_PAWN7 = new Pawn(1, 6, Piece.Color.black, "blackPawn7");
            B_PAWN8 = new Pawn(1, 7, Piece.Color.black, "blackPawn8");

            //ALB
            W_ROOK1 = new Rook(7, 0, Piece.Color.white, "whiteRook1");
            W_ROOK2 = new Rook(7, 7, Piece.Color.white, "whiteRook2");
            W_KNIGHT1 = new Knight(7, 1, Piece.Color.white, "whiteKnight1");
            W_KNIGHT2 = new Knight(7, 6, Piece.Color.white, "whiteKnight2");
            W_BISHOP1 = new Bishop(7, 2, Piece.Color.white, "whiteBishop1");
            W_BISHOP2 = new Bishop(7, 5, Piece.Color.white, "whiteBishop2");
            W_QUEEN = new Queen(7, 3, Piece.Color.white, "whiteQueen");
            W_KING = new King(7, 4, Piece.Color.white, "whiteKing");

            W_PAWN1 = new Pawn(6, 0, Piece.Color.white, "whitePawn1");
            W_PAWN2 = new Pawn(6, 1, Piece.Color.white, "whitePawn2");
            W_PAWN3 = new Pawn(6, 2, Piece.Color.white, "whitePawn3");
            W_PAWN4 = new Pawn(6, 3, Piece.Color.white, "whitePawn4");
            W_PAWN5 = new Pawn(6, 4, Piece.Color.white, "whitePawn5");
            W_PAWN6 = new Pawn(6, 5, Piece.Color.white, "whitePawn6");
            W_PAWN7 = new Pawn(6, 6, Piece.Color.white, "whitePawn7");
            W_PAWN8 = new Pawn(6, 7, Piece.Color.white, "whitePawn8");
        }

        public static bool checkIfGameOver()
        {
            //draw = checkIfDraw();
            gameOverForBlack = findKing(Piece.Color.black) != null ? false : true;
            gameOverForWhite = findKing(Piece.Color.white) != null ? false : true;

            gameOver = gameOverForBlack || gameOverForWhite;
            //|| draw;
            return gameOver;
        }

        //private static bool checkIfDraw()
        //{
        //    /*conform linkului prezentat in bibliografie, poate fi pat daca:
        //   "-King vs. king (scor 200)
        //    -King and bishop vs. king (scor 203)
        //    -King and knight vs. king (scor 203)
        //    -King and bishop vs. king and bishop of the same color as the opponent's bishop" (scor 206 si bishop.name de numere 1 si 2)             
        //     */

        //    Minimax.calculateScoreForColor(Piece.Color.black);
        //    Minimax.calculateScoreForColor(Piece.Color.white);

        //    King blackKing = Board.findKing(Piece.Color.black);
        //    King whiteKing = Board.findKing(Piece.Color.white);

        //    int sumOfScores = Minimax.scoreForWhite + Minimax.scoreForBlack;

        //    if (sumOfScores == 200 || sumOfScores == 203 || sumOfScores == 206 && )
        //        return true;

        //        for (int i = 0; i < 8; i++)
        //        {
        //            if(!isBlackInCheck)
        //        }
        //}
    }
}

