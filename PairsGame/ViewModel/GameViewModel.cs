using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using PairsGame.Commands;
using PairsGame.Model;
using PairsGame.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PairsGame.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _numRows;
        public int NumRows
        {
            get { return _numRows; }
            set
            {
                _numRows = value;
                SetPropertyChanged(nameof(NumRows));
            }
        }
        private int _numCols;
        public int NumCols
        {
            get { return _numCols; }
            set
            {
                _numCols = value;
                SetPropertyChanged(nameof(NumCols));
            }
        }
        private bool _isFirstSelection = true;
        private int level;
        public static Users user { get; set; }
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                SetPropertyChanged("Username");
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                SetPropertyChanged("Level");
            }
        }
        public int firstSelectedIndex { get; set; } = -1;
        private ObservableCollection<MyImage> _defaultCollection;
        public ObservableCollection<MyImage> DefaultCollection
        {
            get
            {
                return _defaultCollection;

            }
            set
            {
                _defaultCollection = value;
                SetPropertyChanged(nameof(DefaultCollection));
            }
        }
        private List<ImageSource> _imageCollection;
        public List<ImageSource> ImageCollection
        {
            get
            {
                return _imageCollection;

            }
            set
            {
                _imageCollection = value;
                SetPropertyChanged(nameof(ImageCollection));
            }
        }


        public bool IsFirstSelection
        {
            get { return _isFirstSelection; }
            set
            {
                _isFirstSelection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFirstSelection)));
            }
        }
        private ICommand imageClickedCommand;
        public ICommand ImageClickedCommand
        {
            get
            {
                if (imageClickedCommand == null)
                    imageClickedCommand = new RelayCommand(SelectImage);
                return imageClickedCommand;
            }
        }
        private ICommand customCommand;
        public ICommand CustomCommand
        {
            get
            {
                if (customCommand == null)
                    customCommand = new RelayCommand(CustomCommandCom);
                return customCommand;
            }
        }

        private void CustomCommandCom(object sender)
        {
            CustomSizeWindow x = new CustomSizeWindow();
            x.Show();

        }

        private ICommand saveGameCommand;
        public ICommand SaveGameCommand
        {
            get
            {
                if (saveGameCommand == null)
                    saveGameCommand = new RelayCommand(SaveGameCom);
                return saveGameCommand;
            }
        }

        private List<string> GetDefaultCollectionPaths()
        {
            var paths = new List<string>();
            foreach (MyImage item in DefaultCollection)
            {
                paths.Add((item.ImageSource as BitmapImage).UriSource.ToString());

            }
            return paths;

        }

        private List<bool> GetBools()
        {
            var bools = new List<bool>();
            foreach (MyImage item in DefaultCollection)
            {
                bools.Add(item.Enabled);
            }
            return bools;
        }
        private List<string> GetImageCollectionPaths()
        {
            var paths = new List<string>();
            foreach (ImageSource item in ImageCollection)
            {
                paths.Add((item as BitmapImage).UriSource.ToString());

            }
            return paths;
        }

        private void SaveGameCom(object obj)
        {

            string json = File.ReadAllText("saves.json");
            ObservableCollection<GameState> gameStates = JsonConvert.DeserializeObject<ObservableCollection<GameState>>(json);
            GameState gameStateToUpdate = gameStates.FirstOrDefault(u => u.username == user.username);
            if (gameStateToUpdate != null)
            {

                gameStateToUpdate.level=Level;
                gameStateToUpdate.rows=NumRows;
                gameStateToUpdate.columns=NumCols;
                gameStateToUpdate.defaultPaths=GetDefaultCollectionPaths();
                gameStateToUpdate.defaultBools=GetBools();
                gameStateToUpdate.imageCollection=GetImageCollectionPaths();
                string updatedJson = JsonConvert.SerializeObject(gameStates);
                File.WriteAllText("saves.json", updatedJson);
            }
            else
            {
                gameStateToUpdate= new GameState();
                gameStateToUpdate.level=Level;
                gameStateToUpdate.username=Username;
                gameStateToUpdate.rows=NumRows;
                gameStateToUpdate.columns=NumCols;
                gameStateToUpdate.defaultPaths=GetDefaultCollectionPaths();
                gameStateToUpdate.defaultBools=GetBools();
                gameStateToUpdate.imageCollection=GetImageCollectionPaths();
                string updatedJson = JsonConvert.SerializeObject(gameStates);
                File.WriteAllText("saves.json", updatedJson);

            }
        }

        private ICommand statisticsCommand;
        public ICommand StatisticsCommand
        {
            get
            {
                if (statisticsCommand == null)
                    statisticsCommand = new RelayCommand(StatisticsCom);
                return statisticsCommand;
            }
        }

        private void StatisticsCom(object obj)
        {
            StatisticsWindow window = new StatisticsWindow();
            window.Show();
        }

        private ICommand aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                    aboutCommand = new RelayCommand(AboutCom);
                return aboutCommand;
            }
        }
        private void AboutCom(object obj)
        {
            AboutWindow about = new AboutWindow();
            about.Show();
        }

        private ICommand newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (newGameCommand == null)
                    newGameCommand = new RelayCommand(NewGameCom);
                return newGameCommand;
            }
        }
        private ICommand standardCommand;
        public ICommand StandardCommand
        {
            get
            {
                if (standardCommand == null)
                    standardCommand = new RelayCommand(StandardCom);
                return standardCommand;
            }
        }

        private void StandardCom(object obj)
        {
            StandardCom();
        }
        private void StandardCom()
        {
            NumRows= 5;
            NumCols= 5;
            NewGame();
        }
        private bool AreImageSourcesEqual(ImageSource imageSource1, ImageSource imageSource2)
        {
            if (imageSource1 == null || imageSource2 == null)
            {
                return false;
            }

            BitmapImage bitmapImage1 = imageSource1 as BitmapImage;
            BitmapImage bitmapImage2 = imageSource2 as BitmapImage;

            if (bitmapImage1 == null || bitmapImage2 == null)
            {
                return false;
            }

            return bitmapImage1.UriSource == bitmapImage2.UriSource;
        }
        private async void SelectImage(object sender)
        {

            int selectedIndex = -1;
            var image = sender as MyImage;
            for (int i = 0; i<DefaultCollection.Count; i++)
            {
                if (DefaultCollection[i].ImageSource== image.ImageSource)
                {
                    selectedIndex=i; break;
                }
            }

            if (firstSelectedIndex != -1)
                if (selectedIndex == firstSelectedIndex) { return; }

            DefaultCollection[selectedIndex].ImageSource = ImageCollection[selectedIndex];
            SetPropertyChanged(nameof(DefaultCollection));

            if (IsFirstSelection)
            {
                firstSelectedIndex = selectedIndex;
                IsFirstSelection = false;
            }
            else
            {
                if (AreImageSourcesEqual(ImageCollection[selectedIndex], ImageCollection[firstSelectedIndex]))
                {

                    await Task.Run(() =>
                    {
                        Thread.Sleep(300);
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            DefaultCollection[selectedIndex].ImageSource = new BitmapImage(new Uri("D:\\MVP\\PairsGame\\PairsGame\\Images\\matched.png", UriKind.Absolute));
                            DefaultCollection[firstSelectedIndex].ImageSource = new BitmapImage(new Uri("D:\\MVP\\PairsGame\\PairsGame\\Images\\matched.png", UriKind.Absolute));
                            DefaultCollection[selectedIndex].Enabled = false;
                            DefaultCollection[firstSelectedIndex].Enabled = false;
                            firstSelectedIndex=-1;
                            IsFirstSelection = true;
                            CheckGameFinished();
                            SetPropertyChanged(nameof(DefaultCollection));
                        });
                    });
                    return;
                }



                IsFirstSelection = true;

                await Task.Run(() =>
                {
                    Thread.Sleep(300);
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        DefaultCollection[selectedIndex].ImageSource = new BitmapImage(new Uri("D:\\MVP\\PairsGame\\PairsGame\\Images\\default.png", UriKind.Absolute));
                        DefaultCollection[firstSelectedIndex].ImageSource = new BitmapImage(new Uri("D:\\MVP\\PairsGame\\PairsGame\\Images\\default.png", UriKind.Absolute));
                        firstSelectedIndex = -1;
                    });
                });
            }

        }

        public static void Shuffle<T>(List<T> collection)
        {
            Random rng = new Random();
            int n = collection.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = collection[k];
                collection[k] = collection[n];
                collection[n] = value;
            }
        }
        public void CheckGameFinished()
        {
            bool finished = true;
            foreach (var item in DefaultCollection)
            {
                if (item.Enabled == true)
                {
                    finished=false;
                    break;
                }
            }
            if (finished)
            {
                MessageBox.Show("You won level " + Level + "!");
                Shuffle(ImageCollection);
                if (Level== 3)
                {
                    MessageBox.Show("You won the game!");
                    string json = File.ReadAllText("users_json.json");
                    ObservableCollection<Users> users = JsonConvert.DeserializeObject<ObservableCollection<Users>>(json);
                    Users userToUpdate = users.FirstOrDefault(u => u.username == user.username);
                    if (userToUpdate != null)
                    {

                        userToUpdate.gamesWon++;

                        string updatedJson = JsonConvert.SerializeObject(users);
                        File.WriteAllText("users_json.json", updatedJson);
                    }
                    return;
                }
                CreateDefaultCollection();
                Level++;
            }
        }

        private void CreateDefaultCollection()
        {
            DefaultCollection = new ObservableCollection<MyImage> { };
            for (int i = 0; i<NumRows; i++)
            {
                for (int j = 0; j<NumCols; j++)
                {
                    BitmapImage temp = new BitmapImage(new Uri("D:\\MVP\\PairsGame\\PairsGame\\Images\\default.png", UriKind.Absolute));
                    MyImage myImage = new MyImage(temp, true);
                    DefaultCollection.Add(myImage);

                }
            }
            if ((NumRows*NumCols)%2==1)
            {
                BitmapImage temp = new BitmapImage(new Uri("D:\\MVP\\PairsGame\\PairsGame\\Images\\matched.png", UriKind.Absolute));
                MyImage myImage = new MyImage(temp, false);
                DefaultCollection[DefaultCollection.Count- 1] = myImage;

            }
        }


        public GameViewModel()
        {

            Messenger.Default.Register<CustomSizeMessage>(this, HandleCustomSizeMessage);
            NewGame();
        }
        private void HandleCustomSizeMessage(CustomSizeMessage message)
        {

            NumRows = message.Rows;
            NumCols = message.Columns;
            if (NumRows*NumCols>40)
            {
                MessageBox.Show("Dimensiune prea mare!");
                return;
            }
            if (NumRows*NumCols<2)
            {
                MessageBox.Show("Dimensiune invalida!");
                return;
            }
            NewGame();
        }

        public void NewGame()
        {

            if (user!=null)
            {
                Username=user.username;
                string json = File.ReadAllText("users_json.json");
                ObservableCollection<Users> users = JsonConvert.DeserializeObject<ObservableCollection<Users>>(json);
                Users userToUpdate = users.FirstOrDefault(u => u.username == user.username);
                if (userToUpdate != null)
                {

                    userToUpdate.gamesPlayed++;

                    string updatedJson = JsonConvert.SerializeObject(users);
                    File.WriteAllText("users_json.json", updatedJson);
                }
                if (NumRows==0||NumCols==0)
                {
                    StandardCom();
                }
                CreateDefaultCollection();
                CreateImageCollection();
                Level=1;
                IsFirstSelection = true;
                Shuffle(ImageCollection);
            }

        }

        private void CreateImageCollection()
        {
            ImageCollection = new List<ImageSource>();
            for (int i = 1; i<=(NumRows*NumCols)/2; i++)
            {
                BitmapImage temp = new BitmapImage(new Uri("D:\\MVP\\PairsGame\\PairsGame\\Images\\image"+i+".png", UriKind.Absolute));
                ImageCollection.Add(temp);
                ImageCollection.Add(temp);
            }
        }

        ~GameViewModel()
        {
            EndGame();
        }
        public void EndGame()
        {
            Level = 1;
            IsFirstSelection = true;
            NumRows = 0;
            NumCols = 0;
            user=null;
        }
        public void NewGameCom(object sender)
        {
            NewGame();
        }


    }
}
