using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{
    /** CREDIT TO: https://www.geeksforgeeks.org/avl-tree-set-1-insertion/
        Made a couple additions:
        Overloaded insert()
        isInTree()
        duplicateCount
        countNodes()
        print()
        toFile()
    **/

    public class Node
    {
        public int height;
        public int key;
        public Node left, right;

        public Node(int d)
        {
            key = d;
            height = 1;
        }

    }
    public class AVLTree
    {
        Node root;
        public int duplicateCount = 0;
        public int nodeCount = 0;

        // A utility function to get 
        // the height of the tree  
        int height(Node N)
        {
            if (N == null)
                return 0;

            return N.height;
        }

        // A utility function to get 
        // maximum of two integers  
        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        // A utility function to right  
        // rotate subtree rooted with y  
        // See the diagram given above.  
        Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            // Perform rotation  
            x.right = y;
            y.left = T2;

            // Update heights  
            y.height = max(height(y.left),
                        height(y.right)) + 1;
            x.height = max(height(x.left),
                        height(x.right)) + 1;

            // Return new root  
            return x;
        }

        // A utility function to left 
        // rotate subtree rooted with x  
        // See the diagram given above.  
        Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            // Perform rotation  
            y.left = x;
            x.right = T2;

            // Update heights  
            x.height = max(height(x.left),
                        height(x.right)) + 1;
            y.height = max(height(y.left),
                        height(y.right)) + 1;

            // Return new root  
            return y;
        }

        // Get Balance factor of node N  
        int getBalance(Node N)
        {
            if (N == null)
                return 0;

            return height(N.left) - height(N.right);
        }

        public void insert(int key)
        {
            root = insert(root, key);
        }

        Node insert(Node node, int key)
        {

            /* 1. Perform the normal BST insertion */
            if (node == null)
                return (new Node(key));

            if (key < node.key)
                node.left = insert(node.left, key);
            else if (key > node.key)
                node.right = insert(node.right, key);
            else
            {
                // Duplicate keys not allowed  
                duplicateCount++;
                return node;
            }

            /* 2. Update height of this ancestor node */
            node.height = 1 + max(height(node.left),
                                height(node.right));

            /* 3. Get the balance factor of this ancestor  
                node to check whether this node became  
                unbalanced */
            int balance = getBalance(node);

            // If this node becomes unbalanced, then there  
            // are 4 cases Left Left Case  
            if (balance > 1 && key < node.left.key)
                return rightRotate(node);

            // Right Right Case  
            if (balance < -1 && key > node.right.key)
                return leftRotate(node);

            // Left Right Case  
            if (balance > 1 && key > node.left.key)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // Right Left Case  
            if (balance < -1 && key < node.right.key)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            /* return the (unchanged) node pointer */
            return node;
        }

        public void inOrder(Node node)
        {
            if (node != null)
            {
                inOrder(node.left);
                Console.Write(node.key / 10001 + "-" + node.key % 10001 + " ");
                inOrder(node.right);
            }
        }

        public void print()
        {
            inOrder(root);
            Console.WriteLine("\n------------");
        }

        public void countNodes()
        {
            nodeCount = 0;
            countNodes(root);
        }

        private void countNodes(Node node)
        {
            if (node != null)
            {
                countNodes(node.left);
                nodeCount++;
                countNodes(node.right);
            }
        }

        public bool isInTree(int val)
        {
            return isInTree(root, val);
        }

        bool isInTree(Node node, int val)
        {
            if (node == null)
            {
                return false;
            }
            return node.key == val ? true : isInTree(node.left, val) || isInTree(node.right, val);
        }

        public int getMax()
        {
            return getMax(root, -1);
        }

        private int getMax(Node node, int val)
        {
            if (node == null)
                return val;
            return getMax(node.right, node.key / 10001);
        }

        //Build the E and P arrays and output to a file
        public void toFile(int numCourses)
        {
            nodeCount = 0;
            //Any course that was never selected will point to 0 in E
            string[] P = new string[numCourses + 1];
            P[0] = numCourses.ToString();
            for (int i = 1; i < P.Length; i++)
                P[i] = "0";

            string E = "0,";
            int prev = 0;
            int eIndex = 0;
            buildEdges(root, ref E, ref P, ref prev, ref eIndex);
            //Write out the arrays to files
            System.IO.File.WriteAllLines("P.txt", P);
            //Remove the extra last comma
            E = E.Substring(0, E.Length - 1);
            System.IO.File.WriteAllLines("E.txt", E.Split(','));
        }

        private void buildEdges(Node node, ref string E, ref string[] P, ref int prev, ref int eIndex)
        {
            if (node == null)
                return;

            buildEdges(node.left, ref E, ref P, ref prev, ref eIndex);

            int courseOne = node.key / 10001;
            int courseTwo = node.key % 10001;
            nodeCount++;

            E += courseTwo.ToString() + ",";
            eIndex++;
            if (courseOne != prev)
                P[courseOne] = eIndex.ToString();
            prev = courseOne;

            buildEdges(node.right, ref E, ref P, ref prev, ref eIndex);
        }
    }
}
