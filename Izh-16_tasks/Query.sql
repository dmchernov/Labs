--1.1 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (������� ShippedDate) ������������
--� ������� ���������� � ShipVia >= 2. ������ �������� ���� ������ ���� ������ ��� ����� ������������ ����������, �������� 
--����������� ������ �Writing International Transact-SQL Statements� � Books Online ������
--�Accessing and Changing Relational Data Overview�. ���� ����� ������������ ����� ��� ���� �������.
--������ ������ ����������� ������ ������� OrderID, ShippedDate � ShipVia. 
--�������� ������ ���� �� ������ ������ � NULL-�� � ������� ShippedDate.

-- ������ � NULL � ������� ShippedDate �� ������ � �������, �.�. NULL �� �������� �����-���� ���������, � ������������ ��� ����������.

use Northwind
GO

Select OrderID, ShippedDate, ShipVia from Orders
where ShippedDate >= Convert(datetime, '06/05/1998', 104) and ShipVia >=2

--1.2 �������� ������, ������� ������� ������ �������������� ������ �� ������� Orders. � ����������� ������� ����������� ��� �������
--ShippedDate ������ �������� NULL ������ �Not Shipped� � ������������ ��������� ������� CAS�. ������ ������ ����������� ������ �������
--OrderID � ShippedDate.
use Northwind
GO

Select
OrderID,
case when ShippedDate is NULL then 'Not Shipped' end
from Orders
where ShippedDate is NULL

--1.3 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate) �� ������� ��� ���� ��� ������� ���
--�� ����������. � ������� ������ ������������� ������ ������� OrderID (������������� � Order Number) � ShippedDate (������������� �
--Shipped Date). � ����������� ������� ����������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�, ��� ���������
--�������� ����������� ���� � ������� �� ���������.
use Northwind
GO

Select
OrderID as 'Order Number',
case when ShippedDate is NULL then 'Not Shipped' else cast(ShippedDate as nvarchar(30)) end as 'Shipped Date' from Orders
where ShippedDate > Convert(datetime, '06/05/1998', 104) or ShippedDate is NULL


--2.1 ������� �� ������� Customers ���� ����������, ����������� � USA � Canada. ������ ������� � ������ ������� ��������� IN.
--����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. ����������� ���������� ������� �� �����
--���������� � �� ����� ����������.
use Northwind
GO

Select ContactName, Country from Customers
where Country in ('USA', 'Canada')
order by ContactName asc, Country asc

--2.2 ������� �� ������� Customers ���� ����������, �� ����������� � USA � Canada. ������ ������� � ������� ��������� IN.
--����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. ����������� ���������� ������� �� ����� ����������.
use Northwind
GO

Select ContactName, Country from Customers
where Country not in ('USA', 'Canada')
order by ContactName asc

--2.3 ������� �� ������� Customers ��� ������, � ������� ��������� ���������. ������ ������ ���� ��������� ������ ���� ��� � ������
--������������ �� ��������. �� ������������ ����������� GROUP BY. ����������� ������ ���� ������� � ����������� �������. 
use Northwind
GO

Select distinct Country from Customers order by Country desc

--3.1 ������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������), ��� ����������� �������� � �����������
--�� 3 �� 10 ������������ � ��� ������� Quantity � ������� Order Details. ������������ �������� BETWEEN. ������ ������ �����������
--������ ������� OrderID.
use Northwind
GO

Select distinct OrderID from [Order Details] where Quantity between 3 and 10

--3.2 ������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� ����� �� ��������� b � g. ������������
--�������� BETWEEN. ���������, ��� � ���������� ������� �������� Germany. ������ ������ ����������� ������ ������� CustomerID �
--Country � ������������ �� Country.
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

--3.3 ������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� ����� �� ��������� b � g, ��
--��������� �������� BETWEEN. � ������� ����� �Execution Plan� ���������� ����� ������ ���������������� 3.2 ��� 3.3 � ��� �����
--���� ������ � ������ ���������� ���������� Execution Plan-a ��� ���� ���� ��������, ���������� ���������� Execution Plan ����
--������ � ������ � ���� ����������� � �� �� ����������� ���� ����� �� ������ � �� ������ ��������� ���� ��������� ���������.
--������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.

