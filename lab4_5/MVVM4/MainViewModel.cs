using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using lab4_5.Models;

namespace lab4_5.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<BolidViewModel> bolids;
        private Dispatcher dispatcher;
        private int numberBolid = 0;

        public ObservableCollection<BolidViewModel> Bolids
        {
            get { return bolids; }
            set
            {
                bolids = value;
                OnPropertyChanged(nameof(Bolids));
            }
        }

        public MainViewModel(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            Bolids = new ObservableCollection<BolidViewModel>();            

            for(int i = 0; i < 10; i++)
            {
                InitializeBolidAsync();
            }
                
            
        }

        public async Task InitializeBolidAsync()
        {
            await Task.Delay(100);
            Bolids.Add(new BolidViewModel(numberBolid, dispatcher));
            numberBolid++;

            OnPropertyChanged(nameof(numberBolid));
        }

        

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }    
}
