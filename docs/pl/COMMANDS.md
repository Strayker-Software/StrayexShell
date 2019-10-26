# Komendy Strayex Shell

## Strayex Shell for Windows

- _hello_ - Wypisuje na ekranie powitanie,
Brak argumentów

- _clear_ - Czyści terminal,
Brak argumentów

- echo - Wyświetla dane w terminalu,
Ta komenda może wyświetlić dane z góry zdefiniowane:
`echo Hi!` wypisze "Hi!" w terminalu,
`echo Hi there!` wypisze "Hi there!" w terminalu,
Jednak może posłużyć do wyświetlenia wartości zmiennych sesji powłoki:
Gdy zmienna o nazwie "MyString" ma wartość "Hi!" stosuje się w argumencie na początku znak Dolara, potem nazwę zmiennej:
`echo $MyString` wypisze "Hi!" w terminalu,
Do obsługi skryptów można wyłączyć komendę za pomocą:
`echo /off`
By ją włączyć:
`echo /on`
Pomoc uzyskuje się komendą:
`echo help`

- _cd_ - Zmienia aktualne środowisko pracy powłoki,
Każda komenda wykonywana przez powłokę odbywa się w jej aktywnym środowisku, czyli danej ścieżce systemu plików.
Domyślnie Strayex Shell przyjmuje startowe środowisko jako folder uruchomienia powłoki.
Przykładowo, by ustawić środowisko działania na partycję C należy zastosować:
`cd C:\`

- _help_ - Wyświetla wszystkie komendy powłoki,
Brak argumentów

- _color_ - Zmienia kolor terminala oraz czcionki,
Gdy do tej komendy zostanie podany jeden argument, powłoka zinterpretuje go jako zmianę koloru tła:
`color Black`
Jednak kiedy będą podane dwa argumenty, pierwszy bedzie kolorem tła, drugi czcionki:
`color Black Green`
Aby przywrócić domyślne ustawienia należy użyć:
`color reset`
Obsługiwane kolory:
- Dla tła - czarny, niebieski, zielony, biały,
- Dla czcionki - czarny, niebieski, zielony, biały,

- _set_ - Tworzy zmienne środowiska,
Zmienne powłoki to "pojemniki" w pamięci, służące do trzymania wartości. Tworzy je się podając w pierwszym argumencie nazwę oraz w drugim wartość.
Wartość może być typu ciągu znaków lub liczby całkowitej.
`set MyString Hi!` tworzy zmienną o nazwie "MyString" z wartością ciągu znaków "Hi!",
Aby stworzyć zmienną o wartości liczby całkowitej:
`set MyInt .123`
Aby usunąć zmienną stosuje się:
`set ! MyString`
Wyświetlenie listy zmiennych w aktywnej sesji powłoki:
`set list`
Pomoc pod komendą:
`set help`

- _exit_ - Zamyka sesję powłoki,
Ta komenda nie zatrzymuje wywołanych procesów ani nie zapisuje postępów pracy.
Brak argumentów

Interpreter poleceń Strayex Shell sprawdza dane wejściowe w danej kolejności:
- Czy to komenda,
- Czy to skrypt (czy zaczyna się kropką),
- Czy to plik do otwarcia lub program firm trzecich,
- Czy to plik wykonywalny podany z rozszerzeniem czy bez,
Jeśli interpreter nic nie znajdzie, wyświetli napis o braku komendy lub pliku.

## Strayex Shell for Linux



## Strayex Shell for Hobby OSes


