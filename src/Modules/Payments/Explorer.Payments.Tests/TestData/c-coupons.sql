﻿INSERT INTO payments."Coupons"(
	"Id", "SellerId", "Code", "DiscountPercentage", "ExpirationDate", "IsGlobal", "TourId", "IsUsed")
	VALUES (-1, -12, 'ABC123BC', 10, null, TRUE, null, FALSE);

INSERT INTO payments."Coupons"(
	"Id", "SellerId", "Code", "DiscountPercentage", "ExpirationDate", "IsGlobal", "TourId", "IsUsed")
	VALUES (-2, -12, '123ABCBC', 20, to_timestamp('2024-11-24T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), FALSE, -1, FALSE);

INSERT INTO payments."Coupons"(
	"Id", "SellerId", "Code", "DiscountPercentage", "ExpirationDate", "IsGlobal", "TourId", "IsUsed")
	VALUES (-3, -12, 'EDFGCQ45', 10, to_timestamp('2024-12-24T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), FALSE, -2, TRUE);