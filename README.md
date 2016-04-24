# UMS-WebRTC
Żeby wam to działało to trzeba:

1. Stworzyć lokalną baze danych w SQL Server o nazwie `Ums`
2. W pliku `/UMS.Web/Web.config` podmienić `Data Source=DESKTOP-TJ02H7K\SQLEXPRESS` na coś czym logujecie się do swojej bazy przez Managment Studio np `Data Source=\SQLEXPRESS`
3. Otwieracie pobrany projekt w Visual Studio, wchodzicie w `View -> Other Windows -> Package Manager Console`
4. Na dole otworzy wam sie konsola i tam jest opcja Default project. Ustawaiacie to na `UMS.Database.DAL` i w konsoli wpisujecie `Update-Database` ENTER :)
5. Odpalacie program tym zielonym przyciskiem co normalnie (albo F5) i powinna wam się otworzyć przeglądarka
