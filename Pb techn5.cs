using System;
using System.Collections.Generic;
using System.IO; // дає створювати файли на компютері
using System.Text.Json; // підключені бібліотеки


class Program // основний клас
{
    //  ми оголосили  тут тому щоб вони були доступні всім функціям програми
    // це поле класу. Це означає, що EquipmentList є списком об’єктів типу Equipment, який доступний для всіх методів у класі Program
    public static List<Equipment> EquipmentList; // Створення глобального списку обладнання, це поле може бути доступне з будь-якого місця в коді.

    // Функція для збереження даних у файл без порядкового номера
    static void SaveDataToFileWithoutOrdinalNumber(string fileName) // це оголошення методу
    {
        // Читання існуючого списку обладнання з файлу
        string jsonText = File.ReadAllText(@"C://Temp//pobutova technica.txt");
        List<Equipment> equipmentList = JsonSerializer.Deserialize<List<Equipment>>(jsonText);

        // Запис нового списку обладнання у файл
        string jsonTextNew = JsonSerializer.Serialize(equipmentList);
        File.WriteAllText(fileName, jsonTextNew);

        // Виведення повідомлення про успішне збереження
        Console.WriteLine("Дані успішно збережені");
    }

    // Головна функція програми
    static void Main(string[] args) // основна точка входу в програму, Коли запускаєм програму, виконання починається з методу Main.

    {
        string filePath = @"C://Temp//pobutova technica.txt";

        // Перевірка наявності файлу
        if (File.Exists(filePath))
        {
            // Читання даних з файлу, якщо файл не порожній
            if (new FileInfo(filePath).Length > 0)
            {
                string jsonText = File.ReadAllText(filePath);
                EquipmentList = JsonSerializer.Deserialize<List<Equipment>>(jsonText);
            }
            else
            {
                EquipmentList = new List<Equipment>();
            }
        }
        else
        {
            Console.WriteLine("Файл не знайдено. Створюємо новий файл...");
            File.Create(filePath).Close();
            EquipmentList = new List<Equipment>();
        }


        {
                // Вивід головного меню NORM
                Console.WriteLine("Меню");
                Console.WriteLine("1. Виведення даних на екран");
                Console.WriteLine("2. Додавання даних з клавіатури");
                Console.WriteLine("3. Додавання даних з файлу");
                Console.WriteLine("4. Видалення або редагування даних");
                Console.WriteLine("5. Операції з даними (сортування, підсумок)");
                Console.WriteLine("6. Збереження даних у файл");
                Console.WriteLine("7. Вихід");

                // Отримання вибору користувача
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))// створює цикл, який продовжується, поки користувач не введе дійсне ціле число. 
                {
                    Console.WriteLine("Будь ласка, введіть дійсне числове значення для вибору.");
                }

