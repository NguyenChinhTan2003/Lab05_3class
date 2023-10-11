﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab05.DAL.Entities;

using System.Data.Entity.Migrations;

namespace Lab05.BUS
{
    public class StudentService
    {
    
        public List<Student> GetALL()
        {
            StudentModel context = new StudentModel();
            return context.Students.ToList();
        }
        public List<Student> GetAllHasMoMajor()
        {
            StudentModel context = new StudentModel();
            return context.Students.Where(p => p.MajorID == null).ToList();
        }
        public List<Student> GetAllHasNoMojor(int facultyID)
        {
            StudentModel context = new StudentModel();

            return context.Students.Where(p => p.MajorID == null && p.FacultyID == facultyID).ToList();
        }
        public Student FinById(string studentId)
        {
            StudentModel context = new StudentModel();
            return context.Students.FirstOrDefault(p => p.StudentID == studentId);
        }
        public void InsertUpdate(Student s)
        {
            StudentModel context = new StudentModel();



            context.Students.AddOrUpdate(s);
            context.SaveChanges();
                

        }
        public void Delete(Student s )
        {
            StudentModel context = new StudentModel();
            Student sv= context.Students.FirstOrDefault(p =>p.StudentID == s.StudentID);
            if (sv != null)
            {
                context.Students.Remove(s);
                context.SaveChanges();
            }
          
        }
        

        
        
    }
}
