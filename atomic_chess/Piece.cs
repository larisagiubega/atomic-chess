using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atomic_chess
{
    public class Piece
    {
        public enum Color
        {
            white = 0, //0 pentru alb
            black      //1 pentru negru
        }

        public Color color { set; get; }

        //variabile folosite pt coordonatele x si y ale pieselor
        public int x;
        public int y;

        public short points;

        public string name;

        public bool hasMoved;
        public Piece()
        {
        }

        public Piece(int x, int y, Color color, string name)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.name = name;
        }

        public virtual bool isCastlingPossible(int newX, int newY)
        {
            return false;
        }

        //metode implementate din interfata

        public virtual bool canMove(int newX, int newY, Color color) //metoda pentru mutare
        {
            return false;
        }

        public void explodePieces(int x, int y)
        {
            string currentCoordinates = Convert.ToString(x) + Convert.ToString(y);//coordonatele actuale (ale piesei si ale panelului unde s-a mutat)
            Board.board[x, y] = null;//pe tabla piesa se sinucide 
            PiecePanel panelNo = (PiecePanel)atomicChess.panelDict[currentCoordinates];//numarul panelului se ia din dictionarul de la coordonatele respective
            panelNo.BackgroundImage = null;//pe panelul respectiv nu va mai exista imagine
            panelNo.isFree = true;


            //sau daca orice alta piesa din jurul lui la distanta de 1 poate fi capturata, indiferent daca este culoarea lui sau a adversarului
            for (int i = 0; i < 8; i++) //parcurgem mutarile legale ale regelui intrucat regele are patratele inconjuratoare in matricea de mutari legale
            {
                if (this.inBounds(x + King.legalMoves[i, 0], y + King.legalMoves[i, 1]) &&
                    Board.board[x + King.legalMoves[i, 0], y + King.legalMoves[i, 1]] != null &&
                    !Board.board[x + King.legalMoves[i, 0], y + King.legalMoves[i, 1]].name.ToLower().Contains("pawn"))
                //daca noile coordonate se afla pe tabla si daca pe tabla la acele coordonate se gaseste o piesa si daca piesa e diferita de pion
                {
                    currentCoordinates = Convert.ToString(x + King.legalMoves[i, 0]) + Convert.ToString(y + King.legalMoves[i, 1]);
                    //preluam noile coordonate curente
                    Board.board[x + King.legalMoves[i, 0], y + King.legalMoves[i, 1]] = null; //eliberam in matrice pe tabla locurile
                    panelNo = (PiecePanel)atomicChess.panelDict[currentCoordinates];//numarul panelului se ia din dictionarul de la coordonatele respective
                    panelNo.isFree = true; //eliberam la nivel de panel
                    panelNo.BackgroundImage = null;//se elibereaza poza de pe panelul corespunzator
                }
            }
        }

        public bool canBeCaptured(int x, int y, Piece.Color color) //metoda care verifica daca piesa poate fi capturata
        {
            if (inBounds(x, y))
                return lookForBishop(x, y, color) || lookForKnight(x, y, color) || lookForRook(x, y, color) || lookForPawn(x, y, color);

            return false;
            //returneaza adevarat daca piesa se afla in tabla si poate fi capturata cel putin de o tura, de un pion, de un cal sau de un nebun 
            //nota: regina se misca asemenea unui nebun si a unei ture, deci nu e nevoie de o metoda speciala pentru ea
        }

        public bool inBounds(int i, int j) //metoda pentru verificarea pozitiei pe tabla, daca e inca pe tabla returneaza true, daca nu, false
        {
            return i >= 0 && i < 8 && j >= 0 && j < 8;
        }

        public bool lookForRook(int x, int y, Piece.Color color)
        {
            //Board.PrintBoard();
            //Console.WriteLine("\n\n");
            Piece currentPiece = null;

            //verifica in jos
            int iterator = 0;
            while (inBounds(x + ++iterator, y))//parcurge de la x-ul actual pana la margine, y ramane acelasi
            {
                currentPiece = Board.board[x + iterator, y];//se preia piesa curenta din matrice

                if (currentPiece != null && (currentPiece.name.ToLower().Contains("rook") || currentPiece.name.ToLower().Contains("queen"))
                    && !currentPiece.name.ToLower().Contains(Convert.ToString(color))) //se verifica daca in matrice la 
                    //coordonatele x+iterator si y daca se gaseste o piesa; in caz afirmativ, daca piesa este o tura sau o regina si este de culoare diferita fata de
                    //culoarea primita ca parametru
                    return true; //se returneaza true
                if (currentPiece != null) //daca nu s-a returnat true, dar se gaseste alta piesa se iese din while
                    break;
            }

            //verifica in sus, aceleasi explicatii ca anterior
            iterator = 0;
            while (inBounds(x + --iterator, y))
            {
                currentPiece = Board.board[x + iterator, y];

                if (currentPiece != null && (currentPiece.name.ToLower().Contains("rook") || currentPiece.name.ToLower().Contains("queen"))
                    && !currentPiece.name.ToLower().Contains(Convert.ToString(color)))
                    return true;
                if (currentPiece != null)
                    break;
            }

            //verifica la dreapta, aceleasi explicatii ca anterior
            iterator = 0;
            while (inBounds(x, y + ++iterator))
            {
                currentPiece = Board.board[x, y + iterator];

                if (currentPiece != null && (currentPiece.name.ToLower().Contains("rook") || currentPiece.name.ToLower().Contains("queen"))
                    && !currentPiece.name.ToLower().Contains(Convert.ToString(color)))
                    return true;
                if (currentPiece != null)
                    break;
            }

            //verifica la stanga, aceleasi explicatii ca anterior
            iterator = 0;
            while (inBounds(x, y + --iterator))
            {
                currentPiece = Board.board[x, y + iterator];

                if (currentPiece != null && (currentPiece.name.ToLower().Contains("rook") || currentPiece.name.ToLower().Contains("queen"))
                   && !currentPiece.name.ToLower().Contains(Convert.ToString(color)))
                    return true;
                if (currentPiece != null)
                    break;
            }
            return false;
        }

        public bool lookForBishop(int x, int y, Piece.Color color)
        {
            Piece currentPiece = null;

            //verifica diagonala din partea dreapta jos
            int iterator = 0;
            while (inBounds(x + ++iterator, y + iterator))
            {
                currentPiece = Board.board[x + iterator, y + iterator];

                if (currentPiece != null && (currentPiece.name.ToLower().Contains("bishop") || currentPiece.name.ToLower().Contains("queen")) &&
                    !currentPiece.name.ToLower().Contains(Convert.ToString(color)))  //se verifica daca in matrice la 
                    //coordonatele x+iterator si y+iterator (diagonal) se gaseste o piesa; in caz afirmativ, daca piesa este un nebun sau o regina
                    //si este de culoare diferita fata de culoarea primita ca parametru
                    return true; //returneaza true
                if (currentPiece != null) //daca nu se returneaza true si se gaseste o piesa diferita de cele doua la acele coordonate
                    break;//se iese din while
            }

            //verifica diagonala din partea stanga jos, aceleasi explicatii ca anterior
            iterator = 0;
            while (inBounds(x + ++iterator, y - iterator))
            {
                currentPiece = Board.board[x + iterator, y - iterator];

                if (currentPiece != null && (currentPiece.name.ToLower().Contains("bishop") || currentPiece.name.ToLower().Contains("queen")) &&
                    !currentPiece.name.ToLower().Contains(Convert.ToString(color))) //daca
                    return true;
                if (currentPiece != null)
                    break;
            }


            //verifica diagonala din partea dreapta sus, aceleasi explicatii ca anterior
            iterator = 0;
            while (inBounds(x - ++iterator, y + iterator))
            {
                currentPiece = Board.board[x - iterator, y + iterator];

                if (currentPiece != null && (currentPiece.name.ToLower().Contains("bishop") || currentPiece.name.ToLower().Contains("queen")) &&
                    !currentPiece.name.ToLower().Contains(Convert.ToString(color))) //daca
                    return true;
                if (currentPiece != null)
                    break;
            }

            //verifica diagonala din partea stanga sus, aceleasi explicatii ca anterior
            iterator = 0;
            while (inBounds(x - ++iterator, y - iterator))
            {
                currentPiece = Board.board[x - iterator, y - iterator];

                if (currentPiece != null && (currentPiece.name.ToLower().Contains("bishop") || currentPiece.name.ToLower().Contains("queen")) &&
                    !currentPiece.name.ToLower().Contains(Convert.ToString(color))) //daca
                    return true;
                if (currentPiece != null)
                    break;
            }
            return false;
        }

        public bool lookForKnight(int x, int y, Piece.Color color)
        {
            Piece currentPiece = null;

            for (int i = 0; i < 8; i++)
            {
                //posibilele mutari ale calului le preluam din matricea de mutari legale din clasa calului
                int row = x + Knight.legalMoves[i, 0]; //linie
                int column = y + Knight.legalMoves[i, 1]; //coloana

                if (inBounds(row, column))//daca linia si coloana nou obtinute se afla inca in tabla
                {
                    currentPiece = Board.board[row, column];//currentPiece devine ceea ce este pe tabla la linia si coloana nou obtinute

                    if (currentPiece != null && currentPiece.name.ToLower().Contains("knight") && //daca exista piesa in matrice la acele coordonate si 
                        !currentPiece.name.ToLower().Contains(Convert.ToString(color))) //daca piesa este cal si nu contine culoarea primita ca parametru (adica este de culoarea
                        //adversarului)
                        return true; //returneaza true
                }
            }
            return false;//altfel returneaza false
        }

        public bool lookForPawn(int x, int y, Piece.Color color)
        {
            string keyForLeftSide = null;
            string keyForRightSide = null;

            //discutii pentru capturarea cu pionii de culoare diferita
            if (Board.beginningTurn == Color.black) //daca incepe negru (daca negru e jos)
            {

                if (color == Color.black)//daca e negru
                {
                    if (inBounds(x - 1, y - 1))//daca o pozitie mai sus si in stanga nu a iesit din tabla
                    {
                        keyForLeftSide = Convert.ToString(x - 1) + Convert.ToString(y - 1);
                    }

                    if (inBounds(x - 1, y + 1))//daca o pozitie mai sus si in dreapta nu a iesit din tabla
                    {
                        keyForRightSide = Convert.ToString(x - 1) + Convert.ToString(y + 1);
                    }

                    if (keyForLeftSide != null)
                    {
                        if (atomicChess.panelDict[keyForLeftSide].imageName.ToLower().Contains("pawn") &&
                              !atomicChess.panelDict[keyForLeftSide].imageName.ToLower().Contains(Convert.ToString(color))) //daca pe panelul din pozitia din dictionar e un pion de culoare diferita
                            return true;
                    }

                    if (keyForRightSide != null)
                    {
                        if (atomicChess.panelDict[keyForRightSide].imageName.ToLower().Contains("pawn") &&
                              !atomicChess.panelDict[keyForRightSide].imageName.ToLower().Contains(Convert.ToString(color))) //daca pe panelul din pozitia din dictionar e un pion de culoare diferita
                            return true;
                    }
                }
            }

            if (color == Color.white)//daca e alb
            {
                if (inBounds(x + 1, y - 1))//daca o pozitie mai jos si in stanga nu a iesit din tabla
                {
                    keyForLeftSide = Convert.ToString(x + 1) + Convert.ToString(y - 1);
                }

                if (inBounds(x + 1, y + 1))//daca o pozitie mai jos si in dreapta nu a iesit din tabla
                {
                    keyForRightSide = Convert.ToString(x + 1) + Convert.ToString(y + 1);
                }

                if (keyForLeftSide != null)
                {
                    if (atomicChess.panelDict[keyForLeftSide].imageName.ToLower().Contains("pawn") &&
                          !atomicChess.panelDict[keyForLeftSide].imageName.ToLower().Contains(Convert.ToString(color))) //daca pe panelul din pozitia din dictionar e un pion de culoare diferita
                        return true;
                }

                if (keyForRightSide != null)
                {
                    if (atomicChess.panelDict[keyForRightSide].imageName.ToLower().Contains("pawn") &&
                          !atomicChess.panelDict[keyForRightSide].imageName.ToLower().Contains(Convert.ToString(color))) //daca pe panelul din pozitia din dictionar e un pion de culoare diferita
                        return true;
                }
            }

            //------------------------------------------------------------------------------------

            if (Board.beginningTurn == Color.white) //daca incepe alb (daca alb e jos)
            {
                if (color == Color.white)
                {
                    if (inBounds(x - 1, y - 1))//daca o pozitie mai sus si in stanga nu a iesit din tabla
                    {
                        keyForLeftSide = Convert.ToString(x - 1) + Convert.ToString(y - 1);
                    }

                    if (inBounds(x - 1, y + 1))//daca o pozitie mai sus si in dreapta nu a iesit din tabla
                    {
                        keyForRightSide = Convert.ToString(x - 1) + Convert.ToString(y + 1);
                    }

                    if (keyForLeftSide != null)
                    {
                        if (atomicChess.panelDict[keyForLeftSide].imageName.ToLower().Contains("pawn") &&
                              !atomicChess.panelDict[keyForLeftSide].imageName.ToLower().Contains(Convert.ToString(color))) //daca pe panelul din pozitia din dictionar e un pion de culoare diferita
                            return true;
                    }

                    if (keyForRightSide != null)
                    {
                        if (atomicChess.panelDict[keyForRightSide].imageName.ToLower().Contains("pawn") &&
                              !atomicChess.panelDict[keyForRightSide].imageName.ToLower().Contains(Convert.ToString(color))) //daca pe panelul din pozitia din dictionar e un pion de culoare diferita
                            return true;
                    }
                }

                if (color == Color.black)//daca e negru
                {
                    if (inBounds(x + 1, y - 1))//daca o pozitie mai jos si in stanga nu a iesit din tabla
                    {
                        keyForLeftSide = Convert.ToString(x + 1) + Convert.ToString(y - 1);
                    }

                    if (inBounds(x + 1, y + 1))//daca o pozitie mai jos si in dreapta nu a iesit din tabla
                    {
                        keyForRightSide = Convert.ToString(x + 1) + Convert.ToString(y + 1);
                    }

                    if (keyForLeftSide != null)
                    {
                        if (atomicChess.panelDict[keyForLeftSide].imageName.ToLower().Contains("pawn") &&
                              !atomicChess.panelDict[keyForLeftSide].imageName.ToLower().Contains(Convert.ToString(color))) //daca pe panelul din pozitia din dictionar e un pion de culoare diferita
                            return true;
                    }

                    if (keyForRightSide != null)
                    {
                        if (atomicChess.panelDict[keyForRightSide].imageName.ToLower().Contains("pawn") &&
                              !atomicChess.panelDict[keyForRightSide].imageName.ToLower().Contains(Convert.ToString(color))) //daca pe panelul din pozitia din dictionar e un pion de culoare diferita
                            return true;
                    }
                }
            }
            return false;
        }
    }
}