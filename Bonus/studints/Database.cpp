#include "Database.h"
#include "Student.h"
#include <vector>
#include <ostream>
#include <iostream>

void Database::ImportStudent(const Student &student) {
    if (students.contains(student.Name)) {
        std::cout << "Studint " << student.Name << " is already in line for the IUP" << std::endl;
    }
    students[student.Name] = student;
    std::cout << "Studint has bean added in line for the IUP" << std::endl;
}

void Database::EraseStudent(const std::string &name) {
    if (students.empty()) {
        std::cout << "All studints are on IUP!" << std::endl;
        return;
    }
    try {
        students.erase(name);
        std::cout << "Studint sent to IUP!" << std::endl;
    } catch (...) {
        std::cout << "Student with name" + name + " wasn't found. Maybe, he is already on IUP" << std::endl;
    }
}

void Database::UpdateMark(const std::string &name, double mark) {
    if (students.empty()) {
        std::cout << "All studints are on IUP!" << std::endl;
        return;
    }
    if(students.contains(name)) {
        students[name].Mark = mark;
        std::cout << "Now studint " << name << " is closer to IUP!" << std::endl;
        return;
    }
    std::cout << "Student with name" + name + " wasn't found. Maybe, he is already on IUP" << std::endl;
}

void Database::PrintDatabase() const{
    if (students.empty()) {
        std::cout << "All studints are on IUP!" << std::endl;
        return;
    }
    for (auto &student : students) {
        std::cout << student.second;
    }
}

void Database::ClearDatabase() {
    if (students.empty()) {
        std::cout << "All studints are already on IUP!" << std::endl;
        return;
    }
    students = std::map<std::string, Student>();
    std::cout << "All studints has been sent to IUP" << std::endl;
}

Student Database::operator[](const std::string &name) const {
    for (const auto &student : students) {
        if (name == student.first) {
            return student.second;
        }
    }
    throw std::invalid_argument("Student with name" + name + " wasn't found. Maybe, he is on IUP");
}

void Database::GetStudentsByYear(int year) {
    if (students.empty()) {
        std::cout << "There are no students in the database. Maybe, they are all on IUP"
                  << std::endl;
        return;
    }
    for (auto &student : students) {
        if (student.second.Year == year) {
            std::cout << student.second;
        }
    }
}

void Database::SortStudents() const{
    if (students.empty()) {
        std::cout << "There are no students in the database. Maybe, they are all on IUP"
                  << std::endl;
        return;
    }
    auto studs = std::vector<Student>();
    for (const auto &student : students) {
        studs.push_back(student.second);
    }
    std::sort(studs.end(), studs.begin());
    for (const auto &student : studs) {
        std::cout << student;
    }
}

Database::~Database() {
    std::ofstream outputStream("database.txt");
    for (auto &student : students) {
        outputStream << student.second.Name << " " << student.second.Year << " "
                     << student.second.Mark << std::endl;
    }
}

