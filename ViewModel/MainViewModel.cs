using BatchRename.AppServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System;

namespace BatchRename.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {



        public IChangeNameService changeNameService;
        public MainViewModel(IChangeNameService changeName)
        {

            changeNameService = changeName;
            //FolderPath = @"C:\Users\zhaoyibo\Desktop\PSSOTemplatePlaceholder";
            FolderPath = AppDomain.CurrentDomain.BaseDirectory;
            Input1 = "PSSOTemplatePlaceholder";
        }

        public string folderPath;
        public string FolderPath
        {
            get { return folderPath; }
            set
            {
                folderPath = value;
                RaisePropertyChanged(() => FolderPath);
            }
        }

        public string input1;
        public string Input1
        {
            get { return input1; }
            set
            {
                input1 = value;
                RaisePropertyChanged(() => Input1);
            }
        }

        public string input2;
        public string Input2
        {
            get { return input2; }
            set
            {
                input2 = value;
                RaisePropertyChanged(() => Input2);
            }
        }


        public string result;
        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                RaisePropertyChanged(() => Result);
            }
        }
        public bool showResult;
        public bool ShowResult
        {
            get { return showResult; }
            set
            {
                showResult = value;
                RaisePropertyChanged(() => ShowResult);
            }
        }
        private RelayCommand _ChangeCommand;
        public RelayCommand ChangeCommand
        {
            get
            {
                if (_ChangeCommand == null)
                    return new RelayCommand(() => Change(), CanChange);
                return _ChangeCommand;
            }
            set
            {
                _ChangeCommand = value;
            }
        }

        public bool CanChange()
        {
            if (string.IsNullOrWhiteSpace(this.FolderPath))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.Input1) || string.IsNullOrWhiteSpace(this.Input2))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Change()
        {
            this.ShowResult = false;
            //this.Result = this.Input1 + this.Input2;

            this.Result = changeNameService.ChangeName(this.FolderPath, this.Input1, this.Input2);
            this.ShowResult = true;
        }
    }
    
}