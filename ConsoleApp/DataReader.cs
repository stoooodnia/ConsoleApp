using System.Collections.ObjectModel;

namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;


    public class DataReader
    {
        private ICollection<ImportedObjectBaseClass> ImportedObjects { get; set; }
        private Dictionary<Tuple<string, string>, ICollection<ImportedObjectBaseClass>> ObjectsSorted { get; set; }
       
        public void ImportData(string fileToImport)
        {
            ImportObjectsFromFile(fileToImport);
            // ProcessImportedData();
            // AssignNumberOfChildren();
            // PrintData();
            // Console.ReadLine();
        }

        private void PrintData()
        {
            // foreach (var database in ImportedObjects)
            // {
            //     if (database.Type == "DATABASE")
            //     {
            //         Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
            //
            //         // print all database's tables
            //         foreach (var table in ImportedObjects)
            //         {
            //             if (table.ParentType.ToUpper() == database.Type)
            //             {
            //                 if (table.ParentName == database.Name) 
            //                 {
            //                     Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
            //
            //                     // print all table's columns
            //                     foreach (var column in _importedObjects)
            //                     {
            //                         if (column.ParentType.ToUpper() == table.Type)
            //                         {
            //                             if (column.ParentName == table.Name)
            //                             {
            //                                 Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
            //                             }
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //     }
            // } 
        }
        private void ImportObjectsFromFile(string fileToImport)
        {
            // ImportedObjects = new List<ImportedObjectBaseClass>();

            var streamReader = new StreamReader(fileToImport);
            // var importedLines = new List<string>();
            
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                var values = streamReader.ReadLine()?.Split(';');
                // importedLines.Add(line);
                var newObject = new ImportedObject();
                newObject.MapFrom(values);
                // append to collection
                ImportedObjects.Add(newObject);
                // append to dictionary
                AppendToDictionary(newObject);
            }
            // for (int i = 0; i <= importedLines.Count; i++)
            // {
            //     var importedLine = importedLines[i];
            //     var values = importedLine.Split(';');
            //     var importedObject = new ImportedObject();
            //     importedObject.MapFrom(values);
            //     // importedObject.Type = values[0];
            //     // importedObject.Name = values[1];
            //     // importedObject.Schema = values[2];
            //     // importedObject.ParentName = values[3];
            //     // importedObject.ParentType = values[4];
            //     // importedObject.DataType = values[5];
            //     // importedObject.IsNullable = values[6];
            //     ImportedObjects.Add(importedObject);
            // }
        }

        private void AppendToDictionary(ImportedObjectBaseClass newObject)
        {
            var newObjectKey = new Tuple<string, string>(newObject.Type, newObject.Name);
            if (ObjectsSorted.ContainsKey(newObjectKey))
            {
                ObjectsSorted[newObjectKey].Add(newObject);
            }
            else
            {
                ObjectsSorted.Add(newObjectKey, new Collection<ImportedObjectBaseClass>{newObject});
            }
        }
        // private void ProcessImportedData()
        // {
        //     // var aaaa = new ImportedObject
        //     // {
        //     //     Name = null,
        //     //     Type = null,
        //     //     Schema = null,
        //     //     ParentName = null,
        //     //     ParentType = null,
        //     //     DataType = null,
        //     //     IsNullable = null,
        //     //     NumberOfChildren = 0
        //     // };
        //     
        //     // clear and correct imported data
        //     // foreach (var importedObject in ImportedObjects)
        //     // {
        //     //     importedObject.Type = importedObject.Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
        //     //     importedObject.Name = importedObject.Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        //     //     importedObject.Schema = importedObject.Schema.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        //     //     importedObject.ParentName = importedObject.ParentName.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        //     //     importedObject.ParentType = importedObject.ParentType.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        //     // }
        // }
        
        private void AssignNumberOfChildren()
        {
        //     for (int i = 0; i < ImportedObjects.Count(); i++)
        //     {
        //         var importedObject = ImportedObjects.ToArray()[i];
        //         foreach (var impObj in ImportedObjects)
        //         {
        //             if (impObj.ParentType == importedObject.Type)
        //             {
        //                 if (impObj.ParentName == importedObject.Name)
        //                 {
        //                     importedObject.NumberOfChildren = 1 + importedObject.NumberOfChildren;
        //                 }
        //             }
        //         }
        //     }
        }
    }

    public class ImportedObject : ImportedObjectBaseClass
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public double NumberOfChildren { get; set; }

        public override void MapFrom(string[] line)
        {
            // map line values to properties
        }

        public override void Print()
        {
            // print out data
        }
    }

    public abstract class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string ProcessString(string stringToProcess)
        {
            return stringToProcess.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
        public abstract void MapFrom(string[] line);

        public abstract void Print();
    }
}
