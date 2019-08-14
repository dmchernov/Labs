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
USE [Northwind]
GO

IF OBJECT_ID('GreatestOrders', 'P') IS NOT NULL
    DROP PROCEDURE GreatestOrders
GO

CREATE PROCEDURE [dbo].[GreatestOrders] @Year int, @ReturnRowNumbers int
AS
SET NOCOUNT ON;

with SubResult (EmployeeID, OrderID, [Cost order]) as
(SELECT
o.EmployeeID,
o.OrderID,
(SELECT sum((od.UnitPrice - (od.UnitPrice * od.Discount)) * od.Quantity) FROM [Northwind].[dbo].[Order Details] od where od.OrderID = o.OrderID) 'Cost order'
from Orders o
where DATEPART(year, o.OrderDate) = @Year
)

select [Full Name], OrderID, [Cost order]
from (
	select row_number() over (order by r1.[Cost order] desc) as 'RowNum', concat(e.FirstName, ' ', e.LastName) as 'Full Name', r1.OrderID, r1.[Cost order]
		from SubResult r1
		left outer join SubResult r2
		on r1.EmployeeID = r2.EmployeeID and r2.[Cost order] > r1.[Cost order]
		left join Employees e on r1.EmployeeID = e.EmployeeID
		where
		r2.[Cost order] is Null
) result
where RowNum <= @ReturnRowNumbers

GO

--13.2 Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку доставки в днях (разница между OrderDate и
--ShippedDate).  В результатах должны быть возвращены заказы, срок которых превышает переданное значение или еще недоставленные заказы. Значению
--по умолчанию для передаваемого срока 35 дней. Название процедуры ShippedOrdersDiff. Процедура должна высвечивать следующие колонки: OrderID,
--OrderDate, ShippedDate, ShippedDelay (разность в днях между ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение).
--Необходимо продемонстрировать использование этой процедуры.
USE [Northwind]
GO

IF OBJECT_ID('ShippedOrdersDiff', 'P') IS NOT NULL
    DROP PROCEDURE ShippedOrdersDiff
GO

CREATE PROCEDURE [dbo].[ShippedOrdersDiff] @ShipDays int = 35
AS
SET NOCOUNT ON;

select o.OrderID, o.OrderDate, o.ShippedDate, DATEDIFF(day, o.OrderDate, o.ShippedDate) as 'ShippedDelay', @ShipDays as 'SpecifiedDelay'
from Orders o
where
DATEDIFF(day, o.OrderDate, o.ShippedDate) > @ShipDays
or o.ShippedDate is null

GO

--13.3 Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, так и подчиненных его подчиненных.
--В качестве входного параметра функции используется EmployeeID. Необходимо распечатать имена подчиненных и выровнять их в тексте (использовать
--оператор PRINT) согласно иерархии подчинения. Продавец, для которого надо найти подчиненных также должен быть высвечен. Название процедуры
--SubordinationInfo. В качестве алгоритма для решения этой задачи надо использовать пример, приведенный в Books Online и рекомендованный
--Microsoft для решения подобного типа задач. Продемонстрировать использование процедуры
USE [Northwind]
GO

IF OBJECT_ID('SubordinationInfo', 'P') IS NOT NULL
    DROP PROCEDURE SubordinationInfo
GO

CREATE PROCEDURE [dbo].[SubordinationInfo] @EmployeeID int
AS
SET NOCOUNT ON;

DECLARE @FullName VARCHAR(30)
   SELECT @FullName = (select concat(FirstName, ' ', LastName) from Employees where EmployeeID = @EmployeeID)
    
   PRINT SPACE(@@NESTLEVEL * 2) + @FullName
   
   DECLARE employees CURSOR LOCAL FOR
      select EmployeeID from Employees where ReportsTo = @EmployeeID

   OPEN employees
      FETCH NEXT FROM employees INTO @EmployeeID
      WHILE @@FETCH_STATUS=0 BEGIN
         EXEC SubordinationInfo @EmployeeID
         FETCH NEXT FROM employees INTO @EmployeeID
      END
   CLOSE employees
   DEALLOCATE employees
GO

--13.4 Написать функцию, которая определяет, есть ли у продавца подчиненные. Возвращает тип данных BIT. В качестве входного параметра функции
--используется EmployeeID. Название функции IsBoss. Продемонстрировать использование функции для всех продавцов из таблицы Employee
USE [Northwind]
GO

IF OBJECT_ID('IsBoss', 'FN') IS NOT NULL
    DROP FUNCTION IsBoss
GO

CREATE FUNCTION [dbo].[IsBoss] (@EmployeeID int)
RETURNS bit
AS
BEGIN

return(Select case when count(*) > 0 then 1 else 0 end from Employees where ReportsTo = @EmployeeID);

END;
GO