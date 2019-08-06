﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Models;
using ViewModels.Contracts;

namespace ViewModels
{
    public class CostCategoryViewModel : ViewModelBase, ICostCategoryViewModel
    {
        private int id;
        public int Id
        {
            get => id;
            set { Set(() => Id, ref id, value); }
        }

        private string name;
        public string Name
        {
            get => name;
            set { Set(() => Name, ref name, value); }
        }

        public CostCategoryViewModel(CostCategory category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}