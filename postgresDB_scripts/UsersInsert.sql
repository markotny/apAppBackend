INSERT INTO "AspNetUsers"
VALUES ('fddb44a3-43ae-44e2-b8a2-0962fa6be039', 'a@a.a', 'A@A.A', 'a@a.a', 'A@A.A', 'f', 'AQAAAAEAACcQAAAAEJwHuI9fREiOjP1gpbhmIH4UMJD+YIlvcR9Od5uvIPTWXp8KjXLCk6ng1PVqBPV91A==', 'CSOETKFG3JI46PNL32HOBWZDGEFAWCPN', 'cc4b6777-0e78-490a-a248-e503babbafc0', NULL, 'f', 'f', NULL, 't', 0 );


\c "TrueHomeDB"

INSERT INTO "role" (RoleName)
VALUES ('admin');

INSERT INTO "user" (Login, Email, IDRole)
VALUES ('a@a.a','a@a.a',1);

INSERT INTO "apartment" (name,city,street,apartmentNumber,imgthumb,imglist,rate,lat,long,iduser) 
VALUES 
('luksus','Wrocław','Sienkiewicza','21/31','https://media-cdn.trulia-local.com/neighborhood-media-service-prod/il/chicago/south-loop/920-il_chi_south_loop_269607_177_256x256_cfill.jpg','{"seven"}',3,'-39.98894','27.16119',1),
('mieszkanko','D�gelis','Ap #988-1740 Nunc Rd.','653-7584 Tortor, St.','https://media-cdn.trulia-local.com/neighborhood-media-service-prod/tx/austin/south-lamar/282-tx_aus_south_lamar_271570_2350_256x256_cfill.jpg','{}',5,'19.10539','11.74472',1),
('domek','Le Puy-en-Velay','6353 Ut, Ave','5','http://www.gatewayapartments.com.hk/img/GatewayApartments.png','{"nine"}',5,'25.23089','112.71901',1),
('zameczek na nadodrzu','Purranque','Ap #891-8040 Nec Rd.','2','https://media-cdn.trulia-local.com/neighborhood-media-service-prod/il/chicago/south-loop/920-il_chi_south_loop_269607_177_256x256_cfill.jpg','{"seven", "nine", "one", "two", "six"}',1,'-3.07401','-93.0161',1),
('kawalerka w piwnicy','Dutse','813-1033 Vitae Rd.','10A ','https://media-cdn.trulia-local.com/neighborhood-media-service-prod/tx/austin/south-lamar/282-tx_aus_south_lamar_271570_2350_256x256_cfill.jpg','{"seven", "nine", "one", "two", "six"}',2,'18.29239','-132.24146',1);
