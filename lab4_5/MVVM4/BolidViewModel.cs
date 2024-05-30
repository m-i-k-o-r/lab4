using lab4_5.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace lab4_5.ViewModel
{
    public class BolidViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private Bolid bolid;
        private Dispatcher dispatcher;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int DistanceCover { get; private set; }
        public string StatusBolid { get; private set; }

        public BolidViewModel(int id, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            Id = id;
            bolid = new Bolid();
            bolid.DistanceCovered += (sender, e) =>
            {
                dispatcher.Invoke(() => DistanceCover = bolid.getDistanceCovered());
                OnPropertyChanged(nameof(DistanceCover));
            };

            bolid.StatusChanged += (sender, newStatus) =>
            {
                dispatcher.Invoke(() => StatusBolid = newStatus.ToString());
                OnPropertyChanged(nameof(StatusBolid));
            };

            Task.Run(() => bolid.StartRace());
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
