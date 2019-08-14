--13.1 �������� ���������, ������� ���������� ����� ������� ����� ��� ������� �� ��������� �� ������������ ���. � ����������� �� �����
--���� ��������� ������� ������ ��������, ������ ���� ������ ���� � ����� �������. � ����������� ������� ������ ���� �������� ���������
--�������: ������� � ������ � �������� �������� (FirstName � LastName � ������: Nancy Davolio), ����� ������ � ��� ���������. � �������
--���� ��������� Discount ��� ������� �������. ��������� ���������� ���, �� ������� ���� ������� �����, � ���������� ������������ �������.
--���������� ������� ������ ���� ����������� �� �������� ����� ������. ��������� ������ ���� ����������� � �������������� ��������� SELECT
--� ��� ������������� ��������. �������� ������� �������������� GreatestOrders. ���������� ������������������ ������������� ���� ��������.
--����� ������ ������������ ������� �������� � ������� Query.sql ���� �������� ��������� �������������� ����������� ������ ��� ������������
--������������ ������ ��������� GreatestOrders. ����������� ������ ������ �������� � ������� ��� ��������� � ������������ ������ ��������
--���� ��� ������������� �������� ��� ���� ��� ������� �� ������������ ��������� ��� � ����������� ��������� �������: ��� ��������, �����
--������, ����� ������. ����������� ������ �� ������ ��������� ������, ���������� � ���������, - �� ������ ��������� ������ ��, ��� �������
--� ����������� �� ����.
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

--13.2 �������� ���������, ������� ���������� ������ � ������� Orders, �������� ���������� ����� �������� � ���� (������� ����� OrderDate �
--ShippedDate).  � ����������� ������ ���� ���������� ������, ���� ������� ��������� ���������� �������� ��� ��� �������������� ������. ��������
--�� ��������� ��� ������������� ����� 35 ����. �������� ��������� ShippedOrdersDiff. ��������� ������ ����������� ��������� �������: OrderID,
--OrderDate, ShippedDate, ShippedDelay (�������� � ���� ����� ShippedDate � OrderDate), SpecifiedDelay (���������� � ��������� ��������).
--���������� ������������������ ������������� ���� ���������.
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

--13.3 �������� ���������, ������� ����������� ���� ����������� ��������� ��������, ��� ����������������, ��� � ����������� ��� �����������.
--� �������� �������� ��������� ������� ������������ EmployeeID. ���������� ����������� ����� ����������� � ��������� �� � ������ (������������
--�������� PRINT) �������� �������� ����������. ��������, ��� �������� ���� ����� ����������� ����� ������ ���� ��������. �������� ���������
--SubordinationInfo. � �������� ��������� ��� ������� ���� ������ ���� ������������ ������, ����������� � Books Online � ���������������
--Microsoft ��� ������� ��������� ���� �����. ������������������ ������������� ���������
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

--13.4 �������� �������, ������� ����������, ���� �� � �������� �����������. ���������� ��� ������ BIT. � �������� �������� ��������� �������
--������������ EmployeeID. �������� ������� IsBoss. ������������������ ������������� ������� ��� ���� ��������� �� ������� Employee
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