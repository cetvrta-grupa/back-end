﻿INSERT INTO payments."Sales"(
	"Id", "ToursIds", "Start", "End", "Discount", "AuthorId")
	VALUES (-1, ARRAY[-1, -2, -3] , '2023-04-06 16:45:00', '2023-04-08 16:45:00', 10, -11);

INSERT INTO payments."Sales"(
	"Id", "ToursIds", "Start", "End", "Discount", "AuthorId")
	VALUES (-2, ARRAY[-1, -2, -3] , '2023-04-06 16:45:00', '2023-04-08 16:45:00', 20, -11);

INSERT INTO payments."Sales"(
	"Id", "ToursIds", "Start", "End", "Discount", "AuthorId")
	VALUES (-3,  ARRAY[-1, -2, -3] , '2023-04-06 16:45:00', '2023-04-08 16:45:00', 30, -11);

INSERT INTO payments."Sales"(
	"Id", "ToursIds", "Start", "End", "Discount", "AuthorId")
	VALUES (-4,  ARRAY[-6] , current_date - interval '2 days', current_date + interval '5 days', 30, -11);