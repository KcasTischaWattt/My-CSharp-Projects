cmake_minimum_required(VERSION 3.21)
project(studints)

set(CMAKE_CXX_STANDARD 20)

add_executable(studints main.cpp Database.h Student.h Database.cpp Student.cpp)