-- ��� ������� �� ������� ������ ������ � �������� ����� ���������� ������������������, �.�. ����� ��� ��������� �������� Country
--�������������� �� ����������� ������� PK_Customers

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


--4.1 � ������� Products ����� ��� �������� (������� ProductName), ��� ����������� ��������� 'chocolade'. ��������, ��� � ���������
--'chocolade' ����� ���� �������� ���� ����� 'c' � �������� - ����� ��� ��������, ������� ������������� ����� �������. ���������:
--���������� ������� ������ ����������� 2 ������.
use Northwind
GO

Select * from Products where ProductName like '%cho_olade%'

--5.1 ����� ����� ����� ���� ������� �� ������� Order Details � ������ ���������� ����������� ������� � ������ �� ���.
--��������� ��������� �� ����� � ��������� � ����� 1 ��� ���� ������ money.  ������ (������� Discount) ���������� ������� ��
--��������� ��� ������� ������. ��� ����������� �������������� ���� �� ��������� ������� ���� ������� ������ �� ��������� �
--������� UnitPrice ����. ����������� ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.
use Northwind
GO

Select format(Convert(money, round(Sum((UnitPrice - (UnitPrice * Discount)) * Quantity), 2), 1), 'N') as 'Totals' from [Order Details]

--5.2 �� ������� Orders ����� ���������� �������, ������� ��� �� ���� ���������� (�.�. � ������� ShippedDate ��� �������� ���� ��������).
--������������ ��� ���� ������� ������ �������� COUNT. �� ������������ ����������� WHERE � GROUP.
use Northwind
GO

Select Count(*) - Count(ShippedDate) from Orders

--5.3 �� ������� Orders ����� ���������� ��������� ����������� (CustomerID), ��������� ������. ������������ ������� COUNT � ��
--������������ ����������� WHERE � GROUP.
use Northwind
GO

Select COUNT(distinct CustomerID) from Orders

--6.1 �� ������� Orders ����� ���������� ������� � ������������ �� �����. � ����������� ������� ���� ����������� ��� ������� c
--���������� Year � Total. �������� ����������� ������, ������� ��������� ���������� ���� �������.
use Northwind
GO

Select DATEPART(year, OrderDate) as 'Year', count(OrderID) as 'Total' from Orders
group by DATEPART(year, OrderDate)

--6.2 �� ������� Orders ����� ���������� �������, c�������� ������ ���������. ����� ��� ���������� �������� � ��� ����� ������ �
--������� Orders, ��� � ������� EmployeeID ������ �������� ��� ������� ��������. � ����������� ������� ���� ����������� ������� �
--������ �������� (������ ������������� ��� ���������� ������������� LastName & FirstName. ��� ������ LastName & FirstName ������
--���� �������� ��������� �������� � ������� ��������� �������. ����� �������� ������ ������ ������������ ����������� �� EmployeeID.)
--� ��������� ������� �Seller� � ������� c ����������� ������� ����������� � ��������� 'Amount'. ���������� ������� ������ ����
--����������� �� �������� ���������� �������.
use Northwind
GO

Select (select e.LastName + ' ' + e.FirstName from Employees e where e.EmployeeID = o.EmployeeID) as 'Seller', Count(*) as 'Amount'
from Orders o
group by o.EmployeeID
order by 'Amount' desc

--6.3 �� ������� Orders ����� ���������� �������, c�������� ������ ��������� � ��� ������� ����������. ���������� ���������� ���
--������ ��� ������� ��������� � 1998 ����. � ����������� ������� ���� ����������� ������� � ������ �������� (�������� ������� �Seller�),
--������� � ������ ���������� (�������� ������� �Customer�)  � ������� c ����������� ������� ����������� � ��������� 'Amount'. � �������
--���������� ������������ ����������� �������� ����� T-SQL ��� ������ � ���������� GROUP (���� �� �������� ������� �������� ������ �ALL�
--� ����������� �������). ����������� ������ ���� ������� �� ID �������� � ����������. ���������� ������� ������ ���� ����������� ��
--��������, ���������� � �� �������� ���������� ������. � ����������� ������ ���� ������� ���������� �� ��������. �.�. � �������������
--������ ������ �������������� ������������� � ���������� � �������� �������� ��� ������� ���������� ��������� �������:
--Seller		Customer	Amount
--ALL 		ALL		<����� ����� ������>
--<���>		ALL		<����� ������ ��� ������� ��������>
--ALL		<���>		<����� ������ ��� ������� ����������>
--<���>		<���>		<����� ������ ������� �������� ��� �������� ����������>
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

