using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
namespace Progra1
{
    class Program
    {
        //The big cant of spaces in the array
        const int INFINITE = 99999999;
        //this is to track the parents of every node
        static List<int> parentTracker = new List<int>();
        //This is the list to put the levels
        static List<int> level = new List<int>();
        //Variable to count assignations of FordFulkerson
        static int asignFF = 0;
        //Variable to count the comparison of FordFulkerson
        static int compFF = 0;

        //Variable to count assignations of Dinic's
        static int asignD = 0;
        //Variable to count the comparison of Dinic's
        static int compD = 0;

        //Timers
        static double TimeFF = 0;
        static double TimeD = 0;

        static Stopwatch tiempoFF;
        static Stopwatch tiempoD;
        //Start Graphs creation
        //This methods receives the matrix size
        static int[,] minimalConnection(int size)
        {
            int[,] graph = new int[size, size];
            Random rnd = new Random();
            for (int i = 0; i < graph.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < graph.GetLength(1); j++)
                {
                    if (j == i + 1)
                    {
                        graph[i, j] = rnd.Next(1, 15);
                    }
                }
            }

           /* int rowLength = graph.GetLength(0);
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
            for (int i = 0; i < graph.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < graph.GetLength(1); j++)
                {

                    graph[i, j] = rnd.Next(1, 15);

                }
            }

            int rowLength = graph.GetLength(0);
            int colLength = graph.GetLength(1);

            /*for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", graph[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }*/
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
            for (int i = 0; i < graph.GetLength(0) - 1; i++)
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
           /* int rowLength = graph.GetLength(0);
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


        //End of graph's creation

        //Start Forf Fulkerson Algorthm
        /*This method receives a copy of the graph, the source, the sink, a the global list to track the 
         parents and the amount of vertex*/
        static bool BreadthFirstSearch(int[,] residualGraph, int source, int sink, List<int> parentTracker, int arraySize)
        {
            Queue<int> queue = new Queue<int>();
            List<bool> visited = new List<bool>();
            asignFF += 2;
            for (int i = 0; i < arraySize; i++)
            {
                //this for becomes the list of visited in false
                visited.Add(false);
                compFF++;
                asignFF += 2;
            }
            compFF++;
            queue.Enqueue(source);
            visited[source] = true;
            parentTracker[source] = -1;
            asignFF += 4;
            while (queue.Count != 0)
            {
                compFF++;
                int u = queue.Dequeue();
                asignFF++;
                for (int v = 0; v < arraySize; v++)
                {
                    compFF++;
                    asignFF++;

                    if (visited[v] == false & residualGraph[u, v] > 0)
                    {
                        queue.Enqueue(v);
                        visited[v] = true;
                        parentTracker[v] = u;
                        compFF += 2;
                        asignFF += 3;
                    }
                    else
                    {
                        compFF += 2;
                    }
                }
                compFF++;
                asignFF++;
            }
            compFF++;
            if (visited[sink])
            {
                compFF++;
                return true;
            }
            compFF++;
            return false;
        }

