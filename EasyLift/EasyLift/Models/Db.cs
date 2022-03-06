using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EasyLift.Models
{
    public class Db
    {
        private readonly SQLiteConnection _ctx;
        public Db(string dbPath)
        {
            _ctx = new SQLiteConnection(dbPath);
            _ctx.CreateTable<Plan>();
            _ctx.CreateTable<Workout>();
            _ctx.CreateTable<WorkoutDetail>();
            _ctx.CreateTable<History>();
            _ctx.CreateTable<Records>();
            _ctx.CreateTable<User>();
            _ctx.CreateTable<WorkoutRMBest>();
        }
        /////////////////////////////////////////////
        ///Plan
        public List<Plan> GetPlans()
        {
            return _ctx.Table<Plan>().ToList();
        }
        public int SevePlan(Plan plan)
        {
            return _ctx.Insert(plan);
        }
        public int DeletePlan(Plan plan)
        {
            if ( !string.IsNullOrEmpty(plan.IdWorkoutDetails) )
            {
                string[] z = plan.IdWorkoutDetails.Split(';');
                List<WorkoutDetail> lstWrkDet =  _ctx.Table<WorkoutDetail>().ToList();
                foreach ( var item in z )
                {
                    if ( !string.IsNullOrEmpty(item) )
                    {
                        WorkoutDetail elm = lstWrkDet.FirstOrDefault(p => p.ID == Convert.ToInt32(item));
                        if( elm != null ) {
                            DeleteWorkoutDetail(elm);
                        }
                    }
                }
            }
            return _ctx.Delete(plan);
        }
        public int UpdatePlan(Plan plan)
        {
            return _ctx.Update(plan);
        }
        /////////////////////////////////////////////
        ///Workout
        public List<Workout> GetWorkout()
        {
            return _ctx.Table<Workout>().ToList();
        }
        public int SeveWorkout(Workout workout)
        {
            return _ctx.Insert(workout);
        }
        /////////////////////////////////////////////
        ///Workout Detail
        public List<WorkoutDetail> GetWorkoutDetail()
        {
            return _ctx.Table<WorkoutDetail>().ToList();
        }
        public int SeveWorkoutDetail(WorkoutDetail workoutDetail)
        {
            _ctx.Insert(workoutDetail);
            List<WorkoutDetail> lst = _ctx.Table<WorkoutDetail>().ToList();
            return lst.LastOrDefault().ID;
        }
        public int UpdateWorkoutDetail(WorkoutDetail workoutDetail)
        {
            _ctx.Update(workoutDetail);
            List<WorkoutDetail> lst = _ctx.Table<WorkoutDetail>().ToList();
            return lst[lst.Count - 1].ID;
        }
        public int DeleteWorkoutDetail(WorkoutDetail workoutDetail)
        {
            return _ctx.Delete(workoutDetail);
        }
        
        /////////////////////////////////////////////
        ///History
        ///
        public List<History> GetHistory()
        {
            return _ctx.Table<History>().ToList();
        }
        public int SeveHistory(History history)
        {
            return _ctx.Insert(history);
        }
        public List<History> GetHistoryWorkout(int id)
        {
            return _ctx.Query<History>($"SELECT * FROM History WHERE IDW = {id}").ToList();
        }
        /////////////////////////////////////////////
        ///Records
        public List<Records> GetRecords()
        {
            return _ctx.Table<Records>().ToList();
        }
        public int SeveRecords(Records records)
        {
            return _ctx.Insert(records);
        }
        ////////////////////////////////////////////////
        ///User
        public User GetUser()
        {
            return _ctx.Table<User>().LastOrDefault();
        }
        public List<User> GetAllUser()
        {
            return _ctx.Table<User>().ToList();
        }
        public int SeveUser(User user)
        {
            return _ctx.Insert(user);
        }
        ////////////////////////////////////////////////
        ///WorkoutRMBest
        public WorkoutRMBest GetWorkoutBest(int id)
        {
            return _ctx.Table<WorkoutRMBest>().FirstOrDefault(p => p.IDW == id);
        }
        public int SaveNewBest(WorkoutRMBest w)
        {
            return _ctx.Insert(w);
        }
        public int UpdateBest(WorkoutRMBest w)
        {
            return _ctx.Update(w);
        }
    }
}
