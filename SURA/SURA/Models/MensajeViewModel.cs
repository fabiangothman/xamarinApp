using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SURA.Models
{
    public class MensajeViewModel: INotifyPropertyChanged
    {
        readonly IList<ModeloMensaje> source;

        public ObservableCollection<ModeloMensaje> Mensajes { get; private set; }
        public IList<ModeloMensaje> EmptyMonkeys { get; private set; }

        public ModeloMensaje PreviousMonkey { get; set; }
        public ModeloMensaje CurrentMonkey { get; set; }
        public ModeloMensaje CurrentItem { get; set; }
        public int PreviousPosition { get; set; }
        public int CurrentPosition { get; set; }
        public int Position { get; set; }

        public ICommand ItemChangedCommand => new Command<ModeloMensaje>(ItemChanged);
        public ICommand PositionChangedCommand => new Command<int>(PositionChanged);
        public ICommand DeleteCommand => new Command<ModeloMensaje>(RemoveMonkey);

        public MensajeViewModel()
        {
            source = new List<ModeloMensaje>();
            CreateMonkeyCollection();

            CurrentItem = Mensajes.Skip(3).FirstOrDefault();
            OnPropertyChanged("CurrentItem");
            Position = 3;
            OnPropertyChanged("Position");
        }

        void CreateMonkeyCollection()
        {
            source.Add(new ModeloMensaje
            {
                Titulo = "¡Muy Pronto!",
                Descripcion = "Podrás descargar tu póliza en Mi Portal",
                ImageUrl = "prontodescarga"
            }); 

            source.Add(new ModeloMensaje
            {
                Titulo = "¡Muy Pronto!",
                Descripcion = "Podrás agendar citas en nuestro Centro Autos SURA aquí",
                ImageUrl = "prontocitas"
            });

            Mensajes = new ObservableCollection<ModeloMensaje>(source);
        }

        void ItemChanged(ModeloMensaje item)
        {
            PreviousMonkey = CurrentMonkey;
            CurrentMonkey = item;
            OnPropertyChanged("PreviousMonkey");
            OnPropertyChanged("CurrentMonkey");
        }

        void PositionChanged(int position)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = position;
            OnPropertyChanged("PreviousPosition");
            OnPropertyChanged("CurrentPosition");
        }

        void RemoveMonkey(ModeloMensaje monkey)
        {
            if (Mensajes.Contains(monkey))
            {
                Mensajes.Remove(monkey);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
