using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc2_student2.Models;

namespace Mvc2_student2.Controllers
{
    public class studentController : Controller
    {
        MvcdbEntities2 db = new MvcdbEntities2();

        // GET: student        
        public ActionResult Index()
        {
            List<Student> students = db.Students.ToList();

            var data = (from std in students
                        join cls in db.Classes on std.StudentClassId equals cls.ClassId
                        select new StudentViewModel
                        {
                            StudentId = std.StudentId,
                            StudentName = std.StudentName,
                            StudentAddress = std.StudentAddress,
                            StudentPhone = std.StudentPhone,
                            ClassName = cls.ClassName,
                            UserSubjects = new List<UserSubjectsModel>
                            {   //Add subjects into list with comma separated
                                new UserSubjectsModel
                                {
                                    SubjectName=string.Join(", ", from stu in db.Subjects
                                                                 join map in db.Mappings on stu.SubjectId equals map.MapSubjectId
                                                                 where map.MapStudentId==std.StudentId
                                                                 select stu.SubjectName)
                                }
                            }
                        }).ToList();

            //string query = "SELECT student_id, student_name,student_add, student_phn, class_name,"
            //            + "subject_name = STUFF((SELECT distinct ', ' + subject_name from tb2_subject ss, tb2_mapping mm, tb2_student stu where stu.student_id = mm.map_stdid and mm.map_subid = ss.subject_id and stu.student_id = tb2_student.student_id for xml PATH('')),1,2,'')"
            //            + "FROM tb2_student JOIN  tb2_class on tb2_student.student_id = tb2_class.class_id JOIN  tb2_mapping on tb2_student.student_id = tb2_mapping.map_stdid JOIN "
            //            + "tb2_subject on tb2_mapping.map_subid = tb2_subject.subject_id group by student_name,tb2_student.student_id,tb2_student.student_add,tb2_student.student_phn,class_name";
            //IEnumerable<StudentViewModel> data = db.Database.SqlQuery<StudentViewModel>(query);

            return View(data.Distinct());
        }


        // GET: student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: student/Create
        public ActionResult Create()
        {
            var subjects = db.Subjects.ToList();
            var classes = db.Classes.ToList();

            ViewBag.subj = (from sb in subjects
                            select new EditViewModel() 
                            {
                                SubjectId = sb.SubjectId,
                                SubjectName = sb.SubjectName,
                                IsChecked = false
                            });

            ViewBag.cls = (from c in classes
                           select new EditViewModel
                           {
                               ClassId = c.ClassId,
                               ClassName = c.ClassName,
                               IsChecked = false
                           });

            return View();
        }

        // POST: student/Create
        [HttpPost]
        public ActionResult Create(EditViewModel addnew, string[] subjectName, string className)
        {
            try
            {
                // TODO: Add insert logic here
                Student student = new Student();
                Mapping mapping = new Mapping();
                Subject subject = new Subject();

                student.StudentId = addnew.StudentId;
                student.StudentName = addnew.StudentName;
                student.StudentAddress = addnew.StudentAddress;
                student.StudentPhone = addnew.StudentPhone;
                student.StudentClassId = Convert.ToInt32(className);
                db.Students.Add(student);
                                                           
                //Insert data into mapping               
                foreach (var item in subjectName)
                {
                    mapping.MapStudentId = student.StudentId;
                    mapping.MapSubjectId = Convert.ToInt32(item);
                    db.Mappings.Add(mapping);
                    db.SaveChanges();
                }

                db.SaveChanges();
                TempData["SuccessMessage"] = "Create Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public List<Subject> GetDropdown()
        {
            var sub = db.Subjects.ToList();
            return sub;
        }

        // GET: student/Edit/5
        public ActionResult Edit(int id)
        {
            //Bind DropDownList for Subject
            var std = db.Students.Where(s => s.StudentId == id).Select(a => a.StudentId).ToList();
            var map = db.Mappings.Where(m => m.MapStudentId == id).Select(m => m.MapSubjectId).ToList();
            ViewBag.subject = new MultiSelectList(GetDropdown(), "SubjectId", "SubjectName", map);

            //Bind DropDownList for Class
            var clsBind = db.Classes.ToList();
            var stdBind = db.Students.Where(x => x.StudentId == id).Select(a => a.StudentClassId).ToList();
            ViewBag.cls = new MultiSelectList(clsBind, "ClassId", "ClassName", stdBind);

            //Find Student data for Edit
            var stdData = db.Students.Where(s => s.StudentId == id).FirstOrDefault();
            var vmData = new EditViewModel
            {
                StudentId = stdData.StudentId,
                StudentName = stdData.StudentName,
                StudentAddress = stdData.StudentAddress,
                StudentPhone = stdData.StudentPhone,
                StudentClassId = stdData.Class.ClassId               
            };

            return View(vmData);
        }

        // POST: student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EditViewModel editView, string[] subjectId, string classId)
        {
            try
            {
                // TODO: Add update logic here
                var user = db.Students.Where(c => c.StudentId == editView.StudentId).FirstOrDefault();
                Mapping mapping = new Mapping();
                user.StudentId = editView.StudentId;
                user.StudentName = editView.StudentName;
                user.StudentAddress = editView.StudentAddress;
                user.StudentPhone = editView.StudentPhone;
                user.StudentClassId = Convert.ToInt32(classId);

                //Remove old data from Mapping table
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Mappings.RemoveRange(db.Mappings.Where(x => x.MapStudentId == id));
                db.SaveChanges();

                //Save new data into Mapping
                foreach (var item in subjectId)
                {
                    mapping.MapStudentId = user.StudentId;
                    mapping.MapSubjectId = Convert.ToInt32(item);
                    db.Mappings.Add(mapping);
                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Update Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
         
         // GET: student/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            // TODO: Add delete logic here
            var user = db.Students.Where(c => c.StudentId == id).FirstOrDefault();
            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Delete Successfully";
            return RedirectToAction("Index");
        }        
    }
}
