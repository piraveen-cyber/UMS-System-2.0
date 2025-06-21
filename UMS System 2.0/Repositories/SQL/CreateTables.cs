using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS_System_2._0.Repositories.SQL
{
    public static class CreateTables
    {
        public static List<string> All => new List<string>
        {
            // Users Table
            @"
            CREATE TABLE IF NOT EXISTS Users (
                UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL UNIQUE,
                Password TEXT NOT NULL,
                Role TEXT NOT NULL CHECK(Role IN ('Admin', 'Teacher'))
            );
            ",

            // Students Table
            @"
            CREATE TABLE IF NOT EXISTS Students (
                StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT NOT NULL,
                GroupName TEXT NOT NULL,
                Gender TEXT,
                DOB DATE,
                Address TEXT,
                Email TEXT,
                Phone TEXT,
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
            );
            ",

            // Teachers Table
            @"
            CREATE TABLE IF NOT EXISTS Teachers (
                TeacherID INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT NOT NULL,
                Email TEXT,
                Phone TEXT,
                Specialization TEXT,
                AssignedGroup TEXT
            );
            ",

            // Subjects Table
            @"
            CREATE TABLE IF NOT EXISTS Subjects (
                SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectName TEXT NOT NULL,
                TeacherID INTEGER,
                FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID)
            );
            ",

            // Courses Table
            @"
            CREATE TABLE IF NOT EXISTS Courses (
                CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                CourseName TEXT NOT NULL,
                Description TEXT
            );
            ",

            // Exams Table
            @"
            CREATE TABLE IF NOT EXISTS Exams (
                ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                ExamName TEXT NOT NULL,
                ExamDate DATE NOT NULL,
                GroupName TEXT NOT NULL
            );
            ",

            // Marks Table
            @"
            CREATE TABLE IF NOT EXISTS Marks (
                MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentID INTEGER NOT NULL,
                SubjectID INTEGER NOT NULL,
                ExamID INTEGER NOT NULL,
                MarksObtained INTEGER NOT NULL,
                FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)
            );
            ",

            // Timetables Table
            @"
            CREATE TABLE IF NOT EXISTS Timetables (
                TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                GroupName TEXT NOT NULL,
                DayOfWeek TEXT NOT NULL,
                TimeSlot TEXT NOT NULL,
                SubjectID INTEGER NOT NULL,
                TeacherID INTEGER,
                Room TEXT NOT NULL,
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID)
            );
            ",

            // Rooms Table
            @"
            CREATE TABLE IF NOT EXISTS Rooms (
                RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                RoomName TEXT NOT NULL,
                RoomType TEXT CHECK(RoomType IN ('Lab', 'Lecture')),
                Capacity INTEGER
            );
            ",

            // Room Allocations Table
            @"
            CREATE TABLE IF NOT EXISTS RoomAllocations (
                AllocationID INTEGER PRIMARY KEY AUTOINCREMENT,
                RoomID INTEGER NOT NULL,
                GroupName TEXT NOT NULL,
                TimeSlot TEXT NOT NULL,
                DayOfWeek TEXT NOT NULL,
                FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
            );
            ",

            // Logs Table
            @"
            CREATE TABLE IF NOT EXISTS Logs (
                LogID INTEGER PRIMARY KEY AUTOINCREMENT,
                Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
                Level TEXT NOT NULL,
                Message TEXT NOT NULL
            );
            "
        };
    }
}