--6.4 ����� ����������� � ���������, ������� ����� � ����� ������. ���� � ������ ����� ������ ���� ��� ��������� ��������� ��� ������
--���� ��� ��������� �����������, �� ���������� � ����� ���������� � ��������� �� ������ �������� � �������������� �����. ��
--������������ ����������� JOIN. � ����������� ������� ���������� ������� ��������� ��������� ��� ����������� �������: �Person�, �Type�
--(����� ���� �������� ������ �Customer� ���  �Seller� � ��������� �� ���� ������), �City�. ������������� ���������� ������� �� �������
--�City� � �� �Person�.
use Northwind
GO

Select ContactName as 'Person', 'Customer' as 'Type', City from Customers
where City in (Select distinct City from Employees)
union
select (LastName + ' ' + FirstName) as 'Person', 'Seller' as 'Type', City from Employees
where City in (Select distinct City from Customers)

--6.5 ����� ���� �����������, ������� ����� � ����� ������. � ������� ������������ ���������� ������� Customers c ����� - ��������������.
--��������� ������� CustomerID � City. ������ �� ������ ����������� ����������� ������. ��� �������� �������� ������, ������� �����������
--������, ������� ����������� ����� ������ ���� � ������� Customers. ��� �������� ��������� ������������ �������.
use Northwind
GO

select distinct c.CustomerID, c.City from Customers c join Customers j on c.City = j.City and c.CustomerID <> j.CustomerID

--����������� ������
select City, count(city) as 'Count' from Customers group by City having count(*) > 1

--6.6 �� ������� Employees ����� ��� ������� �������� ��� ������������, �.�. ���� �� ������ �������. ��������� ������� � �������
--'User Name' (LastName) � 'Boss'. � �������� ������ ���� ��������� ����� �� ������� LastName. ��������� �� ��� �������� � ���� �������?

-- � ����������� ������� ������������ �� ��� ��������. �� ������ ������������ ������.
use Northwind
GO

select e.LastName as 'User Name', b.LastName as 'Boss' from Employees e join Employees b on e.ReportsTo=b.EmployeeID

--7.1 ���������� ���������, ������� ����������� ������ 'Western' (������� Region). ���������� ������� ������ ����������� ��� ����:
--'LastName' �������� � �������� ������������� ���������� ('TerritoryDescription' �� ������� Territories). ������ ������ ������������
--JOIN � ����������� FROM. ��� ����������� ������ ����� ��������� Employees � Territories ���� ������������ ����������� ��������� ���
--���� Northwind.
use Northwind
GO

select e.LastName, t.TerritoryDescription from Employees e
inner join EmployeeTerritories et on e.EmployeeID = et.EmployeeID
inner join Territories t on et.TerritoryID = t.TerritoryID
inner join Region r on t.RegionID = r.RegionID
where r.RegionDescription = 'Western'

--8.1 ��������� � ����������� ������� ����� ���� ���������� �� ������� Customers � ��������� ���������� �� ������� �� ������� Orders.
--������� �� ��������, ��� � ��������� ���������� ��� �������, �� ��� ����� ������ ���� �������� � ����������� �������. �����������
--���������� ������� �� ����������� ���������� �������.
use Northwind
GO

select c.ContactName, count(o.OrderID) as 'Orders Count'
from Customers c left outer join Orders o on o.CustomerID = c.CustomerID
group by c.ContactName
order by 'Orders Count' asc

--9.1 ��������� ���� ����������� ������� CompanyName � ������� Suppliers, � ������� ��� ���� �� ������ �������� �� ������
--(UnitsInStock � ������� Products ����� 0). ������������ ��������� SELECT ��� ����� ������� � �������������� ��������� IN.
--����� �� ������������ ������ ��������� IN �������� '=' ?

