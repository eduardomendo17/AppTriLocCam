using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTriLocCam.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        // Comandos
        private Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        private Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        // Propiedades
        private string _Picture;
        public string Picture 
        { 
            get => _Picture; 
            set => SetProperty(ref _Picture, value);
        }

        // Constructor
        public CameraViewModel()
        { }

        // Métodos
        private async void TakePictureAction()
        {
            // Inicializa la cámara
            await CrossMedia.Current.Initialize();

            // Si la cámara no está disponible o no está soportada lanza un mensaje y termina este método
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            // Toma la fotografía y la regresa en el objeto file
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "AppTriLocCam",
                Name = "cam_picture.jpg"
            });

            // Si el objeto file es null termina este método
            if (file == null) return;

            // Asignamos la ruta de la fotografía en la propiedad de la imagen
            Picture = file.Path;

            /*image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });*/
        }

        private async void SelectPictureAction()
        {
            // Inicializa la cámara
            await CrossMedia.Current.Initialize();

            // Si el seleccionar fotografía no está disponible o no está soportada lanza un mensaje y termina este método
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Pick Photo", ":( No pick photo available.", "OK");
                return;
            }

            // Selecciona la fotografía del carrete y la regresa en el objeto file
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            // Si el objeto file es null termina este método
            if (file == null) return;

            // Asignamos la ruta de la fotografía en la propiedad de la imagen
            Picture = file.Path;

            /*image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });*/
        }
    }
}
