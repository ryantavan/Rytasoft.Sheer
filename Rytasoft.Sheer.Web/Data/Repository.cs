using Rytasoft.Sheer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rytasoft.Sheer.Web.Data
{
    public class Repository
    {
        private static List<Student> students = new List<Student>() {
            new Student() { FirstName = "Britney ", LastName = "Spears", Id = 1 , Score=90 },
            new Student() { FirstName = "Taylor", LastName = "Swift", Id = 2 , Score= 99 },
            new Student() { FirstName = "Sam", LastName = "Smith", Id = 3 , Score = 92 },
            new Student() { FirstName = "Bryan", LastName = "Adams", Id = 4 , Score = 98 }     
        }.ToList();

        public List<Student> GetStudents() {
            return students;
        }
        public Student GetStudent(int studentId)
        {
            return students.Where(x => x.Id == studentId).Single();
        }

        public int AddStudent(Student student) {
            students.Add(student);
            return students.Count() + 1;
        }

        public void UpdateStudent(Student student) {
            var itemIndex =  students.FindIndex(x => x.Id == student.Id);
            students.RemoveAt(itemIndex);
            students.Insert(itemIndex, student);
        }

        public void DeleteStudent(int Id) {
            var foundStudent = students.Where(x => x.Id == Id).FirstOrDefault();
            students.Remove(foundStudent);

        }

    }
}