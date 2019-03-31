INSERT INTO "AspNetUsers"
VALUES ('fddb44a3-43ae-44e2-b8a2-0962fa6be039', 'a@a.a', 'A@A.A', 'a@a.a', 'A@A.A', 'f', 'AQAAAAEAACcQAAAAEJwHuI9fREiOjP1gpbhmIH4UMJD+YIlvcR9Od5uvIPTWXp8KjXLCk6ng1PVqBPV91A==', 'CSOETKFG3JI46PNL32HOBWZDGEFAWCPN', 'cc4b6777-0e78-490a-a248-e503babbafc0', NULL, 'f', 'f', NULL, 't', 0 );


\c "TrueHomeDB"

INSERT INTO "role" (RoleName)
VALUES ('admin');

INSERT INTO "user" (Login, Email, IDRole)
VALUES ('a@a.a','a@a.a',1);

INSERT INTO "apartment" (name,city,street,apartmentNumber,imgthumb,imglist,rate,lat,long,iduser) 
VALUES 
('kek','Wrocław','Sienkiewicza','21/31','G3C 5K8','{"seven"}',3,'-39.98894','27.16119',1),
('lulz','D�gelis','Ap #988-1740 Nunc Rd.','653-7584 Tortor, St.','Z9T 2X9','{}',5,'19.10539','11.74472',1),
('kex','Le Puy-en-Velay','6353 Ut, Ave','XDD','C4K 4F0','{"nine"}',5,'25.23089','112.71901',1),
('XDDDDDDDD','Purranque','Ap #891-8040 Nec Rd.','kek','B3Z 2N4','{"seven", "nine", "one", "two", "six"}',1,'-3.07401','-93.0161',1),
('XKKKK','Dutse','813-1033 Vitae Rd.','Ap ','J8V 6A6','{"seven", "nine", "one", "two", "six"}',2,'18.29239','-132.24146',1);
