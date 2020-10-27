﻿using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class CustomerFilterPreferencesRepository : ICustomerFilterPreferencesRepository
    {
        public readonly string _csvFilePath;// = @"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\assist-purchase-backend\Backend\Devices.csv";
        
        readonly static Utility.CustomerPreferenceFilterHandeler _csvHandler = new Utility.CustomerPreferenceFilterHandeler();
        public CustomerFilterPreferencesRepository(string filepath)
        {
            this._csvFilePath = filepath;
            
        }

        public DataModels.FilterDataModel getCustomerPreferencesByIP(string ip)
        {
            return _csvHandler.GetPreferences(ip, _csvFilePath);
        }

        public bool saveCustomerPreferencesToFile(string ip, DataModels.FilterDataModel filters)
        {
            return _csvHandler.saveToCsv(ip,filters,_csvFilePath);
        }
    }
}
