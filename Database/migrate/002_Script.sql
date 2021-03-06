BEGIN_SETUP:
/****** Object:  Table [dbo].[tbFeature]    Script Date: 01/17/2016 12:33:24 ******/
SET IDENTITY_INSERT [dbo].[tbFeature] ON

-- Modules
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (1, N'General Ledger', 1, NULL)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (2, N'Recievable', 2, NULL)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (3, N'Payable', 3, NULL)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (4, N'Cash Management', 4, NULL)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (5, N'Inventory Management', 5, NULL)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (6, N'Purchase Management', 6, NULL)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (7, N'Order Management', 7, NULL)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (8, N'System Administration', 8, NULL)

-- GL Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (9, N'Set Of Books', 1, 1)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (10, N'Chart of Account', 2, 1)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (11, N'Calendar', 3, 1)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (12, N'Currency', 4, 1)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (13, N'Journal Voucher', 5, 1)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (14, N'Posting', 6, 1)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (15, N'Period', 7, 1)

-- GL Reports
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (16, N'Reports', 8, 1)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (17, N'Audit Trail', 1, 16)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (18, N'Ledgers', 2, 16)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (19, N'Trial balance', 3, 16)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (20, N'Profit & loss', 4, 16)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (21, N'Balance Sheet', 5, 16)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (22, N'Comparatives balances', 6, 16)

-- Receivable Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (23, N'Customer Master', 1, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (24, N'Customer Sites', 2, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (25, N'Invoice Source', 3, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (26, N'Receipt Classes', 4, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (27, N'Invoices', 5, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (28, N'Debit / Credit Memo', 6, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (29, N'Receipts', 7, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (30, N'Remittance', 8, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (31, N'Sales Tax/VAT', 9, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (32, N'Receivable Periods', 10, 2)

-- Receivable Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (33, N'Reports', 11, 2)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (34, N'Invoice Printout', 1, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (35, N'Receipt Printout', 2, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (36, N'Invoice Audit Trial', 3, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (37, N'Receipt Audit Trial', 4, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (38, N'Customer Age Analysis', 5, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (39, N'Customer wise Sales report', 6, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (40, N'Period wise Activity', 7, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (41, N'Sales Tax Reports', 8, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (42, N'Customer wise Receipt Clearance', 9, 33)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (43, N'Sales Register', 10, 33)


-- Payable Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (44, N'Vendors', 1, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (45, N'Vendors Sites', 2, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (46, N'Invoice Types', 3, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (47, N'Invoice Batches', 4, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (48, N'Invoices', 5, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (49, N'Payment Batches', 6, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (50, N'Payments', 7, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (51, N'withholding Tax', 8, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (52, N'Payable Periods', 9, 3)



-- Payable Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (53, N'Reports', 10, 3)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (54, N'Invoice Print out', 1, 53)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (55, N'Payment Printout', 2, 53)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (56, N'Invoice Audit Trial', 3, 53)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (57, N'Payment Audit Trial', 4, 53)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (58, N'Payment Due List', 5, 53)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (59, N'Purchase Register', 6, 53)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (60, N'Withholding tax report', 7, 53)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (61, N'Period wise Activity', 8, 53)



-- Cash Management Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (62, N'Banks', 1, 4)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (63, N'Bank Account', 2, 4)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (64, N'Bank Reconciliation', 3, 4)

-- Cash Management Reports
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (65, N'Reports', 4, 4)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (66, N'Bank Statement', 1, 65)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (67, N'Fund Statement', 2, 65)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (68, N'Pending Recon items', 3, 65)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (69, N'bank Recon Statement', 4, 65)
--INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (70, N'', 5, 65)


-- Inventory Management Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (71, N'Item Master', 1, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (72, N'Warehouses', 2, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (73, N'Stock Locator', 3, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (74, N'Lot/Batches', 4, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (75, N'Serial Numbers', 5, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (76, N'Cost Management', 6, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (77, N'Stock Receipt', 7, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (78, N'Incoming Quality', 8, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (79, N'Move order', 9, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (80, N'Misc Transaction', 10, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (81, N'Inventory periods', 11, 5)


-- Inventory Management Reports
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (82, N'Reports', 12, 5)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (83, N'Stock Position', 1, 82)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (84, N'Stock Valuation', 2, 82)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (85, N'Stock Ledger', 3, 82)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (86, N'Stock movement Audit Trial', 4, 82)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (87, N'Receipt Printout', 5, 82)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (88, N'Move order Print', 6, 82)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (89, N'Inventory Transaction Detail', 7, 82)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (90, N'Misc Transaction Detail', 8, 82)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (91, N'Quality Report', 9, 82)


-- Purchasing Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (92, N'Buyer Information', 1, 6)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (93, N'RFQ', 2, 6)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (94, N'Purchase Agreements', 3, 6)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (95, N'Purchase Requisition', 4, 6)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (96, N'Purchase Order', 5, 6)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (97, N'Shipment Schedule', 6, 6)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (98, N'Purchasing periods', 7, 6)

-- Purchasing Reports
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (99, N'Reports', 8, 6)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (100, N'Purchase Order Printout', 1, 99)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (101, N'Purchase Agreement Printout', 2, 99)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (102, N'Requisition Printout', 3, 99)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (103, N'Pending Purchase order', 4, 99)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (104, N'Upcoming arrivals', 5, 99)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (105, N'Pending requisitions', 6, 99)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (106, N'RFQ Printout', 7, 99)


-- Order Management Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (107, N'Order Types', 1, 7)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (108, N'Contracts', 2, 7)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (109, N'Sales Orders', 3, 7)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (110, N'Shipping Transaction', 4, 7)

-- Order Management Reports
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (111, N'Reports', 5, 7)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (112, N'Sales order printout', 1, 111)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (113, N'Pending Orders', 2, 111)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (114, N'Delivery note', 3, 111)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (115, N'Date wise order detail', 4, 111)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (116, N'Customer wise Order detail', 5, 111)

-- System Administration Inputs
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (117, N'Manage User', 1, 8)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (118, N'Manage Features', 2, 8)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (119, N'Manage Actions', 3, 8)


-- System Administration Reports
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (120, N'Report', 4, 8)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (121, N'User List', 1, 120)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (122, N'Userwise Feature List', 2, 120)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (123, N'User Sessions', 3, 120)
INSERT [dbo].[tbFeature] ([Id], [Name], [SequenceNo], [ParentId]) VALUES (124, N'Userwise Entries Trail', 4, 120)


SET IDENTITY_INSERT [dbo].[tbFeature] OFF

END_SETUP: