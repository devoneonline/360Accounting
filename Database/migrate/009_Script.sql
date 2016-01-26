BEGIN_SETUP:

delete from [dbo].[tbFeature]
GO

alter table [dbo].[tbFeature]
	add Href varchar(255),
	Class varchar(50)


GO
SET IDENTITY_INSERT [dbo].[tbFeature] ON
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (1, N'General Ledger', 1, NULL, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (2, N'Recievable', 2, NULL, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (3, N'Payable', 3, NULL, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (4, N'Cash Management', 4, NULL, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (5, N'Inventory Management', 5, NULL, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (6, N'Purchase Management', 6, NULL, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (7, N'Order Management', 7, NULL, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (8, N'System Administration', 8, NULL, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (9, N'Set Of Books', 1, 1, N'~/SetOfBook', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (10, N'Chart of Account', 2, 1, N'~/Account', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (11, N'Calendar', 3, 1, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (12, N'Currency', 4, 1, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (13, N'Journal Voucher', 5, 1, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (14, N'Posting', 6, 1, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (15, N'Period', 7, 1, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (16, N'Reports', 8, 1, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (17, N'Audit Trail', 1, 16, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (18, N'Ledgers', 2, 16, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (19, N'Trial balance', 3, 16, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (20, N'Profit & loss', 4, 16, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (21, N'Balance Sheet', 5, 16, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (22, N'Comparatives balances', 6, 16, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (23, N'Customer Master', 1, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (24, N'Customer Sites', 2, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (25, N'Invoice Source', 3, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (26, N'Receipt Classes', 4, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (27, N'Invoices', 5, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (28, N'Debit / Credit Memo', 6, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (29, N'Receipts', 7, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (30, N'Remittance', 8, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (31, N'Sales Tax/VAT', 9, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (32, N'Receivable Periods', 10, 2, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (33, N'Reports', 11, 2, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (34, N'Invoice Printout', 1, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (35, N'Receipt Printout', 2, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (36, N'Invoice Audit Trial', 3, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (37, N'Receipt Audit Trial', 4, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (38, N'Customer Age Analysis', 5, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (39, N'Customer wise Sales report', 6, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (40, N'Period wise Activity', 7, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (41, N'Sales Tax Reports', 8, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (42, N'Customer wise Receipt Clearance', 9, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (43, N'Sales Register', 10, 33, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (44, N'Vendors', 1, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (45, N'Vendors Sites', 2, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (46, N'Invoice Types', 3, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (47, N'Invoice Batches', 4, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (48, N'Invoices', 5, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (49, N'Payment Batches', 6, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (50, N'Payments', 7, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (51, N'withholding Tax', 8, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (52, N'Payable Periods', 9, 3, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (53, N'Reports', 10, 3, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (54, N'Invoice Print out', 1, 53, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (55, N'Payment Printout', 2, 53, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (56, N'Invoice Audit Trial', 3, 53, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (57, N'Payment Audit Trial', 4, 53, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (58, N'Payment Due List', 5, 53, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (59, N'Purchase Register', 6, 53, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (60, N'Withholding tax report', 7, 53, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (61, N'Period wise Activity', 8, 53, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (62, N'Banks', 1, 4, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (63, N'Bank Account', 2, 4, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (64, N'Bank Reconciliation', 3, 4, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (65, N'Reports', 4, 4, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (66, N'Bank Statement', 1, 65, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (67, N'Fund Statement', 2, 65, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (68, N'Pending Recon items', 3, 65, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (69, N'bank Recon Statement', 4, 65, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (71, N'Item Master', 1, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (72, N'Warehouses', 2, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (73, N'Stock Locator', 3, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (74, N'Lot/Batches', 4, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (75, N'Serial Numbers', 5, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (76, N'Cost Management', 6, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (77, N'Stock Receipt', 7, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (78, N'Incoming Quality', 8, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (79, N'Move order', 9, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (80, N'Misc Transaction', 10, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (81, N'Inventory periods', 11, 5, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (82, N'Reports', 12, 5, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (83, N'Stock Position', 1, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (84, N'Stock Valuation', 2, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (85, N'Stock Ledger', 3, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (86, N'Stock movement Audit Trial', 4, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (87, N'Receipt Printout', 5, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (88, N'Move order Print', 6, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (89, N'Inventory Transaction Detail', 7, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (90, N'Misc Transaction Detail', 8, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (91, N'Quality Report', 9, 82, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (92, N'Buyer Information', 1, 6, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (93, N'RFQ', 2, 6, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (94, N'Purchase Agreements', 3, 6, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (95, N'Purchase Requisition', 4, 6, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (96, N'Purchase Order', 5, 6, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (97, N'Shipment Schedule', 6, 6, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (98, N'Purchasing periods', 7, 6, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (99, N'Reports', 8, 6, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (100, N'Purchase Order Printout', 1, 99, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (101, N'Purchase Agreement Printout', 2, 99, N'~/', N'fa fa-fw fa-table')
GO
print 'Processed 100 total records'
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (102, N'Requisition Printout', 3, 99, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (103, N'Pending Purchase order', 4, 99, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (104, N'Upcoming arrivals', 5, 99, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (105, N'Pending requisitions', 6, 99, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (106, N'RFQ Printout', 7, 99, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (107, N'Order Types', 1, 7, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (108, N'Contracts', 2, 7, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (109, N'Sales Orders', 3, 7, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (110, N'Shipping Transaction', 4, 7, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (111, N'Reports', 5, 7, N'~/', N'fa fa-fw fa-caret-down')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (112, N'Sales order printout', 1, 111, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (113, N'Pending Orders', 2, 111, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (114, N'Delivery note', 3, 111, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (115, N'Date wise order detail', 4, 111, N'~/', N'fa fa-fw fa-table')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (116, N'Customer wise Order detail', 5, 111, N'~/', N'fa fa-fw fa-table')

INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (117, N'Manage Companies', 1, 8, N'~/Company', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (118, N'Manage User', 1, 8, N'~/User', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (119, N'Manage Features', 2, 8, N'~/User', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (120, N'Manage Actions', 3, 8, N'~/User', N'fa fa-fw fa-edit')

INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (121, N'Report', 4, 8, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (122, N'User List', 1, 121, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (123, N'Userwise Feature List', 2, 121, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (124, N'User Sessions', 3, 121, N'~/', N'fa fa-fw fa-edit')
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId], [Href], [Class]) VALUES (125, N'Userwise Entries Trail', 4, 121, N'~/', N'fa fa-fw fa-edit')
SET IDENTITY_INSERT [dbo].[tbFeature] OFF
/****** Object:  ForeignKey [FK_tbFeature_tbFeature]    Script Date: 01/25/2016 13:56:40 ******/

END_SETUP: