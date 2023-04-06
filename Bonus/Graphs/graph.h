#include <fstream>
#include <iostream>
#include <map>
#include <set>
#include <sstream>
#include <vector>

/// Класс граф для хранения графа
class Graph {
public:
    /// Вектор для хранения матрицы смежности
    std::vector<std::vector<int>> adjacencyMatrix;
    /// Вектор для  хранения матрицы инцендентности
    std::vector<std::vector<int>> incidenceMatrix;
    /// Мап для хранения списка смежности
    std::map<int, std::set<int>> adjacencyList;
    /// Сет пар для хранения списка рёбер
    std::set<std::pair<int, int>> edgeList;
    /// Количество вершин
    size_t vertex_count = 0;
    /// Количество рёбер
    size_t edges_count = 0;
    /// Тип ввода матрицы(из файла или с консоли)
    int type_of_enter;

    /// Подсчет рёбер(дуг)
    void CountEdges() {
        edges_count = 0;
        for (int i = 0; i < vertex_count; ++i) {
            for (int j = 0; j < vertex_count; ++j) {
                edges_count += adjacencyMatrix[i][j];
            }
        }
        // Если граф не направленный - делим количество дуг на два
        if (!IsDirected())
            edges_count /= 2;
    }

    /// Подсчёт полустепеней захода
    void CountIncomeDegrees() {
        std::cout << "Graph is directed. Income Degrees:\n";
        for (int i = 0; i < adjacencyMatrix.size(); ++i) {
            int income_degree = 0;
            for (auto &j : adjacencyMatrix) {
                income_degree += j[i];
            }
            std::cout << i << " - " << income_degree << "\n";
        }
    }

    /// Подсчет полустепеней исхода
    void CountOutcomeDegrees() {
        std::cout << "Outcome degrees:\n";
        for (const auto &c : adjacencyList) {
            std::cout << c.first << " - " << c.second.size() << "\n";
        }
    }

    /// Подсчёт полустепени захода для одной вершины
    void CountIncomeDegree(int &vertex) {
        std::cout << "Graph is directed. Income Degree: ";
        int income_degree = 0;
        for (auto &j : adjacencyMatrix) {
            income_degree += j[vertex];
        }
        std::cout << income_degree << "\n";
    }

    /// Подсчёт полустепени исхода для одной вершины
    void CountOutcomeDegree(int &vertex) {
        std::cout << "Outcome degree: ";
        std::cout << adjacencyList[vertex].size() << "\n";
    }

    /// Конвертация матрицы смежности в список рёбер
    void ConvertAdjMatrixToEdgeList() {
        for (int i = 0; i < vertex_count; ++i) {
            for (int j = 0; j < vertex_count; ++j) {
                if (adjacencyMatrix[i][j])
                    edgeList.insert(std::pair<int, int>(i, j));
            }
        }
    }

    /// Конвертация списка рёбер в список смежности
    void ConvertEdgeListToAdjList() {
        for (auto &c : edgeList) {
            adjacencyList[c.first].insert(c.second);
        }
    }

    /// Конвертация списка смежности в матрицу смежности
    void ConvertAdjListToAdjMatrix() {
        adjacencyMatrix = std::vector<std::vector<int> >(vertex_count,
                                                         std::vector<int>(vertex_count, 0));
        for (int i = 0; i < vertex_count; i++) {
            for (auto j : adjacencyList[i])
                adjacencyMatrix[i][j] = 1;
        }
    }

    /// Проверка на ориентированность графа
    bool IsDirected() {
        for (int i = 0; i < vertex_count; ++i) {
            for (int j = 0; j < vertex_count; ++j) {
                if (adjacencyMatrix[i][j] != adjacencyMatrix[j][i])
                    return true;
            }
        }
        return false;
    }

