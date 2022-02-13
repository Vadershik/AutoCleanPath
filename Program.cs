using System;
using System.IO;

/* Всем драсьте,это AutoCleanPath. 
* Он нужен для быстрой сортировки по папкам файлов если вам лень это делать
* Программу старался написать как можно более читаемо и просто
* Ну и естественно без г-кода)
* Думаю она сможет хотя бы пару людям помочь.
* Написал данное чудо спонтанно. Даже не думал что всё выйдет.
* Появилась данная идея когда смотрел недо-программиста Гошу Дударя который показывал 3 проекта на питоне
* за 5 минут. После этого захотелось повторить 1 его проект,но уже на C#
* Если у вас есть идеи как можно упростить программу или дополнить,то пишите или кидайте запрос.
* Г-код не предлагать. Это мой хлеб.
* Ну и это в принципе всё. Всем до связи.
*/

namespace AutoCleanPath
{
    public class Program
    {
        static string path = @""; //Путь к папке(папа,вернись пожалуйста)
        static DirectoryInfo dir; //Класс для работы с папкой( ну пап((( )
        static FileInfo[] Files; //Класс с файлами в папке
        static string sourcefile = @""; //Путь к файлу
        static string destinationfile = @""; //Куда переносится файл
        static string typefile = ""; //Тип файла в зависимости от которого и переносится файл в ту или иную папку

        //Дальше идут типы файлов,я перечислил самый частые, вы можете дополнить если желаете
        static string[] phototype = {"jpg","png","psd","jpeg","gif","raw","tiff","jp2","ico"};
        static string[] videotype = {"mp4","avi","mov","wmv","mpeg","mkv","3gp"};
        static string[] audiotype = {"mp3","ogg","wav","ape","flac","wma"};

        static void Main(string[] args)
        {
            Console.WriteLine("Здраствуйте.");
            Console.WriteLine("Введите путь в котором нужно отсортировать файлы по папкам:");
            path = Console.ReadLine(); //Получаем путь
            //Проверяем введён ли путь
            if(CheckPathTrue(path)) Console.WriteLine("Ошибка: вы не ввели путь,перезапустите и попробуйте снова");
            //Если да,тогда присваиваем его
            else dir = new DirectoryInfo(path);
            //Начинаем сортировку по папкам
            Sorting();
        }

        static void Sorting() {
            if(!dir.Exists) { //Проверка есть ли такая директори
                Console.WriteLine("Ошибка: вы не ввели путь,перезапустите и попробуйте снова"); //Если нет,то кидаем ошибку
            }
            //Тут делаем папки куда убираем файлы
            dir.CreateSubdirectory("Photo");
            dir.CreateSubdirectory("Video");
            dir.CreateSubdirectory("Files");
            dir.CreateSubdirectory("Audio");

            //Получаем файлы в директории
            Files = dir.GetFiles();
            //Проверяем каждый тип файла
            for(int i = 0; i < Files.Length; i++) {
                sourcefile = $"{Files[i]}"; //Получаем путь файла
                string type = Files[i].Name.Split('.')[1];
                for(int j = 0; j < phototype.Length; j++) {
                    if(phototype[j]==type) typefile = "Photo";
                }
                for(int j = 0; j < videotype.Length; j++) {
                    if(videotype[j]==type) typefile = "Video";
                }
                for(int j = 0; j < audiotype.Length; j++) {
                    if(audiotype[j]==type) typefile = "Audio";
                }
                if(typefile=="") typefile="Files";

                //После этих махинаций переносим файл в тот,или иной каталог который ему попался
                destinationfile = $"{path}/{typefile}/{Files[i].Name}";
                File.Move(sourcefile, destinationfile); //Вот это само перемещение файла
            }
        }
        //Это ненужная хрень,но пускай будет. По скольку как говорил Конфуций Шарпович
        //
        // "Если работает - не трогай"
        //(C) Конфуций Шарпович 500 года до н.э.
        static bool CheckPathTrue(string path) {
            return path=="";
        }
    }
}
