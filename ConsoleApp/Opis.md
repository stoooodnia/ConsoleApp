# Pierwsze spostrzeżenia

Co tak w zasadzie ma robić dany kod? Jest pare możliwości..

## "czerwona lampka",

1. Zła nazwa importu "dataa => data"
2. Czy program ma działać tylko dla konkretnego pliku, czy może powinien wczytywać nazwę od użytkownika?
3. Program.cs czy internal i czy main nie powinien być private przypadkiem
4. Metoda importAndPrintData nie spełnia zasady "Single responsibility" -> rozbić metodę
5. "Assign number of children" 52 linijka ma złożoność O(n^2), pętla w pętli.
6. 68 linijka, złożoność O(n^3) 3 pętle foreach zagnieżdżone.
7. gettery i settery nie mają modyfikatorów dostępu
8. Pola metod są publiczne wszędzie, nie spełnia zasad enkapsulacji danych
9. Zwrócić uwagę na "var", czy wszędzie to ma sens
10. Klasa importedObject, overload konstruktor zamiast czegoś takiego jak jest teraz, nie zgodne z zasadami OOP
11. Dlaczego metoda ImportAndPrintData przyjmuje opcjonalny argument?
12. W zasadzie importedObjects to są linie tego obiektu? :D
13. Metody powinny działać na BaseClass
14. Metody powinny mapować 

## Na co powinno się zwrócić uwagę przy obsłudze csv

1. Pusty wiersz
2. Nie pełne dane (pusty string)
3. Nie równe kolumny

## Co do tej pory zmieniłem
1. Dodałem internal do tych klas bo nie było to wyspecyfikowane
2. Wyrzuciłem funkcjonalności do oddzielnych metod
3. Klasa ImportedObject nie musi mieć pola Name bo klasa z której dziedziczy już je ma
4. Niepotrzebne importy
5. Zmiana typu importObject na baseclass
6. Zmiana z IEnumerable na ICollection aby mieć dostęp do add (może być też list)




            

