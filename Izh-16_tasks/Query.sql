--1.1 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) включительно
--и которые доставлены с ShipVia >= 2. Формат указания даты должен быть верным при любых региональных настройках, согласно 
--требованиям статьи “Writing International Transact-SQL Statements” в Books Online раздел
--“Accessing and Changing Relational Data Overview”. Этот метод использовать далее для всех заданий.
--Запрос должен высвечивать только колонки OrderID, ShippedDate и ShipVia. 
--Пояснить почему сюда не попали заказы с NULL-ом в колонке ShippedDate.

-- Заказы с NULL в колонке ShippedDate не попали в выборку, т.к. NULL не является каким-либо значением, а иллюстрирует его отсутствие.

use Northwind
GO

Select OrderID, ShippedDate, ShipVia from Orders
where ShippedDate >= Convert(datetime, '06/05/1998', 104) and ShipVia >=2

--1.2 Написать запрос, который выводит только недоставленные заказы из таблицы Orders. В результатах запроса высвечивать для колонки
--ShippedDate вместо значений NULL строку ‘Not Shipped’ – использовать системную функцию CASЕ. Запрос должен высвечивать только колонки
--OrderID и ShippedDate.
use Northwind
GO

Select
OrderID,
case when ShippedDate is NULL then 'Not Shipped' end
from Orders
where ShippedDate is NULL

--1.3 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) не включая эту дату или которые еще
--не доставлены. В запросе должны высвечиваться только колонки OrderID (переименовать в Order Number) и ShippedDate (переименовать в
--Shipped Date). В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, для остальных
--значений высвечивать дату в формате по умолчанию.
use Northwind
GO

Select
OrderID as 'Order Number',
case when ShippedDate is NULL then 'Not Shipped' else cast(ShippedDate as nvarchar(30)) end as 'Shipped Date' from Orders
where ShippedDate > Convert(datetime, '06/05/1998', 104) or ShippedDate is NULL


--2.1 Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. Запрос сделать с только помощью оператора IN.
--Высвечивать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты запроса по имени
--заказчиков и по месту проживания.
use Northwind
GO

Select ContactName, Country from Customers
where Country in ('USA', 'Canada')
order by ContactName asc, Country asc

--2.2 Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. Запрос сделать с помощью оператора IN.
--Высвечивать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты запроса по имени заказчиков.
use Northwind
GO

Select ContactName, Country from Customers
where Country not in ('USA', 'Canada')
order by ContactName asc

--2.3 Выбрать из таблицы Customers все страны, в которых проживают заказчики. Страна должна быть упомянута только один раз и список
--отсортирован по убыванию. Не использовать предложение GROUP BY. Высвечивать только одну колонку в результатах запроса. 
use Northwind
GO

Select distinct Country from Customers order by Country desc

--3.1 Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться), где встречаются продукты с количеством
--от 3 до 10 включительно – это колонка Quantity в таблице Order Details. Использовать оператор BETWEEN. Запрос должен высвечивать
--только колонку OrderID.
use Northwind
GO

Select distinct OrderID from [Order Details] where Quantity between 3 and 10

--3.2 Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g. Использовать
--оператор BETWEEN. Проверить, что в результаты запроса попадает Germany. Запрос должен высвечивать только колонки CustomerID и
--Country и отсортирован по Country.
use Northwind
GO
set showplan_text on
GO

Select CustomerID, Country from Customers where Country between 'b' and 'h'
order by Country
GO

set showplan_text off
GO
  --|--Sort(ORDER BY:([Northwind].[dbo].[Customers].[Country] ASC))
       --|--Clustered Index Scan(OBJECT:([Northwind].[dbo].[Customers].[PK_Customers]), WHERE:([Northwind].[dbo].[Customers].[Country]>=N'b' AND [Northwind].[dbo].[Customers].[Country]<=N'h'))

--3.3 Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g, не
--используя оператор BETWEEN. С помощью опции “Execution Plan” определить какой запрос предпочтительнее 3.2 или 3.3 – для этого
--надо ввести в скрипт выполнение текстового Execution Plan-a для двух этих запросов, результаты выполнения Execution Plan надо
--ввести в скрипт в виде комментария и по их результатам дать ответ на вопрос – по какому параметру было проведено сравнение.
--Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.

-- Оба запроса на текущем наборе данных и индексов имеют одинаковую производительность, т.к. поиск при сравнении значений Country
--осуществляется по кластерному индексу PK_Customers

use Northwind
GO

set showplan_text on
GO