-- � �������� ����������� ������ ��������� IN ����� ������������ �������� '=' ������ � ��� ������, ���� ��������� ���������� ������
-- ���� ��������, �.�. �������� '=' ����� ������������ �������. �������� IN ������� ����������� ���������� ���������� � ����������� ����:
-- = X1 or = X2 or = X3 or = ... or = Xn
use Northwind
GO

select CompanyName from Suppliers where SupplierID in (
select SupplierID from Products where UnitsInStock = 0)

--10.1 ��������� ���� ���������, ������� ����� ����� 150 �������. ������������ ��������� ��������������� SELECT.
use Northwind
GO

select * from Employees e
where e.EmployeeID = 
(select o.EmployeeID from Orders o
 where e.EmployeeID = o.EmployeeID
  group by o.EmployeeID having count(o.EmployeeID) > 150)

--11.1 ��������� ���� ���������� (������� Customers), ������� �� ����� �� ������ ������ (��������� �� ������� Orders).
--������������ ��������������� SELECT � �������� EXISTS.
use Northwind
GO

select * from Customers c where not exists (select * from Orders o where o.CustomerID = c.CustomerID)

--12.1 ��� ������������ ����������� ��������� Employees ��������� �� ������� Employees ������ ������ ��� ���� ��������, � �������
--���������� ������� Employees (������� LastName ) �� ���� �������. ���������� ������ ������ ���� ������������ �� �����������.
use Northwind
GO

select distinct SUBSTRING(LastName, 1, 1) as 'Index' from Employees order by 'Index' asc

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
use Northwind
GO

exec [dbo].[GreatestOrders] @Year = 1996, @ReturnRowNumbers = 10

--����������� ������
SELECT
concat(e.FirstName, ' ', e.LastName) as 'Full Name',
o.OrderID,
(SELECT sum((od.UnitPrice - (od.UnitPrice * od.Discount)) * od.Quantity) FROM [Northwind].[dbo].[Order Details] od where od.OrderID = o.OrderID) 'Cost order'
from Orders o
left join Employees e on e.EmployeeID = o.EmployeeID
where DATEPART(year, o.OrderDate) = 1996
and o.EmployeeID = 5
order by [Cost order] desc

--13.2 �������� ���������, ������� ���������� ������ � ������� Orders, �������� ���������� ����� �������� � ���� (������� ����� OrderDate �
--ShippedDate).  � ����������� ������ ���� ���������� ������, ���� ������� ��������� ���������� �������� ��� ��� �������������� ������. ��������
--�� ��������� ��� ������������� ����� 35 ����. �������� ��������� ShippedOrdersDiff. ��������� ������ ����������� ��������� �������: OrderID,
--OrderDate, ShippedDate, ShippedDelay (�������� � ���� ����� ShippedDate � OrderDate), SpecifiedDelay (���������� � ��������� ��������).
--���������� ������������������ ������������� ���� ���������.
use Northwind
GO

exec [dbo].[ShippedOrdersDiff] @ShipDays = 10
exec [dbo].[ShippedOrdersDiff]

--13.3 �������� ���������, ������� ����������� ���� ����������� ��������� ��������, ��� ����������������, ��� � ����������� ��� �����������.
--� �������� �������� ��������� ������� ������������ EmployeeID. ���������� ����������� ����� ����������� � ��������� �� � ������ (������������
--�������� PRINT) �������� �������� ����������. ��������, ��� �������� ���� ����� ����������� ����� ������ ���� ��������. �������� ���������
--SubordinationInfo. � �������� ��������� ��� ������� ���� ������ ���� ������������ ������, ����������� � Books Online � ���������������
--Microsoft ��� ������� ��������� ���� �����. ������������������ ������������� ���������
use Northwind
GO

exec SubordinationInfo @EmployeeID = 2

--13.4 �������� �������, ������� ����������, ���� �� � �������� �����������. ���������� ��� ������ BIT. � �������� �������� ��������� �������
--������������ EmployeeID. �������� ������� IsBoss. ������������������ ������������� ������� ��� ���� ��������� �� ������� Employees.
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