        //This method receives the original graph, the source, the sink, and the amount of vertex in the graph
        static int FordFulkersonAlgorithm(int[,] Graph, int source, int sink, int arraySize)
        {
            //TimeSpan stop;
            //TimeSpan start = new TimeSpan(DateTime.Now.Ticks);
            tiempoFF = Stopwatch.StartNew();
            int u;
            int v;
            int[,] residualGraph = Graph;
            int maxFlow = 0;
            asignFF += 2;

            while (BreadthFirstSearch(residualGraph, source, sink, parentTracker, arraySize))
            {
                compFF++;
                int pathFlow = INFINITE;
                v = sink;
                asignFF += 2;
                while (v != source)
                {
                    u = parentTracker[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                    v = parentTracker[v];
                    compFF++;
                    asignFF += 3;

                }
                compFF++;
                v = sink;
                asignFF++;

                while (v != source)
                {
                    u = parentTracker[v];
                    residualGraph[u, v] -= pathFlow;
                    residualGraph[v, u] += pathFlow;
                    v = parentTracker[v];
                    compFF++;
                    asignFF += 4;
                }
                maxFlow += pathFlow;
                compFF++;
                asignFF++;
            }
            compFF++;
            //stop = new TimeSpan(DateTime.Now.Ticks);
            //TimeFF = (stop.Subtract(start).TotalMilliseconds);
            tiempoFF.Stop();
            return maxFlow;
        }
        //End of Ford Fulkerson

        //Start of Dinics
        static bool Bfs(int[,] graph, int[,] matrix, int start, int final)
        {
            int size = graph.GetLength(0) - 1;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            asignD += 2;
            for (int i = 0; i < size + 1; i++)
            {
                level.Add(0);
                asignD += 2;
                compD++;
            }
            level[start] = 1;
            asignD += 2;
            compD ++;
            while (queue.Count() != 0)
            {
                int k = queue.Dequeue();
                asignD++;
                compD++;
                for (int i = 0; i <= size; i++)
                {
                    if ((matrix[k, i] < graph[k, i]) & (level[i] == 0))
                    {
                        level[i] = level[k] + 1;
                        queue.Enqueue(i);
                        asignD += 2;
                        compD += 2;

                    }
                    else
                    {
                        compD += 2;
                    }

                }
                asignD ++;
                compD  ++;
            }
            compD++;

            return level[final] > 0;
        }

        static int Dfs(int[,] graph, int[,] matrix, int k, int cp)
        {
            //Stopwatch sw = Stopwatch.StartNew();
            //sw.Stop();

            int temp = cp;
            asignD++;
            if (k == graph.GetLength(0) - 1)
            {
                compD++;
                return cp;
            }
            else
              compD++;
            for (int i = 0; i <= graph.GetLength(0) - 1; i++)
            {
                compD++;
                asignD++;
                if ((level[i] == level[k] + 1) & (matrix[k, i] < graph[k, i]))
                {
                    int f = Dfs(graph, matrix, i, Math.Min(temp, graph[k, i] - matrix[k, i]));
                    matrix[k, i] = matrix[k, i] + f;
                    matrix[i, k] = matrix[i, k] - f;
                    temp = temp - f;
                    asignD += 4;
                    compD += 2;
                }
                else
                {
                    compD += 2;
                }
            }
            asignD++;
            compD ++;
            return cp - temp;

        }
        static int maxFlow(int[,] graph, int start, int final)
        {
            //TimeSpan stopped;
            //TimeSpan starter = new TimeSpan(DateTime.Now.Ticks);
            tiempoD = Stopwatch.StartNew();
            int n = graph.GetLength(0);
            int[,] matrix = new int[n, n];
            asignD++;
            for (int i = 0; i >= n; i++)
            {
                asignD++;
                compD++;
                for (int j = 0; j >= n; j++)
                {
                    matrix[i, j] = 0;
                    asignD += 2;
                    compD++;
                }
                asignD++;
                compD++;
            }
            compD++;
            int flow = 0;
            asignD += 2;
            while (Bfs(graph, matrix, start, final))
            {
                flow = flow + Dfs(graph, matrix, start, 100000);
                level.Clear();
                asignD++;
            }
            compD++;
            //stopped = new TimeSpan(DateTime.Now.Ticks);
            //TimeD = (stopped.Subtract(starter).TotalMilliseconds);
            tiempoD.Stop();
            return flow;
        }

        static int[,] copiarGrafo(int[,] grafol)
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
            int [] values = new int[] { 10, 50, 100, 500, 1000 };
            for (int i = 0; i < values.Length; i++)
            {
                //Minimal connection
                int[,] graphMin = minimalConnection(values[i]);
                int[,] graphCopy = copiarGrafo(graphMin);
                int arraySize = graphMin.GetLength(0);
                for (int j = 0; j < arraySize; j++)
                {
                    parentTracker.Add(0);
                }
                int result = FordFulkersonAlgorithm(graphMin, 0, arraySize - 1, arraySize);
                int max_flow_value = maxFlow(graphCopy, 0, arraySize - 1);
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("~~~~ Conexión mínima ~~~~ ");
                Console.WriteLine("Tamaño del  grafo " + values[i]);
                Console.WriteLine("Ford Fulkerson Algorithm");
                Console.WriteLine("The maximun possible flow is " + result);
                Console.WriteLine("Cantidad de asignaciones " + asignFF);
                Console.WriteLine("Cantidad de comparaciones " + compFF);
                Console.WriteLine("Cantidad de pasos " + asignFF + compFF);
                Console.WriteLine("Tiempo  " + tiempoFF.Elapsed.TotalSeconds + " milisegundos");
                Console.WriteLine("*****************************************************");
                Console.WriteLine();
                Console.WriteLine("Dinic's Algorithm");
                Console.WriteLine("Tamaño del  grafo " + values[i]);
                Console.WriteLine("max_flow_value is " + max_flow_value);
                Console.WriteLine("Cantidad de asignaciones " + asignD);
                Console.WriteLine("Cantidad de comparaciones " + compD);
                Console.WriteLine("Cantidad de pasos " + asignD + compD);
                Console.WriteLine("Tiempo  " + tiempoD.Elapsed.TotalMilliseconds + " milisegundos");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                asignD = 0;
                asignFF = 0;
                compD = 0;
                compFF = 0;
                TimeD = 0;
                TimeFF = 0;
                tiempoFF.Reset();
                tiempoD.Reset();
                level.Clear();
                parentTracker.Clear();
                //Three conections
                int[,] graphThree = threeConnections(values[i]);
                int[,] graphCopyThree = copiarGrafo(graphThree);
                arraySize = graphThree.GetLength(0);
                for (int j = 0; j < arraySize; j++)
                {
                    parentTracker.Add(0);
                }
                result = FordFulkersonAlgorithm(graphThree, 0, arraySize - 1, arraySize);
                max_flow_value = maxFlow(graphCopyThree, 0, arraySize - 1);
                Console.WriteLine("~~~~ Conexión de 3 vértices ~~~~");
                Console.WriteLine("Tamaño del  grafo " + values[i]);
                Console.WriteLine("Ford Fulkerson Algorithm");
                Console.WriteLine("The maximun possible flow is " + result);
                Console.WriteLine("Cantidad de asignaciones " + asignFF);
                Console.WriteLine("Cantidad de comparaciones " + compFF);
                Console.WriteLine("Cantidad de pasos " + asignFF + compFF);
                Console.WriteLine("Tiempo  " + tiempoFF.Elapsed.TotalSeconds + " milisegundos");
                Console.WriteLine("*****************************************************");
                Console.WriteLine();
                Console.WriteLine("Dinic's Algorithm");
                Console.WriteLine("Tamaño del  grafo " + values[i]);
                Console.WriteLine("max_flow_value is " + max_flow_value);
                Console.WriteLine("Cantidad de asignaciones " + asignD);
                Console.WriteLine("Cantidad de comparaciones " + compD);
                Console.WriteLine("Cantidad de pasos " + asignD + compD);
                Console.WriteLine("Tiempo  " + tiempoD.Elapsed.TotalMilliseconds + " milisegundos");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                asignD = 0;
                asignFF = 0;
                compD = 0;
                compFF = 0;
                TimeD = 0;
                TimeFF = 0;
                tiempoFF.Reset();
                tiempoD.Reset();
                level.Clear();
                parentTracker.Clear();

                //Strong conections
                int[,] graphStrong = stronglyConnected(values[i]);
                int[,] graphCopyStrong = copiarGrafo(graphStrong);
                arraySize = graphThree.GetLength(0);
                for (int j = 0; j < arraySize; j++)
                {
                    parentTracker.Add(0);
                }
                result = FordFulkersonAlgorithm(graphStrong, 0, arraySize - 1, arraySize);
                max_flow_value = maxFlow(graphCopyStrong, 0, arraySize - 1);
                Console.WriteLine("~~~~ Fuertemente conexo ~~~~");
                Console.WriteLine("Tamaño del  grafo " + values[i]);
                Console.WriteLine("Ford Fulkerson Algorithm");
                Console.WriteLine("The maximun possible flow is " + result);
                Console.WriteLine("Cantidad de asignaciones " + asignFF);
                Console.WriteLine("Cantidad de comparaciones " + compFF);
                Console.WriteLine("Cantidad de pasos " + asignFF + compFF);
                Console.WriteLine("Tiempo  " + tiempoFF.Elapsed.TotalSeconds + " milisegundos");
                Console.WriteLine("*****************************************************");
                Console.WriteLine();
                Console.WriteLine("Dinic's Algorithm");
                Console.WriteLine("Tamaño del  grafo " + values[i]);
                Console.WriteLine("max_flow_value is " + max_flow_value);
                Console.WriteLine("Cantidad de asignaciones " + asignD);
                Console.WriteLine("Cantidad de comparaciones " + compD);
                Console.WriteLine("Cantidad de pasos " + asignD + compD);
                Console.WriteLine("Tiempo  " + tiempoD.Elapsed.TotalMilliseconds + " milisegundos");

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------");
                asignD = 0;
                asignFF = 0;
                compD = 0;
                compFF = 0;
                TimeD = 0;
                TimeFF = 0;
                tiempoFF.Reset();
                tiempoD.Reset();
                level.Clear();
                parentTracker.Clear();
            }
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
