#include "Student.h"
#include <tuple>

std::ostream &operator<<(std::ostream &out, const Student &student) {
    out << "Name: " << student.Name << " Year: " << student.Year << " Mark: " << student.Mark
        << std::endl;
    return out;
}

std::istream &operator>>(std::istream &in, Student &student) {
    in >> student.Name >> student.Year >> student.Mark;
    return in;
}

bool operator<(Student &a, Student &b) {
    return std::make_tuple(a.Mark, a.Name, a.Year) < std::make_tuple(b.Mark, b.Name, b.Year);
}