Select CustomerID, Country from Customers where Country like '[b-gB-G]%'
order by Country
GO

set showplan_text off
GO

  --|--Sort(ORDER BY:([Northwind].[dbo].[Customers].[Country] ASC))
       --|--Clustered Index Scan(OBJECT:([Northwind].[dbo].[Customers].[PK_Customers]), WHERE:([Northwind].[dbo].[Customers].[Country] like N'[b-gB-G]%'))


--4.1 В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'. Известно, что в подстроке
--'chocolade' может быть изменена одна буква 'c' в середине - найти все продукты, которые удовлетворяют этому условию. Подсказка:
--результаты запроса должны высвечивать 2 строки.
use Northwind
GO

Select * from Products where ProductName like '%cho_olade%'

--5.1 Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним.
--Результат округлить до сотых и высветить в стиле 1 для типа данных money.  Скидка (колонка Discount) составляет процент из
--стоимости для данного товара. Для определения действительной цены на проданный продукт надо вычесть скидку из указанной в
--колонке UnitPrice цены. Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
use Northwind
GO

Select format(Convert(money, round(Sum((UnitPrice - (UnitPrice * Discount)) * Quantity), 2), 1), 'N') as 'Totals' from [Order Details]

--5.2 По таблице Orders найти количество заказов, которые еще не были доставлены (т.е. в колонке ShippedDate нет значения даты доставки).
--Использовать при этом запросе только оператор COUNT. Не использовать предложения WHERE и GROUP.
use Northwind
GO

Select Count(*) - Count(ShippedDate) from Orders

--5.3 По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. Использовать функцию COUNT и не
--использовать предложения WHERE и GROUP.
use Northwind
GO

Select COUNT(distinct CustomerID) from Orders

--6.1 По таблице Orders найти количество заказов с группировкой по годам. В результатах запроса надо высвечивать две колонки c
--названиями Year и Total. Написать проверочный запрос, который вычисляет количество всех заказов.
use Northwind
GO

Select DATEPART(year, OrderDate) as 'Year', count(OrderID) as 'Total' from Orders
group by DATEPART(year, OrderDate)

--6.2 По таблице Orders найти количество заказов, cделанных каждым продавцом. Заказ для указанного продавца – это любая запись в
--таблице Orders, где в колонке EmployeeID задано значение для данного продавца. В результатах запроса надо высвечивать колонку с
--именем продавца (Должно высвечиваться имя полученное конкатенацией LastName & FirstName. Эта строка LastName & FirstName должна
--быть получена отдельным запросом в колонке основного запроса. Также основной запрос должен использовать группировку по EmployeeID.)
--с названием колонки ‘Seller’ и колонку c количеством заказов высвечивать с названием 'Amount'. Результаты запроса должны быть
--упорядочены по убыванию количества заказов.
use Northwind
GO

Select (select e.LastName + ' ' + e.FirstName from Employees e where e.EmployeeID = o.EmployeeID) as 'Seller', Count(*) as 'Amount'
from Orders o
group by o.EmployeeID
order by 'Amount' desc

--6.3 По таблице Orders найти количество заказов, cделанных каждым продавцом и для каждого покупателя. Необходимо определить это
--только для заказов сделанных в 1998 году. В результатах запроса надо высвечивать колонку с именем продавца (название колонки ‘Seller’),
--колонку с именем покупателя (название колонки ‘Customer’)  и колонку c количеством заказов высвечивать с названием 'Amount'. В запросе
--необходимо использовать специальный оператор языка T-SQL для работы с выражением GROUP (Этот же оператор поможет выводить строку “ALL”
--в результатах запроса). Группировки должны быть сделаны по ID продавца и покупателя. Результаты запроса должны быть упорядочены по
--продавцу, покупателю и по убыванию количества продаж. В результатах должна быть сводная информация по продажам. Т.е. в резульирующем
--наборе должны присутствовать дополнительно к информации о продажах продавца для каждого покупателя следующие строчки:
--Seller		Customer	Amount
--ALL 		ALL		<общее число продаж>
--<имя>		ALL		<число продаж для данного продавца>
--ALL		<имя>		<число продаж для данного покупателя>
--<имя>		<имя>		<число продаж данного продавца для даннного покупателя>
use Northwind
GO

Select
case 
	when o.EmployeeID is null then 'ALL'
	else (select e.LastName + ' ' + e.FirstName from Employees e where e.EmployeeID = o.EmployeeID)
end as 'Seller',
case
	when o.CustomerID is NULL then 'ALL'
	else (select c.CompanyName from Customers c where c.CustomerID = o.CustomerID)
