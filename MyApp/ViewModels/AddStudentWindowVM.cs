using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MyApp.Migrations;
using MyApp.Models;
using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static SQLite.SQLite3;

namespace MyApp.ViewModels
{
    public partial class AddStudentWindowVM:ObservableObject
    {
        public Students Student { get; set; }
        public string Title { get; set; }
        public Module SelectedModule { get; set; }
        public static Results SelectedResult { get; set; }
        public ObservableCollection<Module> Modules { get; set; }
        public static ObservableCollection<Results> Results { get; set; }
        public List<Module> StudentModules { get; set; }

        public AddStudentWindowVM()
        {
        }

        public AddStudentWindowVM(Students std)
        {
            Title = "ADD RESULT";
            using var db = new DataContext();
            Student = db.Dbstudnets.Include(s => s.Results).ThenInclude(r => r.Module).FirstOrDefault(s => s.Id == std.Id);
            Modules = new ObservableCollection<Module>(db.Dbmodules.ToList());
            Results = new ObservableCollection<Results>(db.Dbstudnets.Include(s => s.Results).ThenInclude(r => r.Module).FirstOrDefault(s => s.Id == std.Id).Results);
            StudentModules = new List<Module>();
        }

        public void deleteResult(Students std)
        {
            using DataContext db = new DataContext();
            {
                var student = db.Dbstudnets.Include(s => s.Results).ThenInclude(r => r.Module).FirstOrDefault(s => s.Id == std.Id);

                if (student != null)
                {

                    var resultsToDelete = new List<Results>(student.Results);
                    var modulesToDelete = new List<Module>();
                    foreach (var result in resultsToDelete)
                    {
                        db.Dbresults.Remove(result);
                        var moduleToDelete = db.Dbmodules.Include(m => m.Students)
                                     .FirstOrDefault(m => m.Id == result.ModuleID);
                        if (moduleToDelete != null)
                        {
                            modulesToDelete.Add(moduleToDelete);
                        }
                    }

                    foreach (var module in modulesToDelete)
                    {
                        student.Modules.Remove(module);

                    }

                    student.GPA = 0.0;
                    db.SaveChanges();
                }
           
            }
        }

        [RelayCommand]
        public void Save()
        {

            using (DataContext db = new DataContext())
            {

                if (Results != null)
                {
                    var student = db.Dbstudnets
                        .Include(s => s.Results)
                        .ThenInclude(r => r.Module).
                        FirstOrDefault(s => s.Id == Student.Id);

                    student.Results.Clear();

                    foreach (var r in Results)
                    {
                        // Check if the Result entity is already being tracked by the context
                        var existingResult = db.Dbresults.Find(r.ID);
                        Results updatedResult;

                        if (existingResult != null)
                        {
                            // If the Result entity is already being tracked, update its values
                            db.Entry(existingResult).CurrentValues.SetValues(r);
                            updatedResult = existingResult;
                        }
                        else
                        {
                            updatedResult = r;
                            // Check if the Module entity is already being tracked by the context
                            var existingModule = db.Dbmodules.Find(r.Module.Id);
                            if (existingModule != null)
                            {
                                // If the Module entity is already being tracked, use the tracked entity instead of the new one
                                updatedResult.Module = existingModule;
                            }
                        }
                        student.Results.Add(updatedResult);
                    }
                    
                    var val1 = Results.Sum(r => r.GPV * r.Module.Credit);
                    var val2 = Results.Sum(r => r.Module.Credit);
                    var val3 = val1 / val2;
                    student.GPA = val3;
                    db.SaveChanges();

                }
                    MessageBox.Show("Saved");
                    Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    currentWindow?.Close();
                
            }
        }


        [RelayCommand]
        public void AddModule()
        {
            using DataContext db = new DataContext();
            if (SelectedModule != null)
            {
       
                bool moduleAdded = Results.Any(r => r.Module.ModuleId==SelectedModule.ModuleId);
                if (moduleAdded)
                {
                    MessageBox.Show("Module Already added");
                }
                else
                {
                   
                    StudentModules.Add(db.Dbmodules.FirstOrDefault(m => m.Name == SelectedModule.Name));

                    Results result = new Results(db.Dbmodules.FirstOrDefault(m => m.Name == SelectedModule.Name));
                    Results.Add(result);

                }
               

            }

        }

        [RelayCommand]
        public void DeleteResult()
        {
            if (SelectedResult != null)
            {
                Results.Remove(SelectedResult);
            }
        }

        [RelayCommand]
        public void AddResult()
        {

            using (DataContext db = new DataContext())
            {
                if (SelectedResult != null)
                {
                    var v = new AddResultWindow(SelectedResult);
                    v.Show();

                }
            }
        }

        [RelayCommand]
        public void Home()
        {
            
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();

        }
    }
}
