using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atomic_chess
{
    class King : Piece, IPiece
    {
        private Piece currentRook = null;
        private bool isInCheck;
        public static bool castlingPerformed;

        public static int[,] legalMoves = new int[,] {
                                            { -1, 0 }, //sus
                                            { -1, -1 }, //stanga sus
                                            { -1, 1 }, //dreapta sus
                                            { 1, 0 }, //jos
                                            { 1, -1 }, //stanga jos
                                            { 1, 1 }, //dreapta jos
                                            { 0, -1 }, //stanga
                                            { 0, 1 } //dreapta
                                            }; //vom folosi matricea atat ca mutari legale ale regelui (luate doar o singura data), cat si ca mutari pentru
                                               //a verifica daca este in sah
        public King(int x, int y, Piece.Color color, string name) : base(x, y, color, name)
        {
            hasMoved = false;
            isInCheck = false;
            points = 1000;
        }

        public override bool canMove(int newX, int newY, Piece.Color color)
        {
            string key = Convert.ToInt32(newX) + Convert.ToString(newY);

            if (atomicChess.panelDict[key].imageName.Contains("Rook") && atomicChess.panelDict[key].imageName.Contains(Convert.ToString(color))
                && !this.hasMoved && !Board.board[newX, newY].hasMoved)//daca se da click pe una dintre turele de aceeasi culoare si nu s-a mutat regele si nici tura
            {
                currentRook = Board.board[newX, newY]; //se preia tura actuala

                if (Board.beginningTurn == Color.black)
                {
                    if ((newY == 7 && (atomicChess.panelDict["04"].isFree && atomicChess.panelDict["05"].isFree && atomicChess.panelDict["06"].isFree) ||
                        (newY == 0 && (atomicChess.panelDict["01"].isFree && atomicChess.panelDict["02"].isFree)))) //sus
                    {
                        performCastling(newX, newY);
                        //MessageBox.Show("ROCADA");
                        return true;
                    }

                    if ((newY == 7 && (atomicChess.panelDict["74"].isFree && atomicChess.panelDict["75"].isFree && atomicChess.panelDict["76"].isFree) ||
                        (newY == 0 && (atomicChess.panelDict["71"].isFree && atomicChess.panelDict["72"].isFree)))) //jos
                    {
                        performCastling(newX, newY);
                        //MessageBox.Show("ROCADA");
                        return true;
                    }
                }
                else
                {
                    if ((newY == 0 && (atomicChess.panelDict["01"].isFree && atomicChess.panelDict["02"].isFree && atomicChess.panelDict["03"].isFree) ||
                       (newY == 7 && (atomicChess.panelDict["05"].isFree && atomicChess.panelDict["06"].isFree)))) //sus
                    {
                        performCastling(newX, newY);
                        //MessageBox.Show("ROCADA");
                        return true;
                    }

                    if ((newY == 0 && (atomicChess.panelDict["71"].isFree && atomicChess.panelDict["72"].isFree && atomicChess.panelDict["73"].isFree) ||
                        (newY == 7 && (atomicChess.panelDict["75"].isFree && atomicChess.panelDict["76"].isFree)))) //jos
                    {
                        performCastling(newX, newY);
                        //MessageBox.Show("ROCADA");
                        return true;
                    }

                }
            }
            else
            {
                //--------------------------------------------------------------------------------------------------------
                //daca nu este rocada se continua cu verificarile
                if (color == Piece.Color.white) //culoarea actuala este alb atunci
                {
                    if (atomicChess.panelDict[key].isFree)//daca unde am dat click este liber (nu se verifica daca este o piesa adversara intrucat
                                                          //daca regele captureaza o piesa adversara vor exploda amandoua si jocul se va incheia)
                    {
                        for (int i = 0; i < 8; i++)//se verifica daca este o pozitie legala cu ajutorul matricei de pozitii
                        {
                            if (newX == this.x + legalMoves[i, 0] && newY == this.y + legalMoves[i, 1])//in caz afirmativ se modifica x, y ale regelui
                                                                                                       //se seteaza flagul de hasMoved si se returneaza true
                            {
                                this.hasMoved = true;
                                return true;
                            }
                        }
                    }
                }

                //analog ca mai sus
                if (color == Piece.Color.black)
                {
                    if (atomicChess.panelDict[key].isFree/* || atomicChess.panelDict[key].imageName.Contains("white")*/) //nu se poate captura o piesa adversara pentru ca astfel regele va exploda
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (newX == this.x + legalMoves[i, 0] && newY == this.y + legalMoves[i, 1])
                            {
                                this.hasMoved = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void performCastling(int newX, int newY)
        {
            //newX = x al turei 
            //newY = y al turei

            castlingPerformed = true;

            if (Board.beginningTurn == Piece.Color.black)//daca incepe negru
            {
                if (newY == 0) //daca rocada se face pe stanga
                {
                    //pentru tura
                    moveOnBoard(newX, newY, newX, 2);//se poate face sus/jos in stanga
                    currentRook.y = 2;

                    //pentru rege
                    moveOnBoard(this.x, this.y, this.x, 1);
                    this.y = 1;//x al regelui ramane 0 sau 7, se modifica doar y

                }

                if (newY == 7) //daca rocada se face pe dreapta
                {
                    //pentru tura
                    moveOnBoard(newX, newY, newX, 4);//se poate face sus/jos in dreapta
                    currentRook.y = 4;

                    //pentru rege
                    moveOnBoard(this.x, this.y, this.x, 5);
                    this.y = 5;//x al regelui ramane 0 sau 7, se modifica doar y
                }

            }
            else //daca incepe alb
            {
                if (newY == 0) //daca rocada se face pe stanga
                {
                    //pentru tura
                    moveOnBoard(newX, newY, newX, 3);//se poate face sus/jos in stanga
                    currentRook.y = 3;

                    //pentru rege
                    moveOnBoard(this.x, this.y, this.x, 2);
                    this.y = 2;//x al regelui ramane 0 sau 7, se modifica doar y
                }

                if (newY == 7) //daca rocada se face pe dreapta
                {
                    //pentru tura
                    moveOnBoard(newX, newY, newX, 5);//se poate face sus/jos in stanga
                    currentRook.y = 5;

                    //pentru rege
                    moveOnBoard(this.x, this.y, this.x, 6);
                    this.y = 6;//x al regelui ramane 0 sau 7, se modifica doar y
                }
            }

        }

        private void moveOnBoard(int xOld, int yOld, int xNew, int yNew)
        {
            string lastCoordinates = Convert.ToString(xOld) + Convert.ToString(yOld);
            string currentCoordinates = Convert.ToString(xNew) + Convert.ToString(yNew);

            PiecePanel lastPanel = (PiecePanel)atomicChess.panelDict[lastCoordinates];//numarul panelului anterior se ia din dictionarul de la coordonatele anterioare
            PiecePanel currentPanel = (PiecePanel)atomicChess.panelDict[currentCoordinates];//numarul panelului curent se ia din dictionarul de la coordonatele curente

            System.Drawing.Image currentPieceImage = ((PiecePanel)atomicChess.panelDict[lastCoordinates]).BackgroundImage; //poza piesei curente se preia

            atomicChess.panelDict[lastCoordinates].isFree = true;//proprietatea isFree din panelul coordonatelor anterioare devine true
            atomicChess.panelDict[currentCoordinates].isFree = false;//proprietatea isFree din panelul coordonatelor anterioare devine false

            lastPanel.BackgroundImage = null;//se reseteaza imaginea 
            currentPanel.BackgroundImage = currentPieceImage;//culoarea panelului devine piesa curenta

            lastPanel.movePiece = false;//se reseteaza capacitatea piesei de a se muta

            Board.board[currentCoordinates[0] - 48, currentCoordinates[1] - 48] = Board.board[lastCoordinates[0] - 48, lastCoordinates[1] - 48];
            Board.board[lastCoordinates[0] - 48, lastCoordinates[1] - 48] = null;

            currentPanel.imageName = lastPanel.imageName;
            lastPanel.imageName = "";
        }

        public void setColorInCheck(Piece.Color color)
        {
            if (this.checkColorInCheck(this.color))
            {
                this.isInCheck = true;
                if (this.color == Piece.Color.black)
                    Board.isBlackInCheck = true;
                else
                    Board.isWhiteInCheck = true;
            }
            else
            {
                if (this.color == Piece.Color.black)
                    Board.isBlackInCheck = false;
                else
                    Board.isWhiteInCheck = false;
            }
        }

        private bool checkColorInCheck(Piece.Color color)
        {
            if (canBeCaptured(this.x, this.y, this.color))//daca regele poate fi capturat
                return true;//se returneaza true

            //sau daca orice alta piesa din jurul lui la distanta de 1 poate fi capturata, indiferent daca este culoarea lui sau a adversarului
            for (int i = 0; i < 8; i++)
            {
                if (canBeCaptured(this.x + legalMoves[i, 0], this.y + legalMoves[i, 1], this.color))
                    return true; //se returneaza true
            }

            return false;//daca nu s-a returnat true pana acum, se returneaza false
        }
    }
}