end as 'Customer', 
count(*)  as 'Amount'
from Orders o
where Datepart(year, OrderDate) = '1998'
group by cube (EmployeeID, CustomerID)
order by 'Seller' asc, 'Customer' asc, 'Amount' desc

--6.4 Найти покупателей и продавцов, которые живут в одном городе. Если в городе живут только один или несколько продавцов или только
--один или несколько покупателей, то информация о таких покупателя и продавцах не должна попадать в результирующий набор. Не
--использовать конструкцию JOIN. В результатах запроса необходимо вывести следующие заголовки для результатов запроса: ‘Person’, ‘Type’
--(здесь надо выводить строку ‘Customer’ или  ‘Seller’ в завимости от типа записи), ‘City’. Отсортировать результаты запроса по колонке
--‘City’ и по ‘Person’.
use Northwind
GO

Select ContactName as 'Person', 'Customer' as 'Type', City from Customers
where City in (Select distinct City from Employees)
union
select (LastName + ' ' + FirstName) as 'Person', 'Seller' as 'Type', City from Employees
where City in (Select distinct City from Customers)

--6.5 Найти всех покупателей, которые живут в одном городе. В запросе использовать соединение таблицы Customers c собой - самосоединение.
--Высветить колонки CustomerID и City. Запрос не должен высвечивать дублируемые записи. Для проверки написать запрос, который высвечивает
--города, которые встречаются более одного раза в таблице Customers. Это позволит проверить правильность запроса.
use Northwind
GO

select distinct c.CustomerID, c.City from Customers c join Customers j on c.City = j.City and c.CustomerID <> j.CustomerID

--Проверочный запрос
select City, count(city) as 'Count' from Customers group by City having count(*) > 1

--6.6 По таблице Employees найти для каждого продавца его руководителя, т.е. кому он делает репорты. Высветить колонки с именами
--'User Name' (LastName) и 'Boss'. В колонках должны быть высвечены имена из колонки LastName. Высвечены ли все продавцы в этом запросе?

-- В результатах запроса отображаются не все продавцы. Из девяти показывается восемь.
use Northwind
GO

select e.LastName as 'User Name', b.LastName as 'Boss' from Employees e join Employees b on e.ReportsTo=b.EmployeeID

--7.1 Определить продавцов, которые обслуживают регион 'Western' (таблица Region). Результаты запроса должны высвечивать два поля:
--'LastName' продавца и название обслуживаемой территории ('TerritoryDescription' из таблицы Territories). Запрос должен использовать
--JOIN в предложении FROM. Для определения связей между таблицами Employees и Territories надо использовать графические диаграммы для
--базы Northwind.
use Northwind
GO

select e.LastName, t.TerritoryDescription from Employees e
inner join EmployeeTerritories et on e.EmployeeID = et.EmployeeID
inner join Territories t on et.TerritoryID = t.TerritoryID
inner join Region r on t.RegionID = r.RegionID
where r.RegionDescription = 'Western'

--8.1 Высветить в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество их заказов из таблицы Orders.
--Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны быть выведены в результатах запроса. Упорядочить
--результаты запроса по возрастанию количества заказов.
use Northwind
GO

select c.ContactName, count(o.OrderID) as 'Orders Count'
from Customers c left outer join Orders o on o.CustomerID = c.CustomerID
group by c.ContactName
order by 'Orders Count' asc

--9.1 Высветить всех поставщиков колонка CompanyName в таблице Suppliers, у которых нет хотя бы одного продукта на складе
--(UnitsInStock в таблице Products равно 0). Использовать вложенный SELECT для этого запроса с использованием оператора IN.
--Можно ли использовать вместо оператора IN оператор '=' ?

-- В подобной конструкции вместо оператора IN можно использовать оператор '=' только в том случае, если подзапрос возвращает строго
-- одно значение, т.к. оператор '=' задаёт единственное условие. Оператор IN условно преобразует результаты подзапроса в конструкцию вида:
-- = X1 or = X2 or = X3 or = ... or = Xn
use Northwind
GO

select CompanyName from Suppliers where SupplierID in (
select SupplierID from Products where UnitsInStock = 0)

--10.1 Высветить всех продавцов, которые имеют более 150 заказов. Использовать вложенный коррелированный SELECT.
use Northwind
GO

select * from Employees e
where e.EmployeeID = 
(select o.EmployeeID from Orders o
 where e.EmployeeID = o.EmployeeID
  group by o.EmployeeID having count(o.EmployeeID) > 150)

