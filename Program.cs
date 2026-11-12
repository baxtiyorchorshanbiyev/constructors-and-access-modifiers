internal class Program
{
    private static void Main()
    {

        // -- 1
        Thermostat livingRoom = new Thermostat("Living room", 20, 23);
        Thermostat bedroom = new Thermostat("Beed Room", 24, 21);
        Thermostat office = new Thermostat("Office", 19, 19);
        livingRoom.AdjustOneDegree();
        livingRoom.AdjustOneDegree();
        bedroom.AdjustOneDegree();
        bool isExpire = livingRoom.SetTarget(31);

        Console.WriteLine($"Joriy: {livingRoom.InitialTemperature}, Maqsad: {livingRoom.TargetTemperature}, Holat: {livingRoom.GetStatus()}");
        Console.WriteLine($"Joriy: {bedroom.InitialTemperature}, Maqsad: {bedroom.TargetTemperature}, Holat: {bedroom.GetStatus()}");
        Console.WriteLine($"Joriy: {office.InitialTemperature}, Maqsad: {office.TargetTemperature}, Holat: {office.GetStatus()}");
        Console.WriteLine($"{isExpire}, Maqsad: {livingRoom.TargetTemperature}");

        // -- 2

        Song song1 = new Song("S01", "Focus", 210);
        Song song2 = new Song("S02", "Break", 185);
        Song song3 = new Song("S03", "Review", 240);

        PlayList playlist1 = new PlayList("Ish vaqti", 3);

        Console.WriteLine("S01 qo'shildi: " + playlist1.AddSong(song1));
        Console.WriteLine("S02 qo'shildi: " + playlist1.AddSong(song2));
        Console.WriteLine("S03 qo'shildi: " + playlist1.AddSong(song3));

        Console.WriteLine("S02 qayta qo'shildi: " + playlist1.AddSong(song2));

        Console.WriteLine("2-qo'shiq (1-marta): " + playlist1.PlaySong(1));
        Console.WriteLine("2-qo'shiq (2-marta): " + playlist1.PlaySong(1));


        Console.WriteLine("-1 indeks: " + playlist1.PlaySong(-1));
        Console.WriteLine("3 indeks: " + playlist1.PlaySong(3));

        playlist1.PrintSongs();
        Console.WriteLine("Umumiy davomiylik: " + playlist1.GetTotalDuration() + " soniya");

        // -- 3

        Book b1 = new Book("B01", "C# Asoslari", 3);
        Book b2 = new Book("B02", "Algoritmlar", 2);
        Book b3 = new Book("B03", "Ma'lumotlar bazasi", 1);
        Book b4 = new Book("B04", "Web dasturlash", 4);

        Library library = new Library(4);

        library.AddBook(b1);
        library.AddBook(b2);
        library.AddBook(b3);
        library.AddBook(b4);

        Console.WriteLine("B01 ni qayta qo'shish: " + library.AddBook(b1));

        Console.WriteLine("B01 dan 1-nusxani olish: " + library.BorrowBook("B01"));
        Console.WriteLine("B01 dan 2-nusxani olish: " + library.BorrowBook("B01"));
        Console.WriteLine("B02 dan 1-nusxani olish: " + library.BorrowBook("B02"));
        Console.WriteLine("B02 dan 2-nusxani olish: " + library.BorrowBook("B02"));
        Console.WriteLine("B03 dan 1-nusxani olish: " + library.BorrowBook("B03"));
        Console.WriteLine("B99 dan olishga urinish: " + library.BorrowBook("B99"));

        Console.WriteLine("B01 ni qaytarish: " + library.ReturnBook("B01"));
        Console.WriteLine("B02 ni qaytarish: " + library.ReturnBook("B02"));

        Console.WriteLine("Kutubxonadagi jami mavjud nusxalar soni: " + library.GetAvailableCopies());
        Console.WriteLine();
        library.PrintUnavailableBooks();
    }

    class Thermostat
    {
        private string _roomName;
        private int _initialTemperature;
        private int _targetTemperature;
        public string RoomName => _roomName;
        public int InitialTemperature => _initialTemperature;
        public int TargetTemperature => _targetTemperature;

        public Thermostat(string roomName, int initialTemperature, int targetTemperature)
        {
            _roomName = roomName;
            _initialTemperature = initialTemperature;
            _targetTemperature = targetTemperature;
        }

        public bool SetTarget(int temperature)
        {
            if (temperature >= 16 && temperature <= 30)
            {
                this._targetTemperature = temperature;
                return true;
            }
            return false;
        }

        public void AdjustOneDegree()
        {
            if (this._initialTemperature < this._targetTemperature)
            {
                this._initialTemperature++;
            }
            else if (this._initialTemperature > this._targetTemperature)
            {
                this._initialTemperature--;
            }
        }

        public string GetStatus()
        {
            if (this._initialTemperature < this._targetTemperature)
            {
                return "Heating";
            }
            else if (this._initialTemperature > this._targetTemperature)
            {
                return "Cooling";
            }
            return "At target";
        }

    }

    class Song
    {
        private string _code;
        private string _title;
        private int _durationSeconds;

        private int _playCount = 0;

        public string Code => _code;
        public string Title => _title;
        public int DurationSeconds => _durationSeconds;
        public int PlayCount => _playCount;

        public Song(string code, string title, int durationSeconds)
        {
            _code = code;
            _title = title;
            _durationSeconds = durationSeconds;
        }

        public void Play()
        {
            _playCount++;
        }

        public string GetInfo()
        {
            return $"{_code} - {_title}, {_durationSeconds} soniya, {_playCount} marta ijro etildi";
        }
    }

    class PlayList
    {
        private string _title;
        private int _limit;
        private Song[] _songs;
        private int _count = 0;

        public string Title => _title;

        public PlayList(string title, int limit)
        {
            _title = title;
            _limit = limit;
            _songs = new Song[_limit];
            _count = 0;
        }

        public bool AddSong(Song song)
        {

            if (_count >= _limit)
            {
                return false;
            }

            for (int i = 0; i < _count; i++)
            {
                if (_songs[i].Code == song.Code)
                {
                    return false;
                }
            }
            _songs[_count] = song;
            _count++;

            return true;
        }

        public bool PlaySong(int index)
        {
            if (index >= 0 && index < _count && _songs[index] != null)
            {
                _songs[index].Play();
                return true;
            }
            return false;
        }

        public int GetTotalDuration()
        {
            int totalDuration = 0;
            for (int i = 0; i < _count; i++)
            {
                totalDuration += _songs[i].DurationSeconds;
            }
            return totalDuration;
        }

        public void PrintSongs()
        {
            Console.WriteLine($"Playlist: {_title}");

            if (_count == 0)
            {
                Console.WriteLine("Pleylist bo'sh.");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine($"{i + 1}. {_songs[i].GetInfo()}");
            }
        }
    }

    class Book
    {
        private string _isbn;
        private string _title;
        private int _copies;
        private int _maxCopies;
        public string ISBN => _isbn;
        public string Title => _title;
        public int Copies => _copies;

        public Book(string isbn, string title, int copies)
        {
            _isbn = isbn;
            _title = title;
            _copies = copies;
            _maxCopies = copies;
        }

        public bool Borrow()
        {
            if (_copies > 0)
            {
                _copies--;
                return true;
            }
            return false;
        }

        public bool Return()
        {
            if (_copies < _maxCopies)
            {
                _copies++;
                return true;
            }
            return false;
        }
    }

    class Library
    {
        private Book[] _books;
        private int _count;

        public Library(int capacity)
        {
            _books = new Book[capacity];
            _count = 0;
        }


        public bool AddBook(Book book)
        {

            if (_count >= _books.Length)
            {
                return false;
            }


            for (int i = 0; i < _count; i++)
            {
                if (_books[i].ISBN == book.ISBN)
                {
                    return false;
                }
            }

            // 3. Kitobni qo'shish
            _books[_count] = book;
            _count++;
            return true;
        }

        public bool BorrowBook(string isbn)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_books[i].ISBN == isbn)
                {
                    return _books[i].Borrow();
                }
            }
            return false;
        }

        public bool ReturnBook(string isbn)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_books[i].ISBN == isbn)
                {
                    return _books[i].Return();
                }
            }
            return false;
        }

        public int GetAvailableCopies()
        {
            int totalCopies = 0;
            for (int i = 0; i < _count; i++)
            {
                totalCopies += _books[i].Copies;
            }
            return totalCopies;
        }

        public void PrintUnavailableBooks()
        {
            Console.WriteLine("Nusxasi qolmagan kitoblar:");
            bool found = false;

            for (int i = 0; i < _count; i++)
            {
                if (_books[i].Copies == 0)
                {
                    Console.WriteLine($"ISBN: {_books[i].ISBN}, Sarlavha: {_books[i].Title}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Hozirda barcha kitoblarning nusxalari mavjud.");
            }
        }
    }
}