using System.Collections.Generic;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Controllers
{
    public interface IStudentController
    {
        static abstract void AddStudent(string name, int courseId);
        static abstract void DeleteStudent(int id);
        static abstract List<Student> GetAllStudents();
        static abstract void UpdateStudent(int id, string name, int courseId);
    }
}