                // Цикл while
                while (choice != 7)
                {
                    // Виведення даних з файлу
                    if (choice == 1)
                    {
                        OutputData();
                    }
                    // Додавання даних з клавіатури
                    else if (choice == 2)
                    {
                        AddDataFromKeyboard();
                    }
                    // Додавання даних з файлу
                    else if (choice == 3)
                    {
                        AddDataFromFile();
                    }
                    // Видалення даних
                    else if (choice == 4)
                    {
                        EditOrDeleteData();
                    }
                    // Операції з даними
                    else if (choice == 5)
                    {
                        OperationsWithData();
                    }
                    // Збереження даних у файл
                    else if (choice == 6)
                    {
                        // Передаємо аргумент `fileName` функції `SaveDataToFile()`
                        SaveDataToFile(@"C://Temp//pobutova technica.txt");
                    }

                    // Вивід головного меню
                    Console.WriteLine("Меню");
                    Console.WriteLine("1. Виведення даних на екран");
                    Console.WriteLine("2. Додавання даних з клавіатури");
                    Console.WriteLine("3. Додавання даних з файлу");
                    Console.WriteLine("4. Видалення або редагування даних");
                    Console.WriteLine("5. Операції з даними (сортування, підсумок)");
                    Console.WriteLine("6. Збереження даних у файл");
                    Console.WriteLine("7. Вихід");

                    // Отримання вибору користувача
                    while (!int.TryParse(Console.ReadLine(), out choice))// створює цикл, який продовжується, поки користувач не введе дійсне ціле число. 
                    {
                        Console.WriteLine("Будь ласка, введіть дійсне числове значення для вибору.");
                    }
                }
            }
        }
    

    
    // Визначення класу Equipment
    //Equipment public означає, що цей клас доступний для всього коду в проекті. Клас - це схема або “синтаксичний цукор” для створення об’єктів, які представляють певну сутніст
    public class Equipment
    {
        // string - це тип даних властивості Name, який вказує, що Name є рядком{ get; set; } - це аксесори, які дозволяють отримувати (get) та встановлювати (set) значення властивості
        public string Name { get; set; }  // Назва обладнання
        public string Manufacturer { get; set; } // Додано нове поле Виробник
        public int Price { get; set; }  // Ціна обладнання
        public int Power { get; set; } // Змінено з string на int Потужність обладнання
        public int OrdinalNumber { get; set; }  // Порядковий номер обладнання
    }

    // 1 Функція для виведення даних NORM
    static void OutputData()
    {
        // Перевірка, чи є обладнання в списку
        if (EquipmentList.Count == 0)
        {
            Console.WriteLine("Список обладнання порожній");
            return;
        }

        // Перевірка наявності файлу
        if (File.Exists(@"C://Temp//pobutova technica.txt"))
        {
            // Читання даних з файлу
            string jsonText = File.ReadAllText(@"C://Temp//pobutova technica.txt");
            List<Equipment> equipmentList = JsonSerializer.Deserialize<List<Equipment>>(jsonText);

            // Виведення даних про кожне обладнання
            foreach (Equipment equipment in equipmentList)// цикл для обходу всіх елементів у списку equipmentList. Кожен елемент у цьому списку є об’єктом типу Equipment.
            {
                int OrdinalNumber = equipment.OrdinalNumber;// порядковий номер обновляється (зверху)
                Console.WriteLine($"Назва: {equipment.Name,-20} Виробник: {equipment.Manufacturer,-20} Ціна: {equipment.Price,-10} Потужність: {equipment.Power,-10} Порядковий номер: {equipment.OrdinalNumber,-5}");
            }
        }
        else
        {
            // Повідомлення про відсутність файлу
            Console.WriteLine("Файл не знайдено");
        }
    }
    // 2 дані з клавіатури NORM
    static void AddDataFromKeyboard()
    {
        string filePath = @"C://Temp//pobutova technica.txt";

        // Перевірка наявності файлу
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не знайдено. Створюємо новий файл...");

            // Створення нового файлу
            File.Create(filePath).Close();

            Console.WriteLine("Файл успішно створено. Будь ласка, внесіть дані.");
            EquipmentList = new List<Equipment>();
        }
        else
        {
            // Читання даних з файлу, якщо файл не порожній
            if (new FileInfo(filePath).Length > 0)
            {
                string jsonText = File.ReadAllText(filePath);
                EquipmentList = JsonSerializer.Deserialize<List<Equipment>>(jsonText);
            }
            else
            {
                EquipmentList = new List<Equipment>();
            }
        }

        // Запит на введення даних для нового об'єкта Equipment
        Console.WriteLine("Введіть назву обладнання:");
        string name = Console.ReadLine();

        // Запит на введення даних для нового об'єкта Equipment (виробника)
        Console.WriteLine("Введіть назву виробника:");
        string manufacturer = Console.ReadLine();

        Console.WriteLine("Введіть ціну обладнання:");
        int price;
        while (!int.TryParse(Console.ReadLine(), out price))
        {
            Console.WriteLine("Будь ласка, введіть дійсне числове значення для ціни.");
        }

        Console.WriteLine("Введіть потужність обладнання:");
        int power;
        while (!int.TryParse(Console.ReadLine(), out power))
        {
            Console.WriteLine("Будь ласка, введіть дійсне числове значення для потужності.");
        }

        // Створення нового об'єкта Equipment
        Equipment equipment = new Equipment
        {
            Name = name,
            Manufacturer = manufacturer,
            Price = price,
            Power = power,
            // Встановлення порядкового номера як поточний розмір списку + 1
            OrdinalNumber = EquipmentList.Count + 1
        };

        // Додавання нового об'єкта Equipment до списку обновлення списку
        EquipmentList.Add(equipment);

        // Серіалізація списку об'єктів Equipment в JSON текст
        string jsonTextNew = JsonSerializer.Serialize(EquipmentList);
        // Запис JSON тексту в файл
        File.WriteAllText(filePath, jsonTextNew);
    }

    // 3 додаємо файл по замовчуванню або надаємо повний шлях NORM
    // static означає, що функція доступна лише в файлі, де вона визначена. void означає, що функція не повертає жодного значення.
    static void AddDataFromFile()
    {
        int fileOption;// оголошення змінної fileOption типу int. Ця змінна ще не ініціалізована, тому її значення не визначено.

        // Це безкінечний цикл, який буде виконуватися до тих пір, поки його не перервуть за допомогою оператора break
        while (true) // додали перевірку на введення за допомогою циклу while і методу int.TryParse()
        {
            // Запит від користувача про вибір файлу
            Console.WriteLine("Виберіть варіант файлу:");
            Console.WriteLine("1. Файл за замовчуванням");
            Console.WriteLine("2. Інший файл");

            // Перевірка введення користувача
            if (!int.TryParse(Console.ReadLine(), out fileOption))
            {
                Console.WriteLine("Будь ласка, введіть число.");
                continue;
            }

            if (fileOption < 1 || fileOption > 2)
            {
                Console.WriteLine("Будь ласка, введіть число від 1 до 2.");
                continue;
            }

            break;
        }

        string filePath;// оголошення змінної
        switch (fileOption)// switch - це тип умовного оператора, який виконує різні дії в залежності від значення змінної або виразу. 
        {
            case 1:
                // Використання файлу за замовчуванням
                filePath = @"C://Temp//dodaj.txt";
                break;

            case 2:
                // Запит повного шляху до іншого файлу
                Console.WriteLine("Введіть повний шлях до файлу:");
                filePath = Console.ReadLine();
                break;

            default:
                // Повідомлення про помилку, якщо вибір невірний
                Console.WriteLine("Невірний вибір");
                return;
        }

        // Перевірка наявності файлу
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не знайдено");
            return;
        }

        // Читання даних з файлу
        string jsonTextNew = File.ReadAllText(filePath);
        List<Equipment> newEquipmentList = JsonSerializer.Deserialize<List<Equipment>>(jsonTextNew);

        // Додавання нових об'єктів Equipment до глобального списку EquipmentList
        foreach (var equipment in newEquipmentList)//конструкція яка використовується для ітерації по всіх елементах колекції newEquipmentList
        {
            equipment.OrdinalNumber = EquipmentList.Count + 1;
            EquipmentList.Add(equipment);
        }

        // Запис оновленого списку EquipmentList у файл
        string jsonTextFinal = JsonSerializer.Serialize(EquipmentList);
        File.WriteAllText(@"C://Temp//pobutova technica.txt", jsonTextFinal);
    }

    //4 Видалення, редагування даних видаляє з підсказкою NORM
    static void EditOrDeleteData()

    {
        // Перевірка, чи існує файл
        if (!File.Exists(@"C://Temp//pobutova technica.txt"))
        {
            Console.WriteLine("Файл не знайдено");
            return;
        }

        // Читання існуючого списку обладнання з файлу
        string jsonText = File.ReadAllText(@"C://Temp//pobutova technica.txt");
        // Десеріалізація JSON тексту в список об'єктів Equipment
        List<Equipment> equipmentList = JsonSerializer.Deserialize<List<Equipment>>(jsonText);
        // Перевірка, чи є обладнання в списку
        if (equipmentList.Count == 0)
        {
            Console.WriteLine("Список обладнання порожній");
            return;
        }

        int index; // оголошення змінної
        while (true)// Це безкінечний цикл, який буде виконуватися до тих пір, поки його не перервуть за допомогою оператора break
        {
            // Запит від користувача про номер елемента, який потрібно видалити або редагувати
            Console.WriteLine("Введіть номер елемента, який потрібно видалити або редагувати:");

            // Перевірка введення користувача
            if (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Будь ласка, введіть число.");
                continue;
            }

            // Перевірка, чи є номер елемента дійсним
            if (index <= 0 || index > equipmentList.Count)//if перевіряє значення змінної index відносно 0 і кількості елементів в equipmentList
            {
                Console.WriteLine("Невірний номер елемента");
                continue;
            }

            break;
        }

        Console.WriteLine("Виберіть дію: 1 - видалення, 2 - редагування");
        int action;// оголосили змінну
        while (!int.TryParse(Console.ReadLine(), out action) || (action != 1 && action != 2))// цикл для перевірки що ввели, робить поки не введем 1 або 2
        {
            Console.WriteLine("Будь ласка, введіть 1 для видалення або 2 для редагування.");
        }

        if (action == 1)// виконуем якщо натиснули 1
        {
            // Видалення елемента зі списку
            equipmentList.RemoveAt(index - 1);

            // Оновлення глобального списку для того щоб нормально виираховувало суму
            EquipmentList = equipmentList;
        }
        else if (action == 2)// виконуем якщо натиснули 2
        {
            // Редагування елемента
            Console.WriteLine("Введіть нову назву обладнання:");
            string newName = Console.ReadLine();

            Console.WriteLine("Введіть нову назву виробника обладнання:");
            string newManufacturer = Console.ReadLine(); // Додано нове поле

            Console.WriteLine("Введіть нову ціну обладнання:");
            int newPrice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введіть нову потужність обладнання:");
            int newPower = Convert.ToInt32(Console.ReadLine()); // Змінено з string на int


            Equipment equipment = equipmentList[index - 1];
            equipment.Name = newName;
            equipment.Manufacturer = newManufacturer; // Додали нове поле
            equipment.Price = newPrice;
            equipment.Power = newPower; // Змінив з string на int
            
        }

        // Оновлення глобального списку для того щоб нормально вираховувало суму
        EquipmentList = equipmentList;

        // Серіалізація оновленого списку об'єктів Equipment в JSON текст
        string jsonTextNew = JsonSerializer.Serialize(EquipmentList);
        // Запис JSON тексту в файл
        File.WriteAllText(@"C://Temp//pobutova technica.txt", jsonTextNew);
    }

    //5 Функція виведення найменшої чи найбільшої ціни, потужності та суми  NORM
    static void OperationsWithData()
{
    // Перевірка наявності обладнання
    if (EquipmentList.Count == 0)
    {
        Console.WriteLine("Список обладнання порожній");
        return;
    }

    int operation = 0;
    while (true)// цикл
    {
            // Вибір операції
            Console.WriteLine("Виберіть операцію:");
            Console.WriteLine("1. Сортувати за ціною від найменшої до найбільшої");
            Console.WriteLine("2. Сортувати за ціною від найбільшої до найменшої");
            Console.WriteLine("3. Сортувати за потужністю від найменшої до найбільшої");
            Console.WriteLine("4. Сортувати за потужністю від найбільшої до найменшої");
            Console.WriteLine("5. Сортувати за виробником (за алфавітом)");
            Console.WriteLine("6. Сортувати за назвою обладнання (за алфавітом)");
            Console.WriteLine("7. Знайти суму");

            // Перевірка введення користувача
            if (!int.TryParse(Console.ReadLine(), out operation))
        {
            Console.WriteLine("Будь ласка, введіть число.");
            continue;
        }

        if (operation < 1 || operation > 7)// введення від 1 до 7
        {
            Console.WriteLine("Будь ласка, введіть число від 1 до 7.");
            continue;
        }

        break;// далі, закінчили цикл
    }

    // Виконання операції
    switch (operation)
    {
        case 1:
        case 2:
            {
                // Сортування списку за ціною
                List<Equipment> sortedList = operation == 1 ?
                    EquipmentList.OrderBy(equipment => equipment.Price).ToList() :
                    EquipmentList.OrderByDescending(equipment => equipment.Price).ToList();

                // Виведення відсортованого списку
                foreach (Equipment equipment in sortedList)//Цикл foreach - це оператор, який використовується для обходу елементів у колекції
                    {
                        Console.WriteLine($"Назва: {equipment.Name,-20} Виробник: {equipment.Manufacturer,-20} Ціна: {equipment.Price,-10} Потужність: {equipment.Power,-10} Порядковий номер: {equipment.OrdinalNumber,-5}");
                    }

                // Запит на збереження відсортованого списку
                Console.WriteLine("Чи бажаєте ви зберегти відсортований список? (y/n)");
                string saveOption = Console.ReadLine();
                if (saveOption.ToLower() == "y")
                {
                    // Збереження відсортованого списку у файл
                    string jsonTextNew = JsonSerializer.Serialize(sortedList);
                    File.WriteAllText(@"C://Temp//pobutova technica.txt", jsonTextNew);
                    Console.WriteLine("Відсортований список успішно збережено");
                }
                break;
            }
        case 3:
        case 4:
            {
                // Сортування за потужністю
                List<Equipment> sortedList = operation == 3 ?
                    EquipmentList.OrderBy(equipment => equipment.Power).ToList() :
                    EquipmentList.OrderByDescending(equipment => equipment.Power).ToList();

                // Виведення відсортованого списку
                foreach (Equipment equipment in sortedList)// Цикл foreach - це оператор, який використовується для обходу елементів у колекції
                    {
                        Console.WriteLine($"Назва: {equipment.Name,-20} Виробник: {equipment.Manufacturer,-20} Ціна: {equipment.Price,-10} Потужність: {equipment.Power,-10} Порядковий номер: {equipment.OrdinalNumber,-5}");
                    }

                // Запит на збереження відсортованого списку
                Console.WriteLine("Чи бажаєте ви зберегти відсортований список? (y/n)");
                string saveOption = Console.ReadLine();
                if (saveOption.ToLower() == "y")
                {
                    // Збереження відсортованого списку у файл
                    string jsonTextNew = JsonSerializer.Serialize(sortedList);
                    File.WriteAllText(@"C://Temp//pobutova technica.txt", jsonTextNew);
                    Console.WriteLine("Відсортований список успішно збережено");
                }
                break;
            }
            case 5:
                {
                    // Сортування за виробником
                    List<Equipment> sortedList = EquipmentList.OrderBy(equipment => equipment.Manufacturer).ToList();

                    // Виведення відсортованого списку
                    foreach (Equipment equipment in sortedList)// Цикл foreach - це оператор, який використовується для обходу елементів у колекції
                    {
                        Console.WriteLine($"Назва: {equipment.Name,-20} Виробник: {equipment.Manufacturer,-20} Ціна: {equipment.Price,-10} Потужність: {equipment.Power,-10} Порядковий номер: {equipment.OrdinalNumber,-5}");
                    }

                    // Запит на збереження відсортованого списку
                    Console.WriteLine("Чи бажаєте ви зберегти відсортований список? (y/n)");
                    string saveOption = Console.ReadLine();
                    if (saveOption.ToLower() == "y")
                    {
                        // Збереження відсортованого списку у файл
                        string jsonTextNew = JsonSerializer.Serialize(sortedList);
                        File.WriteAllText(@"C://Temp//pobutova technica.txt", jsonTextNew);
                        Console.WriteLine("Відсортований список успішно збережено");
                    }
                    break;
                }
            case 6:
                {
                    // Сортування за назвою обладнання
                    List<Equipment> sortedList = EquipmentList.OrderBy(equipment => equipment.Name).ToList();

                    // Виведення відсортованого списку
                    foreach (Equipment equipment in sortedList)
                    {
                        Console.WriteLine($"Назва: {equipment.Name,-20} Виробник: {equipment.Manufacturer,-20} Ціна: {equipment.Price,-10} Потужність: {equipment.Power,-10} Порядковий номер: {equipment.OrdinalNumber,-5}");
                    }

                    // Запит на збереження відсортованого списку
                    Console.WriteLine("Чи бажаєте ви зберегти відсортований список? (y/n)");
                    string saveOption = Console.ReadLine();
                    if (saveOption.ToLower() == "y")
                    {
                        // Збереження відсортованого списку у файл
                        string jsonTextNew = JsonSerializer.Serialize(sortedList);
                        File.WriteAllText(@"C://Temp//pobutova technica.txt", jsonTextNew);
                        Console.WriteLine("Відсортований список успішно збережено");
                    }
                    break;
                }
            case 7:
            {
                // Знаходження суми
                int sum = 0;
                foreach (Equipment equipment in EquipmentList)//Цикл foreach - це оператор, який використовується для обходу елементів у колекції
                    {
                    sum += equipment.Price;
                }

                // Виведення результату
                Console.WriteLine("Сума:");
                Console.WriteLine(sum);
                break;
            }
        default:
            {
                // Помилка
                Console.WriteLine("Невірний вибір");
                break;
            }
    }
}
//6 Функція `SaveDataToFile() зберігається або по замовчуванню або вводимо повний шлях NORM

