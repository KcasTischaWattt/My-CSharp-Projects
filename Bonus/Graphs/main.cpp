#include <fstream>
#include <iostream>
#include "graph.h"

//Настоятельно рекоммендую

/// Выбор типа хранения матрицы
int chooseType() {
    std::string type;
    do {
        std::cout << "Choose the type:\n";
        std::cout << "1) Adjacency matrix\n";
        std::cout << "2) Adjacency list\n";
        std::cout << "3) List of graph edges\n";
        std::cout << "More information in readme.txt\n";
        std::cin >> type;
        if (std::stoi(type) < 1 || std::stoi(type) > 3)
            std::cout << "Something went wrong. Please try again\n";
    } while (std::stoi(type) < 1 || std::stoi(type) > 3);
    return std::stoi(type);
}

/// Выбор, откуда вводить граф
int enterGraph() {
    std::string way;
    do {
        std::cout << "Choose the way to enter the graph:\n";
        std::cout << "1) From console\n";
        std::cout << "2) From file\n";
        std::cin >> way;
        if (std::stoi(way) < 1 || std::stoi(way) > 2)
            std::cout << "Something went wrong. Please try again\n";
    } while (std::stoi(way) < 1 || std::stoi(way) > 2);
    return std::stoi(way);
}

/// Диалоговые действия для выбора, куда выводить матрицу
int outputGraph() {
    std::string way;
    do {
        std::cout << "Choose the way to print the graph:\n";
        std::cout << "1) To console\n";
        std::cout << "2) To file\n";
        std::cin >> way;
        if (std::stoi(way) < 1 || std::stoi(way) > 2)
            std::cout << "Something went wrong. Please try again\n";
    } while (std::stoi(way) < 1 || std::stoi(way) > 2);
    return std::stoi(way);
}

/// Диалоговые действия для выбора действий с графом
int graphFuncs() {
    std::string choice;
    do {
        std::cout << "\nWhat do you want to do with the graph?\n";
        std::cout << "1) Show degrees of all vertices\n";
        std::cout << "2) Show degree of one vertex\n";
        std::cout << "3) Counting the number of edges/arcs in a graph\n";
        std::cout << "4) Print the graph\n";
        std::cout << "5) Re-enter the graph\n";
        std::cout << "0) Exit\n";
        std::cin >> choice;
        if (std::stoi(choice) < 0 || std::stoi(choice) > 5)
            std::cout << "Something went wrong. Please try again\n";
        if (!std::stoi(choice)) {
            std::cout << "Process termination......";
            std::cout << "Process stopped.";
        }
    } while (std::stoi(choice) < 0 || std::stoi(choice) > 5);
    return std::stoi(choice);
}

/// Вывод степеней всех вершин
void showDegrees(Graph &graph) {
    if (graph.IsDirected()) {
        graph.CountIncomeDegrees();
        graph.CountOutcomeDegrees();
    } else {
        std::cout << "Graph is  not directed.\n";
        graph.CountOutcomeDegrees();
    }
}

/// Вывод степени одной вершины
void showDegree(Graph &graph) {
    std::cout << "Enter the vertex:\n";
    int vertex;
    std::cin >> vertex;
    if (graph.IsDirected()) {
        graph.CountIncomeDegree(vertex);
        graph.CountOutcomeDegree(vertex);
    } else {
        std::cout << "Graph is  not directed.\n";
        graph.CountOutcomeDegree(vertex);
    }
}

/// Вывод количества рёбер графа
void showEdges(Graph &graph) {
    if (graph.IsDirected()) {
        std::cout << "Graph is directed. \nNumber of arcs: ";
    } else {
        std::cout << "Graph is not directed. \nNumber of edges: ";
    }
    graph.CountEdges();
    std::cout << graph.edges_count << "\n";
}

/// Вывод графа на печать
void printTheGraph(Graph &g) {
    int type = chooseType();
    int type_of_output = outputGraph();
    // Программе по сути без разницы, куда выводить граф - для нее это один поток
    std::ofstream output_file("output.txt", std::ofstream::out);
    std::ostream &output = type_of_output == 1 ? std::cout : output_file;
    if (type_of_output == 1)
        std::cout << "Your graph:\n";
    switch (type) {
        case 1:
            g.PrintAdjMatrix(output);
            break;
        case 2:
            g.PrintAdjList(output);
            break;
        case 3:
            g.PrintEdgeList(output);
            break;
        default:
            std::cout << "Something went wrong. Please try again\n";
            break;
    }
    if (type_of_output == 2)
        std::cout << "The graph is printed in output.txt\n";
}

///  Ввод графа в программу
Graph EnterTheGraph() {
    int type = chooseType();
    Graph g;
    g.type_of_enter = enterGraph();
    // Программе по сути без разницы, откуда читать граф - для нее это один поток
    std::ifstream input_file("input.txt", std::ifstream::in);
    std::istream &input = g.type_of_enter == 1 ? std::cin : input_file;
    switch (type) {
        case 1:
            g.AdjMatrixEnter(input);
            break;
        case 2:
            g.AdjListEnter(input);
            break;
        case 3:
            g.EdgeListEnter(input);
            break;
        default:
            std::cout << "Something went wrong. Please try again\n";
            break;
    }
    return g;
}

/// Главный узел, где происходит обработка команд для действий над графом
void graphTransformations(Graph &g) {
    int a = graphFuncs();
    while (a) {
        switch (a) {
            case 1:
                showDegrees(g);
                break;
            case 2:
                showDegree(g);
                break;
            case 3:
                showEdges(g);
                break;
            case 4:
                printTheGraph(g);
                break;
            case 5:
                g = EnterTheGraph();
                break;
            default:
                break;
        }
        a = graphFuncs();
    }
}

/// Сам мейн. Это всё.
int main() {
    Graph g = EnterTheGraph();
    graphTransformations(g);
}
