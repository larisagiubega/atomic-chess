using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atomic_chess
{
    public class Minimax
    {
        public static int scoreForWhite = 0;
        public static int scoreForBlack = 0;

        public int moveValue;
        public int bestMoveValue;

        Dictionary<string, string> possibleMoves = new Dictionary<string, string>();

        public static void calculateScoreForColor(Piece.Color color)
        {
            int score = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board.board[i, j] != null && Board.board[i, j].name.ToLower().Contains(Convert.ToString(color)))
                    {
                        score += Board.board[i, j].points;
                    }
                }
            }

            if (color == Piece.Color.white)
                scoreForWhite = score;
            else scoreForBlack = score;
        }

        private int scoreEvaluator()
        {
            calculateScoreForColor(Piece.Color.black);
            calculateScoreForColor(Piece.Color.white);
            return Board.beginningTurn == Piece.Color.white ? scoreForWhite - scoreForBlack : scoreForBlack - scoreForWhite;
        }

        private int evaluateBoard()
        {
            int totalEvaluation = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board.board[i, j] != null)
                        totalEvaluation = totalEvaluation + Board.board[i, j].points;
                }
            }
            return totalEvaluation;
        }

        public static List<string> returnPossibleMovesForPiece(int x, int y)
        {
            Piece currentPiece;
            List<string> possibleMovesForPiece = new List<string>();

            if (Board.board[x, y] != null)
            {
                currentPiece = Board.board[x, y];
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (currentPiece.canMove(i, j, currentPiece.color))
                        {
                            possibleMovesForPiece.Add(Convert.ToString(i) + Convert.ToString(j));
                        }
                    }
                }
                return possibleMovesForPiece;
            }
            else return null;
        }

        public static void printMovesForPiece(int x, int y)
        {
            List<string> movesForPiece = returnPossibleMovesForPiece(x, y);

            if (movesForPiece != null)
                foreach (string move in movesForPiece)
                {
                    Console.WriteLine(move);
                }
        }

        private void returnAllPossibleMovesForColor(Piece.Color color) //pune in dictionarul possibleMoves toate mutarile posibile ale culorii primite ca parametru
                                                                    //sub forma pozitie-curenta - pozitie-posibila (unde pozitie-posibila e o valoare din lista de pozitii posibile ale 
                                                                    //piesei curente)
        {
            possibleMoves.Clear(); //golim dictionarul
            string key; //declaram cheia pentru a o calcula la fiecare piesa de culoarea primita drept parametru

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                { 
                    if (Board.board[i, j] != null && Board.board[i, j].color == color) //parcurgem toata tabla, iar in cazul in care gasim o piesa de culoarea color
                    {
                        key = Convert.ToString(i) + Convert.ToString(j); //preluam cheia (formata din coordonatele piesei actuale)
                        foreach (string possibleMove in returnPossibleMovesForPiece(i, j)) //introducem in dictionar
                            possibleMoves.Add(key, possibleMove); //fiecare combinatie coordonate-actuale <> coordonate-posibile
                    }
                }
            }
        }


        private int minimax(int depth, bool maximizingPlayer, int alpha, int beta, Dictionary<string, string> possibleMoves)
        {
            if (depth == 0)
                return scoreEvaluator();

            if (maximizingPlayer)
            {
                bestMoveValue = int.MinValue;
                foreach (KeyValuePair<string, string> key in possibleMoves)
                {
                    moveValue = minimax(depth - 1, false, alpha, beta, possibleMoves);
                    bestMoveValue = Math.Max(bestMoveValue, moveValue);
                    alpha = Math.Max(alpha, bestMoveValue);
                    if (beta <= alpha)
                        break;
                }
                return bestMoveValue;

            }
            else
            {
                bestMoveValue = int.MaxValue;
                foreach (KeyValuePair<string, string> key in possibleMoves)
                {
                    moveValue = minimax(depth - 1, false, alpha, beta, possibleMoves);
                    bestMoveValue = Math.Min(bestMoveValue, moveValue);
                    beta = Math.Min(beta, bestMoveValue);
                    if (beta <= alpha)
                        break;
                }
                return bestMoveValue;


            }
        }

        //    function minimax(node, depth, isMaximizingPlayer, alpha, beta):

        //if node is a leaf node :
        //    return value of the node

        //if isMaximizingPlayer :
        //    bestVal = -INFINITY 
        //    for each child node :
        //        value = minimax(node, depth+1, false, alpha, beta)
        //        bestVal = max(bestVal, value)
        //        alpha = max(alpha, bestVal)
        //        if beta <= alpha:
        //            break
        //    return bestVal

        //else :
        //    bestVal = +INFINITY 
        //    for each child node :
        //        value = minimax(node, depth+1, true, alpha, beta)
        //        bestVal = min(bestVal, value)
        //        beta = min(beta, bestVal)
        //        if beta <= alpha:
        //            break
        //    return bestVal

        // Calling the function for the first time.
        //minimax(0, 0, true, -INFINITY, +INFINITY)
    }
}
