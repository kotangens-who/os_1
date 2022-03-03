using System;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Text.Json;
using System.Xml.Serialization;

namespace os
{
    [Serializable]
    public class Car//объявление класса
    {
        public string Marka; //марка 
        public string Cvet; //цвет 
        public string Model; //модель 
        public string Vypusk; //год
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            bool humus = true;
            while (humus)
            {
                Console.WriteLine("Выберите функцию программы:");
                Console.WriteLine("Выйти из программы - 0");
                Console.WriteLine("Вывести информацию в консоль о логических дисках, именах, метке тома, размере и типе файловой системы - 1");
                Console.WriteLine("Работа с файлами - 2");
                Console.WriteLine("Работа с форматом JSON - 3");
                Console.WriteLine("Работа с форматом XML - 4");
                Console.WriteLine("Работа с zip архивами - 5");
                string vbr = Console.ReadLine();

                switch (vbr)
                {
                    case "0":
                        {
                            humus = false;
                            break;
                        }
                    case "1":
                        {
                            DriveInfo[] drives = DriveInfo.GetDrives();

                            foreach (DriveInfo drive in drives)
                            {
                                Console.WriteLine($"Название: {drive.Name}");
                                Console.WriteLine($"Тип: {drive.DriveType}");
                                if (drive.IsReady)
                                {
                                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                    case "2":
                        {
                            string path = "C:\\Users\\student\\source\\repos\\os_1\\test\\";
                            Console.WriteLine("Выберите функцию:");
                            Console.WriteLine("Выйти в главное меню - 0");
                            Console.WriteLine("Создать файл - 1");
                            Console.WriteLine("Записать в файл строку, введённую пользователем - 2");
                            Console.WriteLine("Прочитать файл в консоль - 3");
                            Console.WriteLine("Удалить файл - 4");
                            string vbr1 = Console.ReadLine();
                            switch (vbr1)
                            {
                                case "0":
                                    {
                                        break;
                                    }
                                case "1":
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                                        if (!dirInfo.Exists)
                                        {
                                            dirInfo.Create();
                                        }
                                        using (FileStream fstream = new FileStream($"{path}\\note.txt", FileMode.Create))
                                        {
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                                        if (!dirInfo.Exists)
                                        {
                                            dirInfo.Create();
                                        }
                                        Console.WriteLine("Введите строку для записи в файл:");
                                        string text = Console.ReadLine();


                                        using (FileStream fstream = new FileStream($"{path}\\note.txt", FileMode.OpenOrCreate))
                                        {
                                            byte[] array = System.Text.Encoding.Default.GetBytes(text);

                                            await fstream.WriteAsync(array, 0, array.Length);
                                            Console.WriteLine("Текст записан в файл");
                                        }
                                        break;
                                    }
                                case "3":
                                    {
                                        FileInfo fInfo = new FileInfo($"{path}\\note.txt");
                                        if (fInfo.Exists)
                                        {
                                            using (FileStream fstream = File.OpenRead($"{path}\\note.txt"))
                                            {
                                                byte[] array = new byte[fstream.Length];

                                                await fstream.ReadAsync(array, 0, array.Length);

                                                string textFromFile = System.Text.Encoding.Default.GetString(array);
                                                Console.WriteLine($"Текст из файла: {textFromFile}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                                case "4":
                                    {
                                        FileInfo f1Info = new FileInfo($"{path}\\note.txt");
                                        if (f1Info.Exists)
                                        {
                                            FileInfo fileInf = new FileInfo($"{path}\\note.txt");
                                            if (fileInf.Exists)
                                            {
                                                fileInf.Delete();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Неверное значение");
                                    break;

                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Выберите функцию:");
                            Console.WriteLine("Выйти в главное меню - 0");
                            Console.WriteLine("Создать новый объект. Выполнить сериализацию объекта в формате JSON и записать в файл. - 1");
                            Console.WriteLine("Прочитать файл в консоль - 2");
                            Console.WriteLine("Удалить файл - 3");
                            string vbr2 = Console.ReadLine();
                            switch (vbr2)
                            {
                                case "0":
                                    {
                                        break;
                                    }
                                case "1":
                                    {
                                        using (FileStream fs = new FileStream("C:\\Users\\student\\source\\repos\\os_1\\test\\user.json", FileMode.OpenOrCreate))
                                        {
                                            Console.WriteLine("Введите имя:");
                                            string name = Console.ReadLine();
                                            Console.WriteLine("Введите возраст:");
                                            int age = Convert.ToInt32(Console.ReadLine());
                                            Person tom = new Person() { Name = name, Age = age };
                                            await JsonSerializer.SerializeAsync<Person>(fs, tom);
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        FileInfo f1Info = new FileInfo("C:\\Users\\student\\source\\repos\\os_1\\test\\user.json");
                                        if (f1Info.Exists)
                                        {
                                            using (FileStream fs = new FileStream("C:\\Users\\student\\source\\repos\\os_1\\test\\user.json", FileMode.OpenOrCreate))
                                            {
                                                Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                                                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                                case "3":
                                    {
                                        FileInfo f1Info = new FileInfo("C:\\Users\\student\\source\\repos\\os_1\\test\\user.json");
                                        if (f1Info.Exists)
                                        {
                                            FileInfo fileInf = new FileInfo("C:\\Users\\student\\source\\repos\\os_1\\test\\user.json");
                                            if (fileInf.Exists)
                                            {
                                                fileInf.Delete();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Неверное значение");
                                    break;
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Выберите функцию:");
                            Console.WriteLine("Выйти в главное меню - 0");
                            Console.WriteLine("Создать файл формате XML из редактора - 1");
                            Console.WriteLine("Записать в файл новые данные из консоли - 2");
                            Console.WriteLine("Прочитать файл в консоль - 3");
                            Console.WriteLine("Удалить файл - 4");
                            string vbr3 = Console.ReadLine();
                            switch (vbr3)
                            {
                                case "0":
                                    {
                                        break;
                                    }
                                case "1":
                                    {
                                        using (FileStream fstream = new FileStream("C:\\Users\\student\\source\\repos\\os_1\\test\\car.xml", FileMode.Create))
                                        {
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        Car avto = new Car(); //объявляем avto экземпляром класса
                                        Console.WriteLine("Введите данные автомобиля");
                                        Console.Write("Марка: ");
                                        avto.Marka = Console.ReadLine(); //считывает марку авто
                                        Console.Write("Модель: ");
                                        avto.Model = Console.ReadLine(); //считывает модель авто
                                        Console.Write("Цвет: ");
                                        avto.Cvet = Console.ReadLine(); //считывает цвет авто
                                        Console.Write("Год выпуска: ");
                                        avto.Vypusk = Console.ReadLine(); //считывает год выпуска авто
                                        StreamWriter writer = new StreamWriter("C:\\Users\\student\\source\\repos\\os_1\\test\\car.xml");
                                        XmlSerializer serializer = new XmlSerializer(typeof(Car));
                                        serializer.Serialize(writer, avto);
                                        writer.Close();
                                        break;
                                    }
                                case "3":
                                    {
                                        Car avto = new Car(); //объявляем avto экземпляром класса
                                        Stream streamout = new FileStream("C:\\Users\\student\\source\\repos\\os_1\\test\\car.xml", FileMode.Open, FileAccess.Read);
                                        XmlSerializer xml = new XmlSerializer(typeof(Car));
                                        avto = (Car)xml.Deserialize(streamout);
                                        streamout.Close();
                                        Console.WriteLine("Марка  " + "Модель  " + "Цвет  " + "Год выпуска  ");
                                        Console.WriteLine(avto.Marka + "    " + avto.Model + "    " + avto.Cvet + "    " + avto.Vypusk);
                                        break;
                                    }
                                case "4":
                                    {
                                        FileInfo f1Info = new FileInfo("C:\\Users\\student\\source\\repos\\os_1\\test\\car.xml");
                                        if (f1Info.Exists)
                                        {
                                            FileInfo fileInf = new FileInfo("C:\\Users\\student\\source\\repos\\os_1\\test\\car.xml");
                                            if (fileInf.Exists)
                                            {
                                                fileInf.Delete();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Неверное значение");
                                    break;
                            }
                            break;
                        }
                    case "5":
                        {
                            string zipfolder = "C:\\Users\\student\\source\\repos\\os_1\\test\\0fold/";
                            string sourceFolder = "C:\\Users\\student\\source\\repos\\os_1\\test\\"; // исходная папка
                            string arhiv = "C:\\Users\\student\\source\\repos\\os_1\\test\\test.zip";
                            string targetFolder = "C:\\Users\\student\\source\\repos\\os_1\\test\\unziped"; // папка, куда распаковывается файл
                            Console.WriteLine("Выберите функцию:");
                            Console.WriteLine("Выйти в главное меню - 0");
                            Console.WriteLine("Создать архив в формате zip - 1");
                            Console.WriteLine("Добавить файл, выбранный пользователем, в архив - 2");
                            Console.WriteLine("Разархивировать файл и вывести данные о нем - 3");
                            Console.WriteLine("Удалить файл и архив - 4");
                            string vbr1 = Console.ReadLine();
                            switch (vbr1)
                            {
                                case "0":
                                    {
                                        break;
                                    }
                                case "1":
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(sourceFolder);
                                        if (!dirInfo.Exists)
                                        {
                                            dirInfo.Create();
                                        }
                                        using (FileStream fstream = new FileStream($"{sourceFolder}\\test.zip", FileMode.Create))
                                        {
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(zipfolder);
                                        if (!dirInfo.Exists)
                                        {
                                            dirInfo.Create();
                                        }
                                        Console.WriteLine("Введите название файла, который хотите заархивировать:");
                                        string zfile = Console.ReadLine();
                                        string z1file = "C:\\Users\\student\\source\\repos\\os_1\\test\\" + zfile;
                                        FileInfo fileInf2 = new FileInfo(z1file);
                                        if (fileInf2.Exists)
                                        {
                                            fileInf2.MoveTo(zipfolder + zfile);
                                        }
                                        ZipFile.CreateFromDirectory(zipfolder, "C:\\Users\\student\\source\\repos\\os_1\\test\\test1.zip");


                                        break;
                                    }
                                case "3":
                                    {
                                        FileInfo fileInf2 = new FileInfo(arhiv);
                                        if (fileInf2.Exists)
                                        {
                                            DirectoryInfo dirInfo = new DirectoryInfo(targetFolder);
                                            if (!dirInfo.Exists)
                                            {
                                                dirInfo.Create();
                                            }
                                            ZipFile.ExtractToDirectory("C:\\Users\\student\\source\\repos\\os_1\\test\\test1.zip", targetFolder);

                                            FileInfo fInfo = new FileInfo(targetFolder + "/note.txt");
                                            if (fInfo.Exists)
                                            {
                                                using (FileStream fstream = File.OpenRead(targetFolder + "/note.txt"))
                                                {
                                                    byte[] array = new byte[fstream.Length];

                                                    await fstream.ReadAsync(array, 0, array.Length);

                                                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                                                    Console.WriteLine($"Текст из файла: {textFromFile}");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Файл не существует");
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }

                                        break;
                                    }
                                case "4":
                                    {
                                        Console.WriteLine("Введите название архива, который хотите удалить:");
                                        string delzip = Console.ReadLine();
                                        delzip = "C:\\Users\\student\\source\\repos\\os_1\\test\\" + delzip;
                                        FileInfo fileInf = new FileInfo(delzip);
                                        if (fileInf.Exists)
                                        {
                                            fileInf.Delete();
                                        }

                                        else
                                        {
                                            Console.WriteLine("Архив не существует");
                                        }
                                        break;
                                    }
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Неверное значение");
                                    break;
                            }

                            break;
                        }
                }
            }
        }
    }
}