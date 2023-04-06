#ifndef UNTITLED1_DATABASE_H
#define UNTITLED1_DATABASE_H

#include <iostream>
#include <vector>
#include <map>
#include "Student.h"
#include <ostream>
#include <sstream>
#include <fstream>

class Database {
    std::map<std::string, Student> students;
public:
    Database() {
        std::string name;
        int year;
        double mark;
        std::ifstream fs;
        fs.open("database.txt");
        if (!fs)
            return;
        std::string line;
        while (fs >> name >> year >> mark) {
            students[name] = Student(name, year, mark);
        }
    }

    void ImportStudent(const Student &student);

    void UpdateMark(const std::string &name, double mark);

    void EraseStudent(const std::string &name);

    void PrintDatabase() const;

    void ClearDatabase();

    void GetStudentsByYear(int year);

    void SortStudents() const;

    Student operator[](const std::string &name) const;

    ~Database();
};


#endif //UNTITLED1_DATABASE_H