--11.1 Высветить всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders).
--Использовать коррелированный SELECT и оператор EXISTS.
use Northwind
GO

select * from Customers c where not exists (select * from Orders o where o.CustomerID = c.CustomerID)

--12.1 Для формирования алфавитного указателя Employees высветить из таблицы Employees список только тех букв алфавита, с которых
--начинаются фамилии Employees (колонка LastName ) из этой таблицы. Алфавитный список должен быть отсортирован по возрастанию.
use Northwind
GO

select distinct SUBSTRING(LastName, 1, 1) as 'Index' from Employees order by 'Index' asc

--13.1 Написать процедуру, которая возвращает самый крупный заказ для каждого из продавцов за определенный год. В результатах не может
--быть несколько заказов одного продавца, должен быть только один и самый крупный. В результатах запроса должны быть выведены следующие
--колонки: колонка с именем и фамилией продавца (FirstName и LastName – пример: Nancy Davolio), номер заказа и его стоимость. В запросе
--надо учитывать Discount при продаже товаров. Процедуре передается год, за который надо сделать отчет, и количество возвращаемых записей.
--Результаты запроса должны быть упорядочены по убыванию суммы заказа. Процедура должна быть реализована с использованием оператора SELECT
--и БЕЗ ИСПОЛЬЗОВАНИЯ КУРСОРОВ. Название функции соответственно GreatestOrders. Необходимо продемонстрировать использование этих процедур.
--Также помимо демонстрации вызовов процедур в скрипте Query.sql надо написать отдельный ДОПОЛНИТЕЛЬНЫЙ проверочный запрос для тестирования
--правильности работы процедуры GreatestOrders. Проверочный запрос должен выводить в удобном для сравнения с результатами работы процедур
--виде для определенного продавца для всех его заказов за определенный указанный год в результатах следующие колонки: имя продавца, номер
--заказа, сумму заказа. Проверочный запрос не должен повторять запрос, написанный в процедуре, - он должен выполнять только то, что описано
--в требованиях по нему.
use Northwind
GO

exec [dbo].[GreatestOrders] @Year = 1996, @ReturnRowNumbers = 10

--Проверочный запрос
SELECT
concat(e.FirstName, ' ', e.LastName) as 'Full Name',
o.OrderID,
(SELECT sum((od.UnitPrice - (od.UnitPrice * od.Discount)) * od.Quantity) FROM [Northwind].[dbo].[Order Details] od where od.OrderID = o.OrderID) 'Cost order'
from Orders o
left join Employees e on e.EmployeeID = o.EmployeeID
where DATEPART(year, o.OrderDate) = 1996
and o.EmployeeID = 5
order by [Cost order] desc

--13.2 Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку доставки в днях (разница между OrderDate и
--ShippedDate).  В результатах должны быть возвращены заказы, срок которых превышает переданное значение или еще недоставленные заказы. Значению
--по умолчанию для передаваемого срока 35 дней. Название процедуры ShippedOrdersDiff. Процедура должна высвечивать следующие колонки: OrderID,
--OrderDate, ShippedDate, ShippedDelay (разность в днях между ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение).
--Необходимо продемонстрировать использование этой процедуры.
use Northwind
GO

exec [dbo].[ShippedOrdersDiff] @ShipDays = 10
exec [dbo].[ShippedOrdersDiff]

--13.3 Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, так и подчиненных его подчиненных.
--В качестве входного параметра функции используется EmployeeID. Необходимо распечатать имена подчиненных и выровнять их в тексте (использовать
--оператор PRINT) согласно иерархии подчинения. Продавец, для которого надо найти подчиненных также должен быть высвечен. Название процедуры
--SubordinationInfo. В качестве алгоритма для решения этой задачи надо использовать пример, приведенный в Books Online и рекомендованный
--Microsoft для решения подобного типа задач. Продемонстрировать использование процедуры
use Northwind
GO

exec SubordinationInfo @EmployeeID = 2

--13.4 Написать функцию, которая определяет, есть ли у продавца подчиненные. Возвращает тип данных BIT. В качестве входного параметра функции
--используется EmployeeID. Название функции IsBoss. Продемонстрировать использование функции для всех продавцов из таблицы Employees.
use Northwind
GO

select dbo.IsBoss(1)
select dbo.IsBoss(2)
select dbo.IsBoss(3)
select dbo.IsBoss(4)
select dbo.IsBoss(5)
select dbo.IsBoss(6)
select dbo.IsBoss(7)
select dbo.IsBoss(8)
select dbo.IsBoss(9)
