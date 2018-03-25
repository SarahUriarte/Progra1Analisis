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
        //Variable to count assignations of FordFulkerson
        static int asignFF = 0;
        //Variable to count the comparison of FordFulkerson
        static int compFF = 0;
        //
        static int stepsCountFF = 0;

        static List<int> level = new List<int>();
        static int aD = 0;
        static int cD = 0;

        
        //Start Graphs creation
        //This methods receives the matrix size
        static int[,] minimalConnection(int size)
        {
            int[,] graph = new int[size, size];
            Random rnd = new Random();
            for (int i = 0; i < graph.GetLength(0)-1; i++)
            {
                for (int j = 1; j < graph.GetLength(1); j++)
                {
                    if (j == i+1)
                    {
                        graph[i, j] = rnd.Next(1,15);
                    }
                }
            }

            int rowLength = graph.GetLength(0);
            int colLength = graph.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", graph[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            return graph;
        }
        static int[,] stronglyConnected(int size)
        {
            int[,] graph = new int[size, size];
            Random rnd = new Random();
            for (int i = 0; i < graph.GetLength(0)-1; i++)
            {
                for (int j = 1; j < graph.GetLength(1); j++)
                {
                    
                   graph[i, j] = rnd.Next(1, 15);
                    
                }
            }

            int rowLength = graph.GetLength(0);
            int colLength = graph.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", graph[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            return graph;
        }
        static bool vertexList(List<int> list, int num)
        {
            int cant = list.Where(x => x == num).Count();
            if (cant > 0)
            {
                return false;
            }
           return true;
        }

        static bool repeatedCount(List<int> list, int num)
        {
            int cant = list.Where(x => x == num).Count();
            if (cant > 3)
            {
                return false;
            }
            return true;
        }
        static int vertex(int num, int size)
        {
            Random rnd = new Random();
            while (true)
            {
                int vert = rnd.Next(1, size);
                if (num != vert & vert != 0)
                {
                    return vert;
                }
            }
        }
        static int[,] threeConnections(int size)
        {
            int[,] graph = new int[size, size];
            List<int> personalVertex = new List<int>();
            List<int> generalVertex = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < graph.GetLength(0)-1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    while (true)
                    {
                        int vert = vertex(i, size);
                        if (vertexList(personalVertex, vert))
                        {
                            if (repeatedCount(generalVertex, vert))
                            {
                                personalVertex.Add(vert);
                                generalVertex.Add(vert);
                                graph[i, vert] = rnd.Next(1, 15);
                                break;
                            }
                        }
                    }
                }
                personalVertex.Clear();
            }
            int rowLength = graph.GetLength(0);
            int colLength = graph.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", graph[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            return graph;
        }


            //End of graph's creation

            //Start Forf Fulkerson Algorthm
            /*This method receives a copy of the graph, the source, the sink, a the global list to track the 
             parents and the amount of vertex*/
            static bool BreadthFirstSearch(int [,] residualGraph,int source, int sink, List<int>parentTracker, int arraySize)
        {
            Queue<int> queue = new Queue<int>();
            List<bool> visited = new List<bool>();
            asignFF += 2;
            for (int i = 0; i < arraySize; i++)
            {
                //this for becomes the list of visited in false
                visited.Add(false);
                compFF++;
                asignFF++;
                stepsCountFF++;
            }
            asignFF++;
            compFF++;
            queue.Enqueue(source);
            visited[source] = true;
            parentTracker[source] = -1;
            asignFF += 2;
            stepsCountFF += 4;
            while (queue.Count != 0){
                compFF++;
                int u = queue.Dequeue();
                asignFF++;
                for (int v = 0; v < arraySize ; v++)
                {
                    compFF++;
                    asignFF++;
                    stepsCountFF++;
                    if (visited[v] == false & residualGraph[u,v] > 0)
                    {
                        queue.Enqueue(v);
                        visited[v] = true;
                        parentTracker[v] = u;
                        compFF += 2;
                        asignFF += 2;
                        stepsCountFF += 5;
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
                stepsCountFF += 2;
                return true;
            }

            return false;
        }

        //This method receives the original graph, the source, the sink, and the amount of vertex in the graph
        static int FordFulkersonAlgorithm(int[,] Graph, int source, int sink, int arraySize)
        {
            int u;
            int v;
            int[,] residualGraph = Graph;
            int maxFlow = 0;
            asignFF += 2;
            stepsCountFF += 2;
            while(BreadthFirstSearch(residualGraph, source,sink, parentTracker, arraySize))
            {
                compFF++;
                int pathFlow = INFINITE;
                v = sink;
                asignFF += 2;
                stepsCountFF += 2;
                while (v != source)
                {
                    u = parentTracker[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                    v = parentTracker[v];
                    compFF++;
                    asignFF += 3;
                    stepsCountFF += 4;
                }
                compFF++;
                v = sink;
                asignFF++;
                stepsCountFF += 4;
                while (v != source)
                {
                    u = parentTracker[v];
                    residualGraph[u, v] -= pathFlow;
                    residualGraph[v, u] += pathFlow;
                    v = parentTracker[v];
                    compFF++;
                    asignFF += 4;
                    stepsCountFF += 5;
                }
                maxFlow += pathFlow;
                stepsCountFF++;
                asignFF++;
            }


            return maxFlow;
        }
        //End of Ford Fulkerson

        //Start of Dinics
        static bool Bfs(int[,] graph, int[,] matrix, int start, int final)
        {
            int size = graph.GetLength(0) - 1;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            for (int i = 0; i < size + 1; i++)
            {
                level.Add(0);
            }
            level[start] = 1;
            aD += 4;
            while (queue.Count() != 0)
            {
                int k = queue.Dequeue();
                for (int i = 0; i <= size; i++)
                {
                    if ((matrix[k, i] < graph[k, i]) & (level[i] == 0))
                    {
                        level[i] = level[k] + 1;
                        queue.Enqueue(i);
                    }
                }

            }
            return level[final] > 0;

        }
        static int Dfs(int[,] graph, int[,] matrix, int k, int cp)
        {
            int temp = cp;
            if (k == graph.GetLength(0) - 1)
            {
                return cp;
            }
            for (int i = 0; i <= graph.GetLength(0) - 1; i++)
            {
                if ((level[i] == level[k] + 1) & (matrix[k, i] < graph[k, i]))
                {
                    int f = Dfs(graph, matrix, i, Math.Min(temp, graph[k, i] - matrix[k, i]));
                    matrix[k, i] = matrix[k, i] + f;
                    matrix[i, k] = matrix[i, k] - f;
                    temp = temp - f;
                }

            }
            return cp - temp;

        }
        static int maxFlow(int[,] graph, int start, int final)
        {
            int n = graph.GetLength(0);


            int[,] matrix = new int[n, n];
            for (int i = 0; i >= n; i++)
            {
                for (int j = 0; j >= n; j++)
                {
                    matrix[i, j] = 0;
                }
            }


            int flow = 0;
            while (Bfs(graph, matrix, start, final))
            {
                flow = flow + Dfs(graph, matrix, start, 100000);
                level.Clear();
            }
            return flow;


        }
        static int [,] copiarGrafo(int[,] grafol)
        {
            int tm = grafol.GetLength(0);
            int[,] graph = new int[tm, tm];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {

                    graph[i, j] = grafol[i, j];

                }
            }
            return graph;
        }

        static void Main(string[] args)
        {
            //int[,] graph = new int[,] { {0,10,10,0,0,0},{0,0,2,4,8,0},{0,0,0,0,9,0},{0,0,0,0,0,10},
            //                           {0,0,0,6,0,10},{0,0,0,0,0,0} };

            /*int[,] graph = new int[,] { {0,9,9,0,0,0},{0,0,10,8,0,0},{0,0,0,1,3,0},{0,0,0,0,0,10},
                                        {0,0,0,8,0,7},{0,0,0,0,0,0} };
            int[,] graphi = new int[,] { {0,9,9,0,0,0},{0,0,10,8,0,0},{0,0,0,1,3,0},{0,0,0,0,0,10},
                                        {0,0,0,8,0,7},{0,0,0,0,0,0} };*/
            int[,] graph = stronglyConnected(10);
            int[,] graphCopy = copiarGrafo(graph);


            int arraySize = graph.GetLength(0);

            for (int i = 0; i < arraySize; i++)
            {
                parentTracker.Add(0);
            }
            int result = FordFulkersonAlgorithm(graph, 0, arraySize - 1, arraySize);
            
            Console.WriteLine("The maximun possible flow is "+ result);
            Console.WriteLine("Cantidad de asignaciones " + asignFF);
            Console.WriteLine("Cantidad de comparaciones " + compFF);
            Console.WriteLine("Cantidad de pasos " + stepsCountFF);
            Console.WriteLine("**************************************");
            Console.WriteLine();
            Console.WriteLine("Dinic's Algorithm");
            int start = 0;
            int final = 5;
            int max_flow_value = maxFlow(graphCopy, start, arraySize - 1);
            Console.WriteLine("max_flow_value is " + max_flow_value);
            Console.ReadKey();

            //int cantidad = a.Where(x => x == 1).Count();
            /*Console.WriteLine("Minimal connections");
            minimalConnection(10);
            Console.WriteLine();
            Console.WriteLine("Three connections");
            threeConnections(10);
            Console.WriteLine();
            Console.WriteLine("Strongly connections");
            stronglyConnected(10);
            Console.WriteLine();

            Console.ReadKey();*/
        }
    }
}
