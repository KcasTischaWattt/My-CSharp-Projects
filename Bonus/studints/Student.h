#ifndef UNTITLED1_STUDENT_H
#define UNTITLED1_STUDENT_H

#include <iostream>

struct Student {
    std::string Name;
    int Year;
    double Mark;

    Student(std::string name, int year, double mark) : Year(year), Mark(mark), Name(name) {}

    Student() = default;
};

std::ostream &operator<<(std::ostream &out, const Student &student);

std::istream &operator>>(std::istream &in, Student &student);

bool operator<(Student& a, Student& b);


#endif //UNTITLED1_STUDENT_H