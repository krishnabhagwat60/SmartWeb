using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartWeb.Data
{
    public static class ClaimStore
    {
        public static List<Claim> claimsList = new List<Claim>()
        {
            new Claim("الصفوف الدراسية","الصفوف الدراسية"),
            new Claim("StageView","StageView"),
            new Claim("StageCreate","StageCreate"),
            new Claim("StageEdit","StageEdit"),
            new Claim("StageDelete","StageDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("الوحدات","الوحدات"),
            new Claim("UnitView","UnitView"),
            new Claim("UnitCreate","UnitCreate"),
            new Claim("UnitEdit","UnitEdit"),
            new Claim("UnitDelete","UnitDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("الدروس","الدروس"),
            new Claim("LessonView","LessonView"),
            new Claim("LessonCreate","LessonCreate"),
            new Claim("LessonEdit","LessonEdit"),
            new Claim("LessonDelete","LessonDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("المذكرات","المذكرات"),
            new Claim("NoteBookView","NoteBookView"),
            new Claim("NoteBookCreate","NoteBookCreate"),
            new Claim("NoteBookEdit","NoteBookEdit"),
            new Claim("NoteBookDelete","NoteBookDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("النصيحة","النصيحة"),
            new Claim("AdviceView","0"),
            new Claim("AdviceCreate","0"),
            new Claim("AdviceEdit","AlertEdit"),
            new Claim("AdviceDelete","0"),
            //-----------------------------------------------------------------------------------
            new Claim("التنبيهات","التنبيهات"),
            new Claim("AlertView","AlertView"),
            new Claim("AlertCreate","AlertCreate"),
            new Claim("AlertEdit","AlertEdit"),
            new Claim("AlertDelete","AlertDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("الدورات","الدورات"),
            new Claim("CourseView","CourseView"),
            new Claim("CourseCreate","CourseCreate"),
            new Claim("CourseEdit","CourseEdit"),
            new Claim("CourseDelete","CourseDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("المحاضرات","المحاضرات"),
            new Claim("LectureView","LectureView"),
            new Claim("LectureCreate","LectureCreate"),
            new Claim("LectureEdit","LectureEdit"),
            new Claim("LectureDelete","LectureDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("ملفات المحاضرة","ملفات المحاضرة"),
            new Claim("LectureFileView","LectureFileView"),
            new Claim("LectureFileCreate","LectureFileCreate"),
            new Claim("LectureFileEdit","0"),
            new Claim("LectureFileDelete","LectureFileDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("إمتحان المحاضرة","إمتحان المحاضرة"),
            new Claim("LectureExamView","LectureExamView"),
            new Claim("LectureExamCreate","LectureExamCreate"),
            new Claim("LectureExamEdit","LectureExamEdit"),
            new Claim("LectureExamDelete","LectureExamDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("الأسئلة","الأسئلة"),
            new Claim("QuestionView","QuestionView"),
            new Claim("QuestionCreate","QuestionCreate"),
            new Claim("QuestionEdit","QuestionEdit"),
            new Claim("QuestionDelete","QuestionDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("الإمتحانات","الإمتحانات"),
            new Claim("ExamView","ExamView"),
            new Claim("ExamCreate","ExamCreate"),
            new Claim("ExamEdit","ExamEdit"),
            new Claim("ExamDelete","ExamDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("بيانات الطلاب","بيانات الطلاب"),
            new Claim("StudentDataView","StudentDataView"),
            new Claim("StudentDataCreate","StudentDataCreate"),
            new Claim("StudentDataEdit","StudentDataEdit"),
            new Claim("StudentDataDelete","StudentDataDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("حسابات الطلاب","حسابات الطلاب"),
            new Claim("StudentAccountView","StudentAccountView"),
            new Claim("StudentAccountCreate","StudentAccountCreate"),
            new Claim("StudentAccountEdit","StudentAccountEdit"),
            new Claim("StudentAccountDelete","StudentAccountDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("تحديد محاضرة طالب","تحديد محاضرة طالب"),
            new Claim("ReportLectureView","ReportLectureView"),
            new Claim("ReportLectureCreate","ReportLectureCreate"),
            new Claim("ReportLectureEdit","0"),
            new Claim("ReportLectureDelete","ReportLectureDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("تقرير الإمتحانات الدورية","تقرير الإمتحانات الدورية"),
            new Claim("ReportExamView","ReportExamView"),
            new Claim("ReportExamCreate","0"),
            new Claim("ReportExamEdit","0"),
            new Claim("ReportExamDelete","ReportExamDelete"),
            //-----------------------------------------------------------------------------------
            new Claim("تقرير مشاهدة المحاضرات","تقرير مشاهدة المحاضرات"),
            new Claim("ReportLectureViewView","ReportLectureViewView"),
            new Claim("ReportLectureViewCreate","0"),
            new Claim("ReportLectureViewEdit","0"),
            new Claim("ReportLectureViewDelete","0"),
            //-----------------------------------------------------------------------------------
            new Claim("تقرير الطالب","تقرير الطالب"),
            new Claim("ReportStudentView","ReportStudentView"),
            new Claim("ReportStudentCreate","0"),
            new Claim("ReportStudentEdit","ReportStudentEdit"),
            new Claim("ReportStudentDelete","0")
        };
    }
}