static void SaveDataToFile(string fileName)
    {
        int saveOption = 0;
        while (true)
        {
            // Вибір варіанту збереження
            Console.WriteLine("Виберіть варіант збереження:");
            Console.WriteLine("1. За замовчуванням");
            Console.WriteLine("2. У файл з іншою назвою");

            // Перевірка введення користувача
            if (!int.TryParse(Console.ReadLine(), out saveOption))
            {
                Console.WriteLine("Будь ласка, введіть число.");
                continue;
            }

            if (saveOption < 1 || saveOption > 2)
            {
                Console.WriteLine("Будь ласка, введіть число від 1 до 2.");
                continue;
            }

            break;
        }

        // Збереження даних
        // Вираз в дужках після switch обчислюється Значення цього виразу порівнюється з виразами в операторах case, знайдено відповідність, виконується код
        switch (saveOption)
        {
            case 1:
                // Збереження за замовчуванням
                File.WriteAllText(fileName, JsonSerializer.Serialize(EquipmentList));
                break;

            case 2:
                // Збереження у файл з іншою назвою
                Console.WriteLine("Введіть повний шлях до файлу для збереження:");
                string newFileName = Console.ReadLine();
                File.WriteAllText(newFileName, JsonSerializer.Serialize(EquipmentList));
                break;

            default:
                // Помилка
                Console.WriteLine("Невірний вибір");
                break;
        }
    }
}