    /// Ввод матрицы смежности
    void AdjMatrixEnter(std::istream &way) {
        if (type_of_enter == 1) {
            std::cout << "Enter number of vertex:\n";
        }
        way >> vertex_count;
        adjacencyMatrix = std::vector<std::vector<int>>(vertex_count,
                                                        std::vector<int>(vertex_count));
        if (type_of_enter == 1) {
            std::cout << "Enter your graph:\n";
        }
        for (int i = 0; i < vertex_count; ++i) {
            for (int j = 0; j < vertex_count; ++j) {
                way >> adjacencyMatrix[i][j];
            }
        }
        ConvertAdjMatrixToEdgeList();
        ConvertEdgeListToAdjList();
        CountEdges();
        std::cout << "Created a graph with " << edges_count << " edges and " << vertex_count
                  << " vertex.\n";
    }

    /// Ввод матрицы инцендентности
    void IncMatrixEnter(std::istream &way) {
        if (type_of_enter == 1) {
            std::cout << "Enter number of vertex and edges:\n";
        }
        way >> vertex_count >> edges_count;
        incidenceMatrix = std::vector<std::vector<int>>(vertex_count,
                                                        std::vector<int>(edges_count));
        if (type_of_enter == 1) {
            std::cout << "Enter your graph:\n";
        }
        for (int i = 0; i < vertex_count; ++i) {
            for (int j = 0; j < edges_count; ++j) {
                way >> incidenceMatrix[i][j];
            }
        }
        std::cout << "Created a graph with " << edges_count << " edges and " << vertex_count
                  << " vertex.\n";
    }

    /// Ввод списка смежности
    void AdjListEnter(std::istream &way) {
        if (type_of_enter == 1) {
            std::cout << "Enter number of vertex:\n";
        }
        way >> vertex_count;
        if (type_of_enter == 1) {
            std::cout << "Enter your graph:\n";
        }
        for (int i = 0; i < vertex_count; ++i) {
            int main_vertex;
            way >> main_vertex;
            adjacencyList[main_vertex] = std::set<int>();
            std::string other_vertex;
            std::getline(way, other_vertex);
            std::stringstream sstream(other_vertex);
            int vertex_mem;
            while (sstream >> vertex_mem) {
                adjacencyList[main_vertex].insert(vertex_mem);
            }
        }
        ConvertAdjListToAdjMatrix();
        ConvertAdjMatrixToEdgeList();
        CountEdges();

        std::cout << "Created a graph with " << edges_count << " edges and " << vertex_count
                  << " vertex.\n";
    }

    /// Ввод списка рёбер
    void EdgeListEnter(std::istream &way) {
        if (type_of_enter == 1) {
            std::cout << "Enter number of pairs:\n";
        }
        int pair_num;
        way >> pair_num;
        if (type_of_enter == 1) {
            std::cout << "Enter your graph:\n";
        }
        for (int i = 0; i < pair_num; ++i) {
            int fst_vertex, scd_vertex;
            way >> fst_vertex >> scd_vertex;
            std::pair<int, int> pair_;
            pair_.first = fst_vertex;
            pair_.second = scd_vertex;
            edgeList.insert(pair_);
        }
        ConvertEdgeListToAdjList();
        vertex_count = adjacencyList.size();
        ConvertAdjListToAdjMatrix();
        CountEdges();

        std::cout << "Created a graph with " << edges_count << " edges and " << vertex_count
                  << " vertex.\n";
    }

    /// Вывод списка рёбер
    void PrintEdgeList(std::ostream &way) {
        for (const auto &i : edgeList) {
            way << i.first << " --> " << i.second << "\n";
        }
    }

    /// Вывод списка смежности
    void PrintAdjList(std::ostream &way) {
        for (const auto &i : adjacencyList) {
            way << i.first;
            if (!i.second.empty())
                way << " --> ";
            for (const auto &j : i.second)
                way << j << "  ";
            way << "\n";
        }
    }

    /// Вывод матрицы инцендентности
    void PrintIncMatrix(std::ostream &way) {
        for (int i = 0; i < vertex_count; i++) {
            for (int j = 0; j < edges_count; j++) {
                way << adjacencyMatrix[i][j] << "   ";
            }
            way << "\n";
        }
    }

    /// Вывод матрицы смежности
    void PrintAdjMatrix(std::ostream &way) {
        for (int i = 0; i < vertex_count; i++) {
            for (int j = 0; j < vertex_count; j++) {
                way << adjacencyMatrix[i][j] << "   ";
            }
            way << "\n";
        }
    }
};
