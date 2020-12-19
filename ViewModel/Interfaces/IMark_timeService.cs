using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

namespace ViewModel.Interfaces
{
    public interface IMark_timeService
    {
        ObservableCollection<Mark_timeViewModel> GetAll();
        Mark_timeViewModel Get(int id);
        void AddIncidentToMark_time(int Mark_timeId, IncidentViewModel incident);
        void RemoveIncidentFromMark_time(int Mark_timeId, int incidentId);
        void CreateMark_time(Mark_timeViewModel Mark_time);
        void DeleteMark_time(int Mark_timeId);
        void UpdateMark_time(Mark_timeViewModel Mark_time);
    }
}
