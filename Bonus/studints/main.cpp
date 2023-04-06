#include <iostream>
#include "Database.h"
#include "Student.h"

int main() {
    std::cout
            << "Welcome to Vseleon manager! Here you can manage your studints by sending them to IUP!"
            << std::endl;
    do {
        std::cout
                << "Select action\n1 - Add studint\n2 - Make studint closer to IUP\n3 - Get info about studint"
                   "\n4 - Send studint to IUP\n5 - Show all studints\n6 - Send all studints to IUP\n7 - Print studints by year"
                   "\n8 - Sort studints by mark\n0 - Exit(send yourself to IUP)" << std::endl;
        int choice;
        std::cin >> choice;
        int year;
        double mark;
        std::string name;
        Database database;
        switch (choice) {
            case 1: {
                std::cout << "Input studint name, year and mark:" << std::endl;
                Student student;
                std::cin >> student;
                database.ImportStudent(student);
                break;
            }
            case 2:
                std::cout << "Input studint name and his mark:" << std::endl;
                std::cin >> name >> mark;
                database.UpdateMark(name, mark);
                break;
            case 3:
                std::cout << "Input name:" << std::endl;
                std::cin >> name;
                try {
                    std::cout << database[name];
                } catch (std::invalid_argument &ia) {
                    std::cout << ia.what() << std::endl;
                }
                break;
            case 4:
                std::cout << "Input studint name u wanna send to IUP:" << std::endl;
                std::cin >> name;
                database.EraseStudent(name);
                break;
            case 5:
                database.PrintDatabase();
                break;
            case 6:
                database.ClearDatabase();
                break;
            case 7:
                std::cout << "Input year:" << std::endl;
                std::cin >> year;
                database.GetStudentsByYear(year);
                break;
            case 8:
                database.SortStudents();
                break;
            case 0:
                return 0;
            default:
                std::cout << "Smth went wrong. Please try again" << std::endl;
                continue;
        }
    } while (true);
}