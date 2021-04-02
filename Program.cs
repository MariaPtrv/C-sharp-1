using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace Lab1
{
    class Program
    {
        public static void ChangeB()
        {
            bool fl = true;
            Console.WriteLine("Выберите цвет фона:\n1 – Черный\n2 – Белый\n3 – Голубой\n0 – Вернуться назад");
            while (fl)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': fl = false; Console.WriteLine("\n\n"); break;
                    case '1': 
                        Console.BackgroundColor = ConsoleColor.Black; 
                        Console.Clear(); 
                        Console.WriteLine("Выберите цвет фона:\n1 – Черный\n2 – Белый\n3 – Голубой\n0 – Вернуться назад"); 
                        break;
                    case '2': 
                        Console.BackgroundColor = ConsoleColor.White; 
                        Console.Clear(); 
                        Console.WriteLine("Выберите цвет фона:\n1 – Черный\n2 – Белый\n3 – Голубой\n0 – Вернуться назад"); 
                        break;
                    case '3': 
                        Console.BackgroundColor = ConsoleColor.DarkCyan; 
                        Console.Clear(); 
                        Console.WriteLine("Выберите цвет фона:\n1 – Черный\n2 – Белый\n3 – Голубой\n0 – Вернуться назад"); 
                        break;
                    default: break;
                }

            }
        }

        public static void ChangeF()
        {
            bool fl = true;
            Console.WriteLine("Выберите цвет шрифта:\n1 – Темно-серый\n2 – Светло-серый \n0 – Вернуться назад");
            while (fl)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': fl = false; Console.WriteLine("\n\n"); break;
                    case '1':
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Clear();
                        Console.WriteLine("Выберите цвет шрифта:\n1 – Темно-серый\n2 – Светло-серый \n0 – Вернуться назад");
                        break;
                    case '2':
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Clear();
                        Console.WriteLine("Выберите цвет шрифта:\n1 – Темно-серый\n2 – Светло-серый \n0 – Вернуться назад");
                        break;
                    default: break;
                }
            }
        }

        public static void ChangeConsoleView() {
            bool fl = true;
            Console.Clear();
            Console.WriteLine("Вы хотите:\n1 – Поменять цвет фона\n2 – Поменять цвет шрифта\n0 – Вернуться назад");
            while (fl)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': fl = false; Console.WriteLine("\n"); break;
                    case '1': 
                        ChangeB();
                        Console.WriteLine("Вы хотите:\n1 – Поменять цвет фона\n2 – Поменять цвет шрифта\n0 – Вернуться назад"); 
                        break;
                    case '2' : 
                        ChangeF();
                        Console.WriteLine("Вы хотите:\n1 – Поменять цвет фона\n2 – Поменять цвет шрифта\n0 – Вернуться назад"); 
                        break;
                    default: break;
                }

            }
        }
        public static void ShowAllTypeInfo() {
            int nRefTypes=0; int nValueTypes=0; int nInterfaceTypes = 0; 
            String maxMethods=""; int maxM = 0;
            String longName ="";
            String maxParam=""; int maxP = 0;
            Assembly myAsm = Assembly.GetExecutingAssembly();
            Type[] thisAssemblyTypes = myAsm.GetTypes();
            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            foreach (Assembly asm in refAssemblies) types.AddRange(asm.GetTypes());
            foreach (var t in types)
            {
                if (t.IsClass) nRefTypes++;
                else if (t.IsValueType) nValueTypes++;
                else if (t.IsInterface) nInterfaceTypes++;
                if (t.GetMethods().Length > maxM) { maxM = t.GetMethods().Length; maxMethods = t.FullName; }
                if (t.FullName.Length > longName.Length) longName = t.FullName;
                foreach (var i in t.GetMethods())
                {
                    if (i.GetParameters().Length > maxP) 
                    {
                        maxParam = i.Name;
                        maxP = i.GetParameters().Length;
                    }
                }
            }

            Console.WriteLine("Общая информация по типам");
            Console.WriteLine($"Подключенные сборки: {refAssemblies.Length}");
            Console.WriteLine($"Всего типов по всем подключенным сборкам: {nRefTypes+nInterfaceTypes+nValueTypes}");
            Console.WriteLine($"Ссылочные типы:  {nRefTypes}");
            Console.WriteLine($"Значимые типы:  {nValueTypes}");
            Console.WriteLine($"Типы-интерфейсы:  {nInterfaceTypes}");
            Console.WriteLine($"Тип с максимальным числом методов:  {maxMethods}");
            Console.WriteLine($"Самое длинное название метода:  {longName} {longName.Length}");
            Console.WriteLine($"Метод с наибольшим числом аргументов:  {maxParam}");
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню");
            switch (char.ToLower(Console.ReadKey(true).KeyChar))
            {
                default: break;
            }
        }
        public static void ShowInfoForThisType(Type t)
        {
            MethodInfo[] allMethods = t.GetMethods();
            var overloads = new System.Collections.Generic.Dictionary<string, int>();
            var minpar = new System.Collections.Generic.Dictionary<string, int>();
            var maxpar = new System.Collections.Generic.Dictionary<string, int>();
            int min; int max;
            string infoAfter = "Нажмите ‘M’ для вывода дополнительной информации по методам:\nНажмите ‘0’ для выхода в главное меню\n";
            string sFieldNames; string sPropertiesNames;
            Console.WriteLine("Информация по типу: {0}", t.FullName);
            if (t.IsValueType) Console.WriteLine("Значимый тип: +");
            else Console.WriteLine("Значимый тип: -");
            Console.WriteLine("Пространство имен: {0}", t.Namespace);
            Console.WriteLine("Сборка: {0}", t.Assembly.GetName().Name);
            Console.WriteLine("Общее число элементов: {0}", t.GetMembers().Length);
            foreach (var m in allMethods)
            {

                if (overloads.ContainsKey(m.Name))
                // в словаре уже есть такое имя, обновляем статистику
                {
                    overloads[m.Name]++;
                    min = minpar[m.Name];
                    max = maxpar[m.Name];
                    if (m.GetParameters().Length > max) maxpar[m.Name] = m.GetParameters().Length;
                    if (m.GetParameters().Length < min) minpar[m.Name] = m.GetParameters().Length;
                }
                else
                // в словаре нет такого имени, добавляем элемент
                {
                    overloads.Add(m.Name, 1);
                    minpar.Add(m.Name, m.GetParameters().Length);
                    maxpar.Add(m.Name, m.GetParameters().Length);
                }
            }
                Console.WriteLine("Число методов: {0}", overloads.Count);
            
            Console.WriteLine("Число свойств: {0}", t.GetProperties().Length);
            Console.WriteLine("Число полей: {0}", t.GetFields().Length);
            string[] fieldNames = new string[t.GetFields().Length];
            for (int i = 0; i < t.GetFields().Length; i++)
            fieldNames[i] = t.GetFields()[i].Name;
            sFieldNames = String.Join(", ", fieldNames);
            if (t.GetFields().Length > 0)  Console.WriteLine("Список полей: {0}", sFieldNames);
            else Console.WriteLine("Список полей: -");

            string[] propertiesNames = new string[t.GetProperties().Length];
            for (int i = 0; i < t.GetProperties().Length; i++)
                propertiesNames[i] = t.GetProperties()[i].Name;
            sPropertiesNames = String.Join(", ", propertiesNames);
            if (t.GetProperties().Length > 0) Console.WriteLine("Список свойств: {0}", sPropertiesNames);
            else Console.WriteLine("Список свойств: -");
            Console.WriteLine("\n\n");
            Console.WriteLine(infoAfter);

                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case 'm':
                        {
                            Console.WriteLine("Методы типа {0}", t.FullName);

                        //foreach (var m in allMethods)
                        //{
                        //    if (overloads.ContainsKey(m.Name))
                        //    // в словаре уже есть такое имя, обновляем статистику
                        //    {
                        //        overloads[m.Name]++;
                        //        min = minpar[m.Name];
                        //        max = maxpar[m.Name];
                        //        if (m.GetParameters().Length > max) maxpar[m.Name] = m.GetParameters().Length;
                        //        if (m.GetParameters().Length < min) minpar[m.Name] = m.GetParameters().Length;
                        //    }
                        //    else
                        //    // в словаре нет такого имени, добавляем элемент
                        //    {
                        //        overloads.Add(m.Name, 1);
                        //        minpar.Add(m.Name, m.GetParameters().Length);
                        //        maxpar.Add(m.Name, m.GetParameters().Length);
                        //    }
                       // }

                    string sName = "Название"; string sOver = "Число перегрузок"; string sParam = "Число параметров";
                        Console.WriteLine($"{sName.PadRight(25, ' ')}{sOver.PadRight(20, ' ')}{sParam}");
                        foreach (var i in overloads) {
                                if (minpar[i.Key].Equals(maxpar[i.Key])) Console.WriteLine($"{i.Key.PadRight(25,' ')}{i.Value.ToString().PadRight(20, ' ')}{minpar[i.Key]}");
                                else Console.WriteLine($"{i.Key.PadRight(25, ' ')}{i.Value.ToString().PadRight(20, ' ')}{minpar[i.Key]}..{maxpar[i.Key]}");
                            }
                            break;
                        }
                    case '0': break;
               
            }

        }
        public static void SelectType() {
            
            string info = "Информация по типам\nВыберите тип:\n----------------------------------------\n1 – uint\n2 – int\n3 – long\n4 – float\n5 – double\n6 – char\n7 - string\n8 – Vector\n9 – Matrix\n0 – Выход в главное меню\n\n";
            bool fl1 = true;
            Console.WriteLine("\n\n{0}",info);
            while (fl1)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0':
                        fl1 = false; Console.WriteLine("\n"); break;
                    case '1':ShowInfoForThisType(typeof(uint)); Console.WriteLine("\n{0}", info);
                        break;
                    case '2': ShowInfoForThisType(typeof(int)); Console.WriteLine("\n{0}", info);
                        break;
                    case '3': ShowInfoForThisType(typeof(long)); Console.WriteLine("\n{0}", info);
                        break;
                    case '4': ShowInfoForThisType(typeof(float)); Console.WriteLine("\n{0}", info);
                        break;
                    case '5': ShowInfoForThisType(typeof(double)); Console.WriteLine("\n{0}", info);
                        break;
                    case '6': ShowInfoForThisType(typeof(char)); Console.WriteLine("\n{0}", info);
                        break;
                    case '7': ShowInfoForThisType(typeof(string)); Console.WriteLine("\n{0}", info);
                        break;
                    case '8': ShowInfoForThisType(typeof(System.Numerics.Vector)); Console.WriteLine("\n{0}", info);
                        break;
                    case '9': ShowInfoForThisType(typeof(Matrix)); Console.WriteLine("\n{0}", info);
                        break;
                    default: break;
                }
            }
        }

        static void Main(string[] args)
        {
            bool fl = true; 
            Console.WriteLine("Информация по типам:\n1 – Общая информация по типам\n2 – Выбрать тип из списка\n3 – Параметры консоли\n0 - Выход из программы");
            while (fl)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0' : 
                        fl = false; 
                        break;
                    case '1' : 
                        ShowAllTypeInfo(); 
                        Console.WriteLine("Информация по типам:\n1 – Общая информация по типам\n2 – Выбрать тип из списка\n3 – Параметры консоли\n0 - Выход из программы"); 
                        break;
                    case '2' :
                        SelectType();
                        Console.WriteLine("Информация по типам:\n1 – Общая информация по типам\n2 – Выбрать тип из списка\n3 – Параметры консоли\n0 - Выход из программы");
                        break;
                    case '3' : 
                        ChangeConsoleView(); 
                        Console.WriteLine("Информация по типам:\n1 – Общая информация по типам\n2 – Выбрать тип из списка\n3 – Параметры консоли\n0 - Выход из программы"); 
                        break;
                    default: break;
                }
            }
        }
    }
}
