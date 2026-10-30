-- Archivo: insertar-registros.sql

-- 1. Insertar 5 automóviles (id, color, año, fabricante, tipo)
INSERT INTO PRQ_Automoviles (color, ano, fabricante, tipo) VALUES
('Rojo', 2020, 'Toyota', 'sedán'),
('Blanco', 2022, 'Nissan', '4x4'),
('Negro', 2019, 'Honda', 'moto'),
('Azul', 2023, 'Ford', '4x4'),
('Gris', 2018, 'Hyundai', 'sedán');

-- 2. Insertar 2 parqueos (id, nombre_provincia, nombre, precio_por_hora)
INSERT INTO PRQ_Parqueo (nombre_provincia, nombre, precio_por_hora) VALUES
('San José', 'Parqueo Central', 1500.00),
('Alajuela', 'Parqueo Aeropuerto', 2000.00);

-- 3. Insertar 15 registros de ingreso de automóviles
-- (consecutivo, id_parqueo, id_automovil, fecha_hora_entrada, fecha_hora_salida)
INSERT INTO PRQ_IngresoAutomoviles (id_parqueo, id_automovil, fecha_hora_entrada, fecha_hora_salida) VALUES
(1, 1, '2026-10-29 08:00:00', '2026-10-29 10:00:00'),
(1, 2, '2026-10-29 08:30:00', '2026-10-29 11:30:00'),
(2, 3, '2026-10-29 09:00:00', '2026-10-29 09:45:00'),
(1, 4, '2026-10-29 09:15:00', '2026-10-29 12:00:00'),
(2, 5, '2026-10-29 10:00:00', NULL), -- Aún no ha salido
(1, 1, '2026-10-29 13:00:00', '2026-10-29 15:00:00'),
(2, 2, '2026-10-29 13:30:00', NULL), -- Aún no ha salido
(1, 3, '2026-10-29 14:00:00', '2026-10-29 16:30:00'),
(2, 4, '2026-10-29 14:15:00', '2026-10-29 17:00:00'),
(1, 5, '2026-10-29 15:00:00', NULL), -- Aún no ha salido
(2, 1, '2026-10-29 16:00:00', '2026-10-29 18:00:00'),
(1, 2, '2026-10-29 16:30:00', '2026-10-29 19:00:00'),
(2, 3, '2026-10-29 17:00:00', NULL), -- Aún no ha salido
(1, 4, '2026-10-29 17:30:00', '2026-10-29 20:00:00'),
(2, 5, '2026-10-29 18:00:00', NULL); -- Aún no ha salido