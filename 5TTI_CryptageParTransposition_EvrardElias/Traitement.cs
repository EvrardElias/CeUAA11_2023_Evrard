using System;
using System.Collections.Generic;
using System.Text;

namespace _5TTI_CryptageParTransposition_EvrardElias
{
    public struct Traitement
    {
        public void RetireEspaces(string chaine, out string chaineSansEspaces)
        {
            int longueurChaine = chaine.Length;
            chaineSansEspaces = "";
            for (int compteur = 0; compteur <= longueurChaine - 1; compteur++)
            {
                if (chaine[compteur] != ' ')
                {
                    chaineSansEspaces = chaineSansEspaces + chaine[compteur];
                }
            }
        }

        public void DimensionMatrice(string cle, string texte, out char[,] matrice)
        {
            int dimension1 = (texte.Length/cle.Length) + 2;
            int dimension2 = cle.Length;
            if (texte.Length % cle.Length != 0)
            {
                dimension1 = dimension1 + 1;
            }
            matrice = new char[dimension1, dimension2];
        }

        public void EcritChainesDansMatrice(string cle, string texte, ref char[,] matrice)
        {
            for (int compteurColonne = 0; compteurColonne <= matrice.GetLength(1) - 1; compteurColonne++)
            {
                matrice[0, compteurColonne] = cle[compteurColonne];
            }
            int placeTexte = 0;
            for (int compteurLigne = 2; compteurLigne < matrice.GetLength(0) - 1; compteurLigne++)
            {
                int compteurColonne = 0;
                while (compteurColonne < matrice.GetLength(1) && placeTexte < texte.Length)
                {
                    matrice[compteurLigne, compteurColonne] = texte[placeTexte];
                    placeTexte = placeTexte + 1;
                    compteurLigne = compteurLigne + 1;
                }
            }
        }

        public void TriLigne1(ref char[,] matrice)
        {
            bool permut;
            do
            {
                char plusGrand;
                permut = false;
                for (int compteurColonne = 0; compteurColonne <= matrice.GetLength(1) - 1; compteurColonne++)
                {
                    if (matrice[0,compteurColonne] > matrice[0,compteurColonne + 1])
                    {
                        plusGrand = matrice[0, compteurColonne];
                        matrice[0,compteurColonne] = matrice[0,compteurColonne + 1];
                        matrice[0,compteurColonne + 1] = plusGrand;
                        permut = true;
                    }
                }
            } while (permut == false);
        }

        public void ClasseCle(string cle, out char[,] matriceTrie)
        {
            matriceTrie = new char[3, cle.Length];
            for (int compteur = 0; compteur < matriceTrie.GetLength(1) - 1; compteur++)
            {
                matriceTrie[0, compteur] = cle[compteur];
                matriceTrie[2, compteur] = Convert.ToChar(0);
            }
            TriLigne1(ref matriceTrie);
            for (int j = 1; j <= matriceTrie.GetLength(1); j++)
            {
                matriceTrie[1, j - 1] = Char.Parse(j.ToString());
            }

        }

        public void AttribueRang(ref char[,] matrice, ref char[,] matriceTrie)
        {
            for (int i = 0; i <= matrice.GetLength(1) - 1; i++)
            {
                bool trouve = false;
                int j = 0;
                while (trouve = false && j < matriceTrie.GetLength(1))
                {
                    if (matrice[0, i] == matriceTrie[0, j] && matriceTrie[2, j] != '1')
                    {
                        matrice[1, j] = matriceTrie[1, j];
                        matriceTrie[2, j] = '1';
                        trouve = true;
                    }
                    j = j + 1;
                }
            }
        }

        public void RealiseCrypt(char[,] matrice, out string chaineCrypt)
        {
            chaineCrypt = "";
            int i = 1;
            while (i <= matrice.GetLength(1))
            {
                bool trouve = false;
                int j = 0;
                while (!trouve && j < matrice.GetLength(1))
                {
                    if (i == matrice[1,j])
                    {
                        for (int k = 2; k <= matrice.GetLength(0) - 1 ; k++)
                        {
                            chaineCrypt += matrice[k, j];
                        }
                        chaineCrypt += " ";
                        trouve = true;
                        i = i + 1;
                    }
                    j = j + 1;
                }
            }
        }
    }
}
