using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progra1
{
    class Program
    {
        //The big cant of spaces in the array
        const int INFINITE = 99999999;
        //this is to track the parents of every node
        static List<int> parentTracker = new List<int>();
        static int asignFF = 0;
        static int compFF = 0;

        //Start Graphs creation
        static int[,] minimalConnection(int size)
        {
            int[,] graph = new int[size, size];
            
            Random rnd = new Random();
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (j == i+1)
                    {
                        
                        graph[i, j] = rnd.Next(1,15);
                    }
                }
            }

            /*int rowLength = graph.GetLength(0);
            int colLength = graph.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", graph[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }*/
            return graph;
        }
        static int[,] stronglyConnected(int size)
        {
            int[,] graph = new int[size, size];
            Random rnd = new Random();
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    
                   graph[i, j] = rnd.Next(1, 15);
                    
                }
            }

            /*int rowLength = graph.GetLength(0);
            int colLength = graph.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", graph[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }*/
            return graph;
        }

        //End of grapg's creation

        //Start Forf Fulkerson Algorthm
        static bool BreadthFirstSearch(int [,] residualGraph,int source, int sink, List<int>parentTracker, int arraySize)
        {
            Queue<int> queue = new Queue<int>();
            List<bool> visited = new List<bool>();
            for (int i = 0; i < arraySize; i++)
            {
                visited.Add(false);
                compFF++;
                asignFF++;
            }
            asignFF++;
            compFF++;
            queue.Enqueue(source);
            visited[source] = true;
            parentTracker[source] = -1;
            asignFF += 2;
            while (queue.Count != 0){
                compFF++;
                int u = queue.Dequeue();
                asignFF++;
                for (int v = 0; v < arraySize ; v++)
                {
                    compFF++;
                    asignFF++;
                    if (visited[v] == false & residualGraph[u,v] > 0)
                    {
                        queue.Enqueue(v);
                        visited[v] = true;
                        parentTracker[v] = u;
                        compFF += 2;
                        asignFF += 2;
                    }
                    else
                    {
                       compFF += 2;
                    }
                }

            }
            if (visited[sink])
            {
                compFF++;
                return true;
            }

            return false;
        }

        static int FordFulkersonAlgorithm(int[,] Graph, int source, int sink, int arraySize)
        {
            int u;
            int v;
            int[,] residualGraph = Graph;
            int maxFlow = 0;

            while(BreadthFirstSearch(residualGraph, source,sink, parentTracker, arraySize))
            {
                int pathFlow = INFINITE;
                v = sink;

                while(v != source)
                {
                    u = parentTracker[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                    v = parentTracker[v];
                }
                v = sink;
                while (v != source)
                {
                    u = parentTracker[v];
                    residualGraph[u, v] -= pathFlow;
                    residualGraph[v, u] += pathFlow;
                    v = parentTracker[v];
                }
                maxFlow += pathFlow;
            }


            return maxFlow;
        }
        //End of Ford Fulkerson


        static void Main(string[] args)
        {
            //int[,] graph = new int[,] { {0,10,10,0,0,0},{0,0,2,4,8,0},{0,0,0,0,9,0},{0,0,0,0,0,10},
            //                           {0,0,0,6,0,10},{0,0,0,0,0,0} };

            /*int[,] graph = new int[,] { {0,9,9,0,0,0},{0,0,10,8,0,0},{0,0,0,1,3,0},{0,0,0,0,0,10},
                                        {0,0,0,8,0,7},{0,0,0,0,0,0} };*/
            int[,] graph = stronglyConnected(10);
            int arraySize = graph.GetLength(0);

            for (int i = 0; i < arraySize; i++)
            {
                parentTracker.Add(0);
            }
            int result = FordFulkersonAlgorithm(graph, 0, arraySize - 1, arraySize);
            
            Console.WriteLine("The maximun possible flow is "+ result);
            Console.ReadKey();
        }//-hola mundo
    }
}
