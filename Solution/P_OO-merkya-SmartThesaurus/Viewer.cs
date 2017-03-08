﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace P_OO_merkya_SmartThesaurus
{
    public class Viewer
    {
        private static Viewer viewerInstance;

        /// <summary>
        /// 
        /// </summary>
        /// \todo types and comments
        /// <param name="_idStartingFolder"></param>
        /// <param name="_type"></param>
        /// <param name="_tempConsoleCurrentTabulation">Temporary, need to be changed (todo)</param>
        public void show(string _idStartingFolder = "0", ViewType _type = ViewType.Everything_Recursive, string _tempConsoleCurrentTabulation = "")
        {
            DB database = DB.getInstance();

            //List the current folder's name, if this is not 0
            if (_idStartingFolder != "0")
            {
                Console.WriteLine(_tempConsoleCurrentTabulation + "-"+ database.getFolderName(_idStartingFolder));
            }

            //Go through all the files, if required
            if (_type == ViewType.Everything_Recursive || _type == ViewType.Files_Only || _type == ViewType.Folders_And_Files)
            {
                DataTable fileDataTable = database.getFiles(_idStartingFolder);
                foreach (DataRow file in fileDataTable.Rows)
                {
                    //output his name
                    Console.WriteLine(_tempConsoleCurrentTabulation + "|-" + file.ItemArray[1]);
                }
            }

            //Go through all the folders, if required
            if (_type == ViewType.Everything_Recursive || _type == ViewType.Folders_And_Files || _type == ViewType.Folders_Only_Recursive)
            {
                DataTable folderDataTable = database.getFolders(_idStartingFolder);


                foreach (DataRow folder in folderDataTable.Rows)
                {

                    //Recurse on it, if required
                    if (_type == ViewType.Everything_Recursive || _type == ViewType.Folders_Only_Recursive)
                    {
                        show(Convert.ToString(folder.ItemArray[0]), _type, _tempConsoleCurrentTabulation + "|");
                    }
                }
            }
        }

        private Viewer()
        {

        }

        public static Viewer getInstance()
        {
            if (viewerInstance == null)
            {
                viewerInstance = new Viewer();
            }

            return viewerInstance;
        }
    }
}