﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PairsGame.View;
using PairsGame.Commands;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using System.Collections.ObjectModel;
using PairsGame.Model;

namespace PairsGame.ViewModel
{
    [Serializable]
    class NewViewModel : ViewModelBase
    {
        private string text;
        public string Text
        {
            get { return this.text; }
            set
            {
                if (!string.Equals(this.text, value))
                {
                    this.text = value;
                    OnPropertyChanged("Text");
                }
            }
        }
        private string selectedImage;
        public string SelectedImage
        {
            get { return this.selectedImage; }
            set
            {
                if (!string.Equals(this.selectedImage, value))
                {
                    this.selectedImage = value;
                    OnPropertyChanged("SelectedImage");
                }
            }
        }
        public ObservableCollection<string> NewDialogImages { get; set; }
        public NewViewModel()
        {
            NewDialogImages = new ObservableCollection<string>();
            NewDialogImages.Add(AppDomain.CurrentDomain.BaseDirectory+"image1.jpg");
            NewDialogImages.Add(AppDomain.CurrentDomain.BaseDirectory + "image2.jpg");
            NewDialogImages.Add(AppDomain.CurrentDomain.BaseDirectory + "image3.jpg");
            NewDialogImages.Add(AppDomain.CurrentDomain.BaseDirectory + "image4.jpg");
            NewDialogImages.Add(AppDomain.CurrentDomain.BaseDirectory + "image5.jpg");
            NewDialogImages.Add(AppDomain.CurrentDomain.BaseDirectory + "image6.jpg");

        }
        private void RaisePropertyChanged()
        {
            throw new NotImplementedException();
        }

        private ICommand addNewCommand;
        public ICommand AddNewCommand
        {
            get
            {
                if (addNewCommand == null)
                    addNewCommand = new RelayCommand(AddNewUser);
                return addNewCommand;
            }
        }
        public void AddNewUser(object parameter)
        {
            Users user = new Users();
            user.username = text;
            user.image = SelectedImage;
            List<Users> l = new List<Users>();
            using (StreamReader r = new StreamReader("users_json.json"))
            {
                string json1 = r.ReadToEnd();
                if (json1 != "")
                {
                    l = JsonConvert.DeserializeObject<List<Users>>(json1);
                }
            }
            bool containsItem = l.Any(item => item.username == user.username);
            if (containsItem)
            {
                MessageBox.Show("User already exists!");
            }
            else
            {
                l.Add(user);
                MainViewModel.Users.Add(user);
                MessageBox.Show("User added!");
            }
            string json = JsonConvert.SerializeObject(l.ToArray());
            System.IO.File.WriteAllText("users_json.json", json);
            SetPropertyChanged("AddNewCommand");